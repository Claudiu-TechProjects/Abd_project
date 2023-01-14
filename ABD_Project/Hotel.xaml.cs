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
        string numeHotel;
        DateTime data_inceput;
        DateTime data_sfarsit;
        int invitati;
        public Hotel(string nume, DateTime data_i,DateTime data_s, int inv)
        {
           
            InitializeComponent();
             numeHotel = nume;
            data_inceput = data_i;
             data_sfarsit = data_s;
           invitati = inv;
            Poza.Source = new BitmapImage(new Uri(@"Images/Hotel/" + numeHotel + "/" + photoIndex + ".jpeg", UriKind.Relative));

            using(var context=new BookingEntities())
            {




            }

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
            Poza.Source = new BitmapImage(new Uri(@"Images/Hotel/" + numeHotel +"/" + photoIndex + ".jpeg", UriKind.Relative));
        }

        private void btn_Back_Photo(object sender, RoutedEventArgs e)
        {
            photoIndex--;
            if (photoIndex <1 )
            {
                photoIndex = 3 ;
            }
            Poza.Source = new BitmapImage(new Uri(@"Images/Hotel" + numeHotel +" / " + photoIndex + ".jpeg", UriKind.Relative));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
