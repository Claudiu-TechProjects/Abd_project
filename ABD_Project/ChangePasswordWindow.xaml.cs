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

namespace ABD_Project
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            
            string oldPassword = oldPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            if (oldPassword != CurrentUser.user.Parola)
            {
                MessageBox.Show("Vechea parola este incorecta.");
                return;
            }

            if (oldPassword == newPassword)
            {
                MessageBox.Show("Vechea parola nu poate fi identica cu noua parola.");
                return;
            }

            using (var context = new BookingEntities())
            {
                var user = context.Users.Where(u => u.Username == CurrentUser.user.Username && u.Parola == oldPassword).ToList();
                user.ForEach(u => u.Parola = newPassword);
                context.SaveChanges();
            }

            MessageBox.Show("Password changed successfully!");
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
