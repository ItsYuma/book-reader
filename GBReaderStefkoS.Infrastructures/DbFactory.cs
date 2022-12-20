using System.Data;
using System.Data.Common;
using GBReaderStefkoS.Repositories;
using GBReaderStefkoS.Repositories.Exceptions;
using MySql.Data.MySqlClient;

namespace GBReaderStefkoS.Infrastructures
{
    public class DbFactory : IDbFactory
    {
        private const string ProviderName = "MySql.Data.MySqlClient";
        private const string DbConnectionString = "Server=192.168.132.200;" + "Port=13306;" + "Database=Q210020;" + "uid=Q210020;" + "pwd=0020;";
        
        private readonly DbProviderFactory _factory;
        //private const string DbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\utilisateur\OneDrive - student.helmo.be\CursusInfo\Bloc2\StructuresDeDonnees\22-23\ExerciceTheorie\C#\AdoDotNet\Helmo.Sd.Theory\Helmo.Sd.Theory.Ado.Tests\DBTests.mdf"";Integrated Security=True";
        
        public DbFactory()
        {
            try
            {
                DbProviderFactories.RegisterFactory(ProviderName, MySqlClientFactory.Instance);
                _factory = DbProviderFactories.GetFactory(ProviderName);
            }
            catch (Exception)
            {
                throw new StorageException();
            }
        }
        
        public IDbConnection GetConnection()
        {
            try
            {
                IDbConnection connection = _factory.CreateConnection();
                connection.ConnectionString = DbConnectionString;
                connection.Open();
                return connection;
            } catch (Exception)
            {
                throw new StorageException();
            }
        }
    }
}