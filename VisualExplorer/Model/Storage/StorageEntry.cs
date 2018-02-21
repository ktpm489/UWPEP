using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Framework.Format;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace VisualExplorer.Model.Storage {

    public enum StorageEntryType {
        File, Folder
    }

    public enum ThumbnailRequestSize : uint {
        GirdFieldItem = 0,
        ColumnFieldItem = 1,
    }

    public enum StorageEntryUsage {
        ParentFolder,
        TilesFieldItem,
        ColumnFieldItem,
        PanelPreview,
    }

    public class StorageEntry {

        private IStorageItem _storageItem;
        private ulong _size;
        public StorageEntryType EntryType;
        public SEContent ContentType;

        // Thumbnail
        public BitmapImage ThumbSource;

        // Getter
        public IStorageItem RawEntry => _storageItem;
        public IStorageItem2 RawEntry2 => _storageItem as IStorageItem2;
        public StorageFile File => _storageItem as StorageFile;
        public StorageFolder Folder => _storageItem as StorageFolder;
        public string DisplayType => (_storageItem as IStorageItemProperties).DisplayType;
        public string Size => FBytesFormatter.Format(_size);

        // Constructor
        public StorageEntry(StorageFolder folder, StorageEntryUsage usage) {
            this.EntryType = StorageEntryType.Folder;
            this.ContentType = SEContent.Folder;
            this._storageItem = folder;
            this.LoadThumbnail(usage);
        }

        public StorageEntry(StorageFile file, StorageEntryUsage usage) {
            this.EntryType = StorageEntryType.File;
            this.ContentType = SEContentAgency.AnyalizeStorageEntryContent(file);
            this._storageItem = file;
            this.LoadThumbnail(usage);
        }

        public StorageEntry(IStorageItem item, StorageEntryUsage usage) {
            if (item is StorageFile file) {
                this.EntryType = StorageEntryType.File;
                this.ContentType = SEContentAgency.AnyalizeStorageEntryContent(file);
                this._storageItem = item;
                this.LoadThumbnail(usage);
            } else {
                this.EntryType = StorageEntryType.Folder;
                this.ContentType = SEContent.Folder;
                this._storageItem = item;
                this.LoadThumbnail(usage);
            }
        }


        // http://www.cnblogs.com/hebeiDGL/archive/2012/09/27/2705478.html
        // Configure Thumbnail
        private async void LoadThumbnail(StorageEntryUsage usage) {

            switch (this.EntryType) {
                case StorageEntryType.File:
                    this.ThumbSource = new BitmapImage();
                    StorageItemThumbnail thumbnail = null;
                    switch (usage) {
                        case StorageEntryUsage.TilesFieldItem:
                        case StorageEntryUsage.ColumnFieldItem:
                            thumbnail = await File.GetThumbnailAsync(
                                SEContentAgency.FieldItemThumbnailModes[(int)ContentType],
                                60, // Requested size
                                ThumbnailOptions.ReturnOnlyIfCached); // TODO: Test the ThumbnailOptions
                            break;
                        case StorageEntryUsage.PanelPreview:
                            thumbnail = await File.GetThumbnailAsync(
                                SEContentAgency.PanelPreviewThumbnailModes[(int)ContentType],
                                150, // Requested size
                                ThumbnailOptions.UseCurrentScale);
                            break;
                        case StorageEntryUsage.ParentFolder:
                            this.ThumbSource = null;
                            return; // 没有必要加载该文件夹的略缩图,直接结束本方法
                    }

                    if (thumbnail is null) {
                        switch (usage) {
                            case StorageEntryUsage.ColumnFieldItem:
                            case StorageEntryUsage.TilesFieldItem:
                                this.ThumbSource.UriSource = SEContentAgency.GetDefaultThumbnailUri(this.ContentType); break;
                            case StorageEntryUsage.PanelPreview:
                                this.ThumbSource.UriSource = SEContentAgency.GetDefaultPreviewUri(this.ContentType); break;
                        }
                    } else {
                        this.ThumbSource.SetSource(thumbnail);
                    }
                    break;
                case StorageEntryType.Folder:
                    switch (usage) {
                        case StorageEntryUsage.TilesFieldItem:
                        case StorageEntryUsage.ColumnFieldItem:
                            this.ThumbSource = new BitmapImage(new Uri("ms-appx:///Assets/THUMB/Folder_Small.png")); break;
                        case StorageEntryUsage.PanelPreview:
                            this.ThumbSource = new BitmapImage(new Uri("ms-appx:///Assets/THUMB/Folder_Medium.png")); break;
                        case StorageEntryUsage.ParentFolder:
                            this.ThumbSource = null;
                            return; // 没有必要加载该文件夹的略缩图,直接结束本方法
                    }
                    break;
            }
        }

        public async Task GetEntrySizeAsyncIfNeed() {
            if(_size == 0) {
                var properties = await _storageItem.GetBasicPropertiesAsync();
                _size = properties.Size;
            }
        }


        public static async Task<bool> EntryExistAsync(StorageEntry entry) {
            // FIXME: 对部分确实存在的文件返回不存在 "G:\STEINS;GATE 线形拘束のフェノグラム.rar"
            switch (entry.EntryType) {
                case StorageEntryType.File:
                    return await Task.Run(() => System.IO.File.Exists(entry.RawEntry.Path));
                case StorageEntryType.Folder:
                    return await Task.Run(() => System.IO.Directory.Exists(entry.RawEntry.Path));
                default: return false;
            }
        }

        /// <summary>
        /// 检查某个名字文件项是否已经存在文件夹中
        /// </summary>
        /// <param name="name">该文件项的名字</param>
        /// <param name="destFolder">搜索查找的目录</param>
        /// <returns>若找到该名字的文件项返回true,否则返回false</returns>
        public static async Task<bool> CheckStorageEntryExistAsync(string name, StorageFolder destFolder) {
            var getResult = await destFolder.TryGetItemAsync(name);
            return getResult != null;
        }
    }
}
