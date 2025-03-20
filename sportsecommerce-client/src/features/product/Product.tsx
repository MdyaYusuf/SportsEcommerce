import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import { IProduct } from "../../model/IProduct";
import { AddShoppingCart } from "@mui/icons-material";
import SearchIcon from '@mui/icons-material/Search';
import { Link } from "react-router";
import { currencyTRY } from "../../utils/formatCurrency";
import { useAppDispatch, useAppSelector } from "../../hooks/hooks";
import { addItemToCart } from "../../features/cart/cartSlice";
import { toast } from "react-toastify";

interface Props {
  product: IProduct;
}

export default function Product({ product }: Props) {

  const { status } = useAppSelector(state => state.cart);
  const dispatch = useAppDispatch();

  return (
    <>
      <Card>
        <CardMedia sx={{ height: 160, backgroundSize: "contain" }} image={`http://localhost:5110/images/${product.imageUrl}`} />
        <CardContent>
          <Typography gutterBottom variant="h6" component="h2" color="text.secondary">
            {product.name}
          </Typography>
          <Typography variant="body2" color="secondary">
            {currencyTRY.format(product.price)}
          </Typography>
        </CardContent>
        <CardActions>
          <Button variant="outlined" size="small" loadingPosition="start" startIcon={<AddShoppingCart />} color="success" loading={status === "pendingAddItem" + product.id} onClick={() => {
            dispatch(addItemToCart({ productId: product.id }))
              .unwrap()
              .then(() => toast.success("Ürün sepete eklendi."))
              .catch(() => {});
          }}>Add to Cart</Button>
          <Button component={Link} to={`/${product.id}`} size="small" startIcon={<SearchIcon />} color="primary">View</Button>
        </CardActions>
      </Card>
    </>
  );
}