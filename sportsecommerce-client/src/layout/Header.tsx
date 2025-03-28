import { ShoppingCart } from "@mui/icons-material";
import { AppBar, Badge, Box, Button, IconButton, List, ListItem, Stack, Toolbar } from "@mui/material";
import { Link, NavLink } from "react-router";
import { useAppSelector } from "../hooks/hooks";

const links = [
  { title: "Home", to: "/" },
  { title: "About", to: "/about" },
  { title: "Contact", to: "/contact" }
];

const authLinks = [
  { title: "Login", to: "/login" },
  { title: "Register", to: "/register" } 
];

const navStyles = {
  color: "inherit",
  textDecoration: "none",
  "&:hover": {
    color: "secondary.light"
  },
  "&.active": {
    color: "warning.light"
  }
}

export default function Header() {

  const { cart } = useAppSelector(state => state.cart);
  const itemCount = cart?.cartItems.reduce((total, item) => total + item.quantity, 0)

  return (
    <AppBar position="static" sx={{ mb:4 , backgroundColor: "text.primary" }}>
      <Toolbar sx={{ display: "flex", justifyContent: "space-around" }}>
        <Box>
          <List sx={{ display: "flex" }}>
            { links.map(link =>
              <ListItem key={link.to} component={NavLink} to={link.to} sx={navStyles}>{link.title}</ListItem>) }
          </List>
        </Box>
        <Box sx={{ display: "flex", alignItems: "center" }}>
          <IconButton component={Link} to="/cart" size="large" edge="start" color="inherit">
            <Badge badgeContent={itemCount} color="secondary">
              <ShoppingCart />
            </Badge>
          </IconButton>
          <Stack direction="row">
            { authLinks.map(authLink => 
              <Button key={authLink.to} component={NavLink} to={authLink.to} sx={navStyles}>{authLink.title}</Button>
            )}
          </Stack>
        </Box>
      </Toolbar>
    </AppBar>
  );
}