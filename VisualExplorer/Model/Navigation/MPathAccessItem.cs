using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualExplorer.Model.Navigation {

    public class MRootAccessObject {
        public List<MDeviceAccessItem> devicesList;
        public List<MPathAccessGroup> groupList;
        public string LastLocation;
    }

    public class MPathAccessGroup {
        public List<MPathAccessItem> itemLists;
        public bool IsExpand;
        public string Title;
    }

    public class ObservableMPathAccessGroup {
        public ObservableCollection<MPathAccessItem> itemLists;
        public bool IsExpand;
        public string Title;
    }

    public class MPathAccessItem {
        public string token;
        public string displayPath;
    }

    public class MDeviceAccessItem : MPathAccessItem {

    }


}
