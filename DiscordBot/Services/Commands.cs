using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Services
{
    public class MoederatorCommands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task ping()
        {
            await ReplyAsync("pong!");
        }
        [Command("Kick")]
        [Discord.Commands.RequireUserPermission(Discord.GuildPermission.KickMembers),]
        public async Task kick(SocketGuildUser user = null)
        {
            if(user == null)
            {
                await Context.Channel.SendMessageAsync("Kein User Angegeben");
                return;
            }
            if(user == Context.User)
            {
                await Context.Channel.SendMessageAsync("man Kann sich nicht selber Kicken");
                return;
            }
            await user.KickAsync();
            await Context.Channel.SendMessageAsync("ich habe @"+ user.Username +" / "+ user.DisplayName +" Gekickt");
        }
    }
}
