import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.jsx";
import { BrowserRouter } from "react-router-dom";
import { ErrorProvider } from "./context/ErrorContext.jsx";
import { LoadingProvider } from "./context/LoadingContext.jsx";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <BrowserRouter>
      <ErrorProvider>
        <LoadingProvider>
            <App />
        </LoadingProvider>
      </ErrorProvider>
    </BrowserRouter>
  </StrictMode>
);
