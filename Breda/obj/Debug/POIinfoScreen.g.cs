﻿#pragma checksum "C:\Users\TYB\Documents\Visual Studio 2010\Projects\Breda Mobiel SVN\Breda\POIinfoScreen.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AE2AC74308FF03A773C4A12C3B352675"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace View {
    
    
    public partial class POIinfoScreen : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock Textbox;
        
        internal System.Windows.Controls.Button closeButton;
        
        internal System.Windows.Controls.Image image1;
        
        internal System.Windows.Controls.Button button1;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Breda;component/POIinfoScreen.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.Textbox = ((System.Windows.Controls.TextBlock)(this.FindName("Textbox")));
            this.closeButton = ((System.Windows.Controls.Button)(this.FindName("closeButton")));
            this.image1 = ((System.Windows.Controls.Image)(this.FindName("image1")));
            this.button1 = ((System.Windows.Controls.Button)(this.FindName("button1")));
        }
    }
}

