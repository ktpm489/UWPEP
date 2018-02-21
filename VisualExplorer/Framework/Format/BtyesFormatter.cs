using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualExplorer.Framework.Format {

    public static class FBytesFormatter {

        static FBytesFormatter() { }

        const int GB = 1024 * 1024 * 1024;  //定义GB的计算常量
        const int MB = 1024 * 1024;         //定义MB的计算常量
        const int KB = 1024;                //定义KB的计算常量

        public static string Format(ulong bytes) {

            if (bytes / GB >= 1)        //如果当前Byte的值大于等于1GB
                return (Math.Round(bytes / (float)GB, 2)).ToString() + " GB";    //将其转换成GB
            else if (bytes / MB >= 1)   //如果当前Byte的值大于等于1MB
                return (Math.Round(bytes / (float)MB, 2)).ToString() + " MB";    //将其转换成MB
            else if (bytes / KB >= 1)   //如果当前Byte的值大于等于1KB
                return (Math.Round(bytes / (float)KB, 2)).ToString() + " KB";    //将其转换成KGB
            else
                return bytes.ToString() + " Byte";   //显示Byte值
        }

    }

}
