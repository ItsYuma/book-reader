using System.Collections;
using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Repositories
{
    //interface for the session repository
    public interface ISessionRepository
    {
        /**
         * return all the sessions published in the database
         */
        IList<ReadingSession> LoadSessions();

        /**
         * save or update the session
         */
        void SaveOrUpdateSession(string bookTitle, string bookIsbn, int pageIndex, string dateTime, IList<int> pagesReaded);
        
        /**
         * return the last page read
         */
        int GetLastPageRead(string bookIsbn);
        
        /**
         * return the pages readed
         */
        IList<int> GetPagesReaded(string bookIsbn);
        
        /**
         * remove the session for this book
         */
        void RemoveSession(string bookIsbn);
        
    }
}