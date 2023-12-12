import React, { SyntheticEvent } from "react";

interface Props {
  onPortfolioCreate: (e: SyntheticEvent) => void;
  symbol: string;
}

const AddPortfolio = ({ onPortfolioCreate, symbol }: Props) => {
  return (
    <form onSubmit={onPortfolioCreate}>
      <input
        id="symbol"
        name="symbol"
        readOnly={true}
        hidden={true}
        value={symbol}
      />
      <button type="submit">Add</button>
    </form>
  );
};

export default AddPortfolio;
