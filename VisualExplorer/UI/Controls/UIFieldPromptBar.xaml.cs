using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Framework.Foundation;
using VisualExplorer.Model.Theme;
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

namespace VisualExplorer.UI.Controls {

    public class PromptBarMessage: BindableBase {
        public string _content;
        public PromptBarMessageType type;
        public Action handler;

        public string Content {
            get { return _content; }
            set {
                _content = value;
                OnPropertyChanged("Content");
            }
        }
    }

    public sealed partial class UIFieldPromptBar : UserControl {

        private ThemeAgency themeAgency = ThemeAgency.Instance;

        private PromptBarMessage PromptMessage = new PromptBarMessage();

        public UIFieldPromptBar() {
            this.InitializeComponent();

        }

        public void Show(PromptBarMessage message) {
            PromptMessage = message;
            this.Visibility = Visibility.Visible;
        }
        public void UpdateContent(string content) {
            PromptMessage.Content = content;
        }

        public void Hide() {
            this.Visibility = Visibility.Collapsed;
        }

        private void PromptButton_Click(object sender, RoutedEventArgs e) {
            PromptMessage.handler();
        }
    }

}
