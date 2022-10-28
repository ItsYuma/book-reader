using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Infrastructures;

public class BookDTO
{
    /*private Author _author;
    private string _titre;
    private string _resume;
    private string _isbn;*/
    //private Book _book;

    /*public BookAuthorDTO(Book book)
    {
        _author = author;
        _titre = titre;
        _resume = resume;
        _isbn = isbn;
        this._book = book;
    }*/

    public Author Author
    {
        get;
        set;
    }

    public string Titre
    {
        get;
        set;
    }

    public string Resume
    {
        get;
        set;
    }

    public string Isbn
    {
        get;
        set;
    }
    
}