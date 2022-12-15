using System.Reflection.Metadata.Ecma335;

namespace GBReaderStefkoS.Domains;

public record Book (Author Author, string Title, string Resume, string Isbn)
{

    public IList<Page> Pages { get; set; } = new List<Page>();
    
    /*private Author _author;
    private string _titre;
    private string _resume;
    private string _isbn;

    public Book(Author author, string titre, string resume, string isbn)
    {
        this._author = author;
        this._titre = titre;
        this._resume = resume;
        this._isbn = isbn;
    }*/

    /*public Author Author
    {
        get { return _author; }
        set { _author = value ?? throw  new NullReferenceException("Autheur ne doit pas être null"); }
    }
    
    public string Titre
    {
        get { return _titre; }
        set { _titre = value ?? throw new NullReferenceException("Titre ne doit pas être null");  }
    }
    
    public string Resume
    {
        get { return _resume; }
        set { _resume = value ?? throw new NullReferenceException("Resume ne doit pas être null"); }
    }
    
    public string Isbn
    {
        get { return _isbn; }
        set { _isbn = value ?? throw new NullReferenceException("Isbn ne doit pas être null"); }
    }*/

    /*public Boolean BookValid()
    {
        if (DataNotNull()) return IsbnIsValid();
        else return false;
    }

    private Boolean DataNotNull()
    {
        return _author != null && !(string.IsNullOrEmpty(_titre) &&
               string.IsNullOrEmpty(_resume) && string.IsNullOrEmpty(_isbn));
    }
    
    private Boolean IsbnIsValid()
    {
        if(_isbn[1] != '-' && _isbn[8] != '-') return false;
        
        string newIsbn =_isbn.Replace("-", String.Empty );
        if (!CodeVerifValide(newIsbn[9])) return false;
        
        newIsbn = newIsbn.Substring(0, 8);
        Int64 nb;
        return Int64.TryParse(newIsbn,out nb);
    }

    private Boolean CodeVerifValide(char ch)
    {
        if(ch == 'x') return true;
        int nbr = (int)Char.GetNumericValue(ch);
        return ch >= 0 && nbr < 10;
    }*/

}