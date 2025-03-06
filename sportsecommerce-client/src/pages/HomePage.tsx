import { useState, useEffect } from "react";
import { IProduct } from "../model/IProduct";
import ProductList from "../components/ProductList";
import { CircularProgress } from "@mui/material";

export default function HomePage() {

  const [products, setProducts] = useState<IProduct[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("http://localhost:5110/api/Products/getall")
      .then(response => response.json())
      .then(result => setProducts(result.data))
      .catch(error => console.log(error))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <CircularProgress />

  return (
    <>
      <ProductList products={products} />
    </>
  );
}