using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace VisualExplorer.Model.Storage {

    /// <summary>
    /// StorageEntry Content 表示文件项的类型
    /// </summary>
    public enum SEContent : int {
        Folder = 0,
        Generic = 1,
        Text = 2,
        Image = 3,
        Audio = 4,
        Video = 5,
    }

    public static class SEContentAgency {

        static SEContentAgency() { }

        //private static readonly string[] TextTypes = new string[1] { "text/plain" };
        //private static readonly string[] ImageTypes = new string[3] { "image/jpeg", "image/png", "image/bmp" };
        //private static readonly string[] VideoTypes = new string[2] { "video/quicktime", "video/mp4" };
        //private static readonly string[] AudioTypes = new string[3] { "audio/mpeg", "audio/mp4", "audio/flac" };

        // https://docs.microsoft.com/en-us/uwp/api/Windows.Storage.FileProperties.ThumbnailMode
        public static readonly ThumbnailMode[] FieldItemThumbnailModes = {
            ThumbnailMode.ListView,      // Folder
            ThumbnailMode.DocumentsView, // Generic
            ThumbnailMode.DocumentsView, // Text
            ThumbnailMode.PicturesView,  // Image
            ThumbnailMode.MusicView,     // Audio
            ThumbnailMode.VideosView,    // Video
        };
        public static readonly ThumbnailMode[] PanelPreviewThumbnailModes = {
            ThumbnailMode.ListView,      // Folder
            ThumbnailMode.DocumentsView, // Generic
            ThumbnailMode.DocumentsView, // Text
            ThumbnailMode.PicturesView,  // Image
            ThumbnailMode.MusicView,     // Audio
            ThumbnailMode.VideosView,    // Video
        };

        public static SEContent AnyalizeStorageEntryContent(StorageFile file) {
            if (file.ContentType.StartsWith("text")) {
                return SEContent.Text;
            } else if (file.ContentType.StartsWith("image")) {
                return SEContent.Image;
            } else if (file.ContentType.StartsWith("audio")) {
                return SEContent.Audio;
            } else if (file.ContentType.StartsWith("video")) {
                return SEContent.Video;
            } else {
                return SEContent.Generic;
            }
        }

        public static Uri GetDefaultThumbnailUri(SEContent contentType) {
            switch(contentType) {
                case SEContent.Image:
                    return new Uri("ms-appx:///Assets/THUMB/Photo_Small.png");
                default:
                    return new Uri("ms-appx:///Assets/THUMB/File_Small.png");
            }
        }

        public static Uri GetDefaultPreviewUri(SEContent contentType) {
            switch(contentType) {
                case SEContent.Image:
                    return new Uri("ms-appx:///Assets/THUMB/Photo_Medium.png");
                default:
                    return new Uri("ms-appx:///Assets/THUMB/Folder_Medium.png");
            }
        }
    }

}
