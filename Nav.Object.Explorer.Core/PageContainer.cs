using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Nav.Object.Explorer.Core
{
    public static class PageContainer
    {
        private static Dictionary<string, Type> views;
        private static Dictionary<string, Type> bases;

        static PageContainer()
        {
            views = new Dictionary<string, Type>();
            bases = new Dictionary<string, Type>();
        }

        public static void RegisterAssembly(Assembly assembly)
        {            
           assembly.GetTypes().Where(t => t.BaseType.Name == "Page").ToList().ForEach(v => RegisterView(v));
           assembly.GetTypes().Where(t => t.BaseType.Name == "PageBase").ToList().ForEach(b => RegisterBase(b));            
        }

        public static void RegisterView(Type type) => views.Add(type.Name, type);

        public static void RegisterBase(Type type) => bases.Add(type.Name.Substring(0, type.Name.Length - 4), type);
        
        public static Type GetPageViewType(string name) => views[name];

        public static Type GetPageBaseType(string name) => bases[name];

        public static object GetPageView(string name) => Activator.CreateInstance(views[name]);
        public static T GetPageView<T>() where T : Page => (T)Activator.CreateInstance(views[typeof(T).Name]);
        
        public static object GetPageBase(string name) => Activator.CreateInstance(bases[name]);
        public static T GetPageBase<T>() where T : PageBase 
            => (T)Activator.CreateInstance(bases[typeof(T).Name.Substring(0, typeof(T).Name.Length - 4)]);

        public static PageBase GetPage(string name)
        {
            GetPageView(name);
            return (PageBase)GetPageBase(name);
        }
    }
}
