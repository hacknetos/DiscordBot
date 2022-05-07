using System;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;
namespace DoscordBot
{
    public class Program : ModuleBase<SocketCommandContext>
    {
        static void Main(string[] args)
        => new Program()
        .RunBotAsync()
        .GetAwaiter()
        .GetResult();

        public static DiscordSocketClient _client;
        private CommandService _commands;
        public IServiceProvider _services;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            string token = Console.ReadLine();
            _client.Log += Client_log;
           await RegisterCommandAsync();
           await _client.LoginAsync(TokenType.Bot, token);
           await _client.StartAsync();

            await _client.SetGameAsync("use sp.");
            await Task.Delay(-1);
            
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), _services);
        }
        public async Task HandleCommandAsync(SocketMessage msg)
        {
            try
            {
                if(msg == null)
                {
                    return;
                }
                var msg2 = msg as SocketUserMessage;
                var context = new SocketCommandContext(_client, msg2);

                if (msg.Author.IsBot) return;
                int argPos = 0;
                if (msg2.HasStringPrefix("sp.", ref argPos))
                {
                    var result = await _commands.ExecuteAsync(context, argPos, _services);
                    if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                    if (result.Error.Equals(CommandError.UnmetPrecondition)) await msg2.Channel.SendMessageAsync(result.ErrorReason);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
        public Task Client_log(LogMessage message) {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }


}