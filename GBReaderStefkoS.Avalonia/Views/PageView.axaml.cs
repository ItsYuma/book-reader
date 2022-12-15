using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Avalonia.UserControls;
using GBReaderStefkoS.Presenters.Events;
using GBReaderStefkoS.Presenters.Views;

namespace GBReaderStefkoS.Avalonia.Views
{
    public partial class PageView : UserControl, IPageView
    {
        public PageView()
        {
            InitializeComponent();
        }

        public void SetData(string bookTitle, int nbPages , int pageIndex, string pageText)
        {
            ChoiceList.Children.Clear();
            EndOfStory.IsVisible = false;
            
            BookTitle.Text = bookTitle;
            NbPages.Text = "Nombre de pages : " + $"{nbPages}";
            PageText.Text = "Texte de la page numéro " + $"{pageIndex} : " + pageText;
        }

        public void AddChoiceToPage(string choiceText, int choiceIndexToEnd)
        {
            var choice = new ChoiceUserControl(choiceText, choiceIndexToEnd);
            choice.SwitchPageRequested += SwitchPage;
            ChoiceList.Children.Add(choice);
        }
        
        public void ShowEndOfStory()
        {
            EndOfStory.IsVisible = true;
        }
        
        private void SwitchPage(object? sender, PageEventArg arg)
        {
            SwitchPageRequested?.Invoke(this, arg);
        }
        
        private void RestartReading(object? sender, RoutedEventArgs e)
        {
            RestartRequested?.Invoke(this, EventArgs.Empty);
        }

        private void BackToHome(object? sender, RoutedEventArgs e)
        {
            QuitRequested?.Invoke(this, EventArgs.Empty);
        }
        
        public event EventHandler<PageEventArg> SwitchPageRequested;
        public event EventHandler<EventArgs> RestartRequested;
        public event EventHandler<EventArgs> QuitRequested;
    }
}