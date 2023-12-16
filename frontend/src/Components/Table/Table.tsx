import React from "react";
import { testIncomeStatementData } from "./testData";
const data = testIncomeStatementData;

type Props = { configs: any; data: any };

const Table = ({ configs, data }: Props) => {
  const renderedRows = data.map((company: any) => {
    return (
      <tr key={company.cik}>
        {configs.map((config: any) => {
          return <td className="p-3">{config.render(company)}</td>;
        })}
      </tr>
    );
  });
  const renderedHeaders = configs.map((config: any) => {
    return (
      <th
        key={config.label}
        className="p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
      >
        {config.label}
      </th>
    );
  });
  return (
    <div className="bg-white shadow rounded-lg p-4 sm:p-6 xl:p-8">
      <table>
        <thead className="min-w-full divide-y divide-gray-200 m-5">
          {renderedHeaders}
        </thead>
        <tbody>{renderedRows}</tbody>
      </table>
    </div>
  );
};

export default Table;
