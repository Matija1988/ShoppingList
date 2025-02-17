import { Alert, Col, Container, Form, Row } from "react-bootstrap";
import InputText from "../components/InputText";
import CustomButton from "../components/CustomButton";

import { Routes, useNavigate } from "react-router-dom";
import { RouteNames } from "../constants/constants";
import { useEffect, useState } from "react";
import useAuthStore from "../data/authStore";

export default function LogIn() {

  const navigate = useNavigate();
  const {login, isAuthenticated} = useAuthStore();
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    if (isAuthenticated) {
      navigate(RouteNames.LANDINGPAGE);
    }
  }, [isAuthenticated, navigate]);

  async function handleSubmit(e) {
    e.preventDefault();

    const data = new FormData(e.target);
    const error = await login({
      username: data.get("username"),
      password: data.get("password"),
    });
    if (error) {
      setErrorMessage(error);
    }
  }

  return (
    <div className="login-container">
      <Form className="login-form" onSubmit={handleSubmit}>
        {errorMessage && <Alert variant="danger">{errorMessage}</Alert>}
        <InputText
        groupClass="login-group"
          className="login-input"
          atribute="username"
          required={true}
        ></InputText>
        <InputText
        groupClass="login-group"
          className="login-input"
          atribute="password"
          required={true}
          type="password"
        ></InputText>

          <CustomButton
            className="login-btn login-btn-primary"
            label="Login"
          ></CustomButton>

        <Row>
          <CustomButton
            className="login-link"
            variant="link"
            label="Not registered? Register as member"
            onClick={() => navigate(RouteNames.REGISTER)}
          ></CustomButton>
        </Row>
      </Form>
    </div>
  );
}
