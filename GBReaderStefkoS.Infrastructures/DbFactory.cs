using System.Data;
using System.Data.Common;
using GBReaderStefkoS.Repositories;
using MySql.Data.MySqlClient;

namespace GBReaderStefkoS.Infrastructures
{
    public class DbFactory : IDbFactory
    {
        private readonly DbProviderFactory _factory;

        private readonly string _dbConnectionString;
        //private const string DbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\utilisateur\OneDrive - student.helmo.be\CursusInfo\Bloc2\StructuresDeDonnees\22-23\ExerciceTheorie\C#\AdoDotNet\Helmo.Sd.Theory\Helmo.Sd.Theory.Ado.Tests\DBTests.mdf"";Integrated Security=True";
        
        public DbFactory(string providerName, string dbConnectionString)
        {
            try
            {
                DbProviderFactories.RegisterFactory(providerName, MySqlClientFactory.Instance);
                _factory = DbProviderFactories.GetFactory(providerName);
                _dbConnectionString = dbConnectionString;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public IDbConnection GetConnection()
        {
            try
            {
                IDbConnection connection = _factory.CreateConnection();
                connection.ConnectionString = _dbConnectionString;
                connection.Open();
                return connection;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}