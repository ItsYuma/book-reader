using GBReaderStefkoS.Domains;

namespace GBReaderStefkoS.Infrastructures.Dto;

public class Mapper
{
    public IList<ReadingSessionDto> ListEntityToListDto(IList<ReadingSession> sessions)
    {
        return sessions.Select(s => new ReadingSessionDto(s.BookTitle, s.BookIsbn, s.PageIndex, s.DateBeginning, s.DateLastReading, s.PagesReaded)).ToList();
    }
    
    public IList<ReadingSession> ListDtoToListEntity(IList<ReadingSessionDto> sessions)
    {
        return sessions.Select(s => new ReadingSession(s.BookTitle, s.BookIsbn, s.PageIndex, s.DateBeginning, s.DateLastReading, s.PagesReaded))
            .Where(s => s.BookIsbn != null && s.BookTitle != null && s.DateBeginning != null && s.DateLastReading != null && s.PageIndex > 0).ToList();
    }
}