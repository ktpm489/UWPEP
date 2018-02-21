using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Model.Navigation;
using VisualExplorer.Resources;
using Windows.UI.Xaml;

namespace VisualExplorer.Model.Preference {

    public sealed partial class PreferenceAgency {

        /// <summary> 应用程序的主题,有Dark和Light两种主题 </summary>
        public ElementTheme ApplicationTheme { get; set; }

        /// <summary> 第一次加载文件夹内文件和文件夹时加载的最大数量 </summary>
        public uint InitialMaxLoadCount { get; set; }
        /// <summary> 每次加载文件夹内文件和文件夹时加载的最大数量 </summary>
        public uint MaxLoadCount { get; set; }


        private void SetDefaultPreference() {
            this.SetLocalSetting(RIdentifiers.ApplicationTheme, ElementTheme.Dark.ToString());
            this.SetLocalSetting(RIdentifiers.LastFieldType, (int)MFolderLocationAgency.ServicePattern.TilesField);

            // 加载文件的数量
            this.SetLocalSetting(RIdentifiers.MaximumLoadCount, 50);
            this.SetLocalSetting(RIdentifiers.InitialMaximumLoadCount, 100);

            // 视图调整
            this.SetLocalSetting(RIdentifiers.TileViewDesiredWidth, 300.0);
            this.SetLocalSetting(RIdentifiers.TileViewItemHeight, 60.0);

            // 是否显示删除提示框
            this.SetLocalSetting(RIdentifiers.ShowDeleteConfirmation, true);

            // 回车的默认行为
            this.SetLocalSetting(RIdentifiers.FiledEnterKeyAction, RIdentifiers.EnterKeyAction.RenameEntry);

            this.InitialPreferenceItems();
        }

        private void InitialPreferenceItems() {
            var themeString = this.GetLocalSetting(RIdentifiers.ApplicationTheme) as string;
            switch (themeString) {
                case nameof(ElementTheme.Dark): this.ApplicationTheme = ElementTheme.Dark; break;
                case nameof(ElementTheme.Light): this.ApplicationTheme = ElementTheme.Light; break;
                case nameof(ElementTheme.Default): this.ApplicationTheme = ElementTheme.Default; break;
            }

            MaxLoadCount = Convert.ToUInt32(this.GetLocalSetting(RIdentifiers.MaximumLoadCount));
            InitialMaxLoadCount = Convert.ToUInt32(this.GetLocalSetting(RIdentifiers.InitialMaximumLoadCount));

        }

    }
}
