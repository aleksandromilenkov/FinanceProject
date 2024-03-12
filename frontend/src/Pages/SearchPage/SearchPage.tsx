import React, { ChangeEvent, SyntheticEvent, useEffect, useState } from "react";
import { searchCompany } from "../../api";
import Navbar from "../../Components/Navbar/Navbar";
import Hero from "../../Components/Hero/Hero";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";
import Search from "../../Components/Search/Search";
import CardList from "../../Components/CardList/CardList";
import { CompanySearch } from "../../company";
import { PortfolioGet } from "../../Models/Portfolio";
import {
  portfolioAddAPI,
  portfolioDeleteAPI,
  portfolioGetAPI,
} from "../../Components/Services/PortfolioService";
import { toast } from "react-toastify";

interface Props {}

const SearchPage = (props: Props) => {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[] | null>(
    []
  );
  const [serverError, setServerError] = useState<string | null>(null);

  const getPortfolio = async () => {
    try {
      const data = await portfolioGetAPI();
      if (data?.data) {
        setPortfolioValues(data.data);
      }
    } catch (err) {
      toast.warning("Could not get porftolio values.");
    }
  };
  useEffect(() => {
    getPortfolio();
  }, []);
  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };
  const handleSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const result = await searchCompany(search);
    console.log("THE RESULT IS: ", result);
    if (typeof result == "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
      setServerError("");
    }
    console.log("THE RESULT.DATA IS: ", searchResult);
  };

  const onPortfolioCreate = async (e: any) => {
    e.preventDefault();
    try {
      console.log(e.target.symbol.value);
      const data = await portfolioAddAPI(e.target.symbol.value);
      if (data?.status.toString().startsWith("2")) {
        toast.success("Stock added to portfolio!");
        getPortfolio();
      }
    } catch (err) {
      toast.warning("Stock can not be added to portfolio!");
    }
  };
  const onPortfolioDelete = async (e: any) => {
    e.preventDefault();
    try {
      const data = await portfolioDeleteAPI(e.target.name.value);
      console.log(data?.status.toString().startsWith("2"));
      if (data?.status.toString().startsWith("2")) {
        toast.success("Stock deleted from portfolio!");
        getPortfolio();
      }
    } catch (err) {
      toast.warning("Stock can not be deleted from portfolio!");
    }
  };
  return (
    <div className="App">
      <Hero />
      <Search
        onSearchSubmit={handleSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />
      <ListPortfolio
        portfolioValues={portfolioValues!}
        onPortfolioDelete={onPortfolioDelete}
      />
      {serverError && <h3>Network Error. Please check Internet Connection</h3>}
      {
        <CardList
          searchResults={searchResult}
          onPortfolioCreate={onPortfolioCreate}
        />
      }
    </div>
  );
};

export default SearchPage;
