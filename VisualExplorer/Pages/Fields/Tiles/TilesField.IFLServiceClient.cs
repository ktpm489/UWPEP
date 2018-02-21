using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Model.Storage;
using Windows.Storage;

namespace VisualExplorer.Pages.Fields.Tiles {

    public sealed partial class TilesField {

        public override void SwitchDestFolderAddress(StorageFolder destFolder) {
            this.folderEntry = new StorageEntry(destFolder, StorageEntryUsage.TilesFieldItem);
            base.OpenFolderAsync(folderEntry);

            this.LoadEntries();
        }
    }

}
