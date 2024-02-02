import { createContext, useContext, useEffect, useState } from "react";
import { UserProfile } from "../../Models/User";
import { useNavigate } from "react-router";
import { loginAPI, registerAPI } from "../Services/AuthService";
import { toast } from "react-toastify";
import axios from "axios";
import React from "react";

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (email: string, username: string, password: string) => void;
  loginUser: (username: string, password: string) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");
    if (user && token) {
      setUser(JSON.parse(user));
      setToken(token);
      // axios will add this to every single request
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    }
    setIsReady(true);
  }, []);

  const registerUser = async (
    email: string,
    username: string,
    password: string
  ) => {
    try {
      const resp = await registerAPI(email, username, password);
      const data = await resp;
      if (data) {
        localStorage.setItem("token", data?.data.token);
        const userObj = {
          username: data?.data.username,
          email: data?.data.email,
        };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(data?.data.token!);
        setUser(userObj!);
        toast.success("Register successfully.");
        navigate("/search");
      }
    } catch (err) {
      toast.warning("Server error occured.");
    }
  };

  const loginUser = async (username: string, password: string) => {
    try {
      const resp = await loginAPI(username, password);
      const data = await resp;
      if (data) {
        localStorage.setItem("token", data?.data.token);
        const userObj = {
          username: data?.data.username,
          email: data?.data.email,
        };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(data?.data.token!);
        setUser(userObj!);
        toast.success("Login successfully.");
        navigate("/search");
      }
    } catch (err) {
      toast.warning("Server error occured.");
    }
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken("");
    navigate("/");
  };

  return (
    <UserContext.Provider
      value={{ loginUser, token, isLoggedIn, logout, registerUser, user }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => useContext(UserContext);
