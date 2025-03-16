import { createContext, PropsWithChildren, useState, useContext } from "react";
import { Cart } from "../model/ICart";

interface CartContextValue {
  cart: Cart | null,
  setCart: (cart: Cart) => void
}

export const CartContext = createContext<CartContextValue | undefined>(undefined);

export function useCartContext() {

  const context = useContext(CartContext);

  if (context === undefined) {
    throw new Error("Provider yok.")
  }

  return context;
}

export function CartContextProvider({ children }: PropsWithChildren<object>) {

  const [cart, setCart] = useState<Cart | null>(null);

  return (
    <CartContext.Provider value={{ cart, setCart }}>
      {children}
    </CartContext.Provider>
  );
}