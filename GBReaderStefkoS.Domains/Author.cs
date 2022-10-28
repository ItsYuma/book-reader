namespace GBReaderStefkoS.Domains;

public record Author(string Name, string FirstName, string Matricule)
{
    /*private string _name;
    private string _firstName;
    private string _matricule;

    public Author(string name, string firstName, string matricule)
    {
        this._name = name;
        this._firstName = firstName;
        this._matricule = matricule;
    }*/
    
    public override string ToString()
    {
        return "Autheur: " + Name + " " + FirstName;
    }
    
}