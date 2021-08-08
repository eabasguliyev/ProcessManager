namespace ProcessManager.Views.Services
{
    public interface IFileDialogService
    {
        public string FileName { get; }

        public bool? ShowDialog();
    }
}