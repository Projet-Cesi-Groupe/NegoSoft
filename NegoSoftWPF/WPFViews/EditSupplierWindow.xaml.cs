using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using Newtonsoft.Json;
using Stripe.Climate;
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
    /// Logique d'interaction pour EditSupplierWindow.xaml
    /// </summary>
    public partial class EditSupplierWindow : Window
    {
        private readonly Guid _supplierId;
        private readonly HttpClient _httpClient;

        public EditSupplierWindow(Guid supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _httpClient = new HttpClient();
            LoadSupplierData();
        }

        private async void LoadSupplierData()
        {
            try
            {
                // Appel à l'API pour obtenir les données du supplier à éditer
                var supplier = await _httpClient.GetFromJsonAsync<SupplierViewModel>($"https://localhost:7101/api/Supplier/{_supplierId}");
                await LoadAddressesAsync();

                if (supplier != null)
                {
                    NameTextBox.Text = supplier.SupName;
                    EmailTextBox.Text = supplier.SupEmail;
                    PhoneTextBox.Text = supplier.SupPhone;
                    AddressIdComboBox.SelectedValue = supplier.SupDefaultAddressId;
                }
                else
                {
                    MessageBox.Show("Supplier not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Création du modèle à partir des champs de la fenêtre
                var updatedSupplier = new SupplierViewModel
                {
                    SupName = NameTextBox.Text,
                    SupEmail = EmailTextBox.Text,
                    SupPhone = PhoneTextBox.Text,
                    SupDefaultAddressId = (Guid)AddressIdComboBox.SelectedValue
                };

                // Appel PUT vers l'API pour mettre à jour le supplier
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7101/api/Supplier/{_supplierId}", updatedSupplier);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Fournisseur mis à jour avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise à jour du fournisseur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating supplier: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadAddressesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Address";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(jsonResponse);
                        AddressIdComboBox.ItemsSource = addresses;
                        AddressIdComboBox.DisplayMemberPath = "AddId";
                        AddressIdComboBox.SelectedValuePath = "AddId";
                        AddressIdComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des Adresses.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }

        private void AddressIdSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddressIdComboBox.SelectedItem is Address selectedAddress)
            {
                string deliveryInfo = $"{selectedAddress.AddDeliveryStreet}, {selectedAddress.AddDeliveryCity}, " +
                                      $"{selectedAddress.AddDeliveryZipCode}, {selectedAddress.AddDeliveryCountry}";
                string billingInfo = $"{selectedAddress.AddBillingStreet}, {selectedAddress.AddBillingCity}, " +
                                     $"{selectedAddress.AddBillingZipCode}, {selectedAddress.AddBillingCountry}";
                DeliveryStreetTextBlock.Text = deliveryInfo;
                BillingStreetTextBlock.Text = billingInfo;
            }
        }
    }
}
