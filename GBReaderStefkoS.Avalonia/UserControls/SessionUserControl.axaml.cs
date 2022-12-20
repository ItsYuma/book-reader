using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GBReaderStefkoS.Avalonia.UserControls
{
    public partial class SessionUserControl : UserControl
    {
        public SessionUserControl()
        {
            InitializeComponent();
        }

        public SessionUserControl(string bookTitle, string bookIsbn, int pageIndex, string dateBeginning, string dateLastReading) : this()
        {
            BookTitle.Text = "Titre du livre : " +bookTitle;
            BookIsbn.Text = "ISBN : " + bookIsbn;
            PageIndex.Text = "Vous êtes arrivé page : " + pageIndex;
            DateStart.Text = "Date de début : " + dateBeginning;
            DateLastReading.Text = "Date de dernière lecture : " + dateLastReading;
        }
    }
}