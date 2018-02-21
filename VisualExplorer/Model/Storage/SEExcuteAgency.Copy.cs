using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace VisualExplorer.Model.Storage {

    // 处理与复制,粘贴有关的操作
    public partial class SEExcuteAgency {

        private bool _isCopyAvailable = true;
        public bool IsCopyAvailable => _isCopyAvailable;
        public bool IsContainsFailedCopyEntry => copyFailedEntries.Count > 0;

        /// <summary> 将要被复制的文件项 </summary>
        private List<StorageEntry> copyingEntries = new List<StorageEntry>();

        private List<IStorageItem> copyFailedEntries = new List<IStorageItem>();

        /// <summary> 将准备要复制的文件保存起来  </summary>
        /// <param name="entriesToCopy">将要执行copy操作的文件项</param>
        public void SetEntriesToCopy(List<StorageEntry> entriesToCopy) {
            copyingEntries.Clear();
            this.copyingEntries = entriesToCopy;
            if (!_isCopyAvailable) _isCopyAvailable = true;
        }

        /// <summary> 将成员属性copyingEntries里的StorageEntry粘贴到指定文件夹 </summary>
        public async Task<List<IStorageItem>> PasteEntriesAsyncTo(StorageFolder destFolder) {
            // 注意文件粘贴失败的情况处理
            if (copyingEntries.Count == 0) return null;
            copyFailedEntries.Clear();

            var copiedEntries = new List<IStorageItem>();

            foreach(var entry in copyingEntries) {
                switch(entry.EntryType) {
                    case StorageEntryType.File:
                        var copiedFile = await this.FileCopy(entry.File, destFolder);
                        if(copiedFile != null) copiedEntries.Add(copiedFile);
                        break;
                    case StorageEntryType.Folder:
                        var copiedFolder = await this.FolderCopy(entry.Folder, destFolder);
                        if (copiedFolder != null) copiedEntries.Add(copiedFolder);
                        break;
                }
            }

            return copiedEntries;
        }

        public async Task CopyEntriesSourceAsyncTo(StorageFolder destFolder, List<StorageEntry> entrieSource) {
            copyFailedEntries.Clear();
            foreach(var entry in entrieSource) {
                switch(entry.EntryType) {
                    case StorageEntryType.File:   await this.FileCopy(entry.File, destFolder); break;
                    case StorageEntryType.Folder: await this.FolderCopy(entry.Folder, destFolder); break;
                }
            }
        }

        public void ShowFailedCopyItemDialogIfNeed() {
            if (copyFailedEntries.Count == 0) return;

            string message = "";
            List<string> fileNames = null;
            if (copyFailedEntries.Count > 10) {
                fileNames = new List<string>();
                for (int i = 0; i < 10; i++) {
                    fileNames.Add(copyFailedEntries[i].Name);
                }
            } else {
                fileNames = (from each_failureItem in copyFailedEntries select each_failureItem.Name).ToList();
            }
            message = string.Join(", ", fileNames);
            message = "Copy files failed: " + message;

            this.ShowMessageDialogAsync(message, "Opeartion conflict");
        }

        /// <summary> 复制单个文件到目标文件夹 </summary>
        /// <param name="file">被复制的文件</param>
        /// <param name="destFolder">复制到的目标文件夹</param>
        /// <returns>被复制的文件的副本</returns>
        public async Task<StorageFile> FileCopy(StorageFile copyiedFile, StorageFolder destFolder) {
            StorageFile file = null;
            try {
                file = await copyiedFile.CopyAsync(destFolder);
            } catch {
                copyFailedEntries.Add(copyiedFile);
            }

            return file;
        }

        /// <summary> 复制多个文件到目标文件夹  </summary>
        /// <param name="files">被复制的多个文件</param>
        /// <param name="destFolder">复制到的目标文件夹</param>
        /// <returns>被复制的文件的副本</returns>
        public async Task<List<StorageFile>> FileCopy(List<StorageFile> copiedFiles, StorageFolder destFolder) {
            List<StorageFile> newFiles = new List<StorageFile>();
            foreach (var file in copiedFiles) {
                try {
                    newFiles.Add(await file.CopyAsync(destFolder));
                } catch {
                    copyFailedEntries.Add(file);
                }
            }
            return newFiles;
        }

        /// <summary> 复制某个文件夹到某人文件夹 </summary>
        /// <param name="copiedFolder">被复制的文件夹</param>
        /// <param name="destFolder">复制到的目标文件夹</param>
        /// <returns>复制的文件夹</returns>
        public async Task<StorageFolder> FolderCopy(StorageFolder copiedFolder, StorageFolder destFolder) {
            if (copiedFolder.Path == destFolder.Path) { return null; } // without this line, it may create folder circularly
            var newCreatedFolder = await destFolder.CreateFolderAsync(copiedFolder.Name); // create a subFolder

            var subFiles = await copiedFolder.GetFilesAsync();  // Get all the files
            foreach (var each_subFile in subFiles) {
                try {
                    await each_subFile.CopyAsync(newCreatedFolder);
                } catch {
                    copyFailedEntries.Add(each_subFile);
                }
            }

            var subFolders = await copiedFolder.GetFoldersAsync(); // Get all the folders
            await FolderCopy(subFolders.ToList(), newCreatedFolder); // Call recursively
            return newCreatedFolder;
        }

        /// <summary> 复制多个文件夹到某人文件夹 </summary>
        /// <param name="folders">被复制的多个文件夹</param>
        /// <param name="destFolder">复制到的目标文件夹</param>
        /// <returns>复制的多个文件夹</returns>
        public async Task<List<StorageFolder>> FolderCopy(List<StorageFolder> folders, StorageFolder destFolder) {
            List<StorageFolder> newCreatedFolders = new List<StorageFolder>();
            foreach (var eachFolder in folders) {
                newCreatedFolders.Add(await FolderCopy(eachFolder, destFolder)); // Call recusively
            }
            return newCreatedFolders;
        }

    }
}
