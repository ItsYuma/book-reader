namespace GBReaderStefkoS.Domains
{
    public record Page(int Index, string Text)
    {
        public IList<Choice> Choices { get; set; } = new List<Choice>();
    }
}