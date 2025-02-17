import { create } from "zustand";
import { persist } from "zustand/middleware";
import authService from "../services/authService";
import { jwtDecode } from "jwt-decode";
import { RouteNames } from "../constants/constants";

const useAuthStore = create (
    persist(
        (set, get) => ({
            user: null,
            userId: null,
            token: null,
            isAuthenticated: false,
            tokenExpiry: null,
            email: null,
            tokenCheckInterval: null,

            login: async (userData) => {
                try {
                    const response = await authService.logInService(userData);

                    if(response.ok) {
                        let token = response.data;

                        if (typeof token === "object" && token.value) {
                            token = token.value; 
                          }
                      
                          if (!token || token.split('.').length !== 3) {
                            console.error("Invalid JWT structure detected:", token);
                            return;
                          }

                          const decodedToken = jwtDecode(token);
    
                          const id = decodedToken.sub;
                          const username = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
                          const expiryTime = decodedToken.exp;
                          const email = decodedToken.email;

                        set({user: username, userId: id, token: token, isAuthenticated: true, tokenExpiry: expiryTime, email: email });

                        localStorage.setItem("Bearer", token);
                        get().startTokenTimer();
                    }

                } catch (error) {
                    console.log("Login failed", error);
                }
            },
            
            logout: () => {
                const { tokenCheckInterval } = get();
                if (tokenCheckInterval) {
                  clearInterval(tokenCheckInterval);
                }
                set({
                  user: null,
                  token: null,
                  isAuthenticated: false,
                  tokenExpiry: null,
                  email: null,
                  tokenCheckInterval: null,
                });
                localStorage.removeItem("Bearer");
            },

            startTokenTimer: () => {
                const checkTokenValidity = () => {
                    const { tokenExpiry } = get();
                    if (!tokenExpiry) {
                      return;
                    }
          
                    const currentTime = Math.floor(Date.now() / 1000);
                    console.log(`Checking token: Expiry=${tokenExpiry}, Now=${currentTime}`);
          
                    if (tokenExpiry < currentTime) {
                      console.log("Token has expired, logging out...");
                      get().logout();
                    }
                  };
          
                  checkTokenValidity();
          
                  const intervalId = setInterval(checkTokenValidity, 30 * 1000);
                  set({ tokenCheckInterval: intervalId });
            },
        }),
        {
            name: "auth-storage",
        }

    )
)

export default useAuthStore;