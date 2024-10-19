using Stripe.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private string _password = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918";
        public adminLogin()
        {
            InitializeComponent();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                StringBuilder builder = new StringBuilder();
                //Le mot de passe est converti en tableau de bytes car la méthode ComputeHash prend en paramètre un tableau de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                bytes = sha256.ComputeHash(bytes);
                foreach (byte b in bytes)
                {
                    //conversion en hexadécimal pour chaque byte du tableau
                    builder.Append(b.ToString("x2"));
                }
                //retourne le mot de passe hashé en majuscule
                return builder.ToString().ToUpper();
            }
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            string enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == hashedPassword;
        }

        private async void Button_Connexion(object sender, RoutedEventArgs e)
        {
            if (VerifyPassword(passBox.Text, _password))
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
