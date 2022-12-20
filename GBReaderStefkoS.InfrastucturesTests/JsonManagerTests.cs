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
    
    [SetUp]
    public void Setup()
    {
        /*_userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        _separator = Path.DirectorySeparatorChar;
        _directoryPath = _userPath + _separator + "ue36";
        _filePath = _directoryPath + _separator + "q210020-session.json";*/
        IJsonManager jsonManager = new JsonManager();
    }

    [Test]
    public void Test1()
    {
         
    }
}