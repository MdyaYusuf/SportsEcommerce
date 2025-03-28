import { createBrowserRouter, Navigate } from "react-router";
import App from "../layout/App";
import ContactPage from "../features/ContactPage";
import AboutPage from "../features/AboutPage";
import HomePage from "../features/HomePage";
import ProductDetailsPage from "../features/product/ProductDetailsPage";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import ShoppingCartPage from "../features/cart/ShoppingCartPage";
import LoginPage from "../features/authentication/LoginPage";
import RegisterPage from "../features/authentication/RegisterPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "about", element: <AboutPage /> },
      { path: "contact", element: <ContactPage /> },
      { path: "cart", element: <ShoppingCartPage /> },
      { path: "login", element: <LoginPage /> },
      { path: "register", element: <RegisterPage /> },
      { path: "/:id", element: <ProductDetailsPage /> },
      { path: "server-error", element: <ServerError /> },
      { path: "not-found", element: <NotFound /> },
      { path: "*", element: <Navigate to="/not-found" /> }
    ]
  }
])