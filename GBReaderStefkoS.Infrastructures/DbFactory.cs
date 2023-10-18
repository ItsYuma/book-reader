using System.Data;
using System.Data.Common;
using GBReaderStefkoS.Repositories;
using GBReaderStefkoS.Repositories.Exceptions;
using MySql.Data.MySqlClient;

namespace GBReaderStefkoS.Infrastructures
{
    public class DbFactory
    {
        private const string ProviderName = "MySql.Data.MySqlClient";
        private const string DbConnectionString = "ConnexionString";
        
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