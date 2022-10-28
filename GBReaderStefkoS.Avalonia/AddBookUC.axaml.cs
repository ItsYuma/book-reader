using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Presenter;

namespace GBReaderStefkoS.Avalonia;

public partial class AddBookUC : UserControl
{

    private MainPresenter _presenter;
    
    public AddBookUC()
    {
        InitializeComponent();
    }
    
    public AddBookUC(string author, string title, string isbn, string resume, MainPresenter p) : this()
    {
        this._presenter = p;
        
        Author.Text = author;
        Title.Text = "Titre : " + title;
        Isbn.Text = "Isbn : " + isbn;
        Resume.Text = "Résumé : " + resume;
    }
    
    /*private void CreateBook(string author, string title, string isbn, string resume)
    {
        Author.Text = "Auteur : " + author;
        Title.Text = "Titre : " + title;
        Isbn.Text = "Isbn : " + isbn;
        Resume.Text = "Résumé : " + resume;
    }*/
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        _presenter.ShowDataBook(Author.Text, Title.Text, Isbn.Text, Resume.Text);
    }
}