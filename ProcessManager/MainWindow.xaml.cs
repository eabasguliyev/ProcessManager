using System.Windows;
using Prism.Events;
using ProcessManager.Events;
using ProcessManager.ViewModels;

namespace ProcessManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMainWindowViewModel _viewModel;

        public MainWindow(IMainWindowViewModel viewModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;

            this.DataContext = _viewModel;
            
            this.Loaded += OnLoaded;

            eventAggregator.GetEvent<OnCloseMainWindowEvent>().Subscribe(OnClose);
        }

        private void OnClose()
        {
            this.Close();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
