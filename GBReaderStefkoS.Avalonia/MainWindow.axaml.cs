using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderStefkoS.Presenter;

namespace GBReaderStefkoS.Avalonia
{
    public partial class MainWindow : Window, IWindow
    {
        private MainPresenter _presenter;
        //private UserControl _actualView;
        /*files = new List<string>();
        FileList.ItemsSource = files;*/

        public MainWindow()
        {
            InitializeComponent();
            SetPresenter(new MainPresenter(this));
            ShowAllBooks();
        }

        public void SetPresenter(MainPresenter presenter)
        {
            this._presenter = presenter;
        }

        /*public void LoadBooks()
        {
            throw new NotImplementedException();
        }*/

        public void ShowAllBooks()
        {
            //_presenter.LoadBooks();
            ActualPanel.Children.Add(new AllBooksUC(_presenter));
        }

        /*public void UpdateActualPanel(UserControl uc)
        {
            ClearWindow();
            ActualPanel.Children.Add(uc);
        }*/

        /*private void Start_OnClick(object? sender, RoutedEventArgs arg)
        {
            _presenter.LoadBooks();
        }*/

        /*public void CreateBook(string auteur, string titre, string isbn, string resume)
        {
            /*if (MainPanel.Children.Contains(Search))
            {
                TextBox search = new TextBox();
                search.Watermark = "Rechercher";
                search.Name = "Search";
            }*/
            
            //MainPanel.Children.Add(search);
            /*var uc = new AllBookUC(auteur, titre, isbn, resume, _presenter, this);
            //_actualView = item;
            AllBooks.Children.Add(uc);
        }*/

        public void ClearWindow()
        {
            ActualPanel.Children.Clear();
        }

        public void ShowDataBook(string author, string title, string isbn, string resume)
        {
            ClearWindow();
            var item = new BookDataUC(_presenter, author, title, isbn, resume);
            ActualPanel.Children.Add(item);
        }
    }
}