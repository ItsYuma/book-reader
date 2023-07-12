using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Infrastructures.Dto;

public class Mapper
{
    /*public List<Book> DTOtoEntity(List<BookDTO> booksDTO)
    {
        List<Book> books = new List<Book>();
        //booksDTO.ForEach(b => books.Add(new Book(b.Author, b.Titre, b.Resume, b.Isbn)));
        return books;
    }*/
    
    //public ReadingSessionDto CreateReadingSessionDto (string title, string isbn, int index, string actualDateTime) => new(title, isbn, index, actualDateTime, actualDateTime);
    
    /*public ReadingSession DtoToEntity(ReadingSessionDto sessions)
    {
        return new ReadingSession(sessions.BookTitle, sessions.BookIsbn, sessions.PageIndex, sessions.DateBeginning, sessions.DateLastReading);
        //return sessions.Select(s => new ReadingSession(s.BookIsbn, s.PageIndex, s.DateBeginning, s.DateLastReading));
    }*/

    public IList<ReadingSessionDto> ListEntityToListDto(IList<ReadingSession> sessions)
    {
        return sessions.Select(s => new ReadingSessionDto(s.BookTitle, s.BookIsbn, s.PageIndex, s.DateBeginning, s.DateLastReading, s.PagesReaded)).ToList();
    }
    
    public IList<ReadingSession> ListDtoToListEntity(IList<ReadingSessionDto> sessions)
    {
        /*var filteredList = sessions
            .Select(
                s => new ReadingSession(s.BookTitle, s.BookIsbn, s.PageIndex, s.DateBeginning, s.DateLastReading)
            .Where(x => x.Elements.Count > 0);*/
        //genrere une methode qui transofrme ma liste de session en liste de sessionDto
        return sessions.Select(s => new ReadingSession(s.BookTitle, s.BookIsbn, s.PageIndex, s.DateBeginning, s.DateLastReading, s.PagesReaded))
            .Where(s => s.BookIsbn != null && s.BookTitle != null && s.DateBeginning != null && s.DateLastReading != null && s.PageIndex > 0).ToList();
        //return sessions.Select(s => new ReadingSessionDto(s.BookIsbn, s.PageIndex, s.DateBeginning, s.DateLastReading));
    }
}