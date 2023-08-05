using GBReaderStefkoS.Presenters.Routes;
using GBReaderStefkoS.Presenters.Views;
using GBReaderStefkoS.Repositories;

namespace GBReaderStefkoS.Presenters
{
    /**
     * Presenter for the stats view
     */
    public class StatsPresenter
    {
        private readonly IStatsView _view;
        private readonly ISwitchContent _router;
        private readonly ISessionRepository _sessionRepository;
        
        /**
         * Constructor
         */
        public StatsPresenter(IStatsView view, ISwitchContent router, ISessionRepository sessionRepository, AllBooksPresenter allBooksPresenter)
        {
            _view = view;
            _router = router;
            _sessionRepository = sessionRepository;
            
            _view.QuitRequested += GoToAllBooksView;
            allBooksPresenter.GoToStats += SetDataToView;
        }

        private void GoToAllBooksView(object? sender, EventArgs e)
        {
            _router.Goto("allBooks");
        }

        private void SetDataToView()
        {
            var listSession = _sessionRepository.LoadSessions();
            _view.SetData(listSession.Count);
            foreach (var session in listSession)
            { 
                _view.AddReadingSessionToView(session.BookTitle, session.BookIsbn, session.PageIndex, session.DateBeginning, session.DateLastReading);
            }
        }
        
        private void SetDataToView(object? sender, EventArgs e) => SetDataToView();
    }
}