using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.Navigation;
using VisualExplorer.Pages.Base;
using VisualExplorer.Pages.Fields.Tiles;
using VisualExplorer.UI.Controls;
using VisualExplorer.UI.PrevisualView;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer.Pages {

    public sealed partial class PageFieldContainer : ThemePage {

        private MFolderLocationAgency FLAgency = new MFolderLocationAgency();

        // Conventient Getter
        public UIPreviewPanel RefPreviewPanel => this.PreviewPanel;
        public UIPrevisualView RefPrevisualView => this.PrevisualView;
        public bool IsInfoPanelOpen => this.InfoPanel.Visibility == Visibility.Visible;

        public PageFieldContainer() {
            this.InitializeComponent();

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            var token = e.Parameter as string;
            var destFolder = await FLAgency.InitialFolderStackAsync(token, AddressBar);
            var navigateInfo = new MNavigateInfo<StorageFolder>(this, destFolder);

            FieldFrame.Navigate(typeof(TilesField), navigateInfo);
        }

        /// <summary> 打开或关闭右侧的展示信息的Panel </summary>
        private void PreviewPanelToggleBtn_Click(object sender, RoutedEventArgs e) {
            InfoPanel.Visibility = InfoPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        public MFolderLocationAgency GetFLAgencyRef() {
            return this.FLAgency;
        }
    }

}
