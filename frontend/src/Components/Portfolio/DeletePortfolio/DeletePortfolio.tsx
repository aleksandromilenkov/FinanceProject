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
      <button type="submit">X</button>
    </form>
  );
};

export default DeletePortfolio;
