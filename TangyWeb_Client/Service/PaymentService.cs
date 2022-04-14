using Newtonsoft.Json;
using System.Text;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Service
{
  public class PaymentService : IPaymentService
  {
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient, IConfiguration configuration)
    {
      _httpClient = httpClient;
    }

    public async Task<SuccessModelDTO> Checkout(StripePaymentDTO model)
    {
      try
      {
        var content = JsonConvert.SerializeObject(model);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);
        string responseResult = response.Content.ReadAsStringAsync().Result;
        if (response.IsSuccessStatusCode)
        {
          var result = JsonConvert.DeserializeObject<SuccessModelDTO>(responseResult);
          return result;
        }
        else
        {
          var error = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
          throw new Exception(error.ErrorMessage);
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}
