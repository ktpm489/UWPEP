using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Model.Preference;
using VisualExplorer.Resources;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace VisualExplorer.Model.Theme {

    public class ThemeAgency : INotifyPropertyChanged {

        private static volatile ThemeAgency instance;
        private static readonly object SyncRoot = new object();
        private ElementTheme appTheme;

        private ThemeAgency() {
            this.appTheme = PreferenceAgency.Default.ApplicationTheme;
        }

        public static ThemeAgency Instance {
            get {
                if (instance != null) {
                    return instance;
                }

                lock (SyncRoot) {
                    if (instance == null) {
                        instance = new ThemeAgency();
                    }
                }

                return instance;
            }
        }


        public ElementTheme AppTheme {
            get { return this.appTheme; }
            set {
                this.appTheme = value;
                PreferenceAgency.Default.ApplicationTheme = value;
                PreferenceAgency.Default.SetLocalSetting(RIdentifiers.ApplicationTheme, value.ToString());
                this.ReconfigureTitleBarButton();
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Title Bar

        public void ConfigureTitleBar() {

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            this.ReconfigureTitleBarButton();
        }

        private void ReconfigureTitleBarButton() {
            switch (this.appTheme) {
                case ElementTheme.Dark: {
                        var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                        titleBar.ButtonBackgroundColor = Colors.Transparent;
                        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                        titleBar.BackgroundColor = Colors.Transparent;
                        titleBar.ButtonForegroundColor = Colors.White;
                    }
                    break;
                case ElementTheme.Light: {
                        var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                        titleBar.ButtonBackgroundColor = Colors.Transparent;
                        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                        titleBar.BackgroundColor = Colors.Transparent;
                        titleBar.ButtonForegroundColor = Colors.Black;
                    }
                    break;
            }
        }

    }

}
