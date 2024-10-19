using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
    /// Logique d'interaction pour EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        private readonly Guid _customerId;
        private readonly HttpClient _httpClient;

        public EditCustomerWindow(Guid customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            _httpClient = new HttpClient();
            LoadCustomerData();
        }

        private async void LoadCustomerData()
        {
            try
            {
                // Appel à l'API pour obtenir les données du client à éditer
                var customer = await _httpClient.GetFromJsonAsync<CustomerViewModel>($"https://localhost:7101/api/Customer/{_customerId}");
                await LoadUsersAsync();

                if (customer != null)
                {
                    FirstNameTextBox.Text = customer.CusLastName;
                    LastNameTextBox.Text = customer.CusFirstName;
                    EmailTextBox.Text = customer.CusEmail;
                    PhoneTextBox.Text = customer.CusPhone;
                    UserIdComboBox.SelectedValue = customer.CusUserId;
                }
                else
                {
                    MessageBox.Show("Customer not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Création du modèle à partir des champs de la fenêtre
                var updatedCustomer = new CustomerViewModel
                {
                    CusLastName = FirstNameTextBox.Text,
                    CusFirstName = LastNameTextBox.Text,
                    CusEmail = EmailTextBox.Text,
                    CusPhone = PhoneTextBox.Text,
                    CusUserId = UserIdComboBox.SelectedValue.ToString()
                };

                // Appel PUT vers l'API pour mettre à jour le customer
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7101/api/Customer/{_customerId}", updatedCustomer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Customer updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }
}
