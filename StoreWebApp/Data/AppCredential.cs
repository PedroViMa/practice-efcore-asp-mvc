using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace StoreWebApp.Data
{
    public class AppCredential
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}
