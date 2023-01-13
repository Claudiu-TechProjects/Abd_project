using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace ABD_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen("/Images/2.jpeg");
            splashScreen.Show(false);
            Thread.Sleep(1000);

            splashScreen.Close(new TimeSpan(0, 0, 0));

            
        }
    }
}
