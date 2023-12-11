import axios from "axios";
import dotenv from "dotenv";
import { CompanySearch } from "./company";

interface SearchResponse {
  data: CompanySearch[];
}

export const searchCompany = async (query: string) => {
  try {
    const data = await axios.get<SearchResponse>(
      `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${process.env.REACT_APP_API_KEY}`
    );
  } catch (err) {
    if (axios.isAxiosError(err)) {
      console.log("ERROR: ", err.message);
      return err.message;
    } else {
      console.log("unexpected error ", err);
      return "Unexpected error occured.";
    }
  }
};
