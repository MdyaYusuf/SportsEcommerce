import { Container, CssBaseline } from "@mui/material";
import { Outlet } from "react-router";
import Header from "./Header";

function App() {

  return (
    <>
      <CssBaseline />
      <Header />
      <Container>
        <Outlet />
      </Container>
    </>
  );
}

export default App
