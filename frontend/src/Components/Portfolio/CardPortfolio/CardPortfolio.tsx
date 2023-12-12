import React from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";

interface Props {
  portfolioValue: string;
  onPortfolioDelete: (portfolioValue: string) => void;
}

const CardPortfolio = ({ portfolioValue, onPortfolioDelete }: Props) => {
  return (
    <>
      <h4>{portfolioValue}</h4>
      <DeletePortfolio
        portfolioName={portfolioValue}
        onPortfolioDelete={onPortfolioDelete}
      />
    </>
  );
};

export default CardPortfolio;
