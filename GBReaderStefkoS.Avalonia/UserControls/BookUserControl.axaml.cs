using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Presenters.Events;

namespace GBReaderStefkoS.Avalonia.UserControls
{
    public partial class BookUserControl : UserControl
    {
        
        public BookUserControl()
        {
            InitializeComponent();
        }
        
        public BookUserControl(string author, string title, string resume, string isbn) : this()
        {
            SetData(author, title, resume, isbn);
        }

        private void SetData(string author, string title, string resume, string isbn)
        {
            Author.Text = "Auteur : " +author;
            Title.Text = "Titre : " + title;
            Resume.Text = "Résumé : " + resume;
            Isbn.Text = "Isbn : " + isbn;
        }

        public void ShowDetails(object? sender, RoutedEventArgs e)
        {
            var isbn = Isbn.Text.Split(" : ")[1];
            DetailRequested?.Invoke(this, new DetailsEventArgs(Title.Text, Resume.Text, isbn));
        }
        
        public event EventHandler<DetailsEventArgs> DetailRequested;
    }
}