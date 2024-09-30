namespace StoreWebApp_API.Authority
{
    public class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        { 
            new Application
            {
                ApplicationId = 1,
                ApplicationName = "StoreWebApp",
                ClientId = "914D22E3-EFF8-4810-B971-13775FAC4E74",
                Secret = "945513FB-56CF-4F1A-B921-12E2FB455BCC",
                Scopes = "read,write"
            }
        };

        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(x => x.ClientId == clientId);
        }
    }
}
