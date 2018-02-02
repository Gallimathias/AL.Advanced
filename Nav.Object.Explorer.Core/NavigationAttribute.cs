using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.Object.Explorer.Core
{
    public class NavigationAttribute : Attribute
    {
        public string Menu { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }


        public NavigationAttribute()
        {
            Active = true;
        }
        public NavigationAttribute(string title) : this()
        {
            Title = title;
        }
    }
}
