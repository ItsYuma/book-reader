using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GBReaderStefkoS.Presenters.Views
{
    public interface IPageView : UserControl
    {
        public IPageView()
        {
            InitializeComponent();
        }
        
    }
}