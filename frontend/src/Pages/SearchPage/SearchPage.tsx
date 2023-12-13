import React, { ChangeEvent, SyntheticEvent, useState } from "react";
import { searchCompany } from "../../api";
import Navbar from "../../Components/Navbar/Navbar";
import Hero from "../../Components/Hero/Hero";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";
import Search from "../../Components/Search/Search";
import CardList from "../../Components/CardList/CardList";
import { CompanySearch } from "../../company";

interface Props {}

const SearchPage = (props: Props) => {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);
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

  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    console.log(e.target.symbol.value);
    if (portfolioValues.includes(e.target.symbol.value)) return;
    setPortfolioValues((prevState) => [...prevState, e.target.symbol.value]);
  };
  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    setPortfolioValues((prevState) => {
      return prevState.filter((val) => val !== e.target.name.value);
    });
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
        portfolioValues={portfolioValues}
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
