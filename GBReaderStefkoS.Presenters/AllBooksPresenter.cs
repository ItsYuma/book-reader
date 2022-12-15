using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Infrastructures;
using GBReaderStefkoS.Presenters.Events;
using GBReaderStefkoS.Presenters.Routes;
using GBReaderStefkoS.Presenters.Views;
using GBReaderStefkoS.Repositories;

namespace GBReaderStefkoS.Presenters
{
    public class AllBooksPresenter
    {
        private readonly IAllBooksView _view;
        private readonly ISwitchContent _router;
        private readonly IDbFactory _factory;
        private IEnumerable<Book>? _allBooks;

        public AllBooksPresenter(IAllBooksView view, ISwitchContent router, IDbFactory factory)
        {
            _view = view;
            _router = router;
            _factory = factory;
            
            SetBooksToView();
            
            _view.SearchRequested += ShowSearchBook;
            _view.ReadingRequested += ShowPagesBook;
        }

        private void SetBooksToView()
        {
            try
            {
                using (IDbManager dbManager = new DbManager(_factory.GetConnection()))
                {
                    _allBooks = dbManager.GetBooks();
                    if (_allBooks == null || _allBooks.Count() == 0)
                    {
                        _view.ShowError("Auncun livre n'est publié");
                    }
                    foreach (var book in _allBooks)
                    {
                        _view.addBookToView(book.Author.ToString(), book.Title, book.Resume, book.Isbn);
                    }
                }
            } catch (Exception e)
            {
                _view.ShowError("Une erreur est survenue lors de la récupération des livres (problème de connexion)");
            }
        }
        
        private void ShowSearchBook(object? sender, SearchEventArg args)
        {
            if (_allBooks != null && _allBooks.Count() > 0)
            {
                IEnumerable<Book> searchBooks =
                    from book in _allBooks
                    where book.Title.ToLower().Contains(args.StringToSearch) || book.Isbn.ToLower().Contains(args.StringToSearch)
                    select book;
                if (searchBooks.Count() == 0)
                {
                    _view.ShowError("Aucun livre trouvé");
                }
                else
                {
                    foreach (var book in searchBooks)
                    {
                        _view.addBookToView(book.Author.ToString(), book.Title, book.Resume, book.Isbn);
                    }
                }
            }
        }
        
        private void ShowPagesBook(object? sender, ReadingEventArg arg)
        {
            var bookSelected =
                (from book in _allBooks
                where book.Isbn.ToLower().Contains(arg.Isbn)
                select book).First();
            
            try
            {
                using (IDbManager dbManager = new DbManager(_factory.GetConnection()))
                {
                    bookSelected.Pages = dbManager.GetPagesFromBook(bookSelected);
                    foreach (var page in bookSelected.Pages)
                    {
                        page.Choices = dbManager.SetChoicesToPage(bookSelected, page);
                    }
                    StartReadingBook?.Invoke(this, new BookEventArg(bookSelected));
                    _router.Goto("pageView");
                }
            } catch (Exception e)
            {
                _view.ShowError("Impossible de récupérer les pages du livre (problème de connexion)");
            }
            
        }
        
        public event EventHandler<BookEventArg>? StartReadingBook;
    }
}