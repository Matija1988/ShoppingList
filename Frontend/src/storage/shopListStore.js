import { create } from "zustand";
import { getAllShopLists, addShopList } from "../storage/database";
import shopListService from "../services/shopListService";

const useShopListStore = create((set, get) => ({
    shopLists: [],

    loadShopListsFromDB: async() => {
        const shopLists = await getAllShopLists();
        set({shopLists});
    },

    fetchShopListsFromAPI: async (userId) => {
        const {shopLists} = get();

        if(shopLists.length === 0) {
            const response = await shopListService.getById("shopLists", userId);
            console.log("Full response:", response);
            if(response.ok) {
                console.log("Received shopLists data:", response.data);
                set({ shopLists: response.data });
                await addShopList(response.data);
            }
            else {
                console.error("Bad response!!!");
            }
        }
    },

    setShopLists: (newShopList) => {
        set({shopLists: newShopList});
        addShopList(newShopList);
    }
})
);

export default useShopListStore;