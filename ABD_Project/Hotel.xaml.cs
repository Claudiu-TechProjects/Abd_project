using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Data.Entity;
using System.Runtime.InteropServices;

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
        
        public Hotel(string nume, DateTime data_i,DateTime data_s)
        {
           
            InitializeComponent();
            numeHotel = nume;
            data_inceput = data_i;

            data_sfarsit = data_s;
            //invitati = inv;

             data_sfarsit = data_s;

            Poza.Source = new BitmapImage(new Uri(@"Images/Hotel/" + numeHotel + "/" + photoIndex + ".jpeg", UriKind.Relative));

            using(var context = new BookingEntities())
            {
                var facilitati = 
                    (from tf in context.TipFacilitate
                     join fd in context.FacilitatiDisponibile on tf.IDTipFacilitate equals fd.IDTipFacilitate
                     join unitati in context.Unitati on fd.IDUnitate equals unitati.IDUnitate
                     where unitati.Nume == numeHotel
                     select new
                     {
                         IDTipFacilitate= tf.Denumire,
                         Pret=tf.Pret
                     }

                    ).ToList();

                //OK
                //TO DO: pune facilitati in datagrid
                DataGridFacilitati.ItemsSource = facilitati;
            }

            using (var context = new BookingEntities())
            {
                var camere_ocupate = (from co in context.CamereOcupate
                             join u in context.Unitati on co.IDUnitate equals u.IDUnitate
                             join tc in context.TipCamera on co.IDTipCamera equals tc.IDTipCamera
                             where u.Nume == nume
                             && ((co.DataInceput >= data_i && co.DataSfarsit >= data_i)
                                 || (co.DataInceput <= data_s && data_s <= co.DataSfarsit))
                             group co by tc.IDTipCamera into g
                             select new
                             {
                                 TipCamera = g.Key,
                                 NumarCamereOcupate = g.Count()
                             }).ToList();

                var camere_disponibile = (from cu in context.CamereUnitati
                                          join u in context.Unitati on cu.IDUnitate equals u.IDUnitate
                                          where u.Nume == nume
                                          select new
                                          {
                                                TipCamera = cu.IDTipCamera,
                                                Disp = cu.NrDisp 
                                          }).ToList();

               var camere_disponibile_new = camere_disponibile.Select(x => new
                {
                    x.TipCamera,
                    Disp = x.Disp - camere_ocupate.Where(y => y.TipCamera == x.TipCamera).Select(y => y.NumarCamereOcupate).FirstOrDefault()
                }).ToList();

                //OK
                //TO DO: pune camere_disponibile_new in datagrid

                CamereDisp.ItemsSource = camere_disponibile_new;

                
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

       

        private void AdaugareCamera(object sender, MouseButtonEventArgs e)
        {
            var rez = CamereDisp.SelectedItem.ToString().Split(' ');
            DataGridRezervare.Items.Add(rez[0]);
            DataGridRezervare.Items.Add((string)rez[1]);
            DataGridRezervare.ItemsSource = rez;
            ///
        }

        private void AdaugareFaciliate(object sender, MouseButtonEventArgs e)
        {
            var rez = DataGridFacilitati.SelectedItem.ToString().Split(' ');
            DataGridRezervare.Items.Add(rez[0]);
            ///
        }
    }
}
