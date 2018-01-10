using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Nav.Object.Explorer.Model;
using Prism.DryIoc;


namespace Nav.Object.Explorer
{
    internal class Bootstrapper : DryIocBootstrapper
    {
        protected Application CurrentApplication => Application.Current;
        protected MainWindowBase ShellBase => (MainWindowBase)CurrentApplication.MainWindow.DataContext;

        protected override DependencyObject CreateShell() 
            => ServiceLocator.Current.GetInstance<MainWindow>();

        protected override void InitializeShell()
        {
            ShellBase.CollectPages();
            CurrentApplication.MainWindow = (MainWindow)Shell;           
            CurrentApplication.MainWindow.Show();
        }
        
    }
}
