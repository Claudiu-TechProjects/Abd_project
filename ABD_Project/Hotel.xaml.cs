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
    /// Interaction logic for Hotel.xaml
    /// </summary>
    public partial class Hotel : Window
    {
         int photoIndex=1;
        public Hotel()
        {
            InitializeComponent();
        }


        private void btn_like(object sender, RoutedEventArgs e)
        {

        }
        private void btn_dislike(object sender, RoutedEventArgs e)
        {

        }

        private void Rezervare(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Next_Photo(object sender, RoutedEventArgs e)
        {
            photoIndex++;
            if(photoIndex > 3)
            {
                photoIndex = 1;
            }
            Poza.Source = new BitmapImage(new Uri(@"Images/Hotel/Intercontinental/" + photoIndex + ".jpeg", UriKind.Relative));
        }

        private void btn_Back_Photo(object sender, RoutedEventArgs e)
        {
            photoIndex--;
            if (photoIndex <1 )
            {
                photoIndex = 3 ;
            }
            Poza.Source = new BitmapImage(new Uri(@"Images/Hotel/Intercontinental/" + photoIndex + ".jpeg", UriKind.Relative));
        }
    }
}
