export interface ICart
{
  cartItems: ICartItem[];
}

export interface ICartItem
{
  productId: string,
  productName: string,
  imageUrl: string,
  quantity: number,
  unitPrice: number
}
