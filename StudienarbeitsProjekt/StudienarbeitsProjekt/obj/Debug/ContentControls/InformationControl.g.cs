﻿#pragma checksum "..\..\..\ContentControls\InformationControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C0BA71080B614A52D2FDF170E52585C4"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.34014
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Controls.Primitives;
using Microsoft.Surface.Presentation.Controls.TouchVisualizations;
using Microsoft.Surface.Presentation.Input;
using Microsoft.Surface.Presentation.Palettes;
using StudienarbeitsProjekt.ContentControls;
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


namespace StudienarbeitsProjekt.ContentControls {
    
    
    /// <summary>
    /// InformationControl
    /// </summary>
    public partial class InformationControl : StudienarbeitsProjekt.ContentControls.MovableScatterViewItem, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Titel;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton Close;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer Scroller;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceTextBox VisitorName;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceTextBox Email;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Definition;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel AuswahlStudiengaenge;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Confirmation;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\ContentControls\InformationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton ConfirmationButton;
        
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
            System.Uri resourceLocater = new System.Uri("/StudienarbeitsProjekt;component/contentcontrols/informationcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ContentControls\InformationControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.Titel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Close = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            return;
            case 3:
            this.Scroller = ((Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer)(target));
            return;
            case 4:
            this.VisitorName = ((Microsoft.Surface.Presentation.Controls.SurfaceTextBox)(target));
            
            #line 31 "..\..\..\ContentControls\InformationControl.xaml"
            this.VisitorName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Name_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Email = ((Microsoft.Surface.Presentation.Controls.SurfaceTextBox)(target));
            
            #line 35 "..\..\..\ContentControls\InformationControl.xaml"
            this.Email.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Email_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Definition = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.AuswahlStudiengaenge = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.Confirmation = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.ConfirmationButton = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            
            #line 69 "..\..\..\ContentControls\InformationControl.xaml"
            this.ConfirmationButton.Click += new System.Windows.RoutedEventHandler(this.Confirmation_Clicked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

