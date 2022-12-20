namespace GBReaderStefkoS.Repositories.Exceptions
{
    public class StorageException : Exception
    {
        public StorageException()
            : base("Une erreur est survenue lors de la connexion")
        { }
        
        public StorageException(string? message)
            : base(message)
        { }
    }
}