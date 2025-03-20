import { TableRow, TableCell } from "@mui/material";
import { currencyTRY } from "../../utils/formatCurrency";
import { useAppSelector } from "../../hooks/hooks";

export default function CartSummary() {

  const { cart } = useAppSelector(state => state.cart);
  const finalTotal = cart?.cartItems.reduce((total, item) => total + (item.quantity * item.unitPrice), 0) ?? 0;
  const tax = finalTotal / 5;
  const net = finalTotal - tax;

  return (
    <>
      <TableRow sx={{ '& > *': { borderBottom: 'none' } }}>
        <TableCell align="right" colSpan={4}>Net Fiyat:</TableCell>
        <TableCell align="right">{currencyTRY.format(net)}</TableCell>
      </TableRow>
      <TableRow sx={{ '& > *': { borderBottom: 'none' } }}>
        <TableCell align="right" colSpan={4}>Vergi (%20):</TableCell>
        <TableCell align="right">{currencyTRY.format(tax)}</TableCell>
      </TableRow>
      <TableRow sx={{ '& > *': { borderBottom: 'none' } }}>
        <TableCell align="right" colSpan={4}>Final Toplam:</TableCell>
        <TableCell align="right">{currencyTRY.format(finalTotal)}</TableCell>
      </TableRow>
    </>
  );
}