import React from "react";
import financeAppLogo from "./financeAppLogo.png";
import { Link } from "react-router-dom";
import { useAuth } from "../Context/useAuth";
interface Props {}

const Navbar = (props: Props) => {
  const user = useAuth().user;
  return (
    <nav className="relative container mx-auto p-6">
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-20">
          <Link to={"/"}>
            <img src={financeAppLogo} alt="" />
          </Link>
          <div className="hidden font-bold lg:flex">
            <Link to={"/search"} className="text-black hover:text-darkBlue">
              Search
            </Link>
          </div>
        </div>
        <div className="flex lg:flex items-center space-x-6 text-back">
          {!user && (
            <div className="hover:text-darkBlue">
              <Link to={"/login"}>Login</Link>
            </div>
          )}
          {!user && (
            <a
              href=""
              className="font-bold rounded text-white bg-lightGreen hover:opacity-70 sm:px-3 py-1 md:px-8 py-3 "
            >
              <Link to={"/signup"}>Signup</Link>
            </a>
          )}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
