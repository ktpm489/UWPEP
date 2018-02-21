using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Model.Dialog;
using VisualExplorer.Model.Navigation;
using VisualExplorer.Model.Preference;
using VisualExplorer.Model.Preview;
using VisualExplorer.Model.Storage;
using VisualExplorer.Model.Utility;
using VisualExplorer.Resources;
using VisualExplorer.UI.Controls;
using VisualExplorer.UI.Dialogs;
using VisualExplorer.UI.PrevisualView;
using Windows.Storage;
using Windows.UI.Xaml.Input;

namespace VisualExplorer.Pages.Base {

    public abstract class BaseField : ThemePage, IFLServiceClient, IPreviewPanelClient, IFieldPage {

        protected UIPreviewPanel PreviewPanel;
        protected UIPrevisualView PrevisualView;
        protected PageFieldContainer Container;
        protected MFolderLocationAgency FLAgency;

        private SEExcuteAgency Excutor = SEExcuteAgency.Current;

        protected KeyEventHandler keyDownHandler = null;

        public BaseField(): base() {

        }

        public abstract void LoadEntries();
        public abstract void RefreshEntries();
        public abstract void SwitchDestFolderAddress(StorageFolder destFolder);
        public abstract Task CopyEntriesSourceAsync(StorageFolder destFolder, IReadOnlyList<IStorageItem> newItems);

        // --------------------------------------------------------------------------------
        // 打开文件相关

        public virtual async void OpenFileAsync(StorageEntry entryToOpen) {
            if (!(await this.CheckStorageEntryExistAsync(entryToOpen))) { return; }

            bool isSucceed = await Excutor.LaunchFileAsync(entryToOpen.File, false);
            if (isSucceed == false) {
                Excutor.ShowMessageDialogAsync($"Open {entryToOpen.RawEntry.Name}" + " failed", "Operation Failed");
            }
        }

        public virtual async void OpenFolderAsync(StorageEntry entryToOpen) {
            if (!(await this.CheckStorageEntryExistAsync(entryToOpen))) { return; }

        }

        public async Task LaunchFolderInExplorer(StorageEntry entry) {
            if (!(await this.CheckStorageEntryExistAsync(entry))) { return; }
            bool isSucceed = false;

            switch(entry.EntryType) {
                case StorageEntryType.Folder:
                    isSucceed = await Excutor.LaunchFolderInExplorer(entry.Folder); break;
                case StorageEntryType.File:
                    isSucceed = await Excutor.LaunchFolderInExplorer(entry.RawEntry2, true); break;
            }

            if(!isSucceed) {
                Excutor.ShowMessageDialogAsync($"Open {entry.RawEntry.Name} {entry.EntryType.ToString()}" + " failed", "Operation Failed");
            }
        }

        public async Task LaunchFolderInExplorer(StorageEntry parent, List<StorageEntry> entries) {
            if (!(await this.CheckStorageEntryExistAsync(parent))) { return; }

            var selectedList = (from entry in entries select entry as IStorageItem2).ToList();
            bool isSucceed = await Excutor.LaunchFolderInExplorer(parent.Folder, selectedList, true);
        }
        // -------------------------------------------------------------------------

        public virtual async Task<bool> RenameEntryAsync(StorageEntry entryToRename, StorageEntry destFolder) {
            if(!(await this.CheckStorageEntryExistAsync(entryToRename))) { return false; }

            (string result, string newName) = await Excutor.ShowSingleLineInputDialog(
                entryToRename.RawEntry.Name, 
                "Message", 
                "Please Input the new name", "Confirm", "Cancel");

            if (result == "Confirm" && !string.IsNullOrEmpty(newName)) {
                var isExisted = await StorageEntry.CheckStorageEntryExistAsync(newName, destFolder.Folder); // 检测该文件名算法冲突
                if (isExisted) {
                    string collisionAction = await Excutor.ShowMultiplyChooseDialogAsync(
                        "Warning", 
                        $"{newName} has already existed in {destFolder.RawEntry.Name} folder.",
                        "Generate new name", "Replace it", "Cancel");
                    switch(collisionAction) {
                        case "Generate new name": return await Excutor.RenameEntryAsync(entryToRename, newName, NameCollisionOption.GenerateUniqueName);
                        case "Replace it": return await Excutor.RenameEntryAsync(entryToRename, newName, NameCollisionOption.ReplaceExisting); 
                        case "Cancel":
                        default: return false;
                    }
                } else {
                    await Excutor.RenameEntryAsync(entryToRename, newName, NameCollisionOption.FailIfExists);
                    return true;
                }
            } else {
                return false;
            }
        }

        public virtual async Task<bool> DeleteEntryAsync(StorageEntry entryToDelete) {
            if (!(await this.CheckStorageEntryExistAsync(entryToDelete))) { return false; }

            if((bool)PreferenceAgency.Default.GetLocalSetting(RIdentifiers.ShowDeleteConfirmation)) {
                var result = await Excutor.ShowMultiplyChooseDialogAsync(
                    "Warning",
                    $"Are you sure you want to move {entryToDelete.RawEntry.Name} to the Recycle Bin?",
                    "Yes", "No");
                if(!string.IsNullOrEmpty(result) && result == "Yes") {
                    Excutor.DeleteEntryAsync(entryToDelete);
                } else {
                    return false;
                }
            } else {
                Excutor.DeleteEntryAsync(entryToDelete);
            }

            return true;
        }

        public virtual async Task<List<IStorageItem>> PasteEntriesAsync(StorageFolder destFolder) {
            if(!Excutor.IsCopyAvailable) {
                await ShowNothingPasteDialogAsync();
                return null;
            }

            List<IStorageItem> newCollection = null;
            await MDialogAgency.WaitThenDisplayMessageAsync(new TimeSpan(0, 0, RConstants.CopyDialogWaitTime), "Copying...", async delegate () {
                newCollection = await Excutor.PasteEntriesAsyncTo(destFolder);
            });

            if (Excutor.IsContainsFailedCopyEntry) {
                Excutor.ShowFailedCopyItemDialogIfNeed();
            }

            return newCollection;
        }

        protected async Task CopyEntriesSourceAsync(StorageFolder destFolder, List<StorageEntry> newItems) {
            await Excutor.CopyEntriesSourceAsyncTo(destFolder, newItems);
        }

        public virtual void SetEntriesCopy(List<StorageEntry> entriesToCopy) {
            Excutor.SetEntriesToCopy(entriesToCopy);
        }


        /// <summary> 检查文件或文件夹是否存在 </summary>
        private async Task<bool> CheckStorageEntryExistAsync(StorageEntry entryToCheck) {
            if(await StorageEntry.EntryExistAsync(entryToCheck)) {
                return true;
            } else {
                Excutor.ShowMessageDialogAsync($"{entryToCheck.EntryType.ToString()} doesn't not exist.", "Operation Failed");
                return false;
            }
        }

        public Task ShowNothingPasteDialogAsync() {
            throw new NotImplementedException();
        }

        public virtual async Task<IStorageItem> CreateStorageEntry(StorageEntry destFolder, StorageEntryType type) {
            (string result, string newName) = await Excutor.ShowSingleLineInputDialog(null, "Message", "Please Input the new name", "Confirm", "Cancel");

            if (result == "Confirm" && !string.IsNullOrEmpty(newName)) {
                var isExisted = await StorageEntry.CheckStorageEntryExistAsync(newName, destFolder.Folder); // 检测该文件名是否冲突
                if (isExisted) {
                    string collisionAction = await Excutor.ShowMultiplyChooseDialogAsync(
                        "Warning",
                        $"{newName} has already existed in {destFolder.RawEntry.Name} folder.",
                        "Generate new name", "Replace it", "Cancel");
                    switch (collisionAction) {
                        case "Generate new name": return await Excutor.CreatedNewEntry(destFolder, newName, type, CreationCollisionOption.GenerateUniqueName);
                        case "Replace it": return await Excutor.CreatedNewEntry(destFolder, newName, type, CreationCollisionOption.ReplaceExisting);
                        case "Cancel":
                        default: return null;
                    }
                } else {
                    return await Excutor.CreatedNewEntry(destFolder, newName, type, CreationCollisionOption.FailIfExists);
                }
            } else {
                return null;
            }
        }


    }
}
