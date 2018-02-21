using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.ViewFixer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer.UI.ViewFix {

    public sealed partial class TilesFieldFixerPanel : UserControl {

        public TilesFieldFixerPanel() {
            this.InitializeComponent();

            this.DesiredWidthSlider.Value = TilesFieldFixer.Default.DesiredWidth;
            this.DesiredWidthSlider.ValueChanged += (_sender, _e) => {
                TilesFieldFixer.Default.DesiredWidth = _e.NewValue;
            };

            this.ItemHeightSlider.Value = TilesFieldFixer.Default.ItemHeight;
            this.ItemHeightSlider.ValueChanged += (_sender, _e) => {
                TilesFieldFixer.Default.ItemHeight = _e.NewValue; ;
            };
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            TilesFieldFixer.Default.SaveProperties();
        }
    }

}
