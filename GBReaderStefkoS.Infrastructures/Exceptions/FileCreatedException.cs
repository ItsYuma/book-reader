namespace GBReaderStefkoS.Infrastructures.Exceptions
{
    public class FileCreatedException : FileNotFoundException
    {
        public FileCreatedException()
            : base("Le fichier Json n'existant pas, celui ci a été créé dans votre dossier UE36 se trouvant dans votre répertoire User")
        { }

        public FileCreatedException(string? message) : base(message)
        { }
    }
}