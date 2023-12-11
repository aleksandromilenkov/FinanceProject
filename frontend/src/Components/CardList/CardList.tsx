import React, { useEffect, useState } from "react";
import Card from "../Card/Card";
import { CompanySearch } from "../../company";
import { v4 as uuidv4 } from "uuid";

interface Props {
  searchResults: CompanySearch[];
}

const CardList = ({ searchResults }: Props) => {
  return (
    <div>
      {searchResults.length <= 0 ? (
        <h1>No results found</h1>
      ) : (
        searchResults.map((result) => (
          <Card id={result.symbol} key={uuidv4()} searchResult={result} />
        ))
      )}
    </div>
  );
};

export default CardList;
