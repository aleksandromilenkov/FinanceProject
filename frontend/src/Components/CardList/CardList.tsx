import React from "react";
import Card from "../Card/Card";

interface Props {}

const CardList = (props: Props) => {
  return (
    <div>
      <Card companyName="Apple" price={100} ticker="AAPL" />
      <Card companyName="Microsoft" price={200} ticker="MSFT" />
      <Card companyName="Tesla" price={300} ticker="TSLA" />
    </div>
  );
};

export default CardList;
