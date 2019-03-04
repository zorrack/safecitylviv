using System;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Transactions;
using Orleans.Persistence.AzureStorage;
using SafeCity.Grains;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SafeCity.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            RunMainAsync().Wait();
        }

        private static async Task RunMainAsync()
        {
            try
            {
                var host = await StartHost();

                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync();
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task<ISiloHost> StartHost()
        {
            var hostBuilder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .AddMemoryGrainStorage("Memory")
                .AddAzureBlobGrainStorage("Azure", 
                    options =>
                    {
                        options.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=safecitystorageaccount;AccountKey=wvQUyVg6pE5pA/BH5h7ZjnctZqnzRrlyLU/bOA39IJzK+OOQCWNB1sBIKQKCWJZPmtcWILcIz1pzhLBnKfGpKg==;EndpointSuffix=core.windows.net";
                        options.ContainerName = "safecityblobstorage";
                    }
                )
                .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = "dev";
                       options.ServiceId = "SafeCity";
                   })
                .ConfigureApplicationParts(parts =>
                   {
                       parts.AddApplicationPart(typeof(IssueMessage).Assembly).WithReferences();
                       //parts.AddApplicationPart(typeof(User).Assembly).WithReferences();
                   })
                .ConfigureLogging(logging => logging.AddConsole());
            
            var host = hostBuilder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
