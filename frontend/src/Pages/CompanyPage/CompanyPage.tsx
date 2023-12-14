import React, { useEffect, useState } from "react";
import { CompanyProfile } from "../../company";
import { getCompanyProfile } from "../../api";
import { useParams } from "react-router";

interface Props {}

const CompanyPage = (props: Props) => {
  const [company, setCompany] = useState<CompanyProfile>();
  const { ticker } = useParams();
  useEffect(() => {
    const getProfileInit = async () => {
      const result = await getCompanyProfile(ticker!);
      setCompany(result?.data[0]);
    };
    getProfileInit();
  }, [ticker]);
  return (
    <>
      {company ? (
        <div>{company.companyName}</div>
      ) : (
        <div>Company not found</div>
      )}
    </>
  );
};

export default CompanyPage;
