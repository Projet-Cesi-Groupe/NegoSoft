using System;
using System.Collections.Generic;
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
    public partial class CustomerOrderDetailsWindow : Window
    {
        private readonly Guid _orderId;
        private readonly HttpClient _httpClient;

        public CustomerOrderDetailsWindow(Guid orderId)
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
                string apiUrl = $"https://localhost:7101/api/CustomerOrder/{_orderId}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                NegoSoftShared.Models.Entities.CustomerOrder order = JsonConvert.DeserializeObject<NegoSoftShared.Models.Entities.CustomerOrder>(jsonResponse);
                await LoadOrderDetailsAsync();

                if (order != null)
                {
                    CustomerNameTextBlock.Text = $"{order.Customer.CusFirstName} {order.Customer.CusLastName}";
                    CustomerEmailTextBlock.Text = order.Customer.CusEmail;
                    CustomerPhoneTextBlock.Text = order.Customer.CusPhone;

                    string deliveryAddress = $"{order.Address.AddDeliveryStreet}, {order.Address.AddDeliveryCity}, {order.Address.AddDeliveryZipCode}, {order.Address.AddDeliveryCountry}";
                    DeliveryAddressTextBlock.Text = deliveryAddress;

                    string billingAddress = $"{order.Address.AddBillingStreet}, {order.Address.AddBillingCity}, {order.Address.AddBillingZipCode}, {order.Address.AddBillingCountry}";
                    BillingAddressTextBlock.Text = billingAddress;

                    OrderStateTextBlock.Text = order.CoState;
                    OrderDateTextBlock.Text = order.CoDate.ToString();
                    OrderTotalTextBlock.Text = order.CoTotal.ToString();
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
