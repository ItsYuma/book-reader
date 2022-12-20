using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Presenters.Views
{
    public interface IStatsView
    {
        void SetData(int nbSessions);
        
        void AddReadingSessionToView(string bookTitle, string bookIsbn, int pageIndex, string dateBeginning, string dateLastReading);
        
        public event EventHandler<EventArgs> QuitRequested;
        
        //void ShowError(string message);
    }
}