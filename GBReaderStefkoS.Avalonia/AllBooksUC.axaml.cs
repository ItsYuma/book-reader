using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Presenters;

namespace GBReaderStefkoS.Avalonia;

public partial class AllBooksUC : UserControl, IAllBooks
{

    private MainPresenter _presenter;
    
    public AllBooksUC()
    {
        InitializeComponent();
        //SetItems();
        //AddBook();
    }
    
    public AllBooksUC(MainPresenter presenter) : this()
    {
        //InitializeComponent();
        SetPresenter(presenter);
        SetItems();
        //AddBook();
    }

    public void SetPresenter(MainPresenter p)
    {
        this._presenter = p;
        _presenter.AllBooks = this;
    }

    private void SetItems()
    {
        _presenter.LoadBooks();
    }

    public void AddBook(string auteur, string titre, string isbn, string resume)
    {
        var item = new AddBookUC(auteur, titre, isbn, resume, _presenter);
        AllBooks.Children.Add(item);
    }

    /*public void CreateBook(string auteur, string titre, string isbn, string resume)
    { 
        var uc = new AllBookUC(auteur, titre, isbn, resume);
        AllBooks.Children.Add(uc);
    }*/


    /*private MainPresenter _presenter;
    //private IWindow _mainWindow;
    
    public AllBookUC()
    {
        InitializeComponent();
    }

    public AllBookUC(string author, string titre, string isbn, string resume, MainPresenter p, IWindow w) : this()
    {
        this._presenter = p;
        //this._mainWindow = w;
        SetItems(author, titre, isbn, resume);
    }

    private void SetItems(string author, string title, string isbn, string resume)
    {
        Author.Text = "Auteur : " + author;
        Title.Text = "Titre : " + title;
        Isbn.Text = "Isbn : " + isbn;
        Resume.Text = "Résumé : " + resume;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        //_mainWindow.ChangeView();
        _presenter.ShowDataBook(Author.Text, Title.Text, Isbn.Text, Resume.Text);
        //Resume.IsVisible = true;
    }*/
    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
    { 
        if (e.Key == Key.Return)
        {
            AllBooks.Children.Clear();
            string s = Search.Text.ToLower();
            _presenter.ShowSearchBook(s);
        }
    }

    public void NoBook()
    {
        var item = new TextBlock();
        item.Text = "Aucun résultat";
        item.HorizontalAlignment = HorizontalAlignment.Center;
        AllBooks.Children.Add(item);
    }
}