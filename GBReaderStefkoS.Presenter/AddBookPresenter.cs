namespace GBReaderStefkoS.Presenter;

public class AddBookPresenter
{
    private IAddBookUC _addBook;

    public AddBookPresenter(IAddBookUC addBook)
    {
        this._addBook = addBook;
    }
}