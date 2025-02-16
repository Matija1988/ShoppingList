import { createContext, useEffect, useState } from "react";
import db from "./database"; 
import productService from "../services/productService";

export const ProductContext = createContext();

export function ProductProvider({ children }) {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    async function fetchProducts() {
      const localProducts = await db.products.toArray(); // Get from Dexie
      
      if (localProducts.length > 0) {
        setProducts(localProducts);
      } else {
        const response = await productService.readAll("products");
        if (response.ok) {
          setProducts(response.data);
          await db.products.bulkPut(response.data); // Save to Dexie
        }
      }
    }
    
    fetchProducts();
  }, []);

  return (
    <ProductContext.Provider value={{ products, setProducts }}>
      {children}
    </ProductContext.Provider>
  );
}
