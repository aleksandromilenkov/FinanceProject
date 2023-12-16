import React, { useEffect, useState } from "react";
import { useOutletContext } from "react-router";
import { CompanyBalanceSheet } from "../../company";
import { getBalanceSheet } from "../../api";
import RatioList from "../RatioList/RatioList";

type Props = {};

const config = [
  {
    label: "Cash",
    render: (company: CompanyBalanceSheet) => company.cashAndCashEquivalents,
  },
  {
    label: "Inventory",
    render: (company: CompanyBalanceSheet) => company.inventory,
  },
  {
    label: "Other Current Assets",
    render: (company: CompanyBalanceSheet) => company.otherCurrentAssets,
  },
  {
    label: "Minority Interest",
    render: (company: CompanyBalanceSheet) => company.minorityInterest,
  },
  {
    label: "Other Non-Current Assets",
    render: (company: CompanyBalanceSheet) => company.otherNonCurrentAssets,
  },
  {
    label: "Long Term Debt",
    render: (company: CompanyBalanceSheet) => company.longTermDebt,
  },
  {
    label: "Total Debt",
    render: (company: CompanyBalanceSheet) => company.otherCurrentLiabilities,
  },
];

const BalanceSheet = (props: Props) => {
  const ticker = useOutletContext<string>();
  const [balanceData, setBalanceData] = useState<CompanyBalanceSheet>();
  useEffect(() => {
    const getBalanceSheetData = async () => {
      const result = await getBalanceSheet(ticker);
      setBalanceData(result?.data[0]);
    };
    getBalanceSheetData();
  }, []);
  return (
    <div>
      {balanceData ? (
        <RatioList data={balanceData} config={config} />
      ) : (
        <>Company not found.</>
      )}
    </div>
  );
};

export default BalanceSheet;
