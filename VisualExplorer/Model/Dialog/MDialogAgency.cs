using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Model.Utility;
using VisualExplorer.Resources;
using VisualExplorer.UI.Dialogs;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace VisualExplorer.Model.Dialog {

    public static class MDialogAgency {

        static MDialogAgency() { }

        public static async void DisplayMessageDialog(string content) {
            var dialog = new ContentDialog {
                Content = content,
                PrimaryButtonText = "Confirm"
            };

            await dialog.ShowAsync();
        }

        public static async Task<StorageFolder> DisplayFolderPicker(
            PickerViewMode viewMode = PickerViewMode.List,
            PickerLocationId preferLocation = PickerLocationId.Desktop) {

            var folderPicker = new FolderPicker() {
                ViewMode = viewMode,
                SuggestedStartLocation = preferLocation
            };
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            return folder;
        }


        public static async Task<StorageFile> DisplaySingleFilePicker(
            PickerViewMode viewMode = PickerViewMode.List,
            PickerLocationId preferLocation = PickerLocationId.Desktop,
            List<string> filters = null) {

            var filePicker = new FileOpenPicker {
                ViewMode = viewMode,
                SuggestedStartLocation = preferLocation
            };

            if (filters is null) {
                filters = new List<string> { "*" };
            }
            foreach (var filter in filters) {
                filePicker.FileTypeFilter.Add(filter);
            }

            StorageFile file = await filePicker.PickSingleFileAsync();
            return file;
        }

        public static async Task WaitThenDisplayMessageAsync(TimeSpan interval, string message, Func<Task> waitOperation) {
            UIWaitDialog waitDialog = null;
            var bgTimer = new TimerMonitor(TimerMonitor.TimerFrequency.Once) {
                Interval = new TimeSpan(0, 0, RConstants.CopyDialogWaitTime),
                TimeOutEvent = async delegate () {
                    waitDialog = new UIWaitDialog() { ContentStr = message };
                    await waitDialog.ShowAsync();
                }
            };
            bgTimer.Launch();

            await waitOperation();

            if (bgTimer.HasStop && waitDialog != null) {
                waitDialog.Hide();
                bgTimer.Terminate();
            }
        }
    }
}
