import { create } from "zustand";
import { persist } from "zustand/middleware";
import db from "./database";

const useProductStore = create(
  persist(
    (set, get) => ({
      products: [],
      setProducts: async (newProducts) => {
        set({ products: newProducts });
        await db.products.bulkPut(newProducts); 
      },
      loadProductsFromDB: async () => {
        const storedProducts = await db.products.toArray();
        if (storedProducts.length > 0) {
          set({ products: storedProducts });
        }
      },
    }),
    {
      name: "product-storage",
    }
  )
);

export default useProductStore;
