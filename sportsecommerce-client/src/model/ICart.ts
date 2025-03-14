export interface CartItem
{
  productId: string,
  productName: string,
  quantity: number,
  unitPrice: number
}

export interface Cart
{
  cartItems: CartItem[];
}