import { ChangeEvent, useState } from "react";
import "./App.css";
import CardList from "./Components/CardList/CardList";
import Search from "./Components/Search/Search";

function App() {
  const [search, setSearch] = useState<string>("");

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(search);
  };
  const handleSearch = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    console.log(e);
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
