﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using FirstBotDiscord.Commands;
using FirstBotDiscord.Configurations;
using FirstBotDiscord.Database;
using FirstBotDiscord.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FirstBotDiscord.Bot
{
    public class Bot
    {
        public IConfiguration Configuration { get; }
        public DataContext Database { get; private set; }
        public StartsRepository StartsRepository { get; private set; }

        public static void Main(string[] args) =>
            new Bot().RodandoBot(args).GetAwaiter().GetResult();

        public async Task RodandoBot(string[] args)
        {

            var discord = new DiscordClient(new DiscordConfiguration
            {
                Token = Parameters.token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });

            this.Database = new DataContext();
            this.StartsRepository = new StartsRepository(this.Database);
            var services = new ServiceCollection()
                .AddSingleton(this.Database)
                .AddSingleton(this.StartsRepository)
                .BuildServiceProvider();


            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                Services = services,
                StringPrefixes = Parameters.Prefix
            });

            commands.RegisterCommands<StartCommands>();
            commands.RegisterCommands<LavaLinkCommands>();
            commands.RegisterCommands<B3Commands>();

            //B3Api.B3Api.B3(args);

            var endPoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1",
                Port = 2333
            };

            var lavaLinkConfig = new LavalinkConfiguration
            {
                Password = "youshallnotpass",
                RestEndpoint = endPoint,
                SocketEndpoint = endPoint
            };

            var lavalink = discord.UseLavalink();
            await discord.ConnectAsync();
            await lavalink.ConnectAsync(lavaLinkConfig);

            //espera infinita, para o bot ficar online continuamente.
            await Task.Delay(-1);
        }
    }
}

