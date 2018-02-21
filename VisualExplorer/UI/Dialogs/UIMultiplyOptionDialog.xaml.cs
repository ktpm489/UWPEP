using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer.UI.Dialogs {

    public sealed partial class UIMultiplyOptionDialog : ThemeContentDialog {

        private List<string> Options;
        private string description;
        private string title;

        private int selectedIndex = -1;

        public UIMultiplyOptionDialog(List<string> options, string description, string title) {
            this.Options = options;
            this.description = description;
            this.title = title;

            this.InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var clickedButton = sender as Button;
            selectedIndex = Options.IndexOf(clickedButton.Content as string);
            this.Hide();
        }

        public async Task<string> DisplayAsync() {
            await this.ShowAsync();

            return selectedIndex == -1 ? null : Options[selectedIndex];
        }
    }
}
