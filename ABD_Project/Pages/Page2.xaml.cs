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
        }

        //Users user = Application.Current.Windows[0];
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login loging = new Login();
            loging.Show();
            Window.GetWindow(this).Close();
        }

<<<<<<< Updated upstream
        private void ChPassword_Click(object sender, RoutedEventArgs e)
        {
            var chPasss = new ChangePasswordWindow();
            chPasss.Show();
=======
        private void btn_Upload(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if(openDialog.ShowDialog() == true)
            {
                PozaProfil.Source = new BitmapImage(new Uri(openDialog.FileName));
            }    
>>>>>>> Stashed changes
        }
    }
}
