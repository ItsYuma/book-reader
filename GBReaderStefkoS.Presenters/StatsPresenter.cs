using GBReaderStefkoS.Infrastructures;
using GBReaderStefkoS.Presenters.Routes;
using GBReaderStefkoS.Presenters.Views;
using GBReaderStefkoS.Repositories;

namespace GBReaderStefkoS.Presenters
{
    public class StatsPresenter
    {
        private readonly IStatsView _view;
        private readonly ISwitchContent _router;
        private readonly IDbFactory _factory;
        
        public StatsPresenter(IStatsView view, ISwitchContent router, IDbFactory factory, AllBooksPresenter allBooksPresenter)
        {
            _view = view;
            _router = router;
            _factory = factory;
            
            //SetDataToView();
            
            _view.QuitRequested += GoToAllBooksView;
            allBooksPresenter.GoToStats += SetDataToView;
        }

        private void GoToAllBooksView(object? sender, EventArgs e)
        {
            _router.Goto("allBooks");
        }

        private void SetDataToView()
        {
            IJsonManager jsonManager = new JsonManager();
            var listSession = jsonManager.LoadSessions();
            _view.SetData(listSession.Count);
            foreach (var session in listSession)
            { 
                _view.AddReadingSessionToView(session.BookTitle, session.BookIsbn, session.PageIndex, session.DateBeginning, session.DateLastReading);
            }
        }
        
        private void SetDataToView(object? sender, EventArgs e) => SetDataToView();
    }
}