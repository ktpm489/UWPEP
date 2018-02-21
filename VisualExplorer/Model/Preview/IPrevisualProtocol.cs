using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Model.Storage;
using Windows.Foundation;
using Windows.Storage;

namespace VisualExplorer.Model.Preview {

    interface IPrevisualProtocol {

        SEContent ObjectType { get; }
        string DisplayName { get; }
        Size RecommendSize { get; set; }

        Task PreparePreviewAsync();
        void ResetDataSource(StorageFile newSourceFile);
    }

    public abstract class MPrevisualObject: IPrevisualProtocol {

        protected StorageFile SourceFile;
        protected SEContent _objectType;
        protected Size _recommendSize;

        public string DisplayName => SourceFile.DisplayName;

        public SEContent ObjectType => this._objectType;

        public Size RecommendSize {
            get { return _recommendSize; }
            set { _recommendSize = value; } }

        public MPrevisualObject(StorageFile sourceFile) {
            this.SourceFile = sourceFile;

        }

        public abstract Task PreparePreviewAsync();
        public abstract void ResetDataSource(StorageFile newSourceFile);
    }

}
