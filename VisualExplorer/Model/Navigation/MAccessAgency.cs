using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VisualExplorer.Framework.Seralization;
using VisualExplorer.Model.Dialog;
using Windows.Storage;

namespace VisualExplorer.Model.Navigation {

    public class MAccessAgency {

        private const string AccessFileName = "AccessList.json";
        private MRootAccessObject _rootObject;
        private bool isModify = false;

        private MAccessAgency() {
            this.LoadAccessListAsync();
        }

        private async void LoadAccessListAsync() {
            if (await ApplicationData.Current.LocalFolder.TryGetItemAsync(AccessFileName) is StorageFile jsonFile) {
                // 文件存在,读取其内容
                StreamReader sr = new StreamReader(await jsonFile.OpenStreamForReadAsync());
                var jsonString = await sr.ReadToEndAsync();

                _rootObject = FJSONHelper.DataContractJSONDeSerizlizer<MRootAccessObject>(jsonString);
                if (_rootObject.groupList is null)
                    _rootObject.groupList = new List<MPathAccessGroup>();

            } else {
                // 这个文件还不存在,初始化类的相关属性为空
                _rootObject = new MRootAccessObject() {
                    groupList = new List<MPathAccessGroup>(),
                    devicesList = new List<MDeviceAccessItem>(),
                    LastLocation = null
                };
            }
        }

        private async void WriteToAccessFileAsync() {
            var jsonFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(AccessFileName, CreationCollisionOption.ReplaceExisting);
            FJSONHelper.ToJSONDataAsync(_rootObject, jsonFile);
        }

        // ------------------------------------------------------------
        // Public Interface

        public List<MPathAccessGroup> AccessGroups {
            get { return this._rootObject.groupList; }
        }
        public List<MDeviceAccessItem> DrivesItems {
            get { return this._rootObject.devicesList; }
        }

        /// <summary>
        /// 添加一个访问列表的类别
        /// </summary>
        /// <param name="groupTitle">列表的类别名</param>
        /// <returns>如果已存在该类别返回null,否则返回新添加的类别</returns>
        public MPathAccessGroup AppendAccessGroup(string groupTitle) {
            if (_rootObject.groupList.Exists(p => p.Title == groupTitle)) {
                // 如果已经有这个类别了
                return null;
            } else {
                var newGroup = new MPathAccessGroup { Title = groupTitle, IsExpand = true, itemLists = null };
                _rootObject.groupList.Add(newGroup);
                this.isModify = true;
                return newGroup;
            }
        }

        /// <summary>
        /// 往某个类别的访问列表里添加项
        /// </summary>
        /// <param name="folder">要添加到访问列表的文件夹</param>
        /// <param name="groupTitle">添加到的类别</param>
        /// <returns>新添加 AccessItem </returns>
        public MPathAccessItem AppendAccessLocation(StorageFolder folder, string groupTitle) {
            var token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder);

            var destGroup = _rootObject.groupList.First(p => p.Title == groupTitle);
            var newItem = new MPathAccessItem { token = token, displayPath = folder.Name };
            if (destGroup.itemLists == null) {
                destGroup.itemLists = new List<MPathAccessItem>();
            }
            destGroup.itemLists.Add(newItem);

            isModify = true;

            return newItem;
        }

        /// <summary>
        /// 添加一个磁盘的路径
        /// </summary>
        /// <param name="deviceFolder">目的磁盘所代表的文件夹</param>
        /// <returns>添加后的 AccessItem,如果该路径不合法或已存在,弹出提示框</returns>
        public MDeviceAccessItem AppendDeviceLocation(StorageFolder deviceFolder) {
            var devicePath = deviceFolder.Path;
            var regex = new Regex("^[A-Z]:\\\\$");
            if (regex.Match(devicePath).Success) {
                if (_rootObject.devicesList.Exists(p => p.displayPath == deviceFolder.DisplayName)) {
                    // 弹出框提示
                    MDialogAgency.DisplayMessageDialog("The device path was existed.");
                } else {
                    var token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(deviceFolder);
                    var newDeviceItem = new MDeviceAccessItem { token = token, displayPath = deviceFolder.DisplayName };
                    _rootObject.devicesList.Add(newDeviceItem);
                    this.isModify = true;

                    return newDeviceItem;
                }
            } else {
                // 弹出框提示
                MDialogAgency.DisplayMessageDialog("Invalid Path");
            }

            return null;
        }

        public void SetLostLocation(MPathAccessItem item) {
            throw new NotImplementedException();
        }

        /// <summary>  更新本地的存储文件 </summary>
        public void UpdateAcessStorageFileIfNeed() {
            if (this.isModify) {
                this.WriteToAccessFileAsync();
                this.isModify = false;
            }
        }


        // ------------------------------------------------------
        // Singleton Support
        private volatile static MAccessAgency _instance = null;
        private static readonly Object LockHelper = new object();

        public static MAccessAgency Default {
            get {
                if (_instance == null) {
                    lock (LockHelper) {
                        if (_instance == null) {
                            _instance = new MAccessAgency();
                        }
                    }
                }
                return _instance;
            }

        }

        // ------------------------------------------------------
    }

}
