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

namespace NegoSoftWPF.WPFViews
{
    /// <summary>
    /// Logique d'interaction pour adminLogin.xaml
    /// </summary>
    public partial class adminLogin : Window
    {
        private string password = "admin";
        public adminLogin()
        {
            InitializeComponent();
        }
        private async void Button_Connexion(object sender, RoutedEventArgs e)
        {
            if(passBox.Text == password)
            {
                MessageBox.Show("Correct password");
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect password");
            }
                  
        }
    }
}
