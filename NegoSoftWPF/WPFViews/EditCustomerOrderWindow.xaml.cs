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
    public partial class EditCustomerOrderWindow : Window
    {
        private readonly Guid _orderId;
        private readonly HttpClient _httpClient;

        public EditCustomerOrderWindow(Guid orderId)
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
                var order = await _httpClient.GetFromJsonAsync<CustomerOrderViewModel>($"https://localhost:7101/api/CustomerOrder/{_orderId}");
                await LoadCustomersAsync();
                await LoadAddressesAsync();
                await LoadOrderDetailsAsync();

                if (order != null)
                {
                    CustomerIdComboBox.SelectedValue = order.CoCustomerId;
                    AddressIdComboBox.SelectedValue = order.CoAddressId;
                    OrderStateTextBox.Text = order.CoState;
                    OrderTotalTextBox.Text = order.CoTotal.ToString();
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

        public async Task LoadCustomersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Customer";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(jsonResponse);

                        CustomerIdComboBox.ItemsSource = customers;
                        CustomerIdComboBox.DisplayMemberPath = "CusId";
                        CustomerIdComboBox.SelectedValuePath = "CusId";   
                        CustomerIdComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des Clients.");
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
                    string apiUrl = $"https://localhost:7101/api/CustomerOrderDetails/customerorder/{_orderId}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<CustomerOrderDetails> orderDetails = JsonConvert.DeserializeObject<List<CustomerOrderDetails>>(jsonResponse);
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
                var updatedOrder = new CustomerOrderViewModel
                {
                    CoCustomerId = (Guid)CustomerIdComboBox.SelectedValue,
                    CoAddressId = (Guid)AddressIdComboBox.SelectedValue,
                    CoState = OrderStateTextBox.Text,
                    CoTotal = float.Parse(OrderTotalTextBox.Text),
                    CoDate = DateTime.Now
                };

                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7101/api/CustomerOrder/{_orderId}", updatedOrder);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Commande mise à jour avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    foreach (var item in OrderDetailsDataGrid.Items)
                    {
                        if (item is CustomerOrderDetails orderDetail)
                        {
                            var detailResponse = await _httpClient.PutAsJsonAsync(
                                $"https://localhost:7101/api/CustomerOrderDetails/{orderDetail.CodId}",
                                orderDetail);

                            if (!detailResponse.IsSuccessStatusCode)
                            {
                                MessageBox.Show($"Erreur lors de la mise à jour du détail de la commande ID: {orderDetail.CodId}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void CustomerIdSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerIdComboBox.SelectedItem is Customer selectedCustomer)
            {
                CustomerNameTextBlock.Text = $"{selectedCustomer.CusFirstName} {selectedCustomer.CusLastName}";
                CustomerEmailTextBlock.Text = selectedCustomer.CusEmail;
                CustomerPhoneTextBlock.Text = selectedCustomer.CusPhone;
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
