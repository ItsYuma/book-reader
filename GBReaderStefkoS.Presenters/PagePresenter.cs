using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Infrastructures;
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
        private readonly ISessionRepository _sessionRepository;
        private Book? _actualBook;
        
        public PagePresenter(IPageView view, ISwitchContent router, ISessionRepository sessionRepository, AllBooksPresenter allBooksPresenter)
        {
            _view = view;
            _router = router;
            _sessionRepository = sessionRepository;
            //_storageRepository = storageRepository;

            allBooksPresenter.StartReadingBook += StartReadingBook;
            _view.SwitchPageAndSaveRequested += SwitchPage;
            _view.RestartRequested += GoToFirstPage;
            _view.QuitRequested += GoToAllBooksView;
             
        }
        
        private void StartReadingBook(object? sender, BookEventArg arg)
        {
            _actualBook = arg.Book;
            
            var pageIndex = _sessionRepository.GetLastPageRead(_actualBook.Isbn);
            GoToPage(pageIndex);
        }
        
        private void GoToPage(int index)
        {
            //_actualBook = arg.Book;
            var page = _actualBook.Pages[index - 1];
            
            SetDataToPage(page);
        }

        private void GoToFirstPage(object? sender, EventArgs arg) => GoToPage(1);

        private void SwitchPage(object? sender, SaveReadingEventArgs args)
        {
            var page = _actualBook.Pages[args.PageIndex - 1];
            if (args.PageIndex == 1 || !_actualBook.PageHaveChoice(args.PageIndex))
            {
                RemoveSession();
            }
            else
            {
                SaveSession(args.PageIndex, args.DateTime);
            }
            
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

        private void GoToAllBooksView(object? sender, EventArgs args)
        {
            _router.Goto("allBooks");  
        }

        private void SaveSession(int pageIndex, string dateTime)
        {
            _sessionRepository.SaveOrUpdateSession(_actualBook.Title, _actualBook.Isbn, pageIndex, dateTime);
        }

        private void RemoveSession()
        {
            _sessionRepository.RemoveSession(_actualBook.Isbn);
        }
    }
}