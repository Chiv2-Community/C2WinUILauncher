using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.WindowsAppSDK.Runtime;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Launcher;

namespace TempDesigner.Models
{
    public class launchpresets
    {
        public List<LaunchPreset> Presets { get; set; }
    }

    public class LaunchPreset : INotifyPropertyChanged
    {
        private int id;
        private string title;
        private string path;
        private string args;
        private string selectedmap;
        private bool islistenserver;
        private bool isheadless;

        public LaunchPreset() { }

        // Fixme: deep clone instead?
        public LaunchPreset(LaunchPreset preset)
        {
            this.ID = preset.id;
            this.Title = preset.title;
            this.path = preset.path;
            this.Args = preset.args;
            this.SelectedMap = preset.selectedmap;
            this.IsListenServer = preset.islistenserver;
            this.IsHeadless = preset.isheadless;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ConstructLaunchArgs()
        {
            string args = "";

            if (this.SelectedMap != "None")
            {
                args += this.SelectedMap;
                if (this.IsListenServer)
                    args += "?listen";
            }

            if (this.IsHeadless)
                args += " -nullrhi";

            if (this.Args.Length > 0)
            {
                args += " ";
                args += this.Args;
            }

            return args;
        }

        //FIXME? This is so ugly
        public int ID
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.OnPropertyChanged();
            }
        }
        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                this.OnPropertyChanged();
            }
        }
        public string Args
        {
            get { return this.args; }
            set
            {
                this.args = value;
                this.OnPropertyChanged();
            }
        }
        public string SelectedMap
        {
            get { return this.selectedmap; }
            set
            {
                this.selectedmap = value;
                this.OnPropertyChanged();
            }
        }
        public bool IsListenServer
        {
            get { return this.islistenserver; }
            set
            {
                this.islistenserver = value;
                this.OnPropertyChanged();
            }
        }
        public bool IsHeadless
        {
            get { return this.isheadless; }
            set
            {
                this.isheadless = value;
                this.OnPropertyChanged();
            }
        }


        public static LaunchPreset GetDefaultPReset()
        {
            var ps = new LaunchPreset()
            {
                ID = 0,
                Title = "Official launch",
                Args = "",
                SelectedMap = "FFA_Wardenglade",
                IsListenServer = false,
                IsHeadless = false,
            };
            return ps;
        }
    }

    [JsonSerializable(typeof(LaunchPreset))]
    internal partial class LaunchPresetContext : JsonSerializerContext
    { }

}
