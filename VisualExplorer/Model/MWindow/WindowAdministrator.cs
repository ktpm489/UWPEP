using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;

namespace VisualExplorer.Model.MWindow {

    using AppWindow = Windows.UI.Xaml.Window;

    enum WindowUsage {
        MainWindow,
        ViewFixer
    }

    struct WindowInfo {
        public WindowUsage usage;
        public int id; // The id of window
    }

    /// <summary>
    /// 该类用于管理Window窗口.
    /// </summary>
    public class WindowAdministrator {

        private List<WindowInfo> infos = new List<WindowInfo>();

        private WindowAdministrator() {

            var coreWindow = CoreApplication.GetCurrentView().CoreWindow;
            var mainWindowID = ApplicationView.GetApplicationViewIdForWindow(coreWindow);
            infos.Add(new WindowInfo { usage = WindowUsage.MainWindow, id = mainWindowID });
        }

        public async void CreateNewMainWindowAsync() {
            await this.CreateWindowAsyncFor(typeof(MainPage), WindowUsage.MainWindow);
        }

        /// <summary> 在另一个线程中创建一个新的窗口 </summary>
        private async Task CreateWindowAsyncFor(Type papeType, WindowUsage usage) {
            // paste from https://docs.microsoft.com/zh-cn/windows/uwp/layout/show-multiple-views
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                Frame frame = new Frame();
                frame.Navigate(papeType, null);
                AppWindow.Current.Content = frame;
                // You have to activate the window in order to show it later.
                AppWindow.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
                this.infos.Add(new WindowInfo { usage = usage, id = newViewId });
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId, ViewSizePreference.Default);
            if (!viewShown) {
                // create window failure.

            }
        }

        // ------------------------------------------------------
        // Singleton Support
        private volatile static WindowAdministrator _instance = null;
        private static readonly Object LockHelper = new object();

        public static WindowAdministrator Default {
            get {
                if (_instance == null) {
                    lock (LockHelper) {
                        if (_instance == null) {
                            _instance = new WindowAdministrator();
                        }
                    }
                }
                return _instance;
            }

        }
        // ------------------------------------------------------
    }
}
