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
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Text;
using System.IO;
using System.Windows.Forms;


namespace ABD_Project.Pages
{
    /// <summary>
    /// Interaction logic for Rezervare.xaml
    /// </summary>
    public partial class Rezervare : Window
    {
        static string path;
        public static int count = 0;
        static Document pdfDoc;
        public Document MakePDF(string HotelName, DateTime start, DateTime end, List<Tuple<string, int>> fac, List<Tuple<int, int>> cam, double sum)
        {
            Document pdfDoc = new Document();
            Aspose.Pdf.Page page = pdfDoc.Pages.Add();
            TextFragment customer = new TextFragment("Welcome " + CurrentUser.user.Username + "!\n");
            customer.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
            page.Paragraphs.Add(customer);

            TextFragment hotel = new TextFragment("Va multumim pentru rezervarea facuta la Hotelul " + HotelName + "!\n");
            page.Paragraphs.Add(hotel);

            TextFragment startDate = new TextFragment("Data de inceput a rezervarii: " + start + ".\n");
            page.Paragraphs.Add(startDate);

            TextFragment endDate = new TextFragment("Data de sfarsit a rezervarii: " + end + ".\n");
            page.Paragraphs.Add(endDate);


            Aspose.Pdf.Table tableFacilitati = new Aspose.Pdf.Table();
            tableFacilitati.Border = new BorderInfo(BorderSide.All, .8f, Aspose.Pdf.Color.Black);

            Row row = tableFacilitati.Rows.Add();
            row.Cells.Add("Facilitate");
            row.Cells.Add("Pret");
            foreach (var facilitate in fac)
            {
                row = tableFacilitati.Rows.Add();
                _ = row.Cells.Add(facilitate.Item1);
                _ = row.Cells.Add(facilitate.Item2.ToString());
            }
            page.Paragraphs.Add(tableFacilitati);


            TextFragment newLine = new TextFragment("\n");
            page.Paragraphs.Add(newLine);


            Aspose.Pdf.Table tableCamere = new Aspose.Pdf.Table();
            tableCamere.Border = new BorderInfo(BorderSide.All, .8f, Aspose.Pdf.Color.Black);

            Row row1 = tableCamere.Rows.Add();
            row1.Cells.Add("Tipul camerei");
            row1.Cells.Add("Locuri");
            foreach (var camera in cam)
            {
                row1 = tableCamere.Rows.Add();
                _ = row1.Cells.Add(camera.Item1.ToString());
                _ = row1.Cells.Add(camera.Item2.ToString());
            }
            page.Paragraphs.Add(tableCamere);

            TextFragment total = new TextFragment("\nTotalul de plata: " + sum + ".\n");
            page.Paragraphs.Add(total);

            return pdfDoc;
        }

        public Rezervare(int id, string status)
        {
            InitializeComponent();

            labelID.Content = id;
            labelStatus.Content = status;

            
            using (var context = new BookingEntities())
            {
                var rez = (from c in context.Rezervari
                           join c2 in context.RezervariUsers on c.IDRezervare equals c2.IDRezervare
                           join c3 in context.Unitati on c2.IDUnitate equals c3.IDUnitate
                           where c.IDRezervare == id
                           select new { 
                           
                               labelIDD=c.IDRezervare,
                              labelDataI= c.StartDate,
                               labelDataS=c.EndDate,
                              labelhotel= c3.Nume,
                             labelStele= c3.Stele,
                            labelSumatotala  = c.PretTotal
                            
                           
                           
                           }).ToList();

                var hotel = rez.Select(x => x.labelhotel).FirstOrDefault();
                labelhotel.Content = hotel;

                var idRez = rez.Select(x => x.labelIDD).FirstOrDefault();
                labelIDD.Content = idRez;

                var Stele = rez.Select(x => x.labelStele).FirstOrDefault();
                labelStele.Content = Stele;

                var start = rez.Select(x => x.labelDataI).FirstOrDefault();
                labelDataI.Content = start;

                var end = rez.Select(x => x.labelDataS).FirstOrDefault();
                labelDataS.Content = end;

                var sum = rez.Select(x => x.labelSumatotala).FirstOrDefault();
                double total=Convert.ToDouble(sum);
                labelSumatotala.Content = sum;

                var facil=(from c in context.Rezervari
                         join c2 in context.FacilitatiRezervare on c.IDRezervare equals c2.IDRezervare
                         join c3 in context.TipFacilitate on c2.IDTipFacilitate equals c3.IDTipFacilitate
                         where c.IDRezervare == id
                         select new {

                             Facilitate=c3.Denumire,
                             Pret=c3.Pret
                      
                         }).ToList();
                Facilitati.ItemsSource = facil;

                var cam=(from c in context.Rezervari
                         join c2 in context.TipCamera on c.IDTipCamera equals c2.IDTipCamera
                         where c.IDRezervare == id
                         select new
                         {
                             Camera=c2.IDTipCamera,
                             Loc=c2.NrLocuri
                         }).ToList();

                Camera.ItemsSource = cam;



                List<Tuple<string, int>> facilitati = facil.Select(x => Tuple.Create(x.Facilitate, x.Pret)).ToList();
                List<Tuple<int, int>> camere = cam.Select(x => Tuple.Create(x.Camera, x.Loc)).ToList();

               pdfDoc= MakePDF(hotel, start, end, facilitati, camere,total);
            }

            


        }

        private void btn_descarcare(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog= new FolderBrowserDialog();
           DialogResult result= dialog.ShowDialog();
            
               path = dialog.SelectedPath;
            

            pdfDoc.Save(path + "\\Invoice0" + count + ".pdf");
            ++count;

        }

        private void btn_trimite(object sender, RoutedEventArgs e)
        {

            pdfDoc.Save("D:\\Facultate\\Anul 3\\Sem1\\Aplicatii cu baze de date\\Proiect\\TEMA_ABD\\Abd_project\\ABD_Project\\bin\\Debug\\Invoice0" + count + ".pdf");
            EmailSender.sendEmail("nitaclaudiu79@gmail.com", "factura", "Factura Rezervare", count);
            File.Delete("D:\\Facultate\\Anul 3\\Sem1\\Aplicatii cu baze de date\\Proiect\\TEMA_ABD\\Abd_project\\ABD_Project\\bin\\Debug\\Invoice0" + count + ".pdf");

        }
    }
}
