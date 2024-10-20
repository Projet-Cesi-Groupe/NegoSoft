using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
    /// Logique d'interaction pour CreateCustomer.xaml
    /// </summary>
    public partial class CreateCustomer : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public CreateCustomer()
        {
            InitializeComponent();
            LoadUsersAsync();
        }

        public async Task LoadUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Customer/users/ids";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonResponse);
                        UserIdComboBox.ItemsSource = users;
                        UserIdComboBox.DisplayMemberPath = "UserName";
                        UserIdComboBox.SelectedValuePath = "Id";
                        UserIdComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des utilisateurs.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var customerViewModel = new CustomerViewModel
            {
                CusFirstName = FirstNameTextBox.Text,
                CusLastName = LastNameTextBox.Text,
                CusEmail = EmailTextBox.Text,
                CusPhone = PhoneTextBox.Text,
                CusUserId = (UserIdComboBox.SelectedItem as User)?.Id.ToString()
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7101/api/customer", customerViewModel);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Client créé avec succès !");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création du client: {ex.Message}");
            }
        }
    }
}
