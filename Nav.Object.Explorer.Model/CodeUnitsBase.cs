using Microsoft.Win32;
using Nav.API;
using Nav.Object.Explorer.Core;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Nav.Object.Explorer.Model
{
    public class CodeUnitsBase : PageBase
    {
        public ObservableCollection<NavObjectItem> Items { get; set; }
        public DelegateCommand<NavObjectItem> LeftDoubleClickCommand { get; private set; }
        public NavObjectItem SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }

        private NavObjectItem selectedItem;

        public CodeUnitsBase() : base(nameof(CodeUnitsBase))
        {
            Items = new ObservableCollection<NavObjectItem>();
            var context = new NavDatabaseContext();
            var dispatcher = Dispatcher.CurrentDispatcher;

            LeftDoubleClickCommand = new DelegateCommand<NavObjectItem>(
                (i) => OnLeftDoubleClick(i));

            Task.Run(() =>
            {

                //if (context.Connection.State != ConnectionState.Open)
                //    return;

                context.NavObject
                    .Where(o => o.Type == (int)ObjectType.CodeUnit)
                    .ToList()
                    .ForEach(o => dispatcher.Invoke(() => Items.Add(new NavObjectItem(o))));

            });
        }

        private void OnLeftDoubleClick(NavObjectItem item)
        {
            var code = NavObjectHelper.GetCode(item.NavObject);

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("No Code to export");
                return;
            }

            var dialog = new SaveFileDialog
            {
                Title = $"Save {item.Name}",
                FileName = $"{item.Name}.cs"
            };

            dialog.FileOk += (s, e) =>
            {
                var sender = (SaveFileDialog)s;
                using (var writer = new StreamWriter(sender.OpenFile()))
                {
                    writer.WriteLine(code);
                }
            };


            dialog.ShowDialog();

        }

        public class NavObjectItem
        {
            public int ID => NavObject.ID;
            public string Name => NavObject.Name;
            public string VersionList => NavObject.VersionList;

            public NavObject NavObject { get; private set; }

            public NavObjectItem(NavObject navObject)
            {
                NavObject = navObject;
            }
        }
    }
}
