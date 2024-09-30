using System.Text.Json;

namespace StoreWebApp.Data
{
    public class WebApiException : Exception
    {
        public ErrorResponse? Response { get; }

        public WebApiException(string errorJson)
        {
            Console.WriteLine(errorJson);
            Response = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
        }
    }
}
