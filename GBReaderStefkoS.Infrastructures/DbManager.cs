using System.Data;
using System.Data.Common;
using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Repositories;
using MySql.Data.MySqlClient;

namespace GBReaderStefkoS.Infrastructures
{
    //TODO try
    public class DbManager : IDbManager
    {
        private readonly IDbConnection _connection;
        
        public DbManager(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public void Dispose() => _connection.Dispose();
        
        //crée une methode qui charge les livres de la base de données
        public IEnumerable<Book>? GetBooks()
        {
            
            var books = new List<Book>();
            
            string sql = "SELECT a.name, a.firstName, a.matricule, b.title, b.resume, b.isbn\n" +
                           "FROM book b\n" +
                           "JOIN author a ON a.authorid = b.authorid\n" +
                           "WHERE ispublish = TRUE";
            using (IDbCommand command = _connection.CreateCommand())
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
            return books;
        }
        
        public IList<Page> GetPagesFromBook(Book? book)
        {
            var pages = new List<Page>();
            
            string sql = "SELECT p.pageIndex, p.text\n" +
                           "FROM page p\n" +
                           "JOIN book b ON b.bookid = p.bookid\n" +
                           "WHERE b.isbn = @isbn\n" +
                           "ORDER by p.pageIndex";
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.CommandText = sql;
                
                IDbDataParameter param = command.CreateParameter();
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
        
        public IList<Choice> SetChoicesToPage (Book? book, Page page)
        {
            var choices = new List<Choice>();
            
            string sql = "SELECT p.pageIndex, c.text\n" +
                         "FROM choice c\n" +
                         "JOIN page p ON p.pageid = c.pageidend\n" +
                         "JOIN page p2 ON p2.pageid = c.pageidstart\n" +
                         "JOIN book b ON b.bookid = c.bookid\n" +
                         "WHERE p2.pageindex = @pageIndexStart and b.Isbn = @bookIsbn";
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.CommandText = sql;

                IDbDataParameter paramPageIndex = command.CreateParameter();
                paramPageIndex.ParameterName = "@pageIndexStart";
                paramPageIndex.Value = page.Index;
                command.Parameters.Add(paramPageIndex);

                IDbDataParameter paramBookIsbn = command.CreateParameter();
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
        
        public Boolean ContainChoice (Page page)
        {
            string sql = "SELECT count(*)" +
                         "FROM choice c\n" +
                         "JOIN page p ON p.pageid = c.pageidend\n" +
                         "JOIN page p2 ON p2.pageid = c.pageidstart\n" +
                         "WHERE p2.pageindex = @pageIndexStart";
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.CommandText = sql;
                
                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@isbn";
                param.Value = page.Index;
                command.Parameters.Add(param);
                
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0) > 0;
                        //Console.WriteLine(reader.GetString(0));
                    }
                }
            }
            return false;
        } 
    }
}