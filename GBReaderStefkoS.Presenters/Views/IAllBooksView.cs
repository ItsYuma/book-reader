using GBReaderStefkoS.Presenters.Events;

namespace GBReaderStefkoS.Presenters.Views
{
    public interface IAllBooksView
    {
        void addBookToView(string author, string title, string resume, string isbn);
        
        void ShowDetailsBookSelected(object? sender, DetailsEventArgs args);

        void ShowError(string message);
        
        event EventHandler<SearchEventArg> SearchRequested;
        event EventHandler<ReadingEventArg> ReadingRequested;
        event EventHandler<EventArgs> StatsRequested;
    }
}