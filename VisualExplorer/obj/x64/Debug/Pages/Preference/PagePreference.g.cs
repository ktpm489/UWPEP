﻿#pragma checksum "E:\Visual Studio Workspace\UWP\VisualExplorer\VisualExplorer\Pages\Preference\PagePreference.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7E9AB0751EDB6E76F23920218E69B5F3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisualExplorer.Pages.Preference
{
    partial class PagePreference : 
        global::VisualExplorer.Pages.Base.ThemePage, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_FrameworkElement_RequestedTheme(global::Windows.UI.Xaml.FrameworkElement obj, global::Windows.UI.Xaml.ElementTheme value)
            {
                obj.RequestedTheme = value;
            }
        };

        private class PagePreference_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IPagePreference_Bindings
        {
            private global::VisualExplorer.Pages.Preference.PagePreference dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::System.WeakReference obj1;

            private PagePreference_obj1_BindingsTracking bindingsTracking;

            public PagePreference_obj1_Bindings()
            {
                this.bindingsTracking = new PagePreference_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 1:
                        this.obj1 = new global::System.WeakReference((global::VisualExplorer.Pages.Base.ThemePage)target);
                        break;
                    default:
                        break;
                }
            }

            // IPagePreference_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            // PagePreference_obj1_Bindings

            public void SetDataRoot(global::VisualExplorer.Pages.Preference.PagePreference newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::VisualExplorer.Pages.Preference.PagePreference obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_themeAgency(obj.themeAgency, phase);
                    }
                }
            }
            private void Update_themeAgency(global::VisualExplorer.Model.Theme.ThemeAgency obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_themeAgency(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_themeAgency_AppTheme(obj.AppTheme, phase);
                    }
                }
            }
            private void Update_themeAgency_AppTheme(global::Windows.UI.Xaml.ElementTheme obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_RequestedTheme(this.obj1.Target as global::VisualExplorer.Pages.Base.ThemePage, obj);
                }
            }

            private class PagePreference_obj1_BindingsTracking
            {
                global::System.WeakReference<PagePreference_obj1_Bindings> WeakRefToBindingObj; 

                public PagePreference_obj1_BindingsTracking(PagePreference_obj1_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<PagePreference_obj1_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_themeAgency(null);
                }

                public void PropertyChanged_themeAgency(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    PagePreference_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::VisualExplorer.Model.Theme.ThemeAgency obj = sender as global::VisualExplorer.Model.Theme.ThemeAgency;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                    bindings.Update_themeAgency_AppTheme(obj.AppTheme, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "AppTheme":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_themeAgency_AppTheme(obj.AppTheme, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                private global::VisualExplorer.Model.Theme.ThemeAgency cache_themeAgency = null;
                public void UpdateChildListeners_themeAgency(global::VisualExplorer.Model.Theme.ThemeAgency obj)
                {
                    if (obj != cache_themeAgency)
                    {
                        if (cache_themeAgency != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_themeAgency).PropertyChanged -= PropertyChanged_themeAgency;
                            cache_themeAgency = null;
                        }
                        if (obj != null)
                        {
                            cache_themeAgency = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_themeAgency;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.DeleteDialogSwitch = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                    #line 41 "..\..\..\..\Pages\Preference\PagePreference.xaml"
                    ((global::Windows.UI.Xaml.Controls.ToggleSwitch)this.DeleteDialogSwitch).Toggled += this.DeleteDialogSwitch_Toggled;
                    #line default
                }
                break;
            case 3:
                {
                    this.EnterActionCombox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 4:
                {
                    this.DarkRadioBtn = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 25 "..\..\..\..\Pages\Preference\PagePreference.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.DarkRadioBtn).Click += this.DarkRadioBtn_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.LightRadioBtn = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 27 "..\..\..\..\Pages\Preference\PagePreference.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.LightRadioBtn).Click += this.LightRadioBtn_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::VisualExplorer.Pages.Base.ThemePage element1 = (global::VisualExplorer.Pages.Base.ThemePage)target;
                    PagePreference_obj1_Bindings bindings = new PagePreference_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

