using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VisualExplorer.Framework.Foundation;
using VisualExplorer.Model.Dialog;
using VisualExplorer.Model.Navigation;
using VisualExplorer.Model.Storage;
using VisualExplorer.Pages.Base;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls;

namespace VisualExplorer.Pages {

    public sealed partial class PageAccessContent : ThemePage {

        private MAccessAgency agency = MAccessAgency.Default;
        private ObservableCollection<ObservableMPathAccessGroup> accessGroups;
        private ObservableCollection<MDeviceAccessItem> deviceItems;

        public PageAccessContent() {
            this.InitializeComponent();

            this.InitializeData();
        }

        private void InitializeData() {
            this.accessGroups = new ObservableCollection<ObservableMPathAccessGroup>();
            this.agency.AccessGroups.ForEach(p => this.accessGroups.Add(
                new ObservableMPathAccessGroup {
                    Title = p.Title, itemLists = ObservableExtension.ToObserCollection(p.itemLists), IsExpand = p.IsExpand
                }));

            this.deviceItems = new ObservableCollection<MDeviceAccessItem>();
            this.agency.DrivesItems.ForEach(p => this.deviceItems.Add(p));
        }

        private async void DeviceAppendBtn_Click(object sender, RoutedEventArgs e) {
            var deviceFolder = await MDialogAgency.DisplayFolderPicker();
            if (deviceFolder is null) { return; } // 如果取消了文件夹的选择,返回退出

            var deviceToAppend = agency.AppendDeviceLocation(deviceFolder);
            if (deviceToAppend != null) { // 添加成功
                this.deviceItems.Add(deviceToAppend);
            }
        }

        private async void AccessGroupAppendBtn_Click(object sender, RoutedEventArgs e) {
            (string result, string text) = await SEExcuteAgency.Current.ShowSingleLineInputDialog(null, "Message", "Please Input the name of Group", "Confirm", "Cancel");
            if (result == "Confirm" && !string.IsNullOrEmpty(text)) {
                var newGroup = this.agency.AppendAccessGroup(text);
                if (newGroup != null) {
                    this.accessGroups.Add(new ObservableMPathAccessGroup {
                        Title = newGroup.Title, itemLists = new ObservableCollection<MPathAccessItem>(), IsExpand = newGroup.IsExpand
                    });
                }
            }
        }

        private async void AccessItemAppendBtn_Click(object sender, RoutedEventArgs e) {
            var deviceFolder = await MDialogAgency.DisplayFolderPicker();
            if (deviceFolder is null) { return; } // 如果取消了文件夹的选择,返回退出

            var categoryTitle = (e.OriginalSource as FrameworkElement).DataContext as string;
            var newAccessItem = agency.AppendAccessLocation(deviceFolder, categoryTitle);
            if (newAccessItem != null) { // 添加成功
                var modifiedGroup = accessGroups.First(p => p.Title == categoryTitle);
                modifiedGroup.itemLists.Add(newAccessItem); ;
            }
        }

        private void AccessItem_Clicked(object sender, ItemClickEventArgs e) {
            var clickedItem = e.ClickedItem as MPathAccessItem;
            this.Frame.Navigate(typeof(PageFieldContainer), clickedItem.token);
        }
    }
}
