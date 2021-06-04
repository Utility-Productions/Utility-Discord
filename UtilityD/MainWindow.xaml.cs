using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace UtilityD
{
    public partial class MainWindow : Window
    {
        Settings settings = new Settings();
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        #region Window
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation d = new DoubleAnimation(0, (Duration)TimeSpan.FromMilliseconds(750));
            d.EasingFunction = new QuarticEase();
            BeginAnimation(FrameworkElement.OpacityProperty, d);
            await Task.Delay(TimeSpan.FromMilliseconds(950));
            Environment.Exit(0);
        }
        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextEditor1.Text = @"Attach:
attach wrd/easy - Attaches to ROBLOX with the chosen api you choose.

Execution:
execfile - Execute a file within discord.
exec - Execute code within discord.

Script Hub:
script reviz/coco - Executes the chosen script.

ROBLOX Commands:
ws [int] - Changes your players walkspeed value.
jp [int] - Changes your players jumppower value.

Bot Commands:
killrblx - Kills all ROBLOX processes.
invite nihon/utility - Invite link to either one or the other servers.
whichapi - Tells you which API is currently attached.
close - Closes the program and shuts off the bot.";
            TopMostCheck.IsChecked = settings.TopMost;
            SetPrefixBox.Text = settings.SavePrefix;
            InputBox1.Text = settings.SaveToken;

            System.Windows.MessageBox.Show("UtilityD Open Source made by:" + "\n" + "Shade#0122 - Main Developer" + "\n" + "ImmuneLion318#1441 - WPF User Interface", "Utility Credits", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void TopMostCheck_Checked(object sender, RoutedEventArgs e)
        {
            settings.TopMost = true;
            settings.Save();
            Topmost = true;
        }
        private void TopMostCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            settings.TopMost = false;
            settings.Save();
            Topmost = false;
        }
        private void SetPrefixButton_Click(object sender, RoutedEventArgs e)
        {
            settings.SavePrefix = SetPrefixBox.Text;
            settings.Save();
            System.Windows.MessageBox.Show("Your prefix has been set: " + SetPrefixBox.Text, "Utility Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void NihonButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/rV3vKju");
        }
        private void UtilityButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.com/invite/UumeGPh5h3");
        }
        private async void SetTokenButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Your token has been set: " + InputBox1.Text, "Utility Information", MessageBoxButton.OK, MessageBoxImage.Information);

            settings.SaveToken = InputBox1.Text;
            settings.Save();

            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            await RegisterCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, InputBox1.Text);
            await _client.SetGameAsync("Utility Productions", "https://discord.com/invite/UumeGPh5h3");
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        #endregion

        #region Discord.net
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix(settings.SavePrefix, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
        #endregion
    }
}
//Coded by Shadee#0122, Desinged by ImmuneLion318#1441