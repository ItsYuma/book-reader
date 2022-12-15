using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Presenters.Events;

namespace GBReaderStefkoS.Avalonia.UserControls
{
    public partial class ChoiceUserControl : UserControl
    {
        public ChoiceUserControl()
        {
            InitializeComponent();
        }
        
        public ChoiceUserControl(string text, int indexToEnd) : this()
        {
            Text.Text = "Texte du choix : " + text + " -> ";
            IndexToEnd.Text = "Aller à la page : " + $"{indexToEnd}";
        }

        private void GoToPageIndex(object? sender, RoutedEventArgs e)
        {
            SwitchPageRequested?.Invoke(this, new PageEventArg(int.Parse(IndexToEnd.Text.Split(" ")[5])));
        }
        
        public event EventHandler<PageEventArg> SwitchPageRequested;
    }
}