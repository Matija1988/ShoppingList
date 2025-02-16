import { createContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { RouteNames } from "../constants/constants";
import authService from "../services/authService";
import { jwtDecode } from "jwt-decode";
import { useUser } from "../context/UserContext";

export const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [authToken, setAuthToken] = useState("");
  const { resetUser, setUserId, setUserOnLogin } = useUser();
  // const [role, setRole] = useState();

  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("Bearer");

    if (token && isLoggedIn) {
      setAuthToken(token);
      setIsLoggedIn(true);
    } else {
      navigate(RouteNames.HOME);
    }
  }, [isLoggedIn]);

  async function login(userData) {
    const response = await authService.logInService(userData)

    if (response.ok) {
      const token = response.data;
  
    if (typeof token === "object" && token.value) {
      token = token.value; 
    }

    if (!token || token.split('.').length !== 3) {
      console.error("Invalid JWT structure detected:", token);
      return;
    }

      localStorage.setItem("Bearer", token);
      setAuthToken(token);
      setIsLoggedIn(true);
      await setUserOnLogin(token);
      const decodedToken = jwtDecode(token);
     // setRole(decodedToken.role);
      navigate(RouteNames.LANDINGPAGE);
    } 
    else 
    {
      localStorage.setItem("Bearer", "");
      setAuthToken("");
      setIsLoggedIn(false);

      return "Invalid username or password";
    }
  }

  function logout() {
    localStorage.setItem("Bearer", "");
    setAuthToken("");
    setIsLoggedIn(false);
    resetUser();
  //  setUserRole(null);
    navigate(RouteNames.HOME);
    location.reload();
  }

  const value = {
    isLoggedIn,
    authToken,
    login,
    logout,
   // role,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
