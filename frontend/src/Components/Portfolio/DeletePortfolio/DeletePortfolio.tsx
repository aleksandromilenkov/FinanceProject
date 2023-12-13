import React from "react";

interface Props {
  portfolioName: string;
  onPortfolioDelete: (e: any) => void;
}

const DeletePortfolio = ({ portfolioName, onPortfolioDelete }: Props) => {
  return (
    <form onSubmit={onPortfolioDelete}>
      <input
        id="name"
        name="name"
        readOnly={true}
        hidden={true}
        value={portfolioName}
      />
      <button className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
        X
      </button>
    </form>
  );
};

export default DeletePortfolio;
