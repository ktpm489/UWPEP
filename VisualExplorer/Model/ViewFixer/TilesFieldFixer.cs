using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualExplorer.Framework.Foundation;
using VisualExplorer.Model.Preference;
using VisualExplorer.Resources;

namespace VisualExplorer.Model.ViewFixer {

    public class TilesFieldFixer : BindableBase, IViewFixer {

        private double preferDesiredWidth = (double)PreferenceAgency.Default.GetLocalSetting(RIdentifiers.TileViewDesiredWidth);
        public double DesiredWidth {
            get { return this.preferDesiredWidth; }
            set {
                this.preferDesiredWidth = value;
                OnPropertyChanged("DesiredWidth");
            }
        }

        private double preferItemHeight = (double)PreferenceAgency.Default.GetLocalSetting(RIdentifiers.TileViewItemHeight);

        public double ItemHeight {
            get { return this.preferItemHeight; }
            set {
                this.preferItemHeight = value;
                OnPropertyChanged("ItemHeight");
            }
        }

        private TilesFieldFixer() {

        }


        public void SaveProperties() {
            PreferenceAgency.Default.SetLocalSetting(RIdentifiers.TileViewDesiredWidth, this.preferDesiredWidth);
            PreferenceAgency.Default.SetLocalSetting(RIdentifiers.TileViewItemHeight, this.preferItemHeight);
        }


        // ------------------------------------------------------
        // Singleton Support
        private volatile static TilesFieldFixer _instance = null;
        private static readonly Object LockHelper = new object();

        public static TilesFieldFixer Default {
            get {
                if (_instance == null) {
                    lock (LockHelper) {
                        if (_instance == null) {
                            _instance = new TilesFieldFixer();
                        }
                    }
                }
                return _instance;
            }

        }

        // ------------------------------------------------------
    }
}
