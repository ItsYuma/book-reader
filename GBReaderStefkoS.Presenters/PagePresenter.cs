using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Presenters.Events;
using GBReaderStefkoS.Presenters.Routes;
using GBReaderStefkoS.Presenters.Views;
using GBReaderStefkoS.Repositories;

namespace GBReaderStefkoS.Presenters
{
    public class PagePresenter
    {
        private readonly IPageView _view;
        private readonly ISwitchContent _router;
        private readonly IDbFactory _factory;
        private Book? _actualBook;
        
        public PagePresenter(IPageView view, ISwitchContent router, IDbFactory factory, AllBooksPresenter allBooksPresenter)
        {
            _view = view;
            _router = router;
            _factory = factory;

            allBooksPresenter.StartReadingBook += StartReadingBook;
            _view.SwitchPageRequested += SwitchPage;
            _view.RestartRequested += GoToFirstPage;
            _view.QuitRequested += GoToAllBooksView;
        }
        
        private void StartReadingBook(object? sender, BookEventArg arg)
        {
            _actualBook = arg.Book;
            
            GoToFirstPage();
        }
        
        private void GoToFirstPage()
        {
            //_actualBook = arg.Book;
            var page = _actualBook.Pages[0];
            
            SetDataToPage(page);
        }

        private void GoToFirstPage(object? sender, EventArgs arg) => GoToFirstPage();

        private void SwitchPage(object? sender, PageEventArg arg)
        {
            var page = _actualBook.Pages[arg.Index - 1];
            SetDataToPage(page);
        }
        
        private void SetDataToPage(Page page)
        {
            _view.SetData(_actualBook.Title, _actualBook.Pages.Count, page.Index ,page.Text);

            if (page.Choices.Count > 0)
            {
                foreach (var choice in page.Choices)
                {
                    _view.AddChoiceToPage(choice.Text, choice.IndexPageToEnd);
                }
            }
            else
            {
                _view.ShowEndOfStory();
            }
        }

        private void GoToAllBooksView(object? sender, EventArgs arg) => _router.Goto("allBooks");
    }
}