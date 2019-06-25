using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TimeX.Core.UI.ToastWindow;

namespace TimeX.Core.UI
{
    public class ListviewHandler
    {
        public static ObservableCollection<LVEvents> LEvents = new ObservableCollection<LVEvents>();

        public static void Intantiate()
        {
            LEvents.CollectionChanged += LEvents_CollectionChanged;
        }

        private static void LEvents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Toast toaster = new Toast();
            toaster.Show();
        }
    }

    public class LVEvents
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Due
        {
            get
            {
                return Date.Subtract(DateTime.Now);
            }
            set
            {

            }
        }
    }
}

