using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Service.IService
{
  public interface ICartService
  {
    public event Action OnChange;
    Task DecrementCart(ShoppingCartVM addToCart);
    Task IncrementCart(ShoppingCartVM removeFromCart);
  }
}
