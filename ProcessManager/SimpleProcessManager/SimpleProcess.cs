using ProcessManager.ViewModels;

namespace ProcessManager.SimpleProcessManager
{
    public class SimpleProcess:ObservableObject
    {
        public int Pid { get; set; }
        public string ProcessName { get; set; }
    }
}