using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualExplorer.Model.Preference {

    using PreferenceRoot = Windows.Storage.ApplicationData;

    public sealed partial class PreferenceAgency {

        private const string PreferencePrefix = "Preference.";
        private const string FirstLaunchKey = PreferencePrefix + "IsApplicationFirstLaunch";
        private Windows.Storage.ApplicationDataContainer LocalSetting = PreferenceRoot.Current.LocalSettings;

        private PreferenceAgency() { }

        public void ConfigureInitialPreferenceIfNeed() {
            if (!LocalSetting.Values.ContainsKey(FirstLaunchKey)) {
                this.SetDefaultPreference();
                LocalSetting.Values[FirstLaunchKey] = true;
            } else {
                this.InitialPreferenceItems();
            }
        }

        /// <summary> 存储本地设置 </summary>
        public void SetLocalSetting(string key, object value) {
            PreferenceRoot.Current.LocalSettings.Values[PreferencePrefix + key] = value;
        }

        /// <summary> 获取本地设置 </summary>
        public object GetLocalSetting(string key) {
            return PreferenceRoot.Current.LocalSettings.Values[PreferencePrefix + key];
        }


        // ------------------------------------------------------
        // Singleton Support
        private volatile static PreferenceAgency _instance = null;
        private static readonly Object LockHelper = new object();

        public static PreferenceAgency Default {
            get {
                if (_instance == null) {
                    lock (LockHelper) {
                        if (_instance == null) {
                            _instance = new PreferenceAgency();
                        }
                    }
                }
                return _instance;
            }

        }
        // ------------------------------------------------------
    }

}
