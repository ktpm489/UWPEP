using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.AddressBar;
using VisualExplorer.Model.Navigation;
using VisualExplorer.Model.Theme;
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

namespace VisualExplorer.UI.Controls {

    public sealed partial class UIFieldAddressBar : UserControl {

        private ObservableCollection<MAddressItem> items = new ObservableCollection<MAddressItem>();
        private ThemeAgency themeAgency = ThemeAgency.Instance;
        public MFolderLocationAgency FLAgency;

        public UIFieldAddressBar() {
            this.InitializeComponent();

        }

        public void InitialAddressStack(Stack<StorageFolder> folderStack) {
            int i = 0;
            foreach(var folder in folderStack.Reverse()) {
                items.Add(new MAddressItem { ItemName = folder.DisplayName, depth = i++});
            }
        }


        private void FolderName_Click(object sender, RoutedEventArgs e) {
            var addressItem = (e.OriginalSource as FrameworkElement).DataContext as MAddressItem;
            var popCount = items.Count - addressItem.depth - 1;
            if(popCount > 0) {
                FLAgency.PopFolder(popCount);
            }
        }


        public void PushAddress(StorageFolder folder) {
            items.Add(new MAddressItem { ItemName = folder.DisplayName, depth = items.Count });
        }

        public bool PopAddress(int count) {
            var lastAddressIndex = items.Count - 1;
            for(var i = 0; i < count; i++) {
                items.RemoveAt(lastAddressIndex--);
            }

            return true;
        }

    }
}
