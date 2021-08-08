using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using ProcessManager.Events;
using ProcessManager.SimpleProcessManager;
using ProcessManager.Views.Services;

namespace ProcessManager.ViewModels
{
    public class MainWindowViewModel:ObservableObject, IMainWindowViewModel
    {
        private readonly SimpleProcessManager.SimpleProcessManager _processManager;
        private readonly IOpenFileDialogService _openFileDialogService;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<SimpleProcess> _processes;
        private SimpleProcess _selectedProcess;

        public MainWindowViewModel(SimpleProcessManager.SimpleProcessManager processManager, IOpenFileDialogService openFileDialogService, IEventAggregator eventAggregator)
        {
            _processManager = processManager;
            _openFileDialogService = openFileDialogService;
            _eventAggregator = eventAggregator;

            Processes = new ObservableCollection<SimpleProcess>();

            NewProcessCommand = new DelegateCommand(CreateNewProcessAsync);
            EndTaskCommand = new DelegateCommand(EndTaskAsync, CanEndTask);
            ExitCommand = new DelegateCommand(Exit);
        }

        public ICommand NewProcessCommand { get; }
        public ICommand EndTaskCommand { get; }
        public ICommand ExitCommand { get; }

        public SimpleProcess SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                _selectedProcess = value;
                OnPropertyChanged();
                ((DelegateCommand)EndTaskCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<SimpleProcess> Processes
        {
            get => _processes;
            set
            {
                _processes = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            var processes = await _processManager.GetProcessesAsync();

            processes.ForEach(Processes.Add);
        }

        private async void EndTaskAsync()
        {
            if (SelectedProcess == null)
                return;

            await _processManager.KillProcessAsync(SelectedProcess, false);

            Processes.Remove(SelectedProcess);

            SelectedProcess = null;
        }

        private bool CanEndTask()
        {
            return SelectedProcess != null;
        }
        
        private void Exit()
        {
            _eventAggregator.GetEvent<OnCloseMainWindowEvent>().Publish();
        }
        private async void CreateNewProcessAsync()
        {
            if (_openFileDialogService.ShowDialog() == false)
                return;


            var simpleProcess = await _processManager.StartProcessAsync(_openFileDialogService.FileName);

            Processes.Add(simpleProcess);
        }
    }
}