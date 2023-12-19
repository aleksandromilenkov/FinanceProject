import React, { useEffect, useState } from "react";
import { CompanyIncomeStatement } from "../../company";
import { useOutletContext } from "react-router";
import { getIncomeStatement } from "../../api";
import RatioList from "../RatioList/RatioList";
import Table from "../Table/Table";
import Spinner from "../Spinner/Spinner";

type Props = {};

const configs = [
  {
    label: "Date",
    render: (company: CompanyIncomeStatement) => company.date,
  },
  {
    label: "Total Revenue",
    render: (company: CompanyIncomeStatement) => company.revenue,
  },
  {
    label: "Net Income",
    render: (company: CompanyIncomeStatement) => company.netIncome,
  },
  {
    label: "Operating Expenses",
    render: (company: CompanyIncomeStatement) => company.operatingExpenses,
  },
  {
    label: "Cost of Revenue",
    render: (company: CompanyIncomeStatement) => company.netIncome,
  },
];

const IncomeStatement = (props: Props) => {
  const ticker = useOutletContext<string>();
  const [incomeData, setIncomeData] = useState<CompanyIncomeStatement[]>([]);
  useEffect(() => {
    const getIncomeData = async () => {
      const result = await getIncomeStatement(ticker);
      setIncomeData(result!.data);
    };
    getIncomeData();
  }, [ticker]);
  return (
    <>
      {incomeData.length > 0 ? (
        <Table configs={configs} data={incomeData} />
      ) : (
        <Spinner />
      )}
    </>
  );
};

export default IncomeStatement;
