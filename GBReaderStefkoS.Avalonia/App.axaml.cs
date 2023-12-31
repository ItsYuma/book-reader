using System;
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

            var storageRepository = new StorageRepository(new DbFactory());
            var sessionRepository = new SessionRepository("q210020-session.json");

            var allBooksView = new AllBooksView();
            var AllBooksPresenter = new AllBooksPresenter(allBooksView, _mainWindow, storageRepository);
            
            var pageView = new PageView();
            var pagePresenter = new PagePresenter(pageView, _mainWindow, sessionRepository, AllBooksPresenter);
            
            var statsView = new StatsView();
            var statsPresenter = new StatsPresenter(statsView, _mainWindow, sessionRepository, AllBooksPresenter);
            
            _mainWindow.RegisterPage("allBooks", allBooksView);
            _mainWindow.RegisterPage("pageView", pageView);
            _mainWindow.RegisterPage("statsView", statsView);
            
        }
    }
}