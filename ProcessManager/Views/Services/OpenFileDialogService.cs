using Microsoft.Win32;

namespace ProcessManager.Views.Services
{
    public class OpenFileDialogService:IOpenFileDialogService
    {
        private readonly OpenFileDialog _openFileDialog;

        public OpenFileDialogService()
        {
            _openFileDialog = new OpenFileDialog();
        }

        public string FileName => _openFileDialog.FileName;
        public bool? ShowDialog()
        {
            return _openFileDialog.ShowDialog();
        }
    }
}