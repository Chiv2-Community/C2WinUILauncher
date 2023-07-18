using Launcher.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using TempDesigner.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Launcher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ServerView : Page
    {
        public ServerView()
        {
            this.InitializeComponent();
            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //String Root = Directory.GetCurrentDirectory();
            //var dir = Directory.GetCurrentDirectory();
            string jsonString = File.ReadAllText("C:\\Users\\rmary\\source\\repos\\Launcher\\Launcher\\Assets\\serverlist.json");
            var servers = JsonSerializer.Deserialize<ServerList>(jsonString);

            var games = servers.Data.Games;
            for (int i = 0; i < games.Count; ++i)
            {
                if (games[i].PlayerUserIds.Count <= 10)
                    continue;

                ServerListItemUserControl sic = new ServerListItemUserControl();
                sic.DataContext = games[i];
                this.RootGrid.Children.Add(sic);
            }
            return;
        }
    }
}
