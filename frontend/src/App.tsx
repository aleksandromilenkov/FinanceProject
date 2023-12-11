import { ChangeEvent, useState } from "react";
import "./App.css";
import CardList from "./Components/CardList/CardList";
import Search from "./Components/Search/Search";
import { CompanySearch } from "./company";
import { searchCompany } from "./api";

function App() {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string>("");
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(search);
  };
  const handleSearch = async (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) => {
    const result = await searchCompany(search);
    if (typeof result == "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
    console.log(searchResult);
  };
  return (
    <div className="App">
      <Search
        onHandleSearch={handleSearch}
        search={search}
        onHandleChange={handleChange}
      />
      <CardList />
    </div>
  );
}

export default App;
