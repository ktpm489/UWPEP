using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualExplorer.Resources {

    public static class RIdentifiers {


        public const string ApplicationTheme        = nameof(ApplicationTheme);
        public const string LastFieldType           = nameof(LastFieldType);
        public const string MaximumLoadCount        = nameof(MaximumLoadCount);
        public const string InitialMaximumLoadCount = nameof(InitialMaximumLoadCount);

        // View Fixer
        public const string TileViewDesiredWidth = nameof(TileViewDesiredWidth);
        public const string TileViewItemHeight   = nameof(TileViewItemHeight);


        public const string ShowDeleteConfirmation = nameof(ShowDeleteConfirmation);

        public const string FiledEnterKeyAction = nameof(FiledEnterKeyAction);
        public struct EnterKeyAction {
            public const string RenameEntry = nameof(RenameEntry);
            public const string LaunchEntry = nameof(LaunchEntry);
        }

    }

}
