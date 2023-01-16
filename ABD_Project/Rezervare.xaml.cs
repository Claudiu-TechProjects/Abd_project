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
        public Rezervare(int id, string status)
        {
            InitializeComponent();

            labelID.Content = id;
            labelStatus.Content = status;
            labelIDD.Content = id;

            
            using (var context = new BookingEntities())
            {
                var rez = (from c in context.Rezervari
                           join c2 in context.RezervariUsers on c.IDRezervare equals c2.IDRezervare
                           join c3 in context.Unitati on c2.IDUnitate equals c3.IDUnitate
                           where c.IDRezervare == id
                           select new { 
                           
                               c.IDRezervare,
                              labelDataI= c.StartDate,
                               labelDataS=c.EndDate,
                              labelhotel= c3.Nume,
                             labelStele= c3.Stele,
                            labelSumatotala  = c.PretTotal
                            
                           
                           
                           }).ToList();
                
                var facil=(from c in context.Rezervari
                         join c2 in context.FacilitatiRezervare on c.IDRezervare equals c2.IDRezervare
                         join c3 in context.TipFacilitate on c2.IDTipFacilitate equals c3.IDTipFacilitate
                         where c.IDRezervare == id
                         select new {

                             Facilitate=c3.Denumire,
                             Pret=c3.Pret
                      
                         }).ToList();


                var cam=(from c in context.Rezervari
                         join c2 in context.TipCamera on c.IDTipCamera equals c2.IDTipCamera
                         where c.IDRezervare == id
                         select new
                         {
                             Camera=c2.IDTipCamera,
                             Loc=c2.NrLocuri
                         }).ToList();


            }


        }

        private void btn_descarcare(object sender, RoutedEventArgs e)
        {



        }

        private void btn_trimite(object sender, RoutedEventArgs e)
        {

        }
    }
}
