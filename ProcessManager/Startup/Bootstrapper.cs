using Autofac;
using Prism.Events;
using ProcessManager.ViewModels;
using ProcessManager.Views.Services;

namespace ProcessManager.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var containerBuilder = new ContainerBuilder();


            containerBuilder.RegisterType<MainWindow>().AsSelf();

            containerBuilder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();

            containerBuilder.RegisterType<OpenFileDialogService>().As<IOpenFileDialogService>();

            containerBuilder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            containerBuilder.RegisterType<SimpleProcessManager.SimpleProcessManager>().AsSelf();

            return containerBuilder.Build();
        }
    }
}