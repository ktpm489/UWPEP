using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer.Pages.Fields.Tiles {

    public sealed partial class TileItemContentPanel : UserControl {

        private StorageEntry Entry {
            get { return this.DataContext as StorageEntry; }
        }

        public TileItemContentPanel() {
            this.InitializeComponent();

            this.DataContextChanged += (_sender, _arg) => {
                this.Bindings.Update();
            };
        }
    }

}
