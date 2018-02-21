using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Framework.Format;
using VisualExplorer.Model.Storage;
using VisualExplorer.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace VisualExplorer.Model.Preview {

    public class MPrevisualImage: MPrevisualObject {

        public BitmapImage ImageSource { get; private set; }

        public MPrevisualImage(StorageFile sourceFile): base(sourceFile) {
            this._objectType = SEContent.Image;

        }

        public override async Task PreparePreviewAsync() {
            await this.ConfigureImageSourceAsync();
            this.ConfigureRecommendSize();
        }

        private async Task ConfigureImageSourceAsync() {
            ImageSource = new BitmapImage();

            using(var rs = await SourceFile.OpenReadAsync()) {
                await ImageSource.SetSourceAsync(rs);
            }
        }

        private void ConfigureRecommendSize() {
            var applicationSize = new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
            var originalImageSize = new Size(ImageSource.PixelWidth, ImageSource.PixelHeight);
            RecommendSize = FSizeFixer.CalcRecommendSize(applicationSize, originalImageSize, RConstants.PrevisualImageEdgeOffset);
        }

        public override void ResetDataSource(StorageFile newSourceFile) {
            this.SourceFile = newSourceFile;
        }

    }
}
