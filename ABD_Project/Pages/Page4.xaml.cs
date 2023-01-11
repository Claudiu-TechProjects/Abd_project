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
        public Page4()
        {
            InitializeComponent();
            NameUser.Content = CurrentUser.user.Prenume;
        }
    }
}
