import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { ILoginRequest } from "../../model/ILoginRequest";
import { IUser } from "../../model/IUser";
import requests from "../../api/requests";
import axios from "axios";
import { IApiError } from "../../model/IApiError";

interface AuthenticationState {
  user: IUser | null;
}

const initialState: AuthenticationState = {
  user: null
}

export const loginUser = createAsyncThunk<IUser, ILoginRequest>(
  "authenticate/login",
  async (data, { rejectWithValue }) => {
    try {
      const response = await requests.Authentication.login(data);

      if (!response.success) {
        const customError: IApiError = {
          Success: false,
          Message: response.message || "Giriş başarısız.",
          StatusCode: response.statusCode || 400
        };

        return rejectWithValue(customError);
      }
      
      localStorage.setItem("user", JSON.stringify(response.data));
      return response.data;
    }
    catch (error: unknown) {
      if (axios.isAxiosError(error)) {
        const apiError = error.response?.data as IApiError | undefined;

        return rejectWithValue(apiError ?? { Success: false, Message: "Bilinmeyen bir hata meydana geldi.", StatusCode: 400 })
      }

      return rejectWithValue({ Success: false, Message: "Bilinmeyen bir hata meydana geldi.", StatusCode: 400 } as IApiError);
    }
  }
)

export const authenticationSlice = createSlice({
  name: "authentication",
  initialState,
  reducers: {},
  extraReducers: (builder => {
    builder.addCase(loginUser.fulfilled, (state, action) => {
      state.user = action.payload
    })
  })
})