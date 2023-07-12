namespace GBReaderStefkoS.Domains
{
    public class ReadingSession
    {
        public string BookTitle { get; set; }
        
        public string BookIsbn { get; set; }
        
        public int PageIndex { get; set; }
        
        public string DateBeginning { get; set; }
        
        public string DateLastReading { get; set; }
        
        public IList<int> PagesReaded { get; set; }
        
        public ReadingSession(string bookTitle, string bookIsbn, int pageIndex, string dateBeginning, string dateLastReading, IList<int> pagesReaded)
        {
            BookTitle = bookTitle;
            BookIsbn = bookIsbn;
            PageIndex = pageIndex;
            DateBeginning = dateBeginning;
            DateLastReading = dateLastReading;
            PagesReaded = pagesReaded;
        }
    }
}