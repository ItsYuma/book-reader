namespace GBReaderStefkoS.Infrastructures.Dto
{
    public record ReadingSessionDto(string BookTitle, string BookIsbn, int PageIndex, string DateBeginning, string DateLastReading, IList<int> PagesReaded);
}