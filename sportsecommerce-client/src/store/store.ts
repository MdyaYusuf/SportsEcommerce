import { configureStore } from "@reduxjs/toolkit";
import { cartSlice } from "../features/cart/cartSlice";
import { productSlice } from "../features/product/productSlice";

export const store = configureStore({
  reducer: {
    product: productSlice.reducer,
    cart: cartSlice.reducer
  }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch