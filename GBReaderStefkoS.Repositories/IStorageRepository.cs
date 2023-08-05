using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Repositories
{
    /**
     * Interface for the repository
     */
    public interface IStorageRepository
    {
        /**
         * return all the books published in the database
         */
        IEnumerable<Book>? GetBooks();
        
        /**
         * return all the pages of the book
         */
        IList<Page> GetPagesFromBook(Book? bookSelected);
        
        /**
         * get the choice for the page
         */
        IList<Choice> SetChoicesToPage(Book? book, Page page);
    }
}