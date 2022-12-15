using System.Data;

namespace GBReaderStefkoS.Repositories
{
    /**
     * Interface for the DbFactory
     */
    public interface IDbFactory
    {
        /**
         * return a new instance of IDbConnection
         */
        IDbConnection GetConnection();
    }
}