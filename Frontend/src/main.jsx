import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import { BrowserRouter } from "react-router-dom";
import { ErrorProvider } from "./context/ErrorContext.jsx";
import { LoadingProvider } from "./context/LoadingContext.jsx";
import { AuthProvider } from "./context/AuthContext.jsx";
import { UserProvider } from "./context/UserContext.jsx";

createRoot(document.getElementById('root')).render(
  <StrictMode>
      <BrowserRouter>
      <ErrorProvider>
        <LoadingProvider>
          <UserProvider>
            <AuthProvider>
              <App />
            </AuthProvider>
          </UserProvider>
        </LoadingProvider>
      </ErrorProvider>
    </BrowserRouter>
  </StrictMode>,
)
