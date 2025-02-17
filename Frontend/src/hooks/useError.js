import { useContext } from "react";
import { ErrorContext } from "../context/ErrorContext";

export default function useError() {
  const context = useContext(ErrorContext);

  if (!context) {
    throw new Error("useError must be used withing ErrorProvider!!!");
  }
  return context;
}