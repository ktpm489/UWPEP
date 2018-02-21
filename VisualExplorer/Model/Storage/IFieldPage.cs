using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace VisualExplorer.Model.Storage {

    /// <summary>
    /// 定义了每种文件浏览视图所应该提供的功能
    /// </summary>
    public interface IFieldPage {

        // ----------------------------------------------------------
        // 打开文件或文件夹

        /// <summary> 以默认的方式打开文件 </summary>
        void OpenFileAsync(StorageEntry entryToOpen);

        /// <summary> 打开某个文件夹 </summary>
        void OpenFolderAsync(StorageEntry entryToOpen);

        Task LaunchFolderInExplorer(StorageEntry entry);
        Task LaunchFolderInExplorer(StorageEntry parent, List<StorageEntry> entries);

        // ----------------------------------------------------------

        /// <summary> 重命名某个文件项 </summary>
        /// <param name="entryToRename">要重命名的文件项</param>
        /// <param name="destFolder">该文件夹所在的文件夹</param>
        /// <returns>若重命名成功,返回true. 否则返回false</returns>
        Task<bool> RenameEntryAsync(StorageEntry entryToRename, StorageEntry destFolder);

        /// <summary> 删除某个文件项 </summary>
        /// <returns>若删除成功,返回true. 否则返回false</returns>
        Task<bool> DeleteEntryAsync(StorageEntry entryToDelete);

        /// <summary> 从SEExcuteAgency获取要粘贴的文件项 </summary>
        Task<List<IStorageItem>> PasteEntriesAsync(StorageFolder destFolder);

        Task CopyEntriesSourceAsync(StorageFolder destFolder, IReadOnlyList<IStorageItem> newItems);

        /// <summary> 设置将要被复制的文件项 </summary>
        void SetEntriesCopy(List<StorageEntry> entriesToCopy);

        /// <summary> 显示对话框,提示用户,粘贴操作不可执行 </summary>
        Task ShowNothingPasteDialogAsync();

        /// <summary> 加载文件项 </summary>
        void LoadEntries();

        /// <summary> 重新加载文件项 </summary>
        void RefreshEntries();

        /// <summary> 创建文件或文件夹 </summary>
        /// <param name="destFolder">新建文件或文件夹的位置</param>
        /// <returns>新创建的文件或文件夹</returns>
        Task<IStorageItem> CreateStorageEntry(StorageEntry destFolder, StorageEntryType type);
        

    }

}
