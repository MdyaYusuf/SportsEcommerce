import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { IApiError } from "../model/IApiError";
import { router } from "../router/Routes";

axios.defaults.baseURL = "http://localhost:5110/api/";

axios.interceptors.response.use(response => {
  return response;
}, (error: AxiosError) => {
  if (!error.response) {
    toast.error("Server cevap vermiyor.");
    return Promise.reject(error);
  }
  const { data, status } = error.response as AxiosResponse;
  const apiError = data as IApiError;
  const errorMessage = apiError?.message ?? "Bilinmeyen bir hata meydana geldi";
  switch (status)
  {
    case 400:
      toast.error(errorMessage);
      break;
    case 401:
      toast.error(errorMessage);
      break;
    case 404:
      router.navigate("/not-found");
      break;
    case 500:
      router.navigate("/server-error", { state: { error: data, status: status } });
      break;
    default:
      break;
  }
  return Promise.reject(error.response);
})

const queries = {
  get: (url: string) => axios.get(url).then((response: AxiosResponse) => response.data),
  post: (url: string, body: object) => axios.post(url, body).then((response: AxiosResponse) => response.data),
  put: (url: string, body: object) => axios.put(url, body).then((response: AxiosResponse) => response.data),
  delete: (url: string) => axios.delete(url).then((response: AxiosResponse) => response.data)
}

const homePage = {
  list: () => queries.get("Products/getall"),
  details: (id: string) => queries.get(`Products/getbyid/${id}`)
}

const requests = {
  homePage
}

export default requests
