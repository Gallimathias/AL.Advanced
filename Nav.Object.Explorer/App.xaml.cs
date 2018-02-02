using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Nav.Object.Explorer
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(SearchType);

            bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            base.OnStartup(e);
        }

        private Type SearchType(Type viewType)
        {
            var baseName = "Nav.Object.Explorer";
            var typeName = $"{viewType.Name}Base";
            var extName = "";

            var type = Type.GetType($"{baseName}.{typeName}, {viewType.Assembly.FullName}", false);

            if (type != null)
                return type;

            extName = "Model";
            type = Type.GetType($"{baseName}.{extName}.{typeName}, {baseName}.{extName}", false);

            if (type != null)
                return type;

            extName = "Core";
            type = Type.GetType($"{baseName}.{extName}.{typeName}, {baseName}.{extName}", false);

            if (type != null)
                return type;

            var exception = new MissingMemberException($"{typeName} not found");

            exception.Data.Add("baseName", baseName);
            exception.Data.Add("typeName", typeName);
            exception.Data.Add("extName", extName);

            throw exception;
        }
    }
}