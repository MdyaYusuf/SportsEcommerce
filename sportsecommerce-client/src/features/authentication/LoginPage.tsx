import { LockOutlined } from "@mui/icons-material";
import { Avatar, Box, Button, Container, Paper, TextField, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { ILoginRequest } from "../../model/ILoginRequest";
import { useAppDispatch } from "../../hooks/hooks";
import { useNavigate } from "react-router";
import { loginUser } from "./authenticationSlice";

export default function LoginPage() {

  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<ILoginRequest>({
    defaultValues: {
      email: "",
      password: ""
    }
  });

  async function submitForm(data: ILoginRequest) {
    await dispatch(loginUser(data));
    navigate("/");
  }

  return (
    <Container maxWidth="xs">
      <Paper sx={{ marginTop: 8, padding: 2 }} elevation={3}>
        <Avatar sx={{ mx: "auto", color: "secondary.main", textAlign: "center", mb: 1 }}>
          <LockOutlined />
        </Avatar>
        <Typography component="h1" variant="h5" sx={{ textAlign: "center" }}>Login</Typography>
        <Box component="form" onSubmit={handleSubmit(submitForm)} sx={{ mt: 2 }} noValidate>
          <TextField
            {...register("email", {required: "Email alanı doldurulmalıdır."})}
            label="Enter email"
            size="small" sx={{ mb: 2 }}
            error={!!errors.email}
            helperText={errors.email?.message}
            fullWidth required autoFocus>
          </TextField>
          <TextField
            {...register("password", {required: "Parola alanı doldurulmalıdır.", minLength: { value: 6, message: "Parola en az 6 karakter olmalıdır."}})}
            label="Enter password"
            type="password"
            size="small"
            error={!!errors.password}
            helperText={errors.password?.message}
            sx={{ mb: 2 }}
            fullWidth required>
          </TextField>
          <Button type="submit" variant="contained" loading={isSubmitting} sx={{ mt: 1 }} fullWidth>Login</Button>
        </Box>
      </Paper>
    </Container>
  );
}