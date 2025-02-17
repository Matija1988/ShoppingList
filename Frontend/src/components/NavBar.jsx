import { Container, Navbar, Nav, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { RouteNames } from "../constants/constants";
import useAuthStore from "../data/authStore";
import { useEffect } from "react";

export default function NavBar() {
  const navigate = useNavigate();

  const {logout, isAuthenticated} = useAuthStore();

  useEffect(() => {
    if (!isAuthenticated) {
      navigate(RouteNames.HOME);
    }
  }, [isAuthenticated, navigate]);

  return (
    <Navbar 
    expand="lg"
    className="bg-body-tertiary" 
    data-bs-theme="dark"
    style={{ position: "fixed", top: 0, width: "100%", zIndex: 1000 }}
>

      <Container>
        <Navbar.Brand onClick={() => navigate(RouteNames.LANDINGPAGE)}>
          APP
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link onClick={() => navigate(RouteNames.LANDINGPAGE)}>HOME</Nav.Link>
            <Nav.Link onClick={() => navigate(RouteNames.PRODUCTS)}>PRODUCTS</Nav.Link>
          </Nav>
        </Navbar.Collapse>
        <Button onClick={logout}>Logout</Button>
      </Container>
    </Navbar>
  );
}
