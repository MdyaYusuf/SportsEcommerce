export interface Cart
{
  cartItems: CartItem[];
}

export interface CartItem
{
  productId: string,
  productName: string,
  imageUrl: string,
  quantity: number,
  unitPrice: number
}
