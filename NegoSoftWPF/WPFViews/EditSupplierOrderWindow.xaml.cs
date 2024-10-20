using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using NegoSoftShared.Models.ViewModels;
using NegoSoftShared.Models.Entities;

namespace NegoSoftWPF.WPFViews
{
    public partial class EditSupplierOrderWindow : Window
    {
        private readonly Guid _orderId;
        private readonly HttpClient _httpClient;

        public EditSupplierOrderWindow(Guid orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            _httpClient = new HttpClient();
            LoadOrderData();
        }

        private async void LoadOrderData()
        {
            try
            {
                // Charger les détails de la commande
                var order = await _httpClient.GetFromJsonAsync<SupplierOrderViewModel>($"https://localhost:7101/api/SupplierOrder/{_orderId}");
                await LoadSuppliersAsync();
                await LoadAddressesAsync();
                await LoadOrderDetailsAsync();

                if (order != null)
                {
                    SupplierIdComboBox.SelectedValue = order.SoSupplierId;
                    AddressIdComboBox.SelectedValue = order.SoAddressId;
                    OrderStateTextBox.Text = order.SoState;
                    OrderTotalTextBox.Text = order.SoTotal.ToString();
                }
                else
                {
                    MessageBox.Show("Commande introuvable", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadSuppliersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Supplier";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonResponse);

                        SupplierIdComboBox.ItemsSource = suppliers;
                        SupplierIdComboBox.DisplayMemberPath = "SupName";
                        SupplierIdComboBox.SelectedValuePath = "SupId";
                        SupplierIdComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des Fournisseurs.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
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

        public async Task LoadOrderDetailsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://localhost:7101/api/SupplierOrderDetails/supplierorder/{_orderId}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<SupplierOrderDetails> orderDetails = JsonConvert.DeserializeObject<List<SupplierOrderDetails>>(jsonResponse);
                        OrderDetailsDataGrid.ItemsSource = orderDetails;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des détails de la commande.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }


        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updatedOrder = new SupplierOrderViewModel
                {
                    SoSupplierId = (Guid)SupplierIdComboBox.SelectedValue,
                    SoAddressId = (Guid)AddressIdComboBox.SelectedValue,
                    SoState = OrderStateTextBox.Text,
                    SoTotal = float.Parse(OrderTotalTextBox.Text),
                    SoDate = DateTime.Now
                };

                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7101/api/SupplierOrder/{_orderId}", updatedOrder);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Commande mise à jour avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    foreach (var item in OrderDetailsDataGrid.Items)
                    {
                        if (item is SupplierOrderDetails orderDetail)
                        {
                            var detailResponse = await _httpClient.PutAsJsonAsync(
                                $"https://localhost:7101/api/SupplierOrderDetails/{orderDetail.SodId}", orderDetail);

                            if (!detailResponse.IsSuccessStatusCode)
                            {
                                MessageBox.Show($"Erreur lors de la mise à jour du détail de la commande ID: {orderDetail.SodId}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise à jour de la commande", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SupplierIdSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SupplierIdComboBox.SelectedItem is Supplier selectedSupplier)
            {
                SupplierNameTextBlock.Text = selectedSupplier.SupName;
                SupplierEmailTextBlock.Text = selectedSupplier.SupEmail;
                SupplierPhoneTextBlock.Text = selectedSupplier.SupPhone;
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
