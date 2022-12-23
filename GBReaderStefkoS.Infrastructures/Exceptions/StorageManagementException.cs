using System.Data.Common;

namespace GBReaderStefkoS.Infrastructures.Exceptions
{
    public class StorageManagementException : Exception
    {
        public StorageManagementException()
            : base("Une erreur est survenue")
        { }
    }
}