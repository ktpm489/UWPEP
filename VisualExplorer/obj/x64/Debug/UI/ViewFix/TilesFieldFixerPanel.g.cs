﻿#pragma checksum "E:\Visual Studio Workspace\UWP\VisualExplorer\VisualExplorer\UI\ViewFix\TilesFieldFixerPanel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FB721530D7412F7CBBD07A1673EC66FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisualExplorer.UI.ViewFix
{
    partial class TilesFieldFixerPanel : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.DesiredWidthBlock = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 2:
                {
                    this.ItemHeightBlock = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 3:
                {
                    this.SaveBlock = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 4:
                {
                    this.SaveButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 28 "..\..\..\..\UI\ViewFix\TilesFieldFixerPanel.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SaveButton).Click += this.SaveButton_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.ItemHeightSlider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 6:
                {
                    this.DesiredWidthSlider = (global::Windows.UI.Xaml.Controls.Slider)(target);
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
            return returnValue;
        }
    }
}
