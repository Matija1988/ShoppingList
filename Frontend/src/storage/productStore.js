import { create } from "zustand";
import { getAllProducts, saveProducts } from "../storage/database";

const useProductStore = create((set, get) => ({
  products: [],
  
  loadProductsFromDB: async () => {
    const products = await getAllProducts();
    set({ products });
  },

  fetchProductsFromAPI: async () => {
    const { products } = get();
    if (products.length === 0) {
      const response = await productService.readAll("products");
      if (response.ok) {
        set({ products: response.data });
        await saveProducts(response.data);
      }
    }
  },

  setProducts: (newProducts) => {
    set({ products: newProducts });
    saveProducts(newProducts);
  }
}));

export default useProductStore;
