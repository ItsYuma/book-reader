using Avalonia.Controls;
using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Infrastructures;
using GBReaderStefkoS.Repositories;


namespace GBReaderStefkoS.Presenter;

public class MainPresenter
{
    private IWindow _window;
    //private IAllBooks _allBooks;
    /*private Author _author;
    private Book _book;*/
    private IAllBooks _allBooks;
    private IRepository _bookRepository;
    private List<Book> _books;

    public MainPresenter(IWindow window){
        this._window = window;
        this._bookRepository = new BookRepository();
        _books = _bookRepository.Load();
        //this._allBooks = addBooks;
    }

    public void ShowAllBooks()
    {
        _window.ShowAllBooks();
    }

    public void LoadBooks()
    {
        //List<Dictionary<string, string>> booksString = new List<Dictionary<string, string>>();
        //var item = new AllBooksUC();
        foreach (var b in _books)
        {
            if(b.BookValid()) _allBooks.AddBook(b.Author.ToString(), b.Titre, b.Isbn, b.Resume);
            //if(b.BookValid()) _allBooks.
        }
        //return booksString;
        //_window.UpdateActualPanel();
    }

    public void ClearWindow()
    {
        _window.ClearWindow();
    }

    public IAllBooks AllBooks
    {
        get { return _allBooks ;}
        set { _allBooks = value ;}
    }

    public void ShowDataBook(string author, string title, string isbn, string resume)
    {
        _window.ShowDataBook(author, title, isbn, resume);
    }

    public void ShowSearchBook(string search)
    {
        IEnumerable<Book> searchBooks =
            from book in _books
            where book.Titre.ToLower().Contains(search) || book.Isbn.ToLower().Contains(search)
            select book;
        if(searchBooks.Count() == 0) _allBooks.NoBook();
        else
        {
            foreach (var b in searchBooks)
            {
                if(b.BookValid()) _allBooks.AddBook(b.Author.ToString(), b.Titre, b.Isbn, b.Resume);
            }
        }
    }
}