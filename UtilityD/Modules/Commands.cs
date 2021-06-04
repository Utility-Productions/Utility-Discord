using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace UtilityD.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        WebClient wc = new WebClient();
        WeAreDevs_API.ExploitAPI wrdapi = new WeAreDevs_API.ExploitAPI();
        EasyExploits.Module easyapi = new EasyExploits.Module();

        #region Embed
        public async Task SuccessfulEmbed(string Desc)
        {
            var embed = new Discord.EmbedBuilder();
            embed.WithTitle("Success Command");
            embed.WithDescription(Desc);
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/783276775429767179/799249486936277032/Logo2.png");
            embed.WithFooter("Utility Productions");
            embed.WithColor(Color.Green);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        public async Task ErrorEmbed(string Desc)
        {
            var embed = new Discord.EmbedBuilder();
            embed.WithTitle("Error Command");
            embed.WithDescription(Desc);
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/783276775429767179/799249486936277032/Logo2.png");
            embed.WithFooter("Utility Productions");
            embed.WithColor(Color.Red);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        public async Task WarningEmbed(string Desc)
        {
            var embed = new Discord.EmbedBuilder();
            embed.WithTitle("Warn Command");
            embed.WithDescription(Desc);
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/783276775429767179/799249486936277032/Logo2.png");
            embed.WithFooter("Utility Productions");
            embed.WithColor(Color.Orange);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        public async Task InfoEmbed(string Desc)
        {
            var embed = new Discord.EmbedBuilder();
            embed.WithTitle("Info Command");
            embed.WithDescription(Desc);
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/783276775429767179/799249486936277032/Logo2.png");
            embed.WithFooter("Utility Productions");
            embed.WithColor(Color.Blue);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        #endregion

        #region Script Hub
        [Command("script reviz")]
        public async Task Reviz()
        {
            if (easyapi.IsAttached() == true)
            {
                easyapi.ExecuteScript(wc.DownloadString("https://raw.githubusercontent.com/Utility-Productions/Utility/main/Scripts/Reviz%20Admin.lua"));
                await SuccessfulEmbed("Script executed: reviz");
            }
            else if (wrdapi.isAPIAttached() == true)
            {
                wrdapi.SendLimitedLuaScript(wc.DownloadString("https://raw.githubusercontent.com/Utility-Productions/Utility/main/Scripts/Reviz%20Admin.lua"));
                await SuccessfulEmbed("Script executed: reviz");
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before executing a script!");
                }
                else
                {
                    await ErrorEmbed("Please attach to ROBLOX before executing a script!");
                }
            }
        }

        [Command("script coco")]
        public async Task Coco()
        {
            if (easyapi.IsAttached() == true)
            {
                easyapi.ExecuteScript(wc.DownloadString("https://raw.githubusercontent.com/Utility-Productions/Utility/main/Scripts/Coco%20Hub.lua"));
                await SuccessfulEmbed("Script executed: coco");
            }
            else if (wrdapi.isAPIAttached() == true)
            {
                wrdapi.SendLimitedLuaScript(wc.DownloadString("https://raw.githubusercontent.com/Utility-Productions/Utility/main/Scripts/Coco%20Hub.lua"));
                await SuccessfulEmbed("Script executed: coco");
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before executing a script!");
                }
                else
                {
                    await ErrorEmbed("Please attach to ROBLOX before executing a script!");
                }
            }
        }

        #endregion

        #region ROBLOX Commands
        [Command("ws")]
        public async Task Walkspeed(string echo)
        {
            if (easyapi.IsAttached() == true)
            {
                easyapi.ExecuteScript("game:GetService('Players').LocalPlayer.Character.Humanoid.WalkSpeed = " + echo);
                await SuccessfulEmbed("Command executed: ws " + echo);
            }
            else if (wrdapi.isAPIAttached() == true)
            {
                wrdapi.SendLuaScript("game:GetService('Players').LocalPlayer.Character.Humanoid.WalkSpeed = " + echo);
                await SuccessfulEmbed("Command executed: ws " + echo);
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before executing a command!");
                }
                else
                {
                    await ErrorEmbed("Please attach to ROBLOX before executing a command!");
                }
            }
        }

        [Command("jp")]
        public async Task JumpPower(string echo)
        {
            if (easyapi.IsAttached() == true)
            {
                easyapi.ExecuteScript("game:GetService('Players').LocalPlayer.Character.Humanoid.JumpPower = " + echo);
                await SuccessfulEmbed("Command executed: jp " + echo);
            }
            else if (wrdapi.isAPIAttached() == true)
            {
                wrdapi.SendLuaScript("game:GetService('Players').LocalPlayer.Character.Humanoid.JumpPower = " + echo);
                await SuccessfulEmbed("Command executed: jp " + echo);
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before executing a command!");
                }
                else
                {
                    await ErrorEmbed("Please attach to ROBLOX before executing a command!");
                }
            }
        }
        #endregion

        #region Bot Commands
        [Command("killrblx")]
        public async Task KillRoblox()
        {
            if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
            {
                await ErrorEmbed("No ROBLOX processes were found!");
            }
            else
            {
                foreach (var process in Process.GetProcessesByName("RobloxPLayerBeta"))
                {
                    process.Kill();
                }
                await SuccessfulEmbed("Command executed: killrblx");
            }
        }

        [Command("close")]
        public async Task Close()
        {
            await SuccessfulEmbed("The bot will shortly go offline! Thank you for using UtilityD!");
            Environment.Exit(1);
        }

        [Command("whichapi")]
        public async Task WhichAPI()
        {
            if (wrdapi.isAPIAttached() == true)
            {
                await InfoEmbed("WeAreDevs API is currently attached!");
            }
            else if (easyapi.IsAttached() == true)
            {
                await InfoEmbed("WeAreDevs API is currently attached!");
            }    
            else
            {
                await InfoEmbed("No API is currently attached!");
            }
        }

        [Command("invite utility")]
        public async Task InviteUtility()
        {
            await InfoEmbed("Discord invite: https://discord.com/invite/UumeGPh5h3");
        }

        [Command("invite nihon")]
        public async Task InviteNihon()
        {
            await InfoEmbed("Discord invite: https://discord.gg/rV3vKju");
        }
        #endregion

        #region Attach/Execution
        [Command("attach wrd")]
        public async Task Wrd()
        {
            if (wrdapi.isAPIAttached() == true)
            {
                await WarningEmbed("WeAreDevs API is already attached to ROBLOX!");
            }
            else if (easyapi.IsAttached() == true)
            {
                await WarningEmbed("EasyExploits API is already attached to ROBLOX");
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before attaching!");
                }
                else
                {
                    wrdapi.LaunchExploit();
                    while (wrdapi.isAPIAttached() == false)
                    {
                        await Task.Delay(1);
                    }
                    if (wrdapi.isAPIAttached() == true)
                    {
                        await Task.Delay(2000);
                        foreach (FileInfo file in new DirectoryInfo(Directory.GetCurrentDirectory() + "\\autoexec").GetFiles("*.txt"))
                        {
                            wrdapi.SendLimitedLuaScript(File.ReadAllText($"./autoexec/{file.Name}"));
                        }
                        foreach (FileInfo file in new DirectoryInfo(Directory.GetCurrentDirectory() + "\\autoexec").GetFiles("*.lua"))
                        {
                            wrdapi.SendLimitedLuaScript(File.ReadAllText($"./autoexec/{file.Name}"));
                        }
                    }
                    await SuccessfulEmbed("Successfully attached!");
                }
            }
        }

        [Command("attach easy")]
        public async Task Easy()
        {
            if (wrdapi.isAPIAttached() == true)
            {
                await WarningEmbed("WeAreDevs API is already attached to ROBLOX!");
            }
            else if (easyapi.IsAttached() == true)
            {
                await WarningEmbed("EasyExploits API is already attached to ROBLOX");
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before attaching!");
                }
                else
                {
                    easyapi.LaunchExploit();
                    while (easyapi.IsAttached() == false)
                    {
                        await Task.Delay(1);
                    }
                    if (easyapi.IsAttached() == true)
                    {
                        await Task.Delay(2000);
                        foreach (FileInfo file in new DirectoryInfo(Directory.GetCurrentDirectory() + "\\autoexec").GetFiles("*.txt"))
                        {
                            easyapi.ExecuteScript(File.ReadAllText($"./autoexec/{file.Name}"));
                        }
                        foreach (FileInfo file in new DirectoryInfo(Directory.GetCurrentDirectory() + "\\autoexec").GetFiles("*.lua"))
                        {
                            easyapi.ExecuteScript(File.ReadAllText($"./autoexec/{file.Name}"));
                        }
                    }
                    await SuccessfulEmbed("Successfully attached!");
                }
            }
        }

        [Command("execfile")]
        public async Task ExecuteFile()
        {
            if (wrdapi.isAPIAttached() == true)
            {
                using (var HttpClient = new HttpClient())
                    wrdapi.SendLimitedLuaScript(await HttpClient.GetStringAsync(Context.Message.Attachments.First().Url));
                await SuccessfulEmbed("File executed!");
            }
            else if (easyapi.IsAttached() == true)
            {
                using (var HttpClient = new HttpClient())
                    easyapi.ExecuteScript(await HttpClient.GetStringAsync(Context.Message.Attachments.First().Url));
                await SuccessfulEmbed("File executed!");
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before executing a file!");
                }
                else
                {
                    await ErrorEmbed("Please attach to ROBLOX before executing a file!");
                }
            }
        }

        [Command("exec")]
        public async Task Execute(string echo)
        {
            var embed = new Discord.EmbedBuilder();
            if (wrdapi.isAPIAttached() == true)
            {
                wrdapi.SendLimitedLuaScript(echo);
                await SuccessfulEmbed("Code executed: " + echo);
            }
            else if (easyapi.IsAttached() == true)
            {
                easyapi.ExecuteScript(echo);
                await SuccessfulEmbed("Code executed: " + echo);
            }
            else
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                {
                    await ErrorEmbed("Please open ROBLOX before executing code!");
                }
                else
                {
                    await ErrorEmbed("Please attach to ROBLOX before executing code!");
                }
            }
        }
        #endregion
    }
}
//Coded by Shadee#0122, Desinged by ImmuneLion318#1441