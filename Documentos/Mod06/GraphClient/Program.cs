using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

namespace GraphCliente
{
    class Program
    {
        private const string _clientId = "312a6482-a29e-43d2-ae43-183e15e1b634";
        private const string _tenantId = "4f8ebfb4-f694-4565-ab13-4bd111c9b4d2";
        public static async Task Main(string[] args)
        {
            IPublicClientApplication app = PublicClientApplicationBuilder.Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            List<string> scopes = new List<string> { "user.read" };

            /*AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
            Console.WriteLine($"Token:\t{result.AccessToken}");*/

            DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);
            GraphServiceClient client = new GraphServiceClient(provider);
            User myProfile = await client.Me.Request().GetAsync();
            Console.WriteLine($"Name:\t{myProfile.DisplayName}");
            Console.WriteLine($"AAD Id:\t{myProfile.Id}");
        }
    }
}
