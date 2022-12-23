using System.Data.Common;
using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Infrastructures;
using GBReaderStefkoS.Presenters.Events;
using GBReaderStefkoS.Presenters.Routes;
using GBReaderStefkoS.Presenters.Views;
using GBReaderStefkoS.Repositories;
using GBReaderStefkoS.Repositories.Exceptions;

namespace GBReaderStefkoS.Presenters
{
    public class AllBooksPresenter
    {
        private readonly IAllBooksView _view;
        private readonly ISwitchContent _router;
        private readonly IStorageRepository _storageRepository;
        private IEnumerable<Book>? _allBooks;

        public AllBooksPresenter(IAllBooksView view, ISwitchContent router, IStorageRepository storageRepository)
        {
            _view = view;
            _router = router;
            _storageRepository = storageRepository;
            
            SetBooksToView();
            
            _view.SearchRequested += ShowSearchBook;
            _view.ReadingRequested += ShowPagesBook;
            _view.StatsRequested += ShowStats;
        }

        private void SetBooksToView()
        {
            try
            {
                _allBooks = _storageRepository.GetBooks();
                
                //_allBooks = dbManager.GetBooks();
                if (_allBooks == null || _allBooks.Count() == 0)
                {
                    _view.ShowError("Auncun livre n'est publié");
                }

                foreach (var book in _allBooks)
                {
                    _view.addBookToView(book.Author.ToString(), book.Title, book.Resume, book.Isbn);
                }
                
                /*using (IDbManager dbManager = new DbManager(_factory.GetConnection()))
                {
                    
                }*/
            }
            catch (StorageException e)
            {
                _view.ShowError(e.Message);
            }
        }
        
        private void ShowSearchBook(object? sender, SearchEventArg args)
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
        
        private void ShowPagesBook(object? sender, ReadingEventArg arg)
        {
            var bookSelected =
                (from book in _allBooks
                where book.Isbn.ToLower().Contains(arg.Isbn)
                select book).First();
            
            try
            {
                bookSelected.Pages = _storageRepository.GetPagesFromBook(bookSelected);
                foreach (var page in bookSelected.Pages)
                {
                    page.Choices = _storageRepository.SetChoicesToPage(bookSelected, page);
                }
                StartReadingBook?.Invoke(this, new BookEventArg(bookSelected));
                _router.Goto("pageView");
            }
            catch (StorageException e)
            {
                _view.ShowError(e.Message);
            }

        }
        
        private void ShowStats(object? sender, EventArgs args)
        {
            GoToStats?.Invoke(this, new EventArgs());
            _router.Goto("statsView");
        }
        
        public event EventHandler<BookEventArg>? StartReadingBook;
        public event EventHandler <EventArgs> GoToStats;
    }
}