﻿#pragma checksum "..\..\FindAndReplaceColumn.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7E167FA2632B595F2995FCF0CDEE94CA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace TableListCommands {
    
    
    /// <summary>
    /// FindAndReplaceColumn
    /// </summary>
    public partial class FindAndReplaceColumn : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\FindAndReplaceColumn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TableListCommands.FindAndReplaceColumn frmFindAndReplaceColumn;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\FindAndReplaceColumn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridCSVContent;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\FindAndReplaceColumn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReplace;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\FindAndReplaceColumn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFind;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\FindAndReplaceColumn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtReplace;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\FindAndReplaceColumn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFind;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\FindAndReplaceColumn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblReplace;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Tables And Lists;component/findandreplacecolumn.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FindAndReplaceColumn.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.frmFindAndReplaceColumn = ((TableListCommands.FindAndReplaceColumn)(target));
            
            #line 4 "..\..\FindAndReplaceColumn.xaml"
            this.frmFindAndReplaceColumn.Loaded += new System.Windows.RoutedEventHandler(this.frmTableCSVGrid_Loaded);
            
            #line default
            #line hidden
            
            #line 5 "..\..\FindAndReplaceColumn.xaml"
            this.frmFindAndReplaceColumn.Closing += new System.ComponentModel.CancelEventHandler(this.frmTableCSVGrid_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.gridCSVContent = ((System.Windows.Controls.DataGrid)(target));
            
            #line 8 "..\..\FindAndReplaceColumn.xaml"
            this.gridCSVContent.Sorting += new System.Windows.Controls.DataGridSortingEventHandler(this.gridCSVContent_Sorting);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnReplace = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\FindAndReplaceColumn.xaml"
            this.btnReplace.Click += new System.Windows.RoutedEventHandler(this.btnInsertRow_Click_1);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 11 "..\..\FindAndReplaceColumn.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtFind = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtReplace = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.lblFind = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lblReplace = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
