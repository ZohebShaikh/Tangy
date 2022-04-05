using Blazored.LocalStorage;
using Tangy_Common;
using TangyWeb_Client.Service.IService;
using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Service
{
  public class CartService : ICartService
  {
    private readonly ILocalStorageService _localStorage;
    public event Action OnChange;
    public CartService(ILocalStorageService localStorage)
    {
      _localStorage = localStorage;
    }

    public async Task IncrementCart(ShoppingCartVM addToCart)
    {
      var cart = await _localStorage.GetItemAsync<List<ShoppingCartVM>>(StaticDetails.ShoppingCart);
      bool itemInCart = false;

      if (cart == null)
      {
        cart = new List<ShoppingCartVM>();
      }
      foreach (var item in cart)
      {
        if (item.ProductId == addToCart.ProductId && item.ProductPriceId == addToCart.ProductPriceId)
        {
          itemInCart = true;
          item.Count += addToCart.Count;
        }
      }
      if (!itemInCart)
      {
        cart.Add(new ShoppingCartVM()
        {
          ProductId = addToCart.ProductId,
          ProductPriceId = addToCart.ProductPriceId,
          Count = addToCart.Count
        });
      }

      await _localStorage.SetItemAsync(StaticDetails.ShoppingCart, cart);
      OnChange.Invoke();
    }
    public async Task DecrementCart(ShoppingCartVM removeFromCart)
    {
      var cart = await _localStorage.GetItemAsync<List<ShoppingCartVM>>(StaticDetails.ShoppingCart);

      // if count is 0 or 1 then we remove the item
      for (int i = 0; i < cart.Count; i++)
      {
        if (cart[i].ProductId == removeFromCart.ProductId && cart[i].ProductPriceId == removeFromCart.ProductPriceId)
        {
          if (cart[i].Count == 1 || removeFromCart.Count == 0)
          {
            cart.Remove(cart[i]);
          }
          else
          {
            cart[i].Count -= removeFromCart.Count;
          }
        }
      }
      await _localStorage.SetItemAsync(StaticDetails.ShoppingCart, cart);
      OnChange.Invoke();
    }
  }
}
