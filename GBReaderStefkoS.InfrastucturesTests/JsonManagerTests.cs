using GBReaderStefkoS.Infrastructures;
using GBReaderStefkoS.Repositories;
using Newtonsoft.Json;

namespace GBReaderStefkoS.InfrastucturesTests;

public class JsonManagerTests
{
    /*private string _userPath;
    private char _separator;
    private string _directoryPath;
    private string _filePath;*/
    private static readonly string _userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    private static readonly char _separator = Path.DirectorySeparatorChar;
    private static readonly string _directoryPath = _userPath + _separator + "ue36";
    
    private static string _jsonFieTest = "q210020-test.json";
    
    //[SetUp]
    //public void Setup()
    //{
      //  
    //}

    // Si le fichier n'existe pas, une exception est catch et le fichier est créé
    [Test]
    public void JsonNotExist()
    {
         ISessionRepository sessionRepository = new SessionRepository(_jsonFieTest);
         Assert.That(File.Exists(_directoryPath + _separator + _jsonFieTest), Is.EqualTo(true));

         sessionRepository.LoadSessions();
         
         Assert.That(File.Exists(_directoryPath + _separator + _jsonFieTest), Is.EqualTo(true));
         
         File.Delete(_directoryPath + _separator + _jsonFieTest);
    }
    
    // si des arguments sont manquants, les sessions auquelles ils manquent des artuments sont ignorées
    [Test]
    public void JsonArgReadingSessionMissing()
    {
        ISessionRepository sessionRepository = new SessionRepository(_jsonFieTest);
        string json = "[{'BookTitle':'stevy en anglais','BookIsbn':,'PageIndex':3,'DateBeginning':'23/12/2022 23:11:58','DateLastReading':'23/12/2022 23:12:05'}]";
        
        writeToFile(_directoryPath + _separator + _jsonFieTest, json);
        
        var sessions = sessionRepository.LoadSessions();
        
        Assert.That(sessions.Count, Is.EqualTo(0));
        
        File.Delete(_directoryPath + _separator + _jsonFieTest);
        
    }

    //si le contenu du fichier ne respecte pas le format json, une exception est catch et le fichier est supprimé et recréé mais vide
    [Test]
    public void JsonNotWellFormated()
    {
        ISessionRepository sessionRepository = new SessionRepository(_jsonFieTest);
        string json = "[{'BookTitle';'stevy en anglais','BookIsbn':,'PageIndex':3,'DateBeginning':'23/12/2022 23:11:58','DateLastReading':'23/12/2022 23:12:05'}]";
        
        writeToFile(_directoryPath + _separator + _jsonFieTest, json);
        
        Assert.That(new FileInfo(_directoryPath + _separator + _jsonFieTest).Length > 0, Is.EqualTo(true));
        
        var sessions = sessionRepository.LoadSessions();
        
        Assert.That(new FileInfo(_directoryPath + _separator + _jsonFieTest).Length > 0, Is.EqualTo(false));
        
        File.Delete(_directoryPath + _separator + _jsonFieTest);
    }
    
    private void writeToFile(string fileName, string content)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.Write(content);
        }
    }
}