import { useState, useEffect } from "react";
import { useParams } from "react-router";
import { IProduct } from "../model/IProduct";
import { CircularProgress, Divider, Grid2, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import requests from "../api/requests";
import NotFound from "../errors/NotFound";

export default function ProductDetailsPage() {

  const { id } = useParams<{id: string}>();
  const [product, setProduct] = useState<IProduct | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (id) {
      requests.homePage.details(id)
        .then(result => setProduct(result.data))
        .catch(error => console.log(error))
        .finally(() => setLoading(false));
    } 
  }, [id]);

  if (loading) return <CircularProgress />

  if (!product) return <NotFound />

  return (
    <Grid2 container spacing={2}>
      <Grid2 size={{ xl: 3, lg: 4, md: 5, sm: 6, xs: 12 }}>
        <img src={`http://localhost:5110/images/${product.imageUrl}`} style={{ width: "100%" }} />
      </Grid2>
      <Grid2 size={{ xl: 9, lg: 8, md: 7, sm: 6, xs: 12 }}>
        <Typography variant="h2">{product.name}</Typography>
        <Divider sx={{ mb: 2 }} />
        <Typography variant="h3" color="secondary">{product.price} TL</Typography>
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
      </Grid2>
    </Grid2>
  );
}