import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.jsx";
import { BrowserRouter } from "react-router-dom";
import { ErrorProvider } from "./context/ErrorContext.jsx";
import { LoadingProvider } from "./context/LoadingContext.jsx";
import { AuthProvider } from "./context/AuthContext.jsx";
import { UserProvider } from "./context/UserContext.jsx";
import { ProductProvider } from "./data/ProductContext.jsx";


createRoot(document.getElementById("root")).render(
  <StrictMode>
    <BrowserRouter>
      <ErrorProvider>
        <LoadingProvider>
          <UserProvider>
            <AuthProvider>
              <ProductProvider>
                <App />
              </ProductProvider>
            </AuthProvider>
          </UserProvider>
        </LoadingProvider>
      </ErrorProvider>
    </BrowserRouter>
  </StrictMode>
);
