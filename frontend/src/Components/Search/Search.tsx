import React, { ChangeEvent, useState } from "react";

type Props = {};

const Search: React.FC<Props> = (props: Props): JSX.Element => {
  const [search, setSearch] = useState<string>("");
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(search);
  };
  const handleSearch = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    console.log(e);
  };
  return (
    <div>
      <input type="text" onChange={(e) => handleChange(e)} value={search} />
      <button
        onClick={(e) => {
          handleSearch(e);
        }}
      >
        Search
      </button>
    </div>
  );
};

export default Search;
