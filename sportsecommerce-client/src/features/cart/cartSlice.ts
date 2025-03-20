import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Cart } from "../../model/ICart";
import requests from "../../api/requests";


export interface CartState {
  cart: Cart | null,
  status: string
}

const initialState: CartState = {
  cart: null,
  status: "idle"
}

export const addItemToCart = createAsyncThunk<Cart, { productId: string, quantity?: number }>(
  "cart/addItemToCart",
  async ({ productId, quantity = 1 }) => {
    return await requests.Cart.addItem(productId, quantity);
  }
);

export const removeItemFromCart = createAsyncThunk<Cart, { productId: string, quantity?: number, key?: string }>(
  "cart/removeItemFromCart",
  async ({ productId, quantity = 1 }) => {
    return await requests.Cart.removeItem(productId, quantity);
  }
);

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    setCart: (state, action) => {
      state.cart = action.payload
    } 
  },
  extraReducers: (builder) => {
    builder.addCase(addItemToCart.pending, (state, action) => {
      console.log(action);
      state.status = "pendingAddItem" + action.meta.arg.productId;
    });

    builder.addCase(addItemToCart.fulfilled, (state, action) => {
      state.cart = action.payload;
      state.status = "idle";
    });

    builder.addCase(addItemToCart.rejected, (state) => {
      state.status = "idle";
    });

    builder.addCase(removeItemFromCart.pending, (state, action) => {
      console.log(action);
      state.status = "pendingRemoveItem" + action.meta.arg.productId + action.meta.arg.key;
    });

    builder.addCase(removeItemFromCart.fulfilled, (state, action) => {
      state.cart = action.payload;
      state.status = "idle";
    });

    builder.addCase(removeItemFromCart.rejected, (state) => {
      state.status = "idle";
    });
  }
})

export const { setCart } = cartSlice.actions;