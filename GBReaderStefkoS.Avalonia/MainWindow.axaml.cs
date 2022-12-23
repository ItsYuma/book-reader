using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderStefkoS.Presenters.Routes;

namespace GBReaderStefkoS.Avalonia
{
    public partial class MainWindow : Window, ISwitchContent
    {
        //private MainPresenter _presenter;
        private readonly IDictionary<string, UserControl> _pages = new Dictionary<string, UserControl>();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        internal void RegisterPage(string pageName, UserControl page)
        {
            _pages[pageName] = page;
            if(Content == null)
            {
                Content = page;
            }
        }

        public void Goto(string page) => Content = _pages[page];
        
    }
}