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
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ABD_Project
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// </summary>
    
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
        }
      
       BookingEntities context=new BookingEntities();   
        private void btn_Login(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                ValidateCredentials(username, password);
               
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        private void btn_Register(object sender, RoutedEventArgs e)
        {
            Register register=new Register();
            register.Show();
            this.Close();
        }

        private void ValidateCredentials(string username, string password)
        {
            var user = (from c in context.Users where c.Username == username && c.Parola == password select c).FirstOrDefault();

            if (user == null)
                throw new Exception("Utilizator sau parola gresite!");

            CurrentUser.user=user;
        }

        private void Shutdown(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
