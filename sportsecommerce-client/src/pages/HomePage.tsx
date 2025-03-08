import { useState, useEffect } from "react";
import { IProduct } from "../model/IProduct";
import ProductList from "../components/ProductList";
import { CircularProgress } from "@mui/material";
import requests from "../api/requests";

export default function HomePage() {

  const [products, setProducts] = useState<IProduct[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    requests.homePage.list()
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