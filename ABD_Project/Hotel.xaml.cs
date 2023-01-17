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
        int idCamera;
        List<Tuple<string, int>> numeFacilitate=new List<Tuple<string, int>>();
        int idTipCamera;
        int index = 0;
        DateTime data_inceput;
        DateTime data_sfarsit;
        int total=0;
        
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

            foreach(var x in numeFacilitate)
            {
                total = total + x.Item2;
            }
            
            using(var context =new BookingEntities() )
            { 
                var pretCam = (from c in context.CamereUnitati
                               join u in context.Unitati on c.IDUnitate equals u.IDUnitate
                               where u.Nume == numeHotel && c.IDTipCamera == idTipCamera
                               select c.Pret).FirstOrDefault();
                total = total + pretCam;
            
            
                Rezervari rez = new Rezervari
                {
                    IDTipCamera=idTipCamera,
                    StartDate = data_inceput,
                    EndDate = data_sfarsit,
                    PretTotal = total
                    
                };

                context.Rezervari.Add(rez);

                

            



          
              
                var idUni = (from c in context.Unitati
                             where c.Nume == numeHotel
                             select c.IDUnitate).FirstOrDefault();
               
               
            

            

            var idj = (from j in context.Rezervari
                               orderby j.IDRezervare descending
                               select j.IDRezervare).First();



                CamereOcupate cam1 = new CamereOcupate
                {
                    IDUnitate = idUni,
                    IDTipCamera = idTipCamera,
                    DataInceput = data_inceput,
                    DataSfarsit = data_sfarsit

                };

                    context.CamereOcupate.Add(cam1);
                
            

           

            
            

                foreach (var C in numeFacilitate)
                {
                    var idF=(from a in context.TipFacilitate 
                             where a.Denumire==C.Item1 
                             select a.IDTipFacilitate).FirstOrDefault();

                    FacilitatiRezervare fac = new FacilitatiRezervare
                    {
                        IDRezervare = idj,
                       IDTipFacilitate=idF

                    };

                    context.FacilitatiRezervare.Add(fac);
                }
                
                

                    RezervariUsers RezvUSer = new RezervariUsers
                    {
                        IDRezervare = idj,
                        IDUnitate = idUni,
                        IDUser=CurrentUser.user.IDUser

                    };

                    context.RezervariUsers.Add(RezvUSer);


                context.SaveChanges();
            }
            
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
           DataGridRezervare.Items.Add(CamereDisp.SelectedItem);
            var rez = CamereDisp.SelectedItem.ToString().Split(' ');


            idTipCamera = Int32.Parse(rez[3].Substring(0, rez[3].Length - 1));
        }

        private void AdaugareFaciliate(object sender, MouseButtonEventArgs e)
        {
            var rez = DataGridFacilitati.SelectedItem.ToString().Split(' ');
            DataGridRezervare.Items.Add(rez);
            string x =rez[3].Substring(0, rez[3].Length - 1);
            int y = Int32.Parse(rez[6]);
            numeFacilitate.Add( Tuple.Create(x, y));
            index++;
            //numeFacilitate.Add(Tuple.Create(x, y));
        }
    }
}
