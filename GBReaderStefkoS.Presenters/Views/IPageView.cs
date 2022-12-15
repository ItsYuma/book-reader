using GBReaderStefkoS.Presenters.Events;

namespace GBReaderStefkoS.Presenters.Views
{
    public interface IPageView
    {
        void SetData(string bookTitle, int nbPages, int pageIndex, string pageText);
        
        void AddChoiceToPage(string choiceText, int choiceIndexToEnd);
        
        void ShowEndOfStory();
        
        public event EventHandler<PageEventArg> SwitchPageRequested;
        public event EventHandler<EventArgs> RestartRequested;
        public event EventHandler<EventArgs> QuitRequested;
        
    }
}