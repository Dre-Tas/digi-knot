using System.Data.Objects;
using System.Linq;
using System.Windows;

namespace DigiKnot.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : Window
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }



    class DbRow
    {
        public string Asset_ID { get; set; }
        public float Price { get; set; }
        public string URL { get; set; }
        public string Manufacturer { get; set; }
    }
}
