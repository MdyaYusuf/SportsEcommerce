import { useEffect } from "react";
import { useParams } from "react-router";
import { Button, CircularProgress, Divider, Grid2, Stack, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import NotFound from "../../errors/NotFound";
import { AddShoppingCart } from "@mui/icons-material";
import { currencyTRY } from "../../utils/formatCurrency";
import { useAppDispatch, useAppSelector } from "../../hooks/hooks";
import { addItemToCart } from "../cart/cartSlice";
import { fetchProductById, selectProductById } from "./productSlice";
import { toast } from "react-toastify";

export default function ProductDetailsPage() {

  const { cart, status } = useAppSelector(state => state.cart);
  const dispatch = useAppDispatch();
  const { id } = useParams<{ id: string }>();
  const product = useAppSelector(state => selectProductById(state, id!));
  const { status: loading } = useAppSelector(state => state.product);

  const item = cart?.cartItems.find(ci => ci.productId == product?.id);

  useEffect(() => {
    if (!product && id) {
      dispatch(fetchProductById(id))
    }
  }, [id, product, dispatch]);

  if (loading === "pendingFetchProductById") return <CircularProgress />

  if (!product) return <NotFound />

  return (
    <Grid2 container spacing={2}>
      <Grid2 size={{ xl: 3, lg: 4, md: 5, sm: 6, xs: 12 }}>
        <img src={`http://localhost:5110/images/${product.imageUrl}`} style={{ width: "100%" }} />
      </Grid2>
      <Grid2 size={{ xl: 9, lg: 8, md: 7, sm: 6, xs: 12 }}>
        <Typography variant="h2">{product.name}</Typography>
        <Divider sx={{ mb: 2 }} />
        <Typography variant="h4" color="secondary">{currencyTRY.format(product.price)}</Typography>
        <TableContainer>
          <Table>
            <TableBody>
              <TableRow>
                <TableCell>Name</TableCell>
                <TableCell>{product.name}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Description</TableCell>
                <TableCell>{product.description}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Stock</TableCell>
                <TableCell>{product.stock}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Category</TableCell>
                <TableCell>{product.category}</TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
        <Stack direction="row" spacing={2} sx={{ mt: 3 }} alignItems="center">
          <Button variant="outlined" size="small" loadingPosition="start" startIcon={<AddShoppingCart />} color="success" loading={status === "pendingAddItem" + product.id} onClick={() => {
            dispatch(addItemToCart({ productId: product.id }))
              .unwrap()
              .then(() => toast.success("Ürün sepete eklendi."))
              .catch(() => {});
          }}>Add to Cart</Button>
          {
            (item?.quantity ?? 0) > 0 && (
              <Typography variant="body2">Sepetinize {item?.quantity} adet eklendi.</Typography>
            )
          }
        </Stack>
      </Grid2>
    </Grid2>
  );
}