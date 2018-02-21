using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.Theme;
using VisualExplorer.Model.MWindow;
using VisualExplorer.Pages.Base;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer {

    public sealed partial class MainPage : ThemePage {

        public MainPage() {
            this.InitializeComponent();

            ThemeAgency.Instance.ConfigureTitleBar();
            Window.Current.SetTitleBar(MainTitleBar);
            MainFrame.Navigate(typeof(Pages.PageAccessContent));
        }

        // Navigation View

        private void NavView_Loaded(object sender, RoutedEventArgs e) {
            NavView.MenuItems.Add(new NavigationMenuItem { Text = "Navigation", Icon = new SymbolIcon(Symbol.GoToStart), Tag = "Navigation" });
            NavView.MenuItems.Add(new NavigationMenuItem { Text = "Games", Icon = new SymbolIcon(Symbol.Video), Tag = "games" });
            NavView.MenuItems.Add(new NavigationMenuItem { Text = "Music", Icon = new SymbolIcon(Symbol.Audio), Tag = "music" });
            NavView.MenuItems.Add(new NavigationMenuItemSeparator());
            NavView.MenuItems.Add(new NavigationMenuItem() { Text = "My content", Icon = new SymbolIcon(Symbol.Folder), Tag = "content" });

            foreach (var item in NavView.MenuItems) {
                if (item is NavigationMenuItem navItem) {
                    navItem.Invoked += NavItem_Invoked;
                }
            }

        }

        private void NavItem_Invoked(NavigationMenuItem sender, object args) {
            switch (sender.Tag) {
                case "Navigation": MainFrame.Navigate(typeof(Pages.PageAccessContent)); break;
                case "games": break;
                case "music": break;
                case "content": break;
            }
        }

        private void NavView_SettingsInvoked(NavigationView sender, object args) {
            MainFrame.Navigate(typeof(Pages.Preference.PagePreference));
        }

        private void NewWindowBtn_Invoked(NavigationMenuItem sender, object args) {
            WindowAdministrator.Default.CreateNewMainWindowAsync();
        }

    }
}
