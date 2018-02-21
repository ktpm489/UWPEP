using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.Preference;
using VisualExplorer.Model.Theme;
using VisualExplorer.Pages.Base;
using VisualExplorer.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer.Pages.Preference {

    public sealed partial class PagePreference : ThemePage {

        public PagePreference() {
            this.InitializeComponent();


            switch (ThemeAgency.Instance.AppTheme) {
                case ElementTheme.Dark:  this.DarkRadioBtn.IsChecked  = true; break;
                case ElementTheme.Light: this.LightRadioBtn.IsChecked = true; break;
            }

            var enterAction = (string)PreferenceAgency.Default.GetLocalSetting(RIdentifiers.FiledEnterKeyAction);
            switch(enterAction) {
                case RIdentifiers.EnterKeyAction.RenameEntry: EnterActionCombox.SelectedIndex = 0; break;
                case RIdentifiers.EnterKeyAction.LaunchEntry: EnterActionCombox.SelectedIndex = 1; break;
            }
            EnterActionCombox.SelectionChanged += EnterActionCombox_SelectionChanged;

            DeleteDialogSwitch.IsOn = (bool)PreferenceAgency.Default.GetLocalSetting(RIdentifiers.ShowDeleteConfirmation);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            base.OnNavigatedFrom(e);

            switch (ThemeAgency.Instance.AppTheme) {
                case ElementTheme.Dark:  this.DarkRadioBtn.IsChecked  = true; break;
                case ElementTheme.Light: this.LightRadioBtn.IsChecked = true; break;
            }
        }

        private void DarkRadioBtn_Click(object sender, RoutedEventArgs e) {
            if (ThemeAgency.Instance.AppTheme == ElementTheme.Light) {
                ThemeAgency.Instance.AppTheme = ElementTheme.Dark;
                (sender as RadioButton).IsChecked = true;
            }
        }

        private void LightRadioBtn_Click(object sender, RoutedEventArgs e) {
            if (ThemeAgency.Instance.AppTheme == ElementTheme.Dark) {
                ThemeAgency.Instance.AppTheme = ElementTheme.Light;
                (sender as RadioButton).IsChecked = true;
            }
        }

        private void EnterActionCombox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var boxItem = e.AddedItems[0] as ComboBoxItem;
            switch (boxItem.Tag as string) {
                case "1": PreferenceAgency.Default.SetLocalSetting(RIdentifiers.FiledEnterKeyAction, RIdentifiers.EnterKeyAction.RenameEntry); break;
                case "2": PreferenceAgency.Default.SetLocalSetting(RIdentifiers.FiledEnterKeyAction, RIdentifiers.EnterKeyAction.LaunchEntry); break;
            }
        }

        private void DeleteDialogSwitch_Toggled(object sender, RoutedEventArgs e) {
            PreferenceAgency.Default.SetLocalSetting(RIdentifiers.ShowDeleteConfirmation, DeleteDialogSwitch.IsOn);
        }
    }
}
