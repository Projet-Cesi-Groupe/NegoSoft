using NegoSoftShared.Models.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http.Json;

namespace NegoSoftWPF.WPFViews
{
    public partial class CreateSupplier : Window
    {
        //attributes
        private static readonly HttpClient client = new HttpClient();
        string addressId;
        public CreateSupplier()
        {
            InitializeComponent();

        }
        private async Task CreateAdressAPIRequest()
        {
            if (DelStreetBox.Text != "" &&
                DelCityBox.Text != "" &&
                DelZipBox.Text != "" &&
                DelCountryBox.Text != "" &&
                BilStreetBox.Text != "" &&
                BilCityBox.Text != "" &&
                BilZipBox.Text != "" &&
                BilCountryBox.Text != "")
            {
                string request = "https://localhost:7101/api/Address";
                addressId = Guid.NewGuid().ToString();
                var address = new NegoSoftWeb.Models.ViewModels.AddressViewModel

                {
                    AddDeliveryStreet = DelStreetBox.Text,
                    AddDeliveryCity = DelCityBox.Text,
                    AddDeliveryZipCode = DelZipBox.Text,
                    AddDeliveryCountry = DelCountryBox.Text,
                    AddBillingStreet = BilStreetBox.Text,
                    AddBillingCity = BilCityBox.Text,
                    AddBillingZipCode = BilZipBox.Text,
                    AddBillingCountry = BilCountryBox.Text
                };
                var rnr = JsonContent.Create(address);

                HttpResponseMessage response = await client.PostAsync(request, rnr);
                string responseBody = await response.Content.ReadAsStringAsync();
                Address createdAdress = JsonConvert.DeserializeObject<Address>(responseBody);
                addressId = createdAdress.AddId.ToString();
            }
            else
            {
                MessageBox.Show("Champ adresse manquant");
            }
        }
        private async Task CreateSupplierAPIRequest()
        {
            if (NameBox.Text != "" &&
                PhoneBox.Text != "")
            {
                string request = "https://localhost:7101/api/Supplier";
                var supplier = new NegoSoftShared.Models.Entities.Supplier()
                {
                    SupId = Guid.NewGuid(),
                    SupName = NameBox.Text,
                    SupDefaultAddressId = Guid.Parse(addressId),
                    SupEmail = MailBox.Text,
                    SupPhone = PhoneBox.Text                    
                };

                var rnr = JsonContent.Create(supplier);

                HttpResponseMessage response = await client.PostAsync(request, rnr);
                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }

        private async void CreateSupplierButton(object sender, EventArgs e)
        {
            await CreateAdressAPIRequest();
            await CreateSupplierAPIRequest();
            Close();
        }


        private void DecimalTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextDecimal((sender as TextBox).Text + e.Text);
        }

        private bool IsTextDecimal(string text)
        {
            return decimal.TryParse(text, out _);
        }
        private void NumericTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));

                if (!IsTextDecimal(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
