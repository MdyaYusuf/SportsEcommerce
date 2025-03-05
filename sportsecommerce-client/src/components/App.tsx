import { useEffect, useState } from "react";
import { IProduct } from "../model/IProduct";
import { Container, CssBaseline } from "@mui/material";
import ProductList from "./ProductList";
import Header from "./Header";

function App() {

  const [products, setProducts] = useState<IProduct[]>([]);

  useEffect(() => {
    fetch("http://localhost:5110/api/Products/getall")
      .then(response => response.json())
      .then(result => setProducts(result.data));
  }, []);

  return (
    <>
      <CssBaseline />
      <Header />
      <Container>
        <ProductList products={products} />
      </Container>
    </>
  );
}

export default App
