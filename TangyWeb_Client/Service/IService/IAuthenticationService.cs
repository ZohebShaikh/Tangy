using Tangy_Models;

namespace TangyWeb_Client.Service.IService
{
  public interface IAuthenticationService
  {
    Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest);
    Task<SignInResponseDTO> Login(SignInRequestDTO signInRequest);
    Task Logout();
  }
}
