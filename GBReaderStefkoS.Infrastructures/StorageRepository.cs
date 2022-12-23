using System.Data;
using System.Data.Common;
using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Repositories;
using GBReaderStefkoS.Repositories.Exceptions;
using MySql.Data.MySqlClient;

namespace GBReaderStefkoS.Infrastructures
{
    //TODO try
    public class StorageRepository : IStorageRepository
    {
        private readonly DbFactory _factory;
        
        public StorageRepository(DbFactory factory)
        {
            _factory = factory;
        }

        //public void Dispose() => _connection.Dispose();
        
        //crée une methode qui charge les livres de la base de données
        public IEnumerable<Book>? GetBooks()
        {
            
            var books = new List<Book>();
            
            string sql = "SELECT a.name, a.firstName, a.matricule, b.title, b.resume, b.isbn\n" +
                           "FROM book b\n" +
                           "JOIN author a ON a.authorid = b.authorid\n" +
                           "WHERE ispublish = TRUE";
            try
            {
                using (IDbConnection connection = _factory.GetConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var author = new Author(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                                var book = new Book(author, reader.GetString(3), reader.GetString(4), reader.GetString(5));
                        
                                books.Add(book); 
                            }
                        }
                    }
                }
            }
            catch (DbException)
            {
                throw new StorageException("Erreur lors de la récupération des livres");
            }
            return books;
        }
        
        public IList<Page> GetPagesFromBook(Book? book)
        {
            try
            {
                var pages = new List<Page>();
            
                string sql = "SELECT p.pageIndex, p.text\n" +
                             "FROM page p\n" +
                             "JOIN book b ON b.bookid = p.bookid\n" +
                             "WHERE b.isbn = @isbn\n" +
                             "ORDER by p.pageIndex";

                using (IDbConnection connection = _factory.GetConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                
                        IDbDataParameter param = command.CreateParameter();
                        param.DbType = DbType.String;
                        param.ParameterName = "@isbn";
                        param.Value = book.Isbn;
                        command.Parameters.Add(param);
                
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var page = new Page(reader.GetInt32(0), reader.GetString(1));
                                pages.Add(page);
                            }
                        }
                    }
                    return pages;
                }
            } 
            catch (DbException)
            {
                throw new StorageException("Erreur lors de la récupération des pages");
            }
            
        }
        
        public IList<Choice> SetChoicesToPage (Book? book, Page page)
        {
            try
            {
                var choices = new List<Choice>();
            
                string sql = "SELECT p.pageIndex, c.text\n" +
                             "FROM choice c\n" +
                             "JOIN page p ON p.pageid = c.pageidend\n" +
                             "JOIN page p2 ON p2.pageid = c.pageidstart\n" +
                             "JOIN book b ON b.bookid = c.bookid\n" +
                             "WHERE p2.pageindex = @pageIndexStart and b.Isbn = @bookIsbn";

                using (IDbConnection connection = _factory.GetConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        IDbDataParameter paramPageIndex = command.CreateParameter();
                        paramPageIndex.DbType = DbType.Int32;
                        paramPageIndex.ParameterName = "@pageIndexStart";
                        paramPageIndex.Value = page.Index;
                        command.Parameters.Add(paramPageIndex);

                        IDbDataParameter paramBookIsbn = command.CreateParameter();
                        paramBookIsbn.DbType = DbType.String;
                        paramBookIsbn.ParameterName = "@bookIsbn";
                        paramBookIsbn.Value = book.Isbn;
                        command.Parameters.Add(paramBookIsbn);

                        using (IDataReader reader = command.ExecuteReader())
                        { 
                            while (reader.Read())
                            { 
                                var choice = new Choice(reader.GetInt32(0), reader.GetString(1));
                        
                                choices.Add(choice);
                            }
                        }
                    }
                    return choices;
                }
            } 
            catch (DbException)
            {
                throw new StorageException("Erreur lors de la récupération des pages");
            }
        }
    }
}