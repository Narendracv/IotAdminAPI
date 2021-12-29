
//#define Certificate 
#define Managed
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IotAdminAPI.Logging;
using Serilog;
#if Managed
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
#endif

#if Certificate
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using IotAdminAPI.Logging;
using Serilog;
#endif
namespace IotAdminAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

#if Certificate
        // <snippet1>
        // using System.Linq;
        // using System.Security.Cryptography.X509Certificates;
        // using Azure.Extensions.AspNetCore.Configuration.Secrets;
        // using Azure.Identity;

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)

                .ConfigureAppConfiguration((context, config) =>
                {

                    if (context.HostingEnvironment.IsProduction())
                    {
                        var builtConfig = config.Build();

                        using var store = new X509Store(StoreLocation.LocalMachine);
                        store.Open(OpenFlags.ReadOnly);
                        var certs = store.Certificates.Find(
                            X509FindType.FindByThumbprint,
                            builtConfig["AzureADCertThumbprint"], false);

                        config.AddAzureKeyVault(new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                                                new ClientCertificateCredential(builtConfig["AzureADDirectoryId"], builtConfig["AzureADApplicationId"], certs.OfType<X509Certificate2>().Single()),
                                                new KeyVaultSecretManager());

                        store.Close();
                    }
                })

                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>()

                ).UseSerilog(SeriLogger.Configure);
        // </snippet1>
#endif

#if Managed
         //<snippet2>
         //using Azure.Security.KeyVault.Secrets;
         //using Azure.Identity;
         //using Azure.Extensions.AspNetCore.Configuration.Secrets;

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                    {
                        var builtConfig = config.Build();
                        var secretClient = new SecretClient(
                            new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                            new DefaultAzureCredential());
                        config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                    }
                })
                 
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>()).UseSerilog(SeriLogger.Configure);
        // </snippet2>
#endif
    }
}
