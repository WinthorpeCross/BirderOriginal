using System;
using Birder2.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System.IO;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace Birder2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    DbInitialiser.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                       .ConfigureAppConfiguration((ctx, builder) =>
                       {
                           var keyVaultEndpoint = GetKeyVaultEndpoint();
                           if (!string.IsNullOrEmpty(keyVaultEndpoint))
                           {
                               var azureServiceTokenProvider = new AzureServiceTokenProvider();
                               var keyVaultClient = new KeyVaultClient(
                                   new KeyVaultClient.AuthenticationCallback(
                                       azureServiceTokenProvider.KeyVaultTokenCallback));
                               builder.AddAzureKeyVault(
                                   keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                           }
                       }
         )
                //.ConfigureAppConfiguration((context, config) =>
                //{
                //    #region snippet1
                //    config.SetBasePath(Directory.GetCurrentDirectory())
                //        .AddJsonFile("appsettings.json", optional: false)
                //        .AddEnvironmentVariables();

                //    var builtConfig = config.Build();

                //    config.AddAzureKeyVault(
                //        $"https://{builtConfig["Vault"]}.vault.azure.net/",
                //        builtConfig["ClientId"],
                //        builtConfig["ClientSecret"]);
                //    #endregion
                //})
                .UseStartup<Startup>()
                .Build();

        private static string GetKeyVaultEndpoint() => "https://birder45378.vault.azure.net";
    }
}