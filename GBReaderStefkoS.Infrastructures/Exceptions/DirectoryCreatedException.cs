namespace GBReaderStefkoS.Infrastructures.Exceptions
{
    public class DirectoryCreatedException : DirectoryNotFoundException
    {
        public DirectoryCreatedException()
            : base("Le Repertoire UE36 n'exisant pas dans le dossier User, celui-ci a été créé et le fichier json y a été ajouté.")
        { }

        public DirectoryCreatedException(string? message) : base(message)
        { }
    }
}