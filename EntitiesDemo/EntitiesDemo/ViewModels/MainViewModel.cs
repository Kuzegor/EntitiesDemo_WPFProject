using EntitiesDemo.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EntitiesDemo.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
		private object _currentViewModel;

		public object CurrentViewModel
		{
			get { return _currentViewModel; }
			set { _currentViewModel = value;
				OnPropertyChanged();
			}
		}

		private CatalogViewModel CatalogViewModel { get; set; }
		private DiscountsViewModel DiscountsViewModel { get; set; }
		private AboutViewModel AboutViewModel { get; set; }
		public MainViewModel()
		{
			CatalogViewModel = new CatalogViewModel();
			DiscountsViewModel = new DiscountsViewModel();
			AboutViewModel = new AboutViewModel();
			CurrentViewModel = CatalogViewModel;

			ShowAboutCommand = new DelegateCommand(x => CurrentViewModel = AboutViewModel);
			ShowDiscountsCommand = new DelegateCommand(x => CurrentViewModel = DiscountsViewModel);
			ShowCatalogCommand = new DelegateCommand(x => CurrentViewModel = CatalogViewModel);

			ShutdownCommand = new DelegateCommand(x => Application.Current.Shutdown());
			MinimizeCommand = new DelegateCommand(x => Application.Current.MainWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new DelegateCommand(x =>
			{
				if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
				{
					Application.Current.MainWindow.WindowState = WindowState.Normal;
					Application.Current.MainWindow.BorderThickness = new Thickness(0);
                }
				else
				{
					Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    Application.Current.MainWindow.BorderThickness = new Thickness(6); // this line doesn't let the window get bigger than the screen
                }
            });
			DragMoveCommand = new DelegateCommand(x => Application.Current.MainWindow.DragMove());
        }

        public DelegateCommand ShowCatalogCommand { get; set; }
        public DelegateCommand ShowDiscountsCommand { get; set; }
        public DelegateCommand ShowAboutCommand { get; set; }

		public DelegateCommand ShutdownCommand { get; set; }
        public DelegateCommand MinimizeCommand { get; set; }
        public DelegateCommand MaximizeCommand { get; set; }
        public DelegateCommand DragMoveCommand { get; set; }
    }
}
