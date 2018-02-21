using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace VisualExplorer.Framework.Seralization {

    public static class FJSONHelper {

        static FJSONHelper() { }

        /// <summary>
        /// 对实体类对象进行序列化的方法
        /// </summary>
        /// <param name="item">实体类对象</param>
        /// <returns>对象对应的JSON字符串</returns>
        public static async Task<string> ToJSONDataAsync(Object item) {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            string jsonString = string.Empty;

            using (MemoryStream ms = new MemoryStream()) {
                serializer.WriteObject(ms, item);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms)) {
                    jsonString = await reader.ReadToEndAsync();
                }
            }


            return jsonString;
        }

        /// <summary>
        /// 把实体类对象序列化后写进文件指定文件中
        /// </summary>
        /// <param name="item">实体类对象</param>
        /// <param name="destFile">写入的目标文件,此操作会覆盖文件</param>
        public static async void ToJSONDataAsync(Object item, StorageFile destFile) {

            var jsonString = await ToJSONDataAsync(item);

            // 获取文件流来进行操作
            using (var raStream = await destFile.OpenAsync(FileAccessMode.ReadWrite)) {
                using (var outStream = raStream.GetOutputStreamAt(0)) {
                    var serializer = new DataContractJsonSerializer(item.GetType());

                    serializer.WriteObject(outStream.AsStreamForWrite(), item);
                    await outStream.FlushAsync();
                }
            }

        }

        /// <summary>
        /// 把JSON字符串反序列化成实体类对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="jsonString">JSON字符串所对应的实体类对象</param>
        /// <returns></returns>
        public static T DataContractJSONDeSerizlizer<T>(string jsonString) {
            var ds = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ds.ReadObject(ms);
            ms.Dispose();
            return obj;
        }
    }

}
