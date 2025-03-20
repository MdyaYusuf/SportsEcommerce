import { Alert, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button } from "@mui/material";
import { Delete, AddCircleOutline, RemoveCircleOutline } from "@mui/icons-material";
import CartSummary from "./CartSummary";
import { currencyTRY } from "../../utils/formatCurrency";
import { useAppDispatch, useAppSelector } from "../../hooks/hooks";
import { addItemToCart, removeItemFromCart } from "./cartSlice";

export default function ShoppingCartPage() {

  const { cart, status } = useAppSelector(state => state.cart);
  const dispatch = useAppDispatch();

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
                  <Button loading={status == "pendingAddItem" + item.productId} onClick={() => dispatch(addItemToCart({productId: item.productId}))}>
                    <AddCircleOutline />
                  </Button>
                  {item.quantity}
                  <Button loading={status === "pendingRemoveItem" + item.productId + "single"} onClick={() => dispatch(removeItemFromCart({productId: item.productId, quantity: 1, key: "single"}))}>
                    <RemoveCircleOutline />
                  </Button>
                </TableCell>
                <TableCell align="right">{currencyTRY.format(item.unitPrice * item.quantity)}</TableCell>
                <TableCell align="right">
                  <Button color="error" loading={status === "pendingRemoveItem" + item.productId + "all"} onClick={() => dispatch(removeItemFromCart({productId: item.productId, quantity: item.quantity, key: "all"}))}>
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