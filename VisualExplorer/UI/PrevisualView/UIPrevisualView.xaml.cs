using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VisualExplorer.Model.Preview;
using VisualExplorer.Model.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VisualExplorer.UI.PrevisualView {

    public sealed partial class UIPrevisualView : UserControl {

        private IPrevisualProtocol PrevisualEntry;
        private UserControl PrevisualControl;

        /// <summary>
        /// 该Popup当前是否显示在屏幕上
        /// </summary>
        public bool IsPrevisualExtend {
            get { return this.PrevisualPopup.IsOpen; }
            set {
                if(value == false && this.PrevisualPopup.IsOpen) {
                    this.DismisssPrevisual();
                }
            }
        }

        public UIPrevisualView() {
            this.InitializeComponent();

        }

        public async void TogglePrevisual(StorageEntry entry) {

            if(this.PrevisualEntry is null) {
                // 当前还没有正在预览的文件,应该开始文件预览的配置
                await CreateNewPrevisualControl(entry);
            } else if(this.PrevisualEntry.DisplayName == entry.File.DisplayName) {
                // 当前已经有正在预览的文件,且触发该函数的是同一个文件,应该关闭预览
                this.DismisssPrevisual();
            } else {
                // 当前已经有正在预览的文件,且触发该函数的是另一个文件,应该注意对相同类型文件的处理
                if (this.PrevisualEntry.ObjectType == entry.ContentType) {
                    await UpdateCurrentPrevisualControl(entry);
                } else {
                    await UpdateNewPrevisualControl(entry);
                }

            }

        }

        /// <summary>
        /// 更新控件和数据绑定
        /// </summary>
        public void UpdatePrevisual(bool isSimilarContent) {
            if (isSimilarContent == false) { // Set to a new Control
                switch (PrevisualEntry.ObjectType) {
                    case SEContent.Image:
                        this.PrevisualControl = new UIPrevisualImage() { DataContext = PrevisualEntry }; break;
                    default:
                        throw new NotImplementedException();
                }
                this.PrevisualContainer.Children.Add(this.PrevisualControl);
            }

            var recommendSize = PrevisualEntry.RecommendSize;
            this.CenterPlacement(-recommendSize.Height / 2, -recommendSize.Width / 2);
            this.PrevisualPopup.IsOpen = true;
        }

        /// <summary>
        /// 之前没有任何预览控件,创建新的预览的控件
        /// </summary>
        private async Task CreateNewPrevisualControl(StorageEntry entry) {
            this.InitialPrevisualModel(entry);

            await PrevisualEntry.PreparePreviewAsync();
            this.UpdatePrevisual(false);
        }

        /// <summary>
        /// 创建一个新的预览控件取代已有的预览控件
        /// </summary>
        private async Task UpdateNewPrevisualControl(StorageEntry entry) {
            // 文件类型与上一个不同
            PrevisualEntry = null;
            this.PrevisualPopup.IsOpen = false;
            this.PrevisualContainer.Children.Remove(PrevisualControl);
            this.InitialPrevisualModel(entry);
            await PrevisualEntry.PreparePreviewAsync();
            this.UpdatePrevisual(false);
        }

        /// <summary>
        /// 重新现有的预览控件,配置新的数据
        /// </summary>
        private async Task UpdateCurrentPrevisualControl(StorageEntry entry) {
            // 文件类型与上一个相同
            this.PrevisualPopup.IsOpen = false;
            PrevisualEntry.ResetDataSource(entry.File);
            await PrevisualEntry.PreparePreviewAsync();
            this.UpdatePrevisual(true);
        }

        /// <summary>
        /// 关闭预览
        /// </summary>
        public void DismisssPrevisual() {
            PrevisualEntry = null;
            this.PrevisualContainer.Children.Remove(PrevisualControl);
            this.PrevisualPopup.IsOpen = false;
        }

        /// <summary>
        /// 调整Popup的偏移
        /// </summary>
        /// <param name="verticalOffset">垂直偏移</param>
        /// <param name="horizontalOffset">水平偏移</param>
        private void CenterPlacement(double verticalOffset, double horizontalOffset) {
            PrevisualPopup.VerticalOffset = verticalOffset;
            PrevisualPopup.HorizontalOffset = horizontalOffset;
        }

        private void InitialPrevisualModel(StorageEntry entry) {
            switch (entry.ContentType) {
                case SEContent.Image: PrevisualEntry = new MPrevisualImage(entry.File); break;
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
