using GBReaderStefkoS.Domains;
using GBReaderStefkoS.Infrastructures.Dto;
using GBReaderStefkoS.Repositories;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;

namespace GBReaderStefkoS.Infrastructures
{
    public class SessionRepository : ISessionRepository
    {
        private static readonly string _userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private static readonly char _separator = Path.DirectorySeparatorChar;
        private static readonly string _directoryPath = _userPath + _separator + "ue36";
        //private static readonly string _filePath = _directoryPath + _separator + "q210020-session.json";
        private readonly string _filePath;

        public SessionRepository(string fileName)
        {
            _filePath = _directoryPath + _separator + fileName;
        }

        public IList<ReadingSession> LoadSessions()
        {
            IList<ReadingSession> allSessions = new List<ReadingSession>();
            try
            {
                using (StreamReader reader = new(_filePath))
                {
                    if (FileEmpty())
                    {
                        return allSessions;
                    }
                    else
                    {
                        string json = reader.ReadToEnd();
                        var allSessionsDto = JsonConvert.DeserializeObject<List<ReadingSessionDto>>(json);
                        allSessions = new Mapper().ListDtoToListEntity(allSessionsDto);
                        // verifie que les sessions n'ont aucun attribut null
                        //allSessions = allSessions.Select(session => session.BookIsbn != null && session.DateBeginning != null && session.DateLastReading != null ? session : throw new JsonException()).ToList();
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(_directoryPath);
                File.Create(_filePath);
            }
            catch (FileNotFoundException)
            {
                File.Create(_filePath);
            }
            catch (JsonReaderException)
            {
                File.Create(_filePath);
            }
            return allSessions;
        } 
        
        public void SaveOrUpdateSession(string bookTitle, string bookIsbn, int pageIndex, string dateTime)
        {
            var allSessions = LoadSessions();
            var sessionExist = SessionExist(allSessions, bookIsbn);
            if (sessionExist)
            {
                UpdateSession(allSessions, bookIsbn, pageIndex, dateTime);
            }
            else
            {
                SaveSession(allSessions, bookTitle, bookIsbn, pageIndex, dateTime);
            }
        }
        
        private bool SessionExist(IList<ReadingSession> allSessions, string bookIsbn)
        {
            return allSessions.Any(session => session.BookIsbn == bookIsbn);
        }
        
        public int GetLastPageRead(string bookIsbn)
        {
            var allSessions = LoadSessions()?? new List<ReadingSession>();
            var session = allSessions.FirstOrDefault(s => s.BookIsbn == bookIsbn);
            return session?.PageIndex ?? 1;
        }
        
        private void SaveSession(IList<ReadingSession> allSessions, string bookTitle, string bookIsbn, int pageIndex, string dateTime)
        {
            var allSessionsDto = new Mapper().ListEntityToListDto(allSessions);
            var newSessionDto = new ReadingSessionDto(bookTitle, bookIsbn, pageIndex, dateTime, dateTime);
            allSessionsDto.Add(newSessionDto);
            
            try
            {
                using (StreamWriter writer = new(_filePath))
                {
                    string json = JsonConvert.SerializeObject(allSessionsDto);
                    writer.Write(json);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(_directoryPath);
                File.Create(_filePath);
            }
            catch (FileNotFoundException)
            {
                File.Create(_filePath);
            }
        }
        
        private void UpdateSession(IList<ReadingSession> allSessions, string bookIsbn, int pageIndex, string dateTime)
        {
            var session = allSessions.First(s => s.BookIsbn == bookIsbn);
            session.PageIndex = pageIndex;
            session.DateLastReading = dateTime;
            
            try
            {
                using (StreamWriter writer = new(_filePath))
                {
                    string json = JsonConvert.SerializeObject(allSessions);
                    writer.Write(json);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(_directoryPath);
                File.Create(_filePath);
            }
            catch (FileNotFoundException)
            {
                File.Create(_filePath);
            }
        }
        
        public void RemoveSession(string bookIsbn)
        {
            var allSessions = LoadSessions();
            var allSessionsDto = new Mapper().ListEntityToListDto(allSessions);
            var sessionToRemove = allSessionsDto.FirstOrDefault(s => s.BookIsbn == bookIsbn);
            allSessionsDto.Remove(sessionToRemove);
            try
            {
                using (StreamWriter writer = new(_filePath))
                {
                    string json = JsonConvert.SerializeObject(allSessionsDto);
                    writer.Write(json);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(_directoryPath);
                File.Create(_filePath);
            }
            catch (FileNotFoundException)
            {
                File.Create(_filePath);
            }
        }

        private bool FileEmpty()
        {
            return new FileInfo(_filePath).Length == 0;
        }
        
    }
}