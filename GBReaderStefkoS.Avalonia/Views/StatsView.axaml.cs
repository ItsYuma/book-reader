using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Avalonia.UserControls;
using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Presenters.Views;

namespace GBReaderStefkoS.Avalonia.Views
{
    public partial class StatsView : UserControl, IStatsView
    {
        public StatsView()
        {
            InitializeComponent();
        }

        public void SetData(int nbSessions)
        {
            NbSessions.Text = "Nombre de livres en cours de lecture : " + nbSessions;
            SessionList.Children.Clear();
        }

        public void AddReadingSessionToView(string bookTitle, string bookIsbn, int pageIndex, string dateBeginning, string dateLastReading)
        {
            var sessionsUserControl = new SessionUserControl(bookTitle, bookIsbn, pageIndex, dateBeginning, dateLastReading);
            SessionList.Children.Add(sessionsUserControl);
        }
        
        /*public void ShowError(string message)
        {
            Error.Text = message;
        }*/
        
        public event EventHandler<EventArgs> QuitRequested;

        private void BackToHome(object? sender, RoutedEventArgs e)
        {
            QuitRequested?.Invoke(this, EventArgs.Empty);
        }
        
    }
}