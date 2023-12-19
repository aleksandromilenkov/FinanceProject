import React, { useEffect, useState } from "react";
import { useOutletContext } from "react-router";
import { CompanyTenK } from "../../company";
import { getTenK } from "../../api";
import TenKFinderItem from "./TenKFinderItem";
import Spinner from "../Spinner/Spinner";

type Props = {
  ticker: string;
};

const TenKFinder = ({ ticker }: Props) => {
  const [companyData, setCompanyData] = useState<CompanyTenK[]>([]);
  useEffect(() => {
    const getTenKData = async () => {
      const result = await getTenK(ticker!);
      setCompanyData(result?.data!);
    };
    getTenKData();
  }, [ticker]);
  return (
    <div className="inline-flex gap-4 rounded-md shadow-sm m-4">
      {companyData ? (
        companyData.slice(0, 5).map((tenK) => <TenKFinderItem tenK={tenK} />)
      ) : (
        <Spinner />
      )}
    </div>
  );
};

export default TenKFinder;
