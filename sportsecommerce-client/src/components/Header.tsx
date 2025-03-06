import { ShoppingCart } from "@mui/icons-material";
import { AppBar, Badge, Box, IconButton, List, ListItem, Toolbar } from "@mui/material";
import { NavLink } from "react-router";

const links = [
  { title: "Home", to: "/" },
  { title: "About", to: "/about" },
  { title: "Contact", to: "/contact" }
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

  return (
    <AppBar position="static" sx={{ mb:4 , backgroundColor: "text.primary" }}>
      <Toolbar sx={{ display: "flex", justifyContent: "space-around" }}>
        <Box>
          <List sx={{ display: "flex" }}>
            { links.map(link =>
              <ListItem key={link.to} component={NavLink} to={link.to} sx={navStyles}>{link.title}</ListItem>) }
          </List>
        </Box>
        <Box>
          <IconButton size="large" edge="start" color="inherit">
            <Badge badgeContent="2" color="secondary">
              <ShoppingCart />
            </Badge>
          </IconButton>
        </Box>
      </Toolbar>
    </AppBar>
  );
}