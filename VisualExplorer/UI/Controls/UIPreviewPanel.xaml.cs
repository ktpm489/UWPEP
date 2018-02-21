using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.Storage;
using VisualExplorer.Model.Theme;
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

    public sealed partial class UIPreviewPanel : UserControl {

        private ThemeAgency themeAgency = ThemeAgency.Instance;
        private StorageEntry Entry => this.DataContext as StorageEntry;

        public UIPreviewPanel() {
            this.InitializeComponent();

            this.DataContextChanged += (_sender, _args) => {
                this.UpdateUI();
            };
        }


        public async void SwitchPreviewSource(StorageEntry displayEntry) {
            this.WholePreviewPanel.Visibility = Visibility.Collapsed;
            var newPreviewEntry = new StorageEntry(displayEntry.RawEntry, StorageEntryUsage.PanelPreview);
            await newPreviewEntry.GetEntrySizeAsyncIfNeed();
            this.DataContext = newPreviewEntry;
        }

        /// <summary>
        /// 更新预览面板显示的内容
        /// </summary>
        private void UpdateUI() {

            if(this.Entry is null) {
                PreviewImage.Source = null;
                ItemNameLabel.Text = "";
                this.WholePreviewPanel.Visibility = Visibility.Collapsed;
            } else {
                PreviewImage.Source = Entry.ThumbSource;
                ItemNameLabel.Text = Entry.RawEntry.Name;
                this.WholePreviewPanel.Visibility = Visibility.Visible;
            }
            
            this.Bindings.Update();
        }

        /// <summary>
        /// 清空该窗口的所有内容和数据
        /// </summary>
        public void ClearPreviewPanel() {
            this.DataContext = null;
            this.UpdateUI();
        }

    }
}
