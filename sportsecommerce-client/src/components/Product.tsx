import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import { IProduct } from "../model/IProduct";
import { AddShoppingCart } from "@mui/icons-material";
import SearchIcon from '@mui/icons-material/Search';
import { Link } from "react-router";
import requests from "../api/requests";
import { useState } from "react";

interface Props {
  product: IProduct;
}

export default function Product({ product }: Props) {

  const [loading, setLoading] = useState(false);

  function handleAddItem(productId: string) {

    setLoading(true);

    requests.Cart.addItem(productId)
      .then(cart => console.log(cart))
      .catch(error => console.log(error))
      .finally(() => setLoading(false));
  }

  return (
    <>
      <Card>
        <CardMedia sx={{ height: 160, backgroundSize: "contain" }} image={`http://localhost:5110/images/${product.imageUrl}`} />
        <CardContent>
          <Typography gutterBottom variant="h6" component="h2" color="text.secondary">
            {product.name}
          </Typography>
          <Typography variant="body2" color="secondary">
            {product.price} TL
          </Typography>
        </CardContent>
        <CardActions>
          <Button variant="outlined" size="small" loadingPosition="start" startIcon={<AddShoppingCart />} color="success" loading={loading} onClick={() => handleAddItem(product.id)}>Add to Cart</Button>
          <Button component={Link} to={`/${product.id}`} size="small" startIcon={<SearchIcon />} color="primary">View</Button>
        </CardActions>
      </Card>
    </>
  );
}