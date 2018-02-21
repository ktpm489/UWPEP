using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.Preview;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer.UI.PrevisualView {

    public sealed partial class UIPrevisualImage : UserControl {

        private MPrevisualImage ImageContent => this.DataContext as MPrevisualImage;

        public UIPrevisualImage() {
            this.InitializeComponent();

            this.DataContextChanged += (_sender, _args) => {
                this.Bindings.Update();
            };
        }

    }
}
