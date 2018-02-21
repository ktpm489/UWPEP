using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Model.Navigation;
using VisualExplorer.Model.Preference;
using VisualExplorer.Model.Storage;
using VisualExplorer.Model.ViewFixer;
using VisualExplorer.Pages.Base;
using VisualExplorer.Resources;
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

namespace VisualExplorer.Pages.Fields.Tiles {

    public sealed partial class TilesField : BaseField {

        private StorageEntry folderEntry;
        private ObservableCollection<StorageEntry> entries = new ObservableCollection<StorageEntry>();
        private StorageEntryLoadInfo loadInfo; // 记录该文件夹中所有的文件加载情况

        private TilesFieldFixer viewFixer = TilesFieldFixer.Default;

        public TilesField() {
            this.InitializeComponent();

            TilesView.SingleSelectionFollowsFocus = true;
            TilesView.ContextFlyout = this.Resources["TileFlyoutMenu"] as MenuFlyout; // 以编程的方式设置右键菜单
        }

        /// <summary> 当页面加载完毕时执行 </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            this.keyDownHandler = new KeyEventHandler(TilesView_KeyDown);
            TilesView.AddHandler(UIElement.KeyDownEvent, keyDownHandler, true);
        }
        /// <summary> 当页面被卸载时执行 </summary>
        private void Page_Unloaded(object sender, RoutedEventArgs e) {
            TilesView.RemoveHandler(UIElement.KeyDownEvent, keyDownHandler);
            keyDownHandler = null;
        }

        // Navigate from PageFieldContainer
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            var info = e.Parameter as MNavigateInfo<StorageFolder>;
            this.folderEntry = new StorageEntry(info.Message, StorageEntryUsage.ColumnFieldItem);
            this.Container = info.Navigator as PageFieldContainer;
            this.FLAgency = this.Container.GetFLAgencyRef();
            this.PreviewPanel = this.Container.RefPreviewPanel;
            this.PrevisualView = this.Container.RefPrevisualView;
            this.FLAgency.SwitchServiceClient(this, MFolderLocationAgency.ServicePattern.TilesField);

            this.LoadEntries();
        }

        public async override void LoadEntries() {
            this.entries.Clear();

            this.loadInfo = await SEFolderAgency.LoadStorageFolderAsync(this.folderEntry.Folder, entries, StorageEntryUsage.TilesFieldItem);
            if (!this.loadInfo.isLoadedAll) { // 文件项未全部加载,给与用户提示
                PromptBar.Show(new UI.Controls.PromptBarMessage {
                    Content = $"Click here to load more(leaving { this.loadInfo.LeavingCount } to load)",
                    type = PromptBarMessageType.LoadMoreItems,
                    handler = async delegate () {
                        this.loadInfo = await SEFolderAgency.LoadMoreStorageItemAsync(this.folderEntry.Folder, entries, StorageEntryUsage.TilesFieldItem, loadInfo);
                        if (this.loadInfo.isLoadedAll) {
                            PromptBar.Hide(); // 所有文件项都已加载完毕,隐藏提示栏
                        } else {
                            PromptBar.UpdateContent($"Click here to load more(leaving { this.loadInfo.LeavingCount } to load)"); // 更新提示栏信息
                        }
                    }
                });
            }
        }

        public override void RefreshEntries() {
            throw new NotImplementedException();
        }

        // 拖拽操作
        private async void TilesView_Drop(object sender, DragEventArgs e) {
            if (!e.DataView.Contains(Windows.ApplicationModel.DataTransfer.StandardDataFormats.StorageItems)) return;
            var items = await e.DataView.GetStorageItemsAsync();
            if (!items.Any()) return;

            await this.CopyEntriesSourceAsync(folderEntry.Folder, items);
        }

        private void TilesView_DragOver(object sender, DragEventArgs e) {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
            e.DragUIOverride.Caption = "Copy the file to " + folderEntry.RawEntry.Name;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
            e.DragUIOverride.IsCaptionVisible = true;
        }

        /// <summary> 从应用里的文件项 </summary>
        private void TilesView_DragItemsStarting(object sender, DragItemsStartingEventArgs e) {
            throw new NotImplementedException();
            //var itemObject = e.Items.FirstOrDefault();
            //List<IStorageItem> fileOrFolders = null;
            //if(itemObject is StorageEntry entry) {
            //    fileOrFolders = new List<IStorageItem> { entry.RawEntry };
            //} else if(itemObject is List<StorageEntry> draggingEntries) {
            //    fileOrFolders = (from eachEntry in draggingEntries select eachEntry.RawEntry).ToList();
            //}

            //e.Data.SetStorageItems(fileOrFolders);
        }

        // 事件

        private void TilesView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(Container.IsInfoPanelOpen && e.AddedItems.First() is StorageEntry infoPanelEntry) {
                switch (infoPanelEntry.EntryType) {
                    case StorageEntryType.File: PreviewPanel.SwitchPreviewSource(infoPanelEntry); break;
                    case StorageEntryType.Folder: break;
                }
            }

            if(PrevisualView.IsPrevisualExtend && e.AddedItems.First() is StorageEntry previsualEntry) {
                switch (previsualEntry.EntryType) {
                    case StorageEntryType.File: PrevisualView.TogglePrevisual(previsualEntry); break;
                    case StorageEntryType.Folder: PrevisualView.IsPrevisualExtend = false; break;
                }
                
            }
        }

        private void TilesView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e) {
            if((e.OriginalSource as FrameworkElement).DataContext is StorageEntry doubleTapEntry) {
                switch(doubleTapEntry.EntryType) {
                    case StorageEntryType.File:   this.OpenFileAsync  (doubleTapEntry); break; // 双击了文件
                    case StorageEntryType.Folder: this.OpenFolderAsync(doubleTapEntry); break; // 双击了文件夹
                }
            }
        }
        private void TilesView_RightTapped(object sender, RightTappedRoutedEventArgs e) {
            TilesView.SelectedItem = (e.OriginalSource as FrameworkElement).DataContext as StorageEntry;
        }

        // 键盘事件

        private async void TilesView_KeyDown(object sender, KeyRoutedEventArgs e) {
            switch(e.Key) {
                case Windows.System.VirtualKey.Escape: {
                        if (PrevisualView.IsPrevisualExtend)
                            PrevisualView.IsPrevisualExtend = false;
                        else if (TilesView.SelectedIndex != -1)
                            TilesView.SelectedIndex = -1;
                    } break;
                case Windows.System.VirtualKey.Space: {
                        PrevisualView.TogglePrevisual(TilesView.SelectedItem as StorageEntry);
                    } break;
                case Windows.System.VirtualKey.Enter: {
                        if(TilesView.SelectedItem is StorageEntry selectedEntry) {
                            switch((string)PreferenceAgency.Default.GetLocalSetting(RIdentifiers.FiledEnterKeyAction)) {
                                case RIdentifiers.EnterKeyAction.RenameEntry:
                                    await this.RenameEntryAsync(selectedEntry, folderEntry); break;
                                case RIdentifiers.EnterKeyAction.LaunchEntry:
                                    switch (selectedEntry.EntryType) {
                                        case StorageEntryType.File:   this.OpenFileAsync(selectedEntry); break;
                                        case StorageEntryType.Folder: this.OpenFolderAsync(selectedEntry); break;
                                    } break;
                            }
                        }
                    } break;
            }
        }

        private async void ItemFlyout_Clicked(object sender, RoutedEventArgs e) {
            var operateItem = TilesView.SelectedItem as StorageEntry;
            switch((sender as MenuFlyoutItem).Tag as string) {
                case "1": // Open
                    switch(operateItem.EntryType) {
                        case StorageEntryType.File:   this.OpenFileAsync(operateItem); break;
                        case StorageEntryType.Folder: this.OpenFolderAsync(operateItem); break;
                    } break;
                case "2": // New Window
                    throw new NotImplementedException();
                case "3": // Copy
                    this.SetEntriesCopy(new List<StorageEntry> { operateItem }); break;
                case "4": // Rename
                    await this.RenameEntryAsync(operateItem, folderEntry); break;
                case "5": // Delete
                    await this.DeleteEntryAsync(operateItem); break;
                case "6": // Explorer
                    await this.LaunchFolderInExplorer(operateItem); break;
            }
        }

        private async void TileFlyout_Clicked(object sender, RoutedEventArgs e) {
            switch ((sender as MenuFlyoutItem).Tag as string) {
                case "1": // Paste
                    await PasteEntriesAsync(this.folderEntry.Folder); break;
                case "2": // Refresh
                    this.RefreshEntries(); break;
                case "3": // New File
                    await this.CreateStorageEntry(folderEntry, StorageEntryType.File); break;
                case "4": // New Folder
                    await this.CreateStorageEntry(folderEntry, StorageEntryType.Folder); break;
                case "5": // Explorer
                    await this.LaunchFolderInExplorer(folderEntry); break;
            }
        }


    }


}