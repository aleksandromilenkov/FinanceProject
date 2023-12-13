import React from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";

interface Props {
  portfolioName: string;
  onPortfolioDelete: (portfolioValue: string) => void;
}

const CardPortfolio = ({ portfolioName, onPortfolioDelete }: Props) => {
  return (
    <div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
      <p className="pt-6 text-xl font-bold">{portfolioName}</p>
      <DeletePortfolio
        portfolioName={portfolioName}
        onPortfolioDelete={onPortfolioDelete}
      />
    </div>
  );
};

export default CardPortfolio;
