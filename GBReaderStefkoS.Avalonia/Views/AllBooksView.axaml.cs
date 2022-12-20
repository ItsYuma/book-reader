using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Avalonia.UserControls;
using GBReaderStefkoS.Presenters.Events;
using GBReaderStefkoS.Presenters.Views;

namespace GBReaderStefkoS.Avalonia.Views
{
    public partial class AllBooksView : UserControl, IAllBooksView
    {
        public AllBooksView()
        {
            InitializeComponent();
            //SetItems();
        }
        
        public void addBookToView(string author, string title, string resume, string isbn)
        {
            var bookUserControl = new BookUserControl(author, title, resume, isbn);

            if (!ResumePanel.IsVisible)
            {
                TitleBookResume.Text = "Titre : " + title;
                Resume.Text = "Résumé : " + resume;
                ResumePanel.IsVisible = true;
            }
            
            bookUserControl.DetailRequested += ShowDetailsBookSelected;
            BookList.Children.Add(bookUserControl);
        }

        public void ShowDetailsBookSelected(object? sender, DetailsEventArgs args)
        {
            TitleBookResume.Text = args.Title;
            Resume.Text = args.Resume;
            Isbn.Text = args.Isbn;
        }

        public void ShowError(string message)
        {
            Search.IsEnabled = false;
            Error.Text = message;
        }

        private void InputElement_OnKeyDown(object? sender, KeyEventArgs arg)
        {
            if (arg.Key == Key.Return)
            {
                BookList.Children.Clear();
                ResumePanel.IsVisible = false;
                SearchRequested?.Invoke(this, new SearchEventArg(Search.Text));
            }
        }
        
        private void StartReading(object? sender, RoutedEventArgs e)
        {
            ReadingRequested?.Invoke(this, new ReadingEventArg(Isbn.Text));
        }
        
        private void ShowStats(object? sender, RoutedEventArgs e)
        {
            StatsRequested?.Invoke(this, EventArgs.Empty);
        }
        
        public event EventHandler<SearchEventArg> SearchRequested;
        public event EventHandler<ReadingEventArg> ReadingRequested;
        public event EventHandler<EventArgs> StatsRequested;
        
    }
}