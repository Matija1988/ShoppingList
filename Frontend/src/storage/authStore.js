import { create } from "zustand";
import { persist } from "zustand/middleware";
import authService from "../services/authService";
import { jwtDecode } from "jwt-decode";
import { RouteNames } from "../constants/constants";
import useProductStore from "./productStore";
import useShopListStore from "./shopListStore";

const useAuthStore = create(
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

          if (response.ok) {
            let token = response.data;

            if (typeof token === "object" && token.value) {
              token = token.value;
            }

            if (!token || token.split(".").length !== 3) {
              console.error("Invalid JWT structure detected:", token);
              return;
            }

            const decodedToken = jwtDecode(token);

            const id = decodedToken.sub;
            const username =
              decodedToken[
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
              ];
            const expiryTime = decodedToken.exp;
            const email = decodedToken.email;

            set({
              user: username,
              userId: id,
              token: token,
              isAuthenticated: true,
              tokenExpiry: expiryTime,
              email: email,
            });

            localStorage.setItem("Bearer", token);
            get().startTokenTimer();

            get().loadStoresAfterLogin(id);
          }
        } catch (error) {
          console.log("Login failed", error);
        }
      },

      loadStoresAfterLogin: async (id) => {
        const { isAuthenticated, userId } = get();

        if (isAuthenticated) {

          let attempts = 0;
          while (!userId && attempts < 30) {
            console.log("Waiting for userId to be available...");
            await new Promise((resolve) => setTimeout(resolve, 100));
            attempts++;
          }
          if (!userId) {
            console.error("Failed to get userId after waiting. Exiting.");
            return;
          }
        }

        const { loadProductsFromDB, fetchProductsFromAPI } =
          useProductStore.getState();
        await loadProductsFromDB();
        await fetchProductsFromAPI();

        const { loadShopListsFromDB, fetchShopListsFromAPI } =
          useShopListStore.getState();
        await loadShopListsFromDB();
        const sl = await fetchShopListsFromAPI(id);
        console.log("ShopList from API", sl);
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
          console.log(
            `Checking token: Expiry=${tokenExpiry}, Now=${currentTime}`
          );

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
);

export default useAuthStore;
