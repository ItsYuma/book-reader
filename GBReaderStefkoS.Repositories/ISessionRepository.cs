using System.Collections;
using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Repositories
{
    public interface ISessionRepository
    {
        IList<ReadingSession> LoadSessions();

        //void SaveSession(string bookTitle, string bookIsbn, int pageIndex, string dateTime);
        
        void SaveOrUpdateSession(string bookTitle, string bookIsbn, int pageIndex, string dateTime, IList<int> pagesReaded);
        
        int GetLastPageRead(string bookIsbn);
        
        IList<int> GetPagesReaded(string bookIsbn);
        
        //bool SessionExist(string bookIsbn);

        void RemoveSession(string bookIsbn);

        //void SaveSession(IList<ReadingSession> allSessions);
    }
}