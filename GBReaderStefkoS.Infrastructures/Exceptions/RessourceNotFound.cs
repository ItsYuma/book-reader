namespace GBReaderStefkoS.Infrastructures.Exceptions
{
    public class RessourceNotFound : FileNotFoundException, DirectoryNotFoundException
    {
        public (string message) : base(message)
        {
            
        }
    }
}