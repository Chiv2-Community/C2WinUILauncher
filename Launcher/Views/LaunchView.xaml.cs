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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using TempDesigner.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Launcher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LaunchView : Page
    {
        LaunchPreset launchModel { get; set; }

        static List<string> defaultMaps = new List<string>
        {
            "None",
            "Arena_FightingPit",
            "Arena_TournamentGrounds",
            "Arena_TournamentGrounds_Joust",
            "BOW_Falmire",
            "BOW_Galencourt",
            "BOW_Wardenglade",
            "BRAWL_Midsommar",
            "BRAWL_Raid",
            "Brawl_Cathedral",
            "Brawl_GreatHall",
            "Brawl_RudhelmHall",
            "CY_Paul",
            "FFA_Courtyard",
            "FFA_Desert",
            "FFA_FightingPit",
            "FFA_Galencourt",
            "FFA_Hippodrome",
            "FFA_TournamentGrounds",
            "FFA_Wardenglade",
            "Frontend",
            "LTS_Courtyard",
            "LTS_Falmire",
            "LTS_FightingPit",
            "LTS_Galencourt",
            "LTS_TournamentGrounds",
            "LTS_Wardenglade",
            "TDM_Courtyard",
            "TDM_Desert",
            "TDM_FightingPit",
            "TDM_Hippodrome",
            "TDM_TournamentGrounds",
            "TDM_Wardenglade",
            "TDM_Wardenglade_horse",
            "TO_Bridgetown",
            "TO_Bulwark",
            "TO_Coxwell",
            "TO_Falmire",
            "TO_Galencourt",
            "TO_Library",
            "TO_Raid",
            "TO_RudhelmSiege",
            "TO_Stronghold",
            "To_DarkForest",
            "To_Lionspire",
            "Tutorial"
        };

        ObservableCollection<string> mapNames = new ObservableCollection<string>(defaultMaps);
        ObservableCollection<LaunchPreset> ocPresets;
        string presetFile = "I:\\chivmodding\\launcher\\LaunchPresets.json";

        public void LaunchGameWithPreset(bool steam, LaunchPreset preset)
        {
            string exePath = "";
            if (steam)
                exePath = "I:\\SteamLibrary\\steamapps\\common\\Chivalry 2\\TBL\\Binaries\\Win64\\";
            else exePath = "I:\\Epic Games\\Chivalry2\\TBL\\Binaries\\Win64\\";

            exePath += "Chivalry2-Win64-Shipping.exe";

            var args = preset.ConstructLaunchArgs();


            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = args;
            start.FileName = exePath;


            using (Process proc = Process.Start(start))
            {
                // Disable spawning multiple processes?
                //proc.WaitForExit();
            }

        }

        public void SaveLaunchProfile(LaunchPreset preset)
        {
            var id = preset.ID;
            ocPresets[id] = preset;

            string jsonS = JsonSerializer.Serialize(ocPresets, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(presetFile, jsonS);
        }
        public LaunchView()
        {
            this.InitializeComponent();
            string jsonString = File.ReadAllText(presetFile);
            ocPresets = JsonSerializer.Deserialize<ObservableCollection<LaunchPreset>>(jsonString);
            for (int i = 0; i < ocPresets.Count; ++i)
            {
                LaunchItemUserControl lic = new LaunchItemUserControl(mapNames);
                //lic.mapNames = mapNames;
                ocPresets[i].ID = i;
                lic.DataContext = new LaunchPreset(ocPresets[i]);
                this.RootGrid.Children.Add(lic);
            }
        }
    }
}
