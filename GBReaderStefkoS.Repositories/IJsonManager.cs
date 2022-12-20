using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Repositories
{
    public interface IJsonManager
    {
        IList<ReadingSession> LoadSessions();

        //void SaveSession(string bookTitle, string bookIsbn, int pageIndex, string dateTime);
        
        void SaveOrUpdateSession(string bookTitle, string bookIsbn, int pageIndex, string dateTime);
        
        int GetLastPageRead(string bookIsbn);
        
        //bool SessionExist(string bookIsbn);

        void RemoveSession(string bookIsbn);

        //void SaveSession(IList<ReadingSession> allSessions);
    }
}