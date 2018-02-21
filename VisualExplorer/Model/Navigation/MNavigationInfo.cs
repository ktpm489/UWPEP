using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualExplorer.Model.Navigation {
    /// <summary>
    /// 用于页面传值的类
    /// </summary>
    /// <typeparam name="T">传递的参数的类型</typeparam>
    public class MNavigateInfo<T> {

        public object Navigator { get; }
        public T Message { get; }

        public MNavigateInfo(object navigator, T message) {
            this.Navigator = navigator;
            this.Message = message;
        }
    }

}
