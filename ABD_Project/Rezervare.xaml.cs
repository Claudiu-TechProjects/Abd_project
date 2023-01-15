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

namespace ABD_Project.Pages
{
    /// <summary>
    /// Interaction logic for Rezervare.xaml
    /// </summary>
    public partial class Rezervare : Window
    {
        public Rezervare(int id,string status)
        {
            InitializeComponent();
            
            labelID.Content = id;
            labelStatus.Content = status;
            labelIDD.Content = id;


        }

        private void btn_descarcare(object sender, RoutedEventArgs e)
        {



        }

        private void btn_trimite(object sender, RoutedEventArgs e)
        {

        }
    }
}
