using System.Net;

namespace BankMicroservice.Persistances.ReturnTypes
{
  public class ReturnModel<T>
  {
    public HttpStatusCode HttpStatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public T DataTitle { get; set; }

  }
}
