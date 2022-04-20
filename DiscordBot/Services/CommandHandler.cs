using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Services
{
    public class CommandHandler  
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _services;
        private readonly IConfiguration _configuration;

        public CommandHandler(DiscordSocketClient _client, IServiceProvider _serviceProvider, CommandService _services, IConfiguration _configuration)
        {
            this._client = _client;
            this._serviceProvider = _serviceProvider;
            this._services = _services;
            this._configuration = _configuration;
        }

        public async Task InitializeAsync(CancellationToken cancellation)
        {
            this._client.MessageReceived += OnUserMessageRecive;
        }

        private async Task OnUserMessageRecive(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message)) return; //schaut ob es Eine Nacrichjt war 
            if (message.Source != MessageSource.Bot) return ;// Schaut ob die Nachriht nicht von einem Bot Kamm 

            var argpos = 0; 
            if (!message.HasStringPrefix(this._configuration["Prefix"], ref argpos) && !message.HasMentionPrefix(this._client.CurrentUser,ref argpos)) return; // Schaut ob es einen falschen Prfix hat  //TODO in das Richtige Ändern 


            var context = new SocketCommandContext(this._client, message);
            await this._services.ExecuteAsync(context, argpos, _serviceProvider);
        }

       
    }
}

