﻿#pragma checksum "E:\Visual Studio Workspace\UWP\VisualExplorer\VisualExplorer\UI\Controls\UIFieldAddressBar.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E53536386E625F57017D48B92E8CEE0F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisualExplorer.UI.Controls
{
    partial class UIFieldAddressBar : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_FrameworkElement_RequestedTheme(global::Windows.UI.Xaml.FrameworkElement obj, global::Windows.UI.Xaml.ElementTheme value)
            {
                obj.RequestedTheme = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_ContentControl_Content(global::Windows.UI.Xaml.Controls.ContentControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.Content = value;
            }
        };

        private class UIFieldAddressBar_obj3_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IUIFieldAddressBar_Bindings
        {
            private global::VisualExplorer.Model.AddressBar.MAddressItem dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.Button obj4;

            public UIFieldAddressBar_obj3_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 4:
                        this.obj4 = (global::Windows.UI.Xaml.Controls.Button)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::VisualExplorer.Model.AddressBar.MAddressItem data = args.NewValue as global::VisualExplorer.Model.AddressBar.MAddressItem;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::VisualExplorer.Model.AddressBar.MAddressItem was expected.");
                 }
                 this.SetDataRoot(data);
                 this.Update();
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(args.Item as global::VisualExplorer.Model.AddressBar.MAddressItem);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.StackPanel)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::VisualExplorer.Model.AddressBar.MAddressItem) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
            }

            // IUIFieldAddressBar_Bindings

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
            }

            // UIFieldAddressBar_obj3_Bindings

            public void SetDataRoot(global::VisualExplorer.Model.AddressBar.MAddressItem newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::VisualExplorer.Model.AddressBar.MAddressItem obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ItemName(obj.ItemName, phase);
                    }
                }
            }
            private void Update_ItemName(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ContentControl_Content(this.obj4, obj, null);
                }
            }
        }

        private class UIFieldAddressBar_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IUIFieldAddressBar_Bindings
        {
            private global::VisualExplorer.UI.Controls.UIFieldAddressBar dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::System.WeakReference obj1;
            private global::Windows.UI.Xaml.Controls.ListView obj2;

            private UIFieldAddressBar_obj1_BindingsTracking bindingsTracking;

            public UIFieldAddressBar_obj1_Bindings()
            {
                this.bindingsTracking = new UIFieldAddressBar_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 1:
                        this.obj1 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.UserControl)target);
                        break;
                    case 2:
                        this.obj2 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    default:
                        break;
                }
            }

            // IUIFieldAddressBar_Bindings

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

            // UIFieldAddressBar_obj1_Bindings

            public void SetDataRoot(global::VisualExplorer.UI.Controls.UIFieldAddressBar newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::VisualExplorer.UI.Controls.UIFieldAddressBar obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_themeAgency(obj.themeAgency, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_items(obj.items, phase);
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
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_RequestedTheme(this.obj1.Target as global::Windows.UI.Xaml.Controls.UserControl, obj);
                }
            }
            private void Update_items(global::System.Collections.ObjectModel.ObservableCollection<global::VisualExplorer.Model.AddressBar.MAddressItem> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj2, obj, null);
                }
            }

            private class UIFieldAddressBar_obj1_BindingsTracking
            {
                global::System.WeakReference<UIFieldAddressBar_obj1_Bindings> WeakRefToBindingObj; 

                public UIFieldAddressBar_obj1_BindingsTracking(UIFieldAddressBar_obj1_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<UIFieldAddressBar_obj1_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_themeAgency(null);
                }

                public void PropertyChanged_themeAgency(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    UIFieldAddressBar_obj1_Bindings bindings;
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
                    this.AddressList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 22 "..\..\..\..\UI\Controls\UIFieldAddressBar.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.FolderName_Click;
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
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    UIFieldAddressBar_obj1_Bindings bindings = new UIFieldAddressBar_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.StackPanel element3 = (global::Windows.UI.Xaml.Controls.StackPanel)target;
                    UIFieldAddressBar_obj3_Bindings bindings = new UIFieldAddressBar_obj3_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::VisualExplorer.Model.AddressBar.MAddressItem) element3.DataContext);
                    element3.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element3, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}
