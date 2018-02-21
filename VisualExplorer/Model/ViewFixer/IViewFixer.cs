using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualExplorer.Model.ViewFixer {

    public enum ViewFixerType {
        TileView,
        ColumnView
    }

    public interface IViewFixer {

        void SaveProperties();
    }

}
