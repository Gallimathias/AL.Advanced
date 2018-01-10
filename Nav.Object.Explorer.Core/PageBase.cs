using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.Object.Explorer.Core
{
    public abstract class PageBase : BindableBase
    {
        public string Title { get => title; protected set => SetProperty(ref title, value); }

        private string title;

        public PageBase(string title)
        {
            Title = title.Replace("Base", "");
        }
    }
}
