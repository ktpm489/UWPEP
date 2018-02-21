using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

    public sealed partial class UIWaitDialog : ThemeContentDialog {

        public string ContentStr {
            get { return MessageTextBlock.Text; }
            set { MessageTextBlock.Text = value; }
        }

        public UIWaitDialog() {
            this.InitializeComponent();
        }


    }
}
