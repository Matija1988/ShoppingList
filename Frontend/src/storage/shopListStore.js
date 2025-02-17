import { create } from "zustand";
import { persist } from "zustand/middleware";
import db from "./database";


const useShopListStore = create(
    persist(
        (set,get) => ({
            shopLists: [],
            setShopLists: async (newShopLists) => {
                set({shopLists: newShopLists});
                await db.shopLists.bulkPut(newShopLists);                
            },
            loadShopListsFromDB: async () => {
                const storedShopLists = await db.newShopLists.toArray();
                if(storedShopLists.lenght > 0) 
                    {
                        set({shopLists: storedShopLists});
                    }
            },
        }),
        {
            name: "shopList-storage",
        }
    )
);

export default useShopListStore;