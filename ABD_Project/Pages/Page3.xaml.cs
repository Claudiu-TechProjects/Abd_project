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
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Design;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Configuration;
using MaterialDesignThemes.Wpf;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace ABD_Project.Pages
{
    public partial class Page3 : Page
    {

        private string OrasSelectat = null;
        private List<string> hotelName = new List<string>();

        public Page3()
        {
            InitializeComponent();
            using (var context = new BookingEntities())
            {
                var orase = (from c in context.Unitati select c.Judet).ToList();
                Orase.ItemsSource = orase;
            }

            using (var context = new BookingEntities())
            {
                var cam = (from c in context.TipCamera select c.NrLocuri).ToList();
            }
        }

        private void ComboBox_SelectionChangedOrase(object sender, SelectionChangedEventArgs e)
        {
            this.OrasSelectat = (string)Orase.SelectedItem;
        }

        private void btn_Cauta_Click(object sender, RoutedEventArgs e)
        {

            //if (!int.TryParse(Invitati.Text, out int valueInvitati))
            //{
            //    _ = MessageBox.Show("Numarul de invitati trebuie sa fie intreg");
            //    return;
            //}


            //if (Invitati.Text == "")
            //    valueInvitati = 0;

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

            /*            using (var context = new BookingEntities())
                        {
                            var query = (from u in context.Unitati
                                         where u.Oras == (string)Orase.SelectedItem &&
                                         (
                           (from tipCamera in context.TipCamera
                            join camereOcupate in context.CamereOcupate on tipCamera.IDTipCamera equals camereOcupate.IDTipCamera
                            join unitati in context.Unitati on camereOcupate.IDUnitate equals unitati.IDUnitate
                            where camereOcupate.IDUnitate == unitati.IDUnitate && camereOcupate.IDTipCamera == tipCamera.IDTipCamera &&
                                                       ((Inceput.SelectedDate >= camereOcupate.DataInceput && Inceput.SelectedDate <= camereOcupate.DataSfarsit) ||
                                                       (Sfarsit.SelectedDate >= camereOcupate.DataInceput && Sfarsit.SelectedDate <= camereOcupate.DataSfarsit)) &&
                                                       (unitati.Oras == (string)Orase.SelectedItem)
                            select (int?)tipCamera.NrLocuri).Sum() ?? 0)
                            <
                            (
                                (from camereUnitati in context.CamereUnitati
                                    join tipCamera in context.TipCamera on camereUnitati.IDTipCamera equals tipCamera.IDTipCamera
                                    join unitati in context.Unitati on camereUnitati.IDUnitate equals unitati.IDUnitate
                                    where unitati.Oras == (string)Orase.SelectedItem
                                    select camereUnitati.NrDisp * tipCamera.NrLocuri).Sum()
                            )
                            select u).ToList();
                            var result = query;
                        }*/

            string connString = @"Data Source=DESKTOP-NQL6DA2;Initial Catalog=Booking;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(
                    @"SELECT *
                        FROM Unitati
                        WHERE Unitati.Oras = 'Bucuresti'
                        AND
                        (
                            SELECT ISNULL(SUM(TipCamera.NrLocuri), 0)
                            FROM TipCamera
                            JOIN CamereOcupate ON TipCamera.IDTipCamera = CamereOcupate.IDTipCamera
                            JOIN Unitati ON Unitati.IDUnitate = CamereOcupate.IDUnitate
                            WHERE CamereOcupate.IDUnitate = Unitati.IDUnitate AND CamereOcupate.IDTipCamera = TipCamera.IDTipCamera
                            AND(('2022-01-15' >= CamereOcupate.DataInceput AND '2022-01-15' <= CamereOcupate.DataSfarsit)
                            OR('2022-01-18' >= CamereOcupate.DataInceput AND '2022-01-18' <= CamereOcupate.DataSfarsit))
                            AND(Unitati.Oras = 'Bucuresti')
                        )
                        <
                        (
                            SELECT ISNULL(SUM(NrDisp * NrLocuri), 0)
                            FROM CamereUnitati
                            JOIN TipCamera ON TipCamera.IDTipCamera = CamereUnitati.IDTipCamera
                            JOIN Unitati ON Unitati.IDUnitate = CamereUnitati.IDUnitate
                            WHERE Unitati.Oras = 'Bucuresti'
                        )", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var context = new BookingEntities();
                        List<Unitati> units = new List<Unitati>();
                        while (reader.Read())
                        {
                            hotelName.Add(String.Format("{0}", reader["Nume"]));
                            units.Add(new Unitati
                            {
                                Nume = String.Format("{0}", reader["Nume"]),
                                Adresa = String.Format("{0}", reader["Adresa"]),
                                Oras = String.Format("{0}", reader["Oras"]),
                                Judet = String.Format("{0}", reader["Judet"]),
                                Stele = int.Parse(String.Format("{0}", reader["Stele"]))
                            });

                        }
                        DataGridHoteluri.ItemsSource = units;
                    }
                }
            }

        }

        private void Acces_Hotel(object sender, MouseButtonEventArgs e)
        {
            //int.TryParse(Invitati.Text, out int valueInvitati);
            var hotel = hotelName[DataGridHoteluri.SelectedIndex];
            Window inregistrare = new Hotel(hotel, Inceput.SelectedDate.Value, Sfarsit.SelectedDate.Value);
            inregistrare.ShowDialog();
        }
    }
}