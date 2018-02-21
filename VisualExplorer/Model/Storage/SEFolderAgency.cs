using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Model.Preference;
using Windows.Storage;

namespace VisualExplorer.Model.Storage {

    public struct StorageEntryLoadInfo {
        public uint entriesCount; // 该目录下文件和文件夹的总数量
        public uint loadedCount; // 目前以加载的数量
        public string path; // 该目录的路径
        public bool isLoadedAll; // 是否已经加载完所有的文件和文件夹

        public uint LeavingCount => entriesCount - loadedCount;
    }

    /// <summary> 该类用于执行定义在文件夹上的操作 </summary>
    public static class SEFolderAgency {

        static SEFolderAgency() { }

        public static async Task<StorageEntryLoadInfo> LoadStorageFolderAsync(StorageFolder destFolder, ObservableCollection<StorageEntry> collection, StorageEntryUsage usage) {
            var itemsCount = Directory.GetFileSystemEntries(destFolder.Path, "*", SearchOption.TopDirectoryOnly).Length;
            
            StorageEntryLoadInfo info;
            info.entriesCount = (uint)itemsCount;
            info.path = destFolder.Path;

            IReadOnlyList<IStorageItem> storageItems = null;
            if (itemsCount > PreferenceAgency.Default.InitialMaxLoadCount) {
                storageItems = await destFolder.GetItemsAsync(0, PreferenceAgency.Default.InitialMaxLoadCount);
                info.isLoadedAll = false;
                info.loadedCount = PreferenceAgency.Default.InitialMaxLoadCount;
            } else {
                storageItems = await destFolder.GetItemsAsync();
                info.isLoadedAll = true;
                info.loadedCount = info.entriesCount;
            }

            foreach (var eachItem in storageItems) {
                var newEntry = new StorageEntry(eachItem, usage);
                collection.Add(newEntry);
            }

            return info;
        }

        public static async Task<StorageEntryLoadInfo> LoadMoreStorageItemAsync(StorageFolder destFolder, ObservableCollection<StorageEntry> collection, StorageEntryUsage usage, StorageEntryLoadInfo info) {
            IReadOnlyList<IStorageItem> storageItems = null;
            if(info.LeavingCount < PreferenceAgency.Default.MaxLoadCount) {
                storageItems = await destFolder.GetItemsAsync(info.loadedCount, info.LeavingCount);
                info.loadedCount = info.entriesCount;
                info.isLoadedAll = true;
            } else {
                storageItems = await destFolder.GetItemsAsync(info.loadedCount, PreferenceAgency.Default.MaxLoadCount);
                info.loadedCount += PreferenceAgency.Default.MaxLoadCount;
            }

            foreach (var eachItem in storageItems) {
                var newEntry = new StorageEntry(eachItem, usage);
                collection.Add(newEntry);
            }

            return info;
        }

    }


}
