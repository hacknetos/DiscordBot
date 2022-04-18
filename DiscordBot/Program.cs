using System;
using System.IO;
using System.Threading.Tasks;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using Discord.Commands;

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
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                    x.AddConfiguration(configuration);
                })
                .ConfigureLogging(x =>
                {

                    x.AddConsole();
                    x.SetMinimumLevel(LogLevel.Debug);


                })
                .ConfigureDiscordShardedHost((Context, config) =>
                {
                    config.SocketConfig = new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Debug,
                        AlwaysDownloadUsers = false,
                        MessageCacheSize = 100,

                    };
                    config.Token = Context.Configuration["Token"];
                })
                .UseCommandService((Context, Config) =>
                {
                    Config.CaseSensitiveCommands = false;
                    Config.LogLevel = LogSeverity.Debug;
                    Config.DefaultRunMode = RunMode.Async;
                })
                .ConfigureServices((Context, services) =>
                {
                    //services
                    //    .AddHostedService<CommandHandler>();
                })
                .UseConsoleLifetime();

            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}