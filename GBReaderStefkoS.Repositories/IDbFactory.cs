using System.Data;

namespace GBReaderStefkoS.Repositories
{
    public interface IDbFactory
    {
        IDbConnection GetConnection();
    }
}