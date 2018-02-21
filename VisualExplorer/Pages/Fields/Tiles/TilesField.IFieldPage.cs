using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Framework.Foundation;
using VisualExplorer.Model.Storage;
using Windows.Storage;

namespace VisualExplorer.Pages.Fields.Tiles {

    // override implementation for IFieldPage Interface
    public sealed partial class TilesField {

        public override void OpenFolderAsync(StorageEntry entryToOpen) {
            base.OpenFolderAsync(entryToOpen);

            this.folderEntry = entryToOpen;
            this.LoadEntries();
            this.FLAgency.PushFolder(entryToOpen.Folder);
        }

        public override async Task<bool> RenameEntryAsync(StorageEntry entryToRename, StorageEntry destFolder) {
            var isSucceed = await base.RenameEntryAsync(entryToRename, destFolder);
            if (isSucceed) {
                var index = entries.IndexOf(entryToRename);
                entries.Remove(entryToRename);
                entries.Insert(index, entryToRename);

                TilesView.SelectedItem = entryToRename;
            }

            return isSucceed;
        }

        public override async Task<bool> DeleteEntryAsync(StorageEntry entryToDelete) {
            var isSucceed = await base.DeleteEntryAsync(entryToDelete);
            if (isSucceed) {
                entries.Remove(entryToDelete);
            }

            return isSucceed;
        }

        public override async Task<IStorageItem> CreateStorageEntry(StorageEntry destFolder, StorageEntryType type) {
            var createdItem = await base.CreateStorageEntry(destFolder, type);
            if(createdItem != null) {
                var newEntry = new StorageEntry(createdItem, StorageEntryUsage.TilesFieldItem);
                entries.Add(newEntry);
                TilesView.SelectedItem = newEntry;
            }

            return createdItem;
        }

        public override async Task<List<IStorageItem>> PasteEntriesAsync(StorageFolder destFolder) {
            var newEntries = await base.PasteEntriesAsync(destFolder);

            // 执行Copy操作
            if (newEntries.Count > 0) {
                var firstFileIndex = ObservableExtension.FirstIndex(entries, p => p.EntryType == StorageEntryType.File);

                List<StorageEntry> newFiles = new List<StorageEntry>(), newFolders = new List<StorageEntry>();
                foreach (var storageItem in newEntries) {
                    var newEntry = new StorageEntry(storageItem, StorageEntryUsage.TilesFieldItem);
                    if (storageItem.IsOfType(StorageItemTypes.File)) {
                        newFiles.Add(newEntry);
                    } else {
                        newFolders.Add(newEntry);
                    }
                }
                // 首先插入文件
                newFiles.ForEach(p => entries.Add(p));
                // 然后插入文件夹
                for(int i = 0; i < newFolders.Count; i++) {
                    entries.Insert(firstFileIndex + i, newFolders[i]);
                }
            }

            return newEntries;
        }

        public override async Task CopyEntriesSourceAsync(StorageFolder destFolder, IReadOnlyList<IStorageItem> newItems) {
            List<StorageEntry> newEntries = new List<StorageEntry>();
            foreach (var item in newItems) {
                var newEntry = new StorageEntry(item, StorageEntryUsage.TilesFieldItem);
                newEntries.Add(newEntry);
            }

            await this.CopyEntriesSourceAsync(destFolder, newEntries);

            // FIXME: 目前乱序直接插入到列表的最后
            newEntries.ForEach(p => entries.Add(p));
        }
    }
}
