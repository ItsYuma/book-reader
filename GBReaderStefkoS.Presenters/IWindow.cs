namespace GBReaderStefkoS.Presenters;

public interface IWindow
{
    // Fournit un presenteur à la vue
    void SetPresenter(MainPresenter presenter);

    //void LoadBooks();

    void SetError(string str);

    void ShowAllBooks();
    
    //void CreateBook(string auteur, string titre, string isbn, string resume);

    void ClearWindow();

    void ShowDataBook(string author, string title, string isbn, string resume);

    //void UpdateActualPanel(UserControl uc);
}