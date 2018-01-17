using Nav.API;
using Nav.Object.Explorer.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.Object.Explorer.Model
{
    public class CodeUnitsBase : PageBase
    {
        public ObservableCollection<NavObjectItem> Items { get; set; }

        public CodeUnitsBase() : base(nameof(CodeUnitsBase))
        {
            Items = new ObservableCollection<NavObjectItem>();
            var context = new NavDatabaseContext();

            Task.Run(() =>
            {

                if (context.Connection.State != ConnectionState.Open)
                    return;

                context.Object
                    .Where(o => o.Type == (int)ObjectType.CodeUnit)
                    .ToList()
                    .ForEach(o => Items.Add(new NavObjectItem(o)));
            });
        }

        public class NavObjectItem
        {
            public int ID => navObject.ID;
            public string Name => navObject.Name;
            public string VersionList => navObject.Version_List;

            private API.Object navObject;

            public NavObjectItem(API.Object navObject)
            {
                this.navObject = navObject;
            }
        }
    }
}
