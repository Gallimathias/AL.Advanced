using Microsoft.Practices.ServiceLocation;
using Nav.Object.Explorer.Core;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nav.Object.Explorer.Model
{
    public class MainWindowBase : BindableBase
    {
        public BindableBase CurrentPage { get => currentPage; set => SetProperty(ref currentPage, value); }
        public ObservableCollection<MenuItemContext> NavigationList { get => navigationList; private set => SetProperty(ref navigationList, value); }


        private BindableBase currentPage;
        private ObservableCollection<MenuItemContext> navigationList;

        public MainWindowBase()
        {
            NavigationList = new ObservableCollection<MenuItemContext>();
        }

        public void CollectPages()
        {
            var list = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(PageBase)).ToList();

            foreach (var page in list)
            {
                var name = page.Name.Substring(0, page.Name.Length - 4).Trim();
                navigationList.Add(new MenuItemContext(name.Replace("_"," ").Trim(), NavigateToPage, name));
            }

            if (NavigationList.Count > 0)
                NavigationList[0].Command.Execute();
        }

        private void NavigateToPage(string pageName)
        {
            if (string.IsNullOrWhiteSpace(pageName))
                return;

            var type = Type.GetType($"Nav.Object.Explorer.Views.{pageName}", false);

            if (type == null)
                return;

            CurrentPage = (BindableBase)ServiceLocator.Current.GetInstance(type);
        }

        public class MenuItemContext
        {
            public string Title { get; set; }
            public string Tag { get; set; }
            public DelegateCommand Command { get; set; }

            public MenuItemContext(string title, Action<string> action)
            {
                Tag = Title = title;
                Command = new DelegateCommand(() => action(Tag));
            }
            public MenuItemContext(string title, Action<string> action, string tag)
                : this(title, action)
            {
                Tag = tag;
            }
        }
    }
}
