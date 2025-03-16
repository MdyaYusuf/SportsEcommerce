import { CircularProgress, Container, CssBaseline } from "@mui/material";
import { Outlet } from "react-router";
import Header from "./Header";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useEffect, useState } from "react";
import { useCartContext } from "../contexts/CartContext";
import requests from "../api/requests";

function App() {

  const { setCart } = useCartContext();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    requests.Cart.get()
      .then(cart => setCart(cart))
      .catch(error => console.log(error))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <CircularProgress />

  return (
    <>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      <CssBaseline />
      <Header />
      <Container>
        <Outlet />
      </Container>
    </>
  );
}

export default App
