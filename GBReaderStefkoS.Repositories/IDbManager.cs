using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Repositories
{
    public interface IDbManager : IDisposable
    {
        IEnumerable<Book> GetBooks();
        
        IList<Page> GetPagesFromBook(Book bookSelected);
        
        void SetChoicesToPage(Book book, Page page);
    }
}