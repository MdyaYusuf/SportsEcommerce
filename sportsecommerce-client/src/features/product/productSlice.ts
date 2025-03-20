import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { IProduct } from "../../model/IProduct";
import requests from "../../api/requests";
import { RootState } from "../../store/store";

export const fetchProducts = createAsyncThunk<IProduct[]>(
  "product/fetchProducts",
  async () => {
    const response = await requests.homePage.list();
    return response.data;
  }
)

export const fetchProductById = createAsyncThunk<IProduct, string>(
  "product/fetchProductById",
  async (productId) => {
    const response = await requests.homePage.details(productId);
    return response.data;
  }
)

const productsAdapter = createEntityAdapter<IProduct>();

const initialState = productsAdapter.getInitialState({
  status: "idle",
  isLoaded: false
});

export const productSlice = createSlice({
  name: "product",
  initialState,
  reducers: {

  },
  extraReducers: (builder) => {
    builder.addCase(fetchProducts.pending, (state) => {
      state.status = "pendingFetchProducts";
    });

    builder.addCase(fetchProducts.fulfilled, (state, action) => {
      productsAdapter.setAll(state, action.payload);
      state.isLoaded = true;
      state.status = "idle";
    });

    builder.addCase(fetchProducts.rejected, (state) => {
      state.status = "idle";
    });

    builder.addCase(fetchProductById.pending, (state) => {
      state.status = "pendingFetchProductById";
    });

    builder.addCase(fetchProductById.fulfilled, (state, action) => {
      productsAdapter.upsertOne(state, action.payload);
      state.status = "idle";
    });

    builder.addCase(fetchProductById.rejected, (state) => {
      state.status = "idle";
    });
  }
})

export const {
  selectById: selectProductById,
  selectIds: selectProductIds,
  selectEntities: selectProductEntities,
  selectAll: selectAllProducts,
  selectTotal: selectTotalProducts,
} = productsAdapter.getSelectors((state: RootState) => state.product);