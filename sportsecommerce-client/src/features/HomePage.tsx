import { useEffect } from "react";
import ProductList from "./product/ProductList";
import { CircularProgress } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../hooks/hooks";
import { fetchProducts, selectAllProducts } from "./product/productSlice";

export default function HomePage() {

  const products = useAppSelector(selectAllProducts);
  const { status, isLoaded } = useAppSelector(state => state.product);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!isLoaded) {
      dispatch(fetchProducts());
    }
  }, [isLoaded, dispatch]);

  if (status === "pendingFetchProducts") return <CircularProgress />

  return (
    <>
      <ProductList products={products} />
    </>
  );
}