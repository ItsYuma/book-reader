using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Presenter;

namespace GBReaderStefkoS.Avalonia;

public partial class BookDataUC : UserControl
{
    private MainPresenter _presenter;
    public BookDataUC()
    {
        InitializeComponent();
    }
    
    public BookDataUC(MainPresenter p, string author, string title, string isbn, string resume) : this()
    {
        //InitializeComponent();
        _presenter = p;
        SetItems(author, title, isbn, resume);
    }
    
    private void SetItems(string author, string title, string isbn, string resume)
    {
        Author.Text = author;
        Title.Text = title;
        Isbn.Text = isbn;
        Resume.Text = resume;
    }

    /*private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }*/
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        _presenter.ClearWindow();
        _presenter.ShowAllBooks();
    }
}