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
using System.Windows.Shapes;

namespace ABD_Project
{
    static public class Booking
    {
        public static BookingEntities context = new BookingEntities();
    }
    static public class CurrentUser
    {
        public static Users user { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Profil(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();

        }

        private void Shutdown(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = sidebar.SelectedItem as NavButton;

            navframe.Navigate(selected.Navlink);

        }

    }
}
