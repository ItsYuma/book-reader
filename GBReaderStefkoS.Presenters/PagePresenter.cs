using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
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
        private readonly ISessionRepository _sessionRepository;
        private Book? _actualBook;
        private Stack<int> _pagesReaded = new Stack<int>();

        public PagePresenter(IPageView view, ISwitchContent router, ISessionRepository sessionRepository, AllBooksPresenter allBooksPresenter)
        {
            _view = view;
            _router = router;
            _sessionRepository = sessionRepository;
            //_storageRepository = storageRepository;

            allBooksPresenter.StartReadingBook += StartReadingBook;
            _view.GoToPreviousPageRequested += GoToPreviousPage;
            _view.SwitchPageAndSaveRequested += SwitchPage;
            _view.RestartRequested += GoToFirstPage;
            _view.QuitRequested += GoToAllBooksView;
             
        }
        
        private void StartReadingBook(object? sender, BookEventArg arg)
        {
            _actualBook = arg.Book;
            
            var pageIndex = _sessionRepository.GetLastPageRead(_actualBook.Isbn);
            
            var tempList = _sessionRepository.GetPagesReaded(_actualBook.Isbn);
            var test = tempList.Reverse();
            
            foreach (int element in test)
            {
                _pagesReaded.Push(element);
            }

            if (pageIndex == 1)
            {
                _pagesReaded.Push(pageIndex);
            }
            
            GoToPage(pageIndex);
        }
        
        private void GoToPage(int index)
        {
            var page = _actualBook.Pages[index - 1];
            
            SetDataToPage(page);
        }

        private void GoToFirstPage(object? sender, EventArgs arg)
        {
            _pagesReaded = new Stack<int>();
            _pagesReaded.Push(1);
            GoToPage(1);  
        } 

        private void SwitchPage(object? sender, SaveReadingEventArgs args)
        {
            var page = _actualBook.Pages[args.PageIndex - 1];
            _pagesReaded.Push(args.PageIndex);

            if (args.PageIndex == 1 || !_actualBook.PageHaveChoice(args.PageIndex))
            {
                RemoveSession();
                if(args.PageIndex == 1) _pagesReaded = new Stack<int>();
            }
            else
            {
                SaveSession(args.PageIndex, args.DateTime);
            }
            
            SetDataToPage(page);
        }
        
        private void GoToPreviousPage(object? sender, PreviousPageEventArg args)
        {
            
            _pagesReaded.Pop();
            var pageIndex = _pagesReaded.Peek();

            if (pageIndex == 1 || !_actualBook.PageHaveChoice(pageIndex))
            {
                RemoveSession();
            }
            else
            {
                SaveSession(pageIndex, args.DateTime);
            }
            
            GoToPage(pageIndex);
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
            _pagesReaded = new Stack<int>();
            _router.Goto("allBooks");  
        }

        private void SaveSession(int pageIndex, string dateTime)
        {
            _sessionRepository.SaveOrUpdateSession(_actualBook.Title, _actualBook.Isbn, pageIndex, dateTime, _pagesReaded.ToList());
        }

        private void RemoveSession()
        {
            _sessionRepository.RemoveSession(_actualBook.Isbn);
        }
    }
}