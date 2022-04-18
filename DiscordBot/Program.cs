using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Discord.Addons.Hosting;
using Discord.WebSocket;

namespace Bot
{
    internal class Program
    {
        static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(x =>
                {
                    var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json",false,true)
                    .Build();

                    x.AddConfiguration(configuration);
                })
                .ConfigureLogging(x => {

                    x.AddConsole();
                    x.SetMinimumLevel(LogLevel.Debug);

                    
                })
                .ConfigureDiscordHost<DiscordSocketClient>((Context , config) =>
                {
                    config.SoketConfig = new DiscordSocketConfig
                    {
                        LogLevel = Discord.Debug,
                        AlwaysDownloadUsers = fasle,
                        MessageCacheSize = 100,

                    };
                    config.token = Context.Configuration["Token"]
                })
        }
    }
}