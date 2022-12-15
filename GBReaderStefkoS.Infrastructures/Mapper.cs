using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Infrastructures;

public class Mapper
{
    public List<Book> DTOtoEntity(List<BookDTO> booksDTO)
    {
        List<Book> books = new List<Book>();
        //booksDTO.ForEach(b => books.Add(new Book(b.Author, b.Titre, b.Resume, b.Isbn)));
        return books;
    }
}