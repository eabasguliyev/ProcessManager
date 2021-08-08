using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using ProcessManager.Startup;
using ProcessManager.ViewModels;

namespace ProcessManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();

            var container = bootstrapper.Bootstrap();

            var window = container.Resolve<MainWindow>();

            window.Show();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unhandled exception: " + e.Exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
            
            e.Handled = true;
        }
    }
}
