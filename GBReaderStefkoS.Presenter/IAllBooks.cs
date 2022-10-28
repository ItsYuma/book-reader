namespace GBReaderStefkoS.Presenter;

public interface IAllBooks
{
    void AddBook(string auteur, string titre, string isbn, string resume);

    void SetPresenter(MainPresenter p);

    void NoBook();
}