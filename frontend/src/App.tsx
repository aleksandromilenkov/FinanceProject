import { ChangeEvent, useState } from "react";
import "./App.css";
import CardList from "./Components/CardList/CardList";
import Search from "./Components/Search/Search";
import { CompanySearch } from "./company";
import { searchCompany } from "./api";

function App() {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };
  const handleSearch = async (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) => {
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
  return (
    <div className="App">
      <Search
        onHandleSearch={handleSearch}
        search={search}
        onHandleChange={handleChange}
      />
      {serverError && <h3>Network Error. Please check Internet Connection</h3>}
      {<CardList searchResults={searchResult} />}
    </div>
  );
}

export default App;
