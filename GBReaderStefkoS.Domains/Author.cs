namespace GBReaderStefkoS.Domains;

public record Author(string Name, string FirstName, string Matricule)
{
    
    public override string ToString()
    {
        return "Autheur: " + Name + " " + FirstName;
    }
    
}