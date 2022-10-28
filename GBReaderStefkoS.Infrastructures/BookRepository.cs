using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Repositories;

using Newtonsoft.Json;

namespace GBReaderStefkoS.Infrastructures;

public class BookRepository : IRepository
{
    private static string _userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    private static char _separator = Path.DirectorySeparatorChar;
    private string _filePath = _userPath + _separator + "ue36" + _separator + "q210020.json";
    //private const string FilePath = "/Users/stevystefko/ue36/q210020.json";
        
    public List<Book> Load()
    {
        //List<Book> books = new List<Book>();

        try
        {
            using (StreamReader r = new StreamReader(_filePath))
            {
                string json = r.ReadToEnd();
                return new Mapper().DTOtoEntity(JsonConvert.DeserializeObject<List<BookDTO>>(json));
                /*foreach (var b in books)
                {
                    Console.WriteLine(b.Author);
                    Console.WriteLine(b.Titre);
                    Console.WriteLine(b.Resume);
                    Console.WriteLine(b.Isbn);
                }*/
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public bool FileExist()
    {
        return File.Exists(_filePath);
    }

    public bool FileEmpty()
    {
        return new FileInfo(_filePath).Length == 0;
    }
}    