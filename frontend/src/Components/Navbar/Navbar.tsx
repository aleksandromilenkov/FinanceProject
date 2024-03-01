import React from "react";
import financeAppLogo from "./financeAppLogo.png";
import { Link } from "react-router-dom";
import { useAuth } from "../Context/useAuth";
interface Props {}

const Navbar = (props: Props) => {
  const { isLoggedIn, user, logout } = useAuth();
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
        {isLoggedIn() ? (
          <div className="flex lg:flex items-center space-x-6 text-back">
            <div className="hover:text-darkBlue">Welcome {user?.username}</div>
            <a
              onClick={logout}
              href=""
              className="font-bold rounded text-white bg-lightGreen hover:opacity-70 sm:px-3 py-1 md:px-8 py-3 "
            >
              Logout
            </a>
          </div>
        ) : (
          <div className="flex lg:flex items-center space-x-6 text-back">
            <div className="hover:text-darkBlue">
              <Link className="hover:text-darkBlue" to={"/login"}>
                Login
              </Link>
            </div>
            <Link
              to={"/signup"}
              className="font-bold rounded text-white bg-lightGreen hover:opacity-70 sm:px-3 py-1 md:px-8 py-3 "
            >
              Signup
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
