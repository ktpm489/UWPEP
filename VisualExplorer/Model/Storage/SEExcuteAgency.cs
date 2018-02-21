using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.UI.Dialogs;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using WinRTXamlToolkit.Controls;

namespace VisualExplorer.Model.Storage {

    /// <summary>
    /// 该类用于执行某些全局的操作或应用外的操作
    /// </summary>
    public partial class SEExcuteAgency {

        private SEExcuteAgency() { }

        /// <summary> 显示提示消息框 </summary>
        /// <param name="content">消息的内容</param>
        /// <param name="title">消息的标题</param>
        public async void ShowMessageDialogAsync(string content, string title) {
            var dialog = new MessageDialog(content, title);
            await dialog.ShowAsync();
        }

        public async Task<string> ShowMultiplyChooseDialogAsync(string title, string text, params string[] buttonTexts) {
            var multiplyOptionsDialog = new UIMultiplyOptionDialog(buttonTexts.ToList(), text, title);
            var result = await multiplyOptionsDialog.DisplayAsync();
            return result;
        }

        /// <summary> 显示单行文件输入框 </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="text">主体提示文本</param>
        /// <param name="buttonTexts">按钮文本</param>
        public async Task<(string result, string text)> ShowSingleLineInputDialog(string defaultText, string title, string text, params string[] buttonTexts) {
            var dialog = new InputDialog {
                ButtonsPanelOrientation = Orientation.Horizontal,
                BackgroundStripeBrush = new SolidColorBrush(Colors.Transparent),
                Background = new SolidColorBrush(Colors.DarkSlateGray),
            };
            if (defaultText != null) dialog.InputText = defaultText;
            var result = await dialog.ShowAsync(title, text, buttonTexts);
            return (result, dialog.InputText);
        }

        /// <summary> 重命名某个文件或文件夹 并将它从目前显示的list中删去 </summary>
        /// <param name="enrty">需要重命名的文件或文件夹</param>
        public async Task<bool> RenameEntryAsync(StorageEntry entry, string newName, NameCollisionOption option) {
            try {
                await entry.RawEntry.RenameAsync(newName, option);
            } catch {
                return false;
            }

            return true;
        }

        /// <summary> 删除文件或文件夹,删除的文件送往回收站  </summary>
        /// <param name="entry">需要删除的文件项</param>
        public async void DeleteEntryAsync(StorageEntry entry) {
            await entry.RawEntry.DeleteAsync(StorageDeleteOption.Default); // Default 表示删除的文件送往回收站
        }

        /// <summary> 常见一个新的文件或文件夹 </summary>
        /// <param name="destFolder">目标文件夹</param>
        /// <param name="name">文件项名</param>
        /// <param name="type">文件项的类型(文件或文件夹)</param>
        /// <returns>创建成功返回true,否则返回false</returns>
        public async Task<IStorageItem> CreatedNewEntry(StorageEntry destFolder, string name, StorageEntryType type, CreationCollisionOption option) {
            switch(type) {
                case StorageEntryType.File: return await destFolder.Folder.CreateFileAsync(name, option);
                case StorageEntryType.Folder: return await destFolder.Folder.CreateFolderAsync(name, option);
                default:  return null;
            }
        }

        // ------------------------------------------------------
        // Singleton
        private volatile static SEExcuteAgency _instance = null;
        private static readonly Object LockHelper = new object();

        public static SEExcuteAgency Current {
            get {
                if (_instance == null) {
                    lock (LockHelper) {
                        if (_instance == null) {
                            _instance = new SEExcuteAgency();
                        }
                    }
                }
                return _instance;
            }

        }
        // ------------------------------------------------------
    }


}
