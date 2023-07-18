using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TempDesigner.Models;
using System.Text.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Launcher
{
    public sealed partial class LaunchItemUserControl : UserControl
    {
        private LaunchPreset initialPreset;
        public ObservableCollection<string> mapNames;
        bool HasChanges = false;

        // Enable Save/Revert buttons
        private void AChanged(object o, PropertyChangedEventArgs e)
        {
            // yea...
            var initJSON = JsonSerializer.Serialize(initialPreset);
            var newJSON = JsonSerializer.Serialize(o);

            HasChanges = (newJSON != initJSON);
            StateChanged();
        }

        void StateChanged()
        {
            this.SaveBtn.IsEnabled = HasChanges;
            this.SaveBtn.Visibility = HasChanges ? Visibility.Visible : Visibility.Collapsed;

            this.RevertBtn.IsEnabled = HasChanges;
            this.RevertBtn.Visibility = HasChanges ? Visibility.Visible : Visibility.Collapsed;
        }

        public LaunchItemUserControl(ObservableCollection<string> mapNames)
        {
            this.mapNames = mapNames;
            this.InitializeComponent();
            StateChanged();
            //((LaunchPreset)this.DataContext).PropertyChanged += AChanged;
            this.DataContextChanged += LaunchItemUserControl_DataContextChanged;

            //mapNames.Add("None");
            //mapNames.Add("sickmap");
            //mapNames.Add("ffa_wardenglade");
        }

        private void LaunchItemUserControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (initialPreset == null)
            {
                initialPreset = new LaunchPreset(args.NewValue as LaunchPreset);
                ((LaunchPreset)args.NewValue).PropertyChanged += AChanged;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // This is shite. is there a way to copy properly?
            //var pres = this.DataContext as LaunchPreset;

            //initialPreset.ID = pres.ID;
            //initialPreset.Title = pres.Title;
            //initialPreset.Args = pres.Args;
            //initialPreset.SelectedMap = pres.SelectedMap;
            //initialPreset.IsListenServer = pres.IsListenServer;
            //initialPreset.IsHeadless = pres.IsHeadless;

            initialPreset = new LaunchPreset(this.DataContext as LaunchPreset);
            HasChanges = false;

            var window = (Application.Current as App)?.m_window as MainWindow;
            window.SaveLaunchProfile(this.DataContext as LaunchPreset);
            StateChanged();
        }

        private void RevertBtn_Click(object sender, RoutedEventArgs e)
        {
            // This is shite. is there a way to copy properly?
            var pres = this.DataContext as LaunchPreset;

            pres.ID = initialPreset.ID;
            pres.Title = initialPreset.Title;
            pres.Args = initialPreset.Args;
            pres.SelectedMap = initialPreset.SelectedMap;
            pres.IsListenServer = initialPreset.IsListenServer;
            pres.IsHeadless = initialPreset.IsHeadless;

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.HasChanges = !this.HasChanges;
            //StateChanged();
        }

        // Make Title editable

        private void TitleEditBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.TitleEditBox.Visibility = Visibility.Collapsed;
            this.TitleEditBox.IsReadOnly = true;
            this.TitleDisplayBox.Visibility = Visibility.Visible;
        }

        private void TitleDisplayBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.TitleDisplayBox.Visibility = Visibility.Collapsed;
            this.TitleEditBox.Visibility = Visibility.Visible;
            this.TitleEditBox.IsReadOnly = false;
            this.TitleEditBox.Focus(FocusState.Programmatic);
        }

        // FIXME: Copypasta
        // Make Args editable
        private void ArgsEditBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.ArgsEditBox.Visibility = Visibility.Collapsed;
            this.ArgsEditBox.IsReadOnly = true;
            this.ArgsTextBlock.Visibility = Visibility.Visible;

        }

        private void ArgsTextBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.ArgsTextBlock.Visibility = Visibility.Collapsed;
            this.ArgsEditBox.Visibility = Visibility.Visible;
            this.ArgsEditBox.IsReadOnly = false;
            this.ArgsEditBox.Focus(FocusState.Programmatic);

        }

        // Launch
        private void SteamBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = (Application.Current as App)?.m_window as MainWindow;
            window.LaunchGameWithPreset(true, this.DataContext as LaunchPreset);
        }

        private void EpicBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = (Application.Current as App)?.m_window as MainWindow;
            window.LaunchGameWithPreset(false, this.DataContext as LaunchPreset);
        }
    }
}
