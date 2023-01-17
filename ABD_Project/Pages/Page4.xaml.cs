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

namespace ABD_Project.Pages
{
    public partial class Page4 : Page
    {
        
        public class Rezerv
        {
           public int Rezervare { get; set; }
            public string Hotel { get; set; }
            public string Data_de_inceput { get; set; }
            public string Data_de_final { get; set; }

            public string Status { get; set; }

             public Rezerv(int id,string hotel,string datai,string datas,string stat)
            {
                Rezervare = id;
                Hotel = hotel;
                Data_de_inceput = datai;
                Data_de_final = datas;
                Status = stat;

            }

            


        }

        public Page4()
        {
            InitializeComponent();
            NameUser.Content = CurrentUser.user.Prenume;
            string user = CurrentUser.user.Username;
            Rezerv r1;


            using (var context = new BookingEntities())
            {
                var rezervari = from c in context.Rezervari
                                join o in context.RezervariUsers on c.IDRezervare equals o.IDRezervare
                                join n in context.Users on o.IDUser equals n.IDUser
                                join u in context.Unitati on o.IDUnitate equals u.IDUnitate
                                where n.Username == user
                                select new
                                {

                                    Rezervare = c.IDRezervare,
                                    Hotel = u.Nume,
                                    Data_de_inceput = c.StartDate.ToString(),
                                    Data_de_final = c.EndDate.ToString(),



                                };

                foreach(var item in rezervari)
                {
                    DateTime data=DateTime.Parse(item.Data_de_final);
                    if(data < DateTime.Now)
                    {
                       string status = "Incheiata";
                        r1 = new Rezerv(item.Rezervare, item.Hotel, item.Data_de_inceput, item.Data_de_final, status);
                        DataGridRezervari.Items.Add(r1);
                    }
                    else
                    {
                        string status = "Activa";
                        r1 = new Rezerv(item.Rezervare, item.Hotel, item.Data_de_inceput, item.Data_de_final, status);
                        DataGridRezervari.Items.Add(r1);
                    }
                }
                

            }
            
        }

        private void Acces_Rezervare(object sender, MouseButtonEventArgs e)
        {
            
            var rez = DataGridRezervari.SelectedItem.ToString().Split(' ');
           
            int.TryParse(rez[0], out int id);
            string Status="activa";
            Rezervare rezervare = new Rezervare(1,Status);
            rezervare.ShowDialog();

        }

    }
    
}
