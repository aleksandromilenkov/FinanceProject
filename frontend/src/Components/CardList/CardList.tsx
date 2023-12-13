import React, { SyntheticEvent, useEffect, useState } from "react";
import Card from "../Card/Card";
import { CompanySearch } from "../../company";
import { v4 as uuidv4 } from "uuid";

interface Props {
  searchResults: CompanySearch[];
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const CardList = ({ searchResults, onPortfolioCreate }: Props) => {
  return (
    <div>
      {searchResults.length <= 0 ? (
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
          No results!
        </p>
      ) : (
        searchResults.map((result) => (
          <Card
            id={result.symbol}
            key={uuidv4()}
            searchResult={result}
            onPortfolioCreate={onPortfolioCreate}
          />
        ))
      )}
    </div>
  );
};

export default CardList;
