import { Container, Typography } from "@mui/material";
import { useLocation } from "react-router";

export default function ServerError() {

  const { state } = useLocation();

  return (
    <Container>
      {
        state?.error ? (
            <Typography variant="h3" gutterBottom>
              {state.error.message} - {state.error.statusCode}
            </Typography>
        ) : (
            <Typography>
              Server Error.
            </Typography>
        )
      }
    </Container>
  );
}