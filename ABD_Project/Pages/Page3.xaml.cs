using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace ABD_Project.Pages
{
    public partial class Page3 : Page
    {
        private string OrasSelectat = null;
        private int TipCameraSelectat = -1;

        public Page3()
        {
            InitializeComponent();
            var orase = (from c in Booking.context.Unitati select c.Judet).ToList();
            Orase.ItemsSource = orase;

            var cam = (from c in Booking.context.TipCamera select c.NrLocuri).ToList();
            Camere.ItemsSource = cam;
        }

        private void ComboBox_SelectionChangedOrase(object sender, SelectionChangedEventArgs e)
        {
            this.OrasSelectat = (string)Orase.SelectedItem;
        }

        private void ComboBox_SelectionChangedCamere(object sender, SelectionChangedEventArgs e)
        {
            this.TipCameraSelectat = (int)Camere.SelectedItem;
        }

        private void btn_Cauta_Click(object sender, RoutedEventArgs e)
        {
            if (Inceput.SelectedDate == null)
            {
                //afiseaza mesaj eroare
                _ = MessageBox.Show("Nu ati selectat o data de inceput!");
                return;
            }

            if (Sfarsit.SelectedDate == null)
            {
                //afiseaza mesaj eroare 
                _ = MessageBox.Show("Nu ati selectat o data de sfarsit!");
                return;
            }

            if (this.OrasSelectat == null)
            {
                //afiseaza mesaj eroare 
                _ = MessageBox.Show("Nu ati selectat un oras!");
                return;
            }

            if (Sfarsit.SelectedDate <= Inceput.SelectedDate)
            {
                _ = MessageBox.Show("Data de inceput a rezervarii nu poate fi mai mica decat data de sfarsit a acesteia.");
            }
            var unitati = from c in Booking.context.Unitati 
                          where c.Judet == this.OrasSelectat 
                          select new
                          {
                              Nume=c.Nume,
                              Oras=c.Oras,
                              Judet= c.Judet,
                              Adresa= c.Adresa,
                              Stele=c.Stele
                              
                          };


            
            
            DataGridHoteluri.ItemsSource = unitati.ToList();


        }

        private void Acces_Hotel(object sender, MouseButtonEventArgs e)
        {
            Hotel doubleclick = new Hotel();
            doubleclick.ShowDialog();
            

        }
    }
}
