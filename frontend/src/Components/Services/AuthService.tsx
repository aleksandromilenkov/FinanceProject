import axios from "axios";
import { handleError } from "../../Helpers/ErrorHandler";
import { UserProfileToken } from "../../Models/User";

const api = "http://localhost:5019/api/";

export const loginAPI = async (username: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/login", {
      username,
      password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const registerAPI = async (
  email: string,
  username: string,
  password: string
) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/register", {
      username,
      password,
      email,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};
