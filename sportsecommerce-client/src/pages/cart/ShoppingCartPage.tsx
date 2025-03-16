import { Alert, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button } from "@mui/material";
import { Delete, AddCircleOutline, RemoveCircleOutline } from "@mui/icons-material";
import { useCartContext } from "../../contexts/CartContext";
import { useState } from "react";
import requests from "../../api/requests";
import { toast } from "react-toastify";
import CartSummary from "./CartSummary";
import { currencyTRY } from "../../utils/formatCurrency";

export default function ShoppingCartPage() {

  const { cart, setCart } = useCartContext();
  const [status, setStatus] = useState({ loading: false, id: "" });

  function handleAddItem(productId: string, id: string) {

    setStatus({ loading: true, id: id });

    requests.Cart.addItem(productId)
      .then(cart => setCart(cart))
      .catch(error => console.log(error))
      .finally(() => setStatus({ loading: false, id: "" }));
  }

  function handleRemoveItem(productId: string, id: string, quantity: number = 1) {

    setStatus({ loading: true, id: id });

    requests.Cart.removeItem(productId, quantity)
      .then((cart) => setCart(cart))
      .catch(error => console.log(error))
      .finally(() => setStatus({ loading: false, id: "" }));
  }

  if (cart?.cartItems.length === 0) return <Alert severity="warning">Sepetinizde ürün yok.</Alert>

  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Product Image</TableCell>
              <TableCell>Product Name</TableCell>
              <TableCell align="right">Unit Price</TableCell>
              <TableCell align="right">Quantity</TableCell>
              <TableCell align="right">Total</TableCell>
              <TableCell align="right"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {cart?.cartItems.map((item) => (
              <TableRow
                key={item.productId}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  <img src={`http://localhost:5110/images/${item.imageUrl}`} style={{ height: 60 }} />
                </TableCell>
                <TableCell component="th" scope="row">
                  {item.productName}
                </TableCell>
                <TableCell align="right">{currencyTRY.format(item.unitPrice)}</TableCell>
                <TableCell align="right">
                  <Button loading={status.loading && status.id === "add" + item.productId} onClick={() => handleAddItem(item.productId, "add" + item.productId)}>
                    <AddCircleOutline />
                  </Button>
                  {item.quantity}
                  <Button loading={status.loading && status.id === "remove" + item.productId} onClick={() => handleRemoveItem(item.productId, "remove" + item.productId)}>
                    <RemoveCircleOutline />
                  </Button>
                </TableCell>
                <TableCell align="right">{currencyTRY.format(item.unitPrice * item.quantity)}</TableCell>
                <TableCell align="right">
                  <Button color="error" loading={status.loading && status.id === "remove_all" + item.productId} onClick={() => {
                    handleRemoveItem(item.productId, "remove_all" + item.productId, item.quantity);
                    toast.error("Ürün sepetinizden silindi.");
                  }} >
                    <Delete />
                  </Button>
                </TableCell>
              </TableRow>
            ))}
            <CartSummary />
          </TableBody>
        </Table>
      </TableContainer>
    </>
  )
}