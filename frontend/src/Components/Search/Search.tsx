import React, { ChangeEvent, useState } from "react";

interface Props {
  onHandleSearch: (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void;
  onHandleChange: (e: ChangeEvent<HTMLInputElement>) => void;
  search: string;
}

const Search: React.FC<Props> = ({
  onHandleChange,
  onHandleSearch,
  search,
}: Props): JSX.Element => {
  return (
    <div>
      <input type="text" onChange={(e) => onHandleChange(e)} value={search} />
      <button
        onClick={(e) => {
          onHandleSearch(e);
        }}
      >
        Search
      </button>
    </div>
  );
};

export default Search;
