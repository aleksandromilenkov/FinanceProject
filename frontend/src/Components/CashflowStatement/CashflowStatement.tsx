import React, { useEffect, useState } from "react";
import { CompanyCashFlow } from "../../company";
import { useOutletContext } from "react-router";
import { getCashflowStatement } from "../../api";
import Table from "../Table/Table";
import Spinner from "../Spinner/Spinner";

type Props = {};

const config = [
  {
    label: "Date",
    render: (company: CompanyCashFlow) => company.date,
  },
  {
    label: "Operating Cashflow",
    render: (company: CompanyCashFlow) => company.operatingCashFlow,
  },
  {
    label: "Property/Machinery Cashflow",
    render: (company: CompanyCashFlow) =>
      company.investmentsInPropertyPlantAndEquipment,
  },
  {
    label: "Other Investing Cashflow",
    render: (company: CompanyCashFlow) => company.otherInvestingActivites,
  },
  {
    label: "Debt Cashflow",
    render: (company: CompanyCashFlow) =>
      company.netCashUsedProvidedByFinancingActivities,
  },
  {
    label: "CapEX",
    render: (company: CompanyCashFlow) => company.capitalExpenditure,
  },
  {
    label: "Free Cash Flow",
    render: (company: CompanyCashFlow) => company.freeCashFlow,
  },
];

const CashflowStatement = (props: Props) => {
  const ticker = useOutletContext<string>();
  const [cashflowData, setCashflowData] = useState<CompanyCashFlow[]>([]);
  useEffect(() => {
    const getCashflowData = async () => {
      const result = await getCashflowStatement(ticker);
      setCashflowData(result!.data);
    };
    getCashflowData();
  }, []);
  return (
    <>
      {cashflowData ? (
        <Table configs={config} data={cashflowData} />
      ) : (
        <Spinner />
      )}
    </>
  );
};

export default CashflowStatement;
