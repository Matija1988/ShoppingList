import { jwtDecode } from "jwt-decode";
import {
  Children,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

const UserContext = createContext();

export const useUser = () => {
  return useContext(UserContext);
};

export const UserProvider = ({ children }) => {
  const [userId, setUserId] = useState(null);
  const [username, setUsername] = useState(null);
  const [email, setEmail] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem("Bearer");
    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        const currentTime = Date.now() / 1000;
        if (decodedToken.exp < currentTime) {
          console.log("Token has expired");
        } else {
          const id = decodedToken.sub;
          const username = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
          const email = decodedToken.email;

          setUserId(id);
          setUsername(username);
          setEmail(email);
        }
      } catch (error) {
        console.log("Error decoding token", error);
      }
    }
  }, []);

  const setUserOnLogin = async (input) => {
    const token = localStorage.getItem("Bearer");
   
    if (!token || token.split('.').length !== 3) {
      console.error("setUserOnLogin: Invalid JWT token detected", token);
      return;
    }
  
    try {
      const decodedToken = jwtDecode(token);
    
      const username = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      setEmail(decodedToken.email);
      setUsername(username);
      setUserId(decodedToken.sub);
    } catch (error) {
      console.error("setUserOnLogin: Error decoding JWT token", error);
    }
  };

  const resetUser = async () => {
    setUserId(null);
    setEmail(null);
    setUsername(null);
  };

  return (
    <UserContext.Provider
      value={{
        userId,
        username,
        email,
        setUserId,
        resetUser,
        setUserOnLogin,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};
