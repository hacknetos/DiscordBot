using System;
using System.Threading.Tasks;
using Discord;
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
        public async Task kick(SocketGuildUser user,String Grund = "")
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
            var embed = new EmbedBuilder
            {
                Title = "Kick",
                Color = Color.Orange,
                
                Description = "ich habe @" + user.Username + " / " + user.DisplayName + " Gekickt \n Grund : \n ```" + Grund +"```",
                            };
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
        [Command("TimeOut")]
        [Discord.Commands.RequireUserPermission(Discord.GuildPermission.KickMembers)]
        public async Task Timeout(SocketGuildUser user, double Houers  ,String Grund = "")
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("Kein User Angegeben");
                return;
            }
            if (user == Context.User)
            {
                await Context.Channel.SendMessageAsync("man Kann sich nicht selber Time outen");
                return;
            }
            TimeSpan time = TimeSpan.FromHours(Houers);
            await user.SetTimeOutAsync(time);
            var embed = new EmbedBuilder
            {
                Title = "Time Oute",
                Color = Color.DarkGreen,
                Description = "ich habe @" + user.Username + " / " + user.DisplayName + " Getimeoutet \n Grund : \n ```" + Grund + "``` \n Time (in Stunden):"+time ,
            };
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }


        [Command("TimeOutRemove")]
        [Discord.Commands.RequireUserPermission(Discord.GuildPermission.KickMembers)]
        public async Task TimeoutRemove(SocketGuildUser user)
        {
            if (Context.User == null) return;
            if (Context.User == user) return;
            await user.RemoveTimeOutAsync();
            var embed = new EmbedBuilder
            {
                Title = "Time Oute",
                Color = Color.DarkGreen,
                Description = "ich habe @" + user.Username + " / " + user.DisplayName + " Vom TimeOut Befereid",
            };
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }


        [Command("Ban")]
        [Discord.Commands.RequireUserPermission(Discord.GuildPermission.BanMembers)]
        public async Task Ban (SocketGuildUser user = null , string Grund = "")
        {
             var embed = new EmbedBuilder { Title = "Ban", Color = Color.DarkRed, ImageUrl = "https://icon-library.com/images/ban-icon/ban-icon-19.jpg" };

            if (Context.User == null)
            {
                embed.Description = "Kein Nutzer angegeben";
                await Context.Channel.SendMessageAsync(embed: embed.Build());
                return;
            }

            if (Context.User == user)
            {
                embed.Description = "Kein Nutzer angegeben";
                await Context.Channel.SendMessageAsync(embed: embed.Build());
                return;
            }

            embed.Description = "Sie Wurden Gebannt der Grund : \n ```" + Grund + "``` Wenn sei einspruch Einlegen Möchten Melden sie se sich hier : \n (https://www.Explein.com)";
            await user.SendMessageAsync(embed: embed.Build() );
            embed.Description = "Ich habe " + user.Username + "Gebannt \n Grund : \n ```" + Grund + "```";
            await Context.Channel.SendMessageAsync(embed: embed.Build());
            await user.BanAsync(24,Grund);

        }
    
    }
}
