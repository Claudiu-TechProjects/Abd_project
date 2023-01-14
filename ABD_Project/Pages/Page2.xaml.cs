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
using Microsoft.Win32;

namespace ABD_Project.Pages
{
    public partial class Page2 : UserControl
    {
        private MainWindow main;

        public Page2()
        { 
            InitializeComponent();
            main = new MainWindow();
            main = Application.Current.Windows[0] as MainWindow;


            labelUser.Content = CurrentUser.user.Username;
            labelnume.Content = CurrentUser.user.Nume;
            labelPrenume.Content = CurrentUser.user.Prenume;
            labelTelefon.Content = CurrentUser.user.Telefon;
            labelEmail.Content = CurrentUser.user.Email;

          
            if (CurrentUser.user.Imagine != null)
            {
                BitmapImage img = new BitmapImage(new Uri(CurrentUser.user.Imagine, UriKind.Absolute));
                PozaProfil.Source = img;
            }
        
         }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login loging = new Login();
            loging.Show();
            Window.GetWindow(this).Close();
        }


        private void ChPassword_Click(object sender, RoutedEventArgs e)
        {
            var chPasss = new ChangePasswordWindow();
            chPasss.Show();
        }
        private void btn_Upload(object sender, RoutedEventArgs e)
        {
            BitmapImage ProfileImg;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpeg;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                ProfileImg = new BitmapImage(new Uri(openDialog.FileName));
                PozaProfil.Source = ProfileImg;
                CurrentUser.user.Imagine=ProfileImg.ToString();

                using (var context = new BookingEntities())
                {
                    var user = context.Users.Where(u => u.Username == CurrentUser.user.Username).ToList();
                    user.ForEach(u => u.Imagine = ProfileImg.ToString());
                    context.SaveChanges();
                }
            }
        }

        private void ChPoza_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage ProfileImg;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpeg;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                ProfileImg = new BitmapImage(new Uri(openDialog.FileName));
                PozaProfil.Source = ProfileImg;
                CurrentUser.user.Imagine = ProfileImg.ToString();

                using (var context = new BookingEntities())
                {
                    var user = context.Users.Where(u => u.Username == CurrentUser.user.Username).ToList();
                    user.ForEach(u => u.Imagine = ProfileImg.ToString());
                    context.SaveChanges();
                   
                }
            }

        }
    }
}
