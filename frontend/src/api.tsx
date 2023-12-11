import axios from "axios";
import { CompanySearch } from "./company";

interface SearchResponse {
  data: CompanySearch[];
}

export const searchCompany = async (query: string) => {
  try {
    console.log("WILL SEARCHING FOR: ", query);
    const data = await axios.get<SearchResponse>(
      `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${process.env.REACT_APP_API_KEY}`
    );
    return data;
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
