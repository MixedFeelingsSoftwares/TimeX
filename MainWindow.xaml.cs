using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeX.Core.UI;

namespace TimeX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FrmMain_Loaded(object sender, RoutedEventArgs e)
        {
            LV_Events.ItemsSource = ListviewHandler.LEvents;
            ListviewHandler.Intantiate();
        }

        private void Btn_createEvent_Click(object sender, RoutedEventArgs e)
        {
            var lvents = new LVEvents()
            {
                Date = DateTime.Now.AddSeconds(10),
                Name = "Test",
            };

            ListviewHandler.LEvents.Add(lvents);
        }

        private void FrmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
