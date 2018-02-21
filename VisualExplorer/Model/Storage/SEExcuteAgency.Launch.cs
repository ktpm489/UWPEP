using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace VisualExplorer.Model.Storage {

    // 处理文件和文件夹打开的操作
    public partial class SEExcuteAgency {

        /// <summary> 从外部程序中打开文件 </summary>
        /// <param name="file">文件打开的对象</param>
        /// <param name="isOpenWith">是否使用Open With菜单,默认为false</param>
        /// <returns>打开成功返回true,否则false</returns>
        public async Task<bool> LaunchFileAsync(StorageFile file, bool isOpenWith = false) {
            var option = new Windows.System.LauncherOptions();
            if (isOpenWith) {
                option.DisplayApplicationPicker = true;
            }

            return await Windows.System.Launcher.LaunchFileAsync(file, option);
        }

        /// <summary> 在资源管理器中打开某个文件所在的文件夹 </summary>
        /// <param name="entry">选择要打开的文件</param>
        /// <param name="isSelected">是否要选中该文件,默认为选中</param>
        /// <returns>打开成功返回true,否则false</returns>
        public async Task<bool> LaunchFolderInExplorer(IStorageItem2 entry, bool isSelected = true) {
            var parentFolder = await entry.GetParentAsync();
            var option = new Windows.System.FolderLauncherOptions();
            if (isSelected) {
                option.ItemsToSelect.Add(entry);
            }

            return await Windows.System.Launcher.LaunchFolderAsync(parentFolder, option);
        }

        /// <summary> 在资源管理器中打开某些文件所在的文件夹 </summary>
        /// <param name="parent">要打开的文件夹</param>
        /// <param name="entries">要被选中的文件项(包括文件和文件夹)</param>
        /// <param name="isSelected">是否要选中这些文件,默认为选中</param>
        /// <returns>打开成功返回true,否则返回false</returns>
        public async Task<bool> LaunchFolderInExplorer(StorageFolder parent, List<IStorageItem2> entries, bool isSelected = true) {
            var option = new Windows.System.FolderLauncherOptions();
            if(isSelected) {
                foreach(var item in entries) {
                    option.ItemsToSelect.Add(item);
                }
            }

            return await Windows.System.Launcher.LaunchFolderAsync(parent, option);
        }

        /// <summary> 在资源管理器中打开某个文件夹 </summary>
        /// <param name="folder">选择要打开的文件夹</param>
        /// <returns>打开成功返回true,否则false</returns>
        public async Task<bool> LaunchFolderInExplorer(StorageFolder folder) {
            return await Windows.System.Launcher.LaunchFolderAsync(folder);
        }
        // -----------------------------------------------------------------------------

    }

}