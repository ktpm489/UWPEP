using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Resources;
using VisualExplorer.UI.Controls;
using Windows.Storage;

namespace VisualExplorer.Model.Navigation {

    // Folder Location Service Client
    public interface IFLServiceClient {

        void SwitchDestFolderAddress(StorageFolder destFolder);
    }

    /// <summary>
    /// 该类用于文件夹切换之间的数据管理
    /// </summary>
    public class MFolderLocationAgency {

        public enum ServicePattern : int {
            TilesField,
            ColumnField
        }

        private UIFieldAddressBar addressBar;
        private Stack<StorageFolder> folderStack = new Stack<StorageFolder>();
        private ServicePattern serviceMode;
        private IFLServiceClient client = null;

        public MFolderLocationAgency() {
            this.serviceMode = (ServicePattern)Preference.PreferenceAgency.Default.GetLocalSetting(RIdentifiers.LastFieldType);
        }

        public void SwitchServiceClient(IFLServiceClient newClient, ServicePattern pattern) {
            this.serviceMode = pattern;
            this.client = newClient;
        }

        public async void InitialFolderStackAsync(StorageFolder destFolder, UIFieldAddressBar bar) {
            // Configure folder Stack
            folderStack.Clear();

            var parentFolder = destFolder;
            List<StorageFolder> parentFolders = new List<StorageFolder> { parentFolder };
            while (true) {
                parentFolder = await parentFolder.GetParentAsync();
                if (parentFolder is null) {
                    break;
                } else {
                    parentFolders.Add(parentFolder);
                }
            }

            parentFolders.Reverse();
            parentFolders.ForEach(p => folderStack.Push(p));

            // Configure Address Bar
            this.addressBar = bar;
            addressBar.FLAgency = this;
            addressBar.InitialAddressStack(folderStack);
        }
        public async Task<StorageFolder> InitialFolderStackAsync(string token, UIFieldAddressBar bar) {
            var destFolder = await this.RetrieveFromFutureAccessListAsync(token);
            this.InitialFolderStackAsync(destFolder, bar);
            return destFolder;
        }

        /// <summary> 访问 FutureAccessList 存储的文件夹 </summary>
        public async Task<StorageFolder> RetrieveFromFutureAccessListAsync(string token) {
            return await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
        }


        public void PushFolder(StorageFolder folder) {
            folderStack.Push(folder);
            addressBar.PushAddress(folder);
        }
        public bool PopFolder(int count) {
            for(var i =0; i< count; i++) {
                folderStack.Pop();
            }

            addressBar.PopAddress(count);
            client.SwitchDestFolderAddress(folderStack.First());

            return true;
        }

    }

}