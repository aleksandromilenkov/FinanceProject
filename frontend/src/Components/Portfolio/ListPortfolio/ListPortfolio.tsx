import React from "react";
import CardPortfolio from "../CardPortfolio/CardPortfolio";

interface Props {
  portfolioValues: string[];
  onPortfolioDelete: (portfolioValue: string) => void;
}

const ListPortfolio: React.FC<Props> = ({
  portfolioValues,
  onPortfolioDelete,
}: Props): JSX.Element => {
  return (
    <>
      <h3>My Portfolio</h3>
      {portfolioValues.length <= 0 && <p>No portfolios added yet.</p>}
      <ul>
        {portfolioValues.length > 0 &&
          portfolioValues.map((val, idx) => (
            <CardPortfolio
              key={idx}
              portfolioValue={val}
              onPortfolioDelete={onPortfolioDelete}
            />
          ))}
      </ul>
    </>
  );
};

export default ListPortfolio;
