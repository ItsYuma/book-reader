using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderStefkoS.Avalonia.Views;
using GBReaderStefkoS.Infrastructures;
using GBReaderStefkoS.Presenters;

namespace GBReaderStefkoS.Avalonia
{
    public partial class App : Application
    {
        
        private MainWindow _mainWindow;
        
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _mainWindow = new MainWindow();
                desktop.MainWindow = _mainWindow;
                
                CreateViewsAndWhatever();
            }
            
            base.OnFrameworkInitializationCompleted();
        }
        
        private void CreateViewsAndWhatever()
        {

            var dbFactory = new DbFactory("MySql.Data.MySqlClient",
                "Server=192.168.132.200;" + "Port=13306;" + "Database=Q210020;" + "uid=Q210020;" + "pwd=0020;");
            
            var allBooksView = new AllBooksView();
            var AllBooksPresenter = new AllBooksPresenter(allBooksView, _mainWindow, dbFactory);
            
            var pageView = new PageView();
            var pagePresenter = new PagePresenter(pageView, _mainWindow, dbFactory, AllBooksPresenter);
            
            _mainWindow.RegisterPage("allBooks", allBooksView);
            _mainWindow.RegisterPage("pageView", pageView);
            
        }
    }
}