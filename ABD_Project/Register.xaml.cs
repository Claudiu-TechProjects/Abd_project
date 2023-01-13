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

namespace ABD_Project
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window

    { 
        public Register()
        {
           
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string email = EmailTextBox.Text;
            string telefon = NumarDeTelefonTextBox.Text;
            string nume = NumeTextBox.Text;
            string prenume = PrenumeTextBox.Text;   
            try
            {
                if (password != confirmPassword)
                    throw new Exception("Passwords do not match. Please try again.");
            
                RegisterUser(username, password, email, telefon, nume, prenume);
                Login login = new Login();
                login.Show();
                MessageBox.Show("Inregistrare reusita!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        private void RegisterUser(string username, string password, string email,string telefon ,string nume, string prenume)
        { using (var context= new BookingEntities()) { 
            var aux= (from c in context.Users where c.Username == username select c).FirstOrDefault();
            if (aux != null)
            {
                throw new Exception("Username existent! Incearca altul");
            }

            Users newUser = new Users
            { 
                Username = username, 
                Parola = password,
                Email = email,
                Telefon = telefon,
                Nume = nume,    
                Prenume = prenume   
            };

            context.Users.Add(newUser);
            
            if(context.SaveChanges()!=1)
            {
               throw new Exception("Inregistrare esuata! Try again!");
         
            }
       } }
        private void Shutdown(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
