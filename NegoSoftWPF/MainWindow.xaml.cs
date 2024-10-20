using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NegoSoftShared.Models.Entities;
using NegoSoftWPF.WPFViews;
using Newtonsoft.Json;
namespace NegoSoftWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Attributs
        private bool displayDetailsButton;
        private string apiUrl;
        private System.Type dataGridType;
        private System.Object selectedItem;
        public MainWindow()
        {
            InitializeComponent();
            LoadProductsFromApi();
            UpdateButtonVisibility();
        }
        private void MenuItem_Produits(object sender, RoutedEventArgs e)
        {
            LoadProductsFromApi();
            displayDetailsButton = false;
            UpdateButtonVisibility();
        }
        private void MenuItem_Clients(object sender, RoutedEventArgs e)
        {
            LoadClientsFromApi();
            displayDetailsButton = false;
            UpdateButtonVisibility();
        }
        private void MenuItem_Fournisseurs(object sender, RoutedEventArgs e)
        {
            LoadSupplierFromApi();
            displayDetailsButton = false;
            UpdateButtonVisibility();
        }
        private void MenuItem_CommandesClient(object sender, RoutedEventArgs e)
        {
            LoadCustomerOrdersFromApi();
            displayDetailsButton = true;
            UpdateButtonVisibility();
        }

        private void MenuItem_CommandesFournisseur(object sender, RoutedEventArgs e)
        {
            LoadSupplierOrdersFromApi();
            displayDetailsButton = true;
            UpdateButtonVisibility();
        }
        private void MenuItem_FamProd(object sender, RoutedEventArgs e)
        {
            LoadTypesFromApi();
            displayDetailsButton = false;
            UpdateButtonVisibility();
        }

        private async void LoadProductsFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/product";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Product> data = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(Product);
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void LoadTypesFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/type";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<NegoSoftShared.Models.Entities.Type> data = JsonConvert.DeserializeObject<List<NegoSoftShared.Models.Entities.Type>>(jsonResponse);
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(NegoSoftShared.Models.Entities.Type);
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void LoadSupplierFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/Supplier";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Supplier> data = JsonConvert.DeserializeObject<List<Supplier>>(jsonResponse);
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(Supplier);
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void LoadClientsFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/Customer";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Customer> data = JsonConvert.DeserializeObject<List<Customer>>(jsonResponse);
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(Customer);
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void LoadCustomerOrdersFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/CustomerOrder";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<CustomerOrder> data = JsonConvert.DeserializeObject<List<CustomerOrder>>(jsonResponse);
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(CustomerOrder);
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void LoadSupplierOrdersFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/SupplierOrder";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<SupplierOrder> data = JsonConvert.DeserializeObject<List<SupplierOrder>>(jsonResponse);
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(SupplierOrder);
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var columnsToShow = new List<string> { "Supplier", "Type", "SupplierOrderDetails", "CustomerOrderDetails", "DefaultAddress", "CustomerOrders", "SupplierOrders", "Products", "Customer", "Address" };

            if (columnsToShow.Contains(e.PropertyName))
            {
                e.Cancel = true;
            }

            e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            switch (dataGridType)
            {
                case System.Type t when t == typeof(CustomerOrder):
                    CreateCustomerOrder createCustomerOrder = new CreateCustomerOrder();
                    bool? resultCusOrd = createCustomerOrder.ShowDialog();
                    break;
                case System.Type t when t == typeof(Customer):
                    CreateCustomer createCustomer = new CreateCustomer();
                    bool? resultCus = createCustomer.ShowDialog();
                    break;
                case System.Type t when t == typeof(Supplier):
                    CreateSupplier createSupplier = new CreateSupplier();
                    bool? resultSupp = createSupplier.ShowDialog();
                    break;
                case System.Type t when t == typeof(Product):
                    CreateProduct createProduct = new CreateProduct();
                    bool? resultProd = createProduct.ShowDialog();
                    break;
                case System.Type t when t == typeof(SupplierOrder):
                    CreateSupplierOrder createSupplierOrder = new CreateSupplierOrder();
                    bool? resultSupOrd = createSupplierOrder.ShowDialog();
                    break;
                case System.Type t when t == typeof(NegoSoftShared.Models.Entities.Type):
                    CreateType createType = new CreateType();
                    bool? resultType = createType.ShowDialog();
                    break;
            }
            refreshDataGrid();

        }
        private void Button_ClickRefresh(object sender, RoutedEventArgs e)
        {
            refreshDataGrid();
        }
        private void UpdateButtonVisibility()
        {
            if (displayDetailsButton)
            {
                DetailsButton.Visibility = Visibility.Visible;
            }
            else
            {
                DetailsButton.Visibility = Visibility.Collapsed;
            }
        }
        private void refreshDataGrid()
        {
            switch (dataGridType)
            {
                case System.Type t when t == typeof(CustomerOrder):
                    LoadCustomerOrdersFromApi();
                    displayDetailsButton = true;
                    break;
                case System.Type t when t == typeof(Customer):
                    LoadClientsFromApi();
                    displayDetailsButton = false;
                    break;
                case System.Type t when t == typeof(Supplier):
                    LoadSupplierFromApi();
                    displayDetailsButton = false;
                    break;
                case System.Type t when t == typeof(Product):
                    LoadProductsFromApi();
                    displayDetailsButton = false;
                    break;
                case System.Type t when t == typeof(SupplierOrder):
                    LoadSupplierOrdersFromApi();
                    displayDetailsButton = true;
                    break;
                case System.Type t when t == typeof(NegoSoftShared.Models.Entities.Type):
                    LoadTypesFromApi();
                    displayDetailsButton = false;
                    break;
            }
            UpdateButtonVisibility();
        }
        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            switch (dataGridType)
            {
                case System.Type t when t == typeof(CustomerOrder) && selectedItem != null:
                    string selectedCusOrder = ((CustomerOrder)selectedItem).CoId.ToString();
                    Guid selectedCusOrderGuid = Guid.Parse(selectedCusOrder);
                    EditCustomerOrderWindow editOrderWindow = new EditCustomerOrderWindow(selectedCusOrderGuid);
                    bool? resultCusOrder = editOrderWindow.ShowDialog();
                    break;
                case System.Type t when t == typeof(Customer) && selectedItem != null:
                    string selectedCustomer = ((Customer)selectedItem).CusId.ToString();
                    Guid selectedCustomerGuid = Guid.Parse(selectedCustomer);
                    EditCustomerWindow editCustomer = new EditCustomerWindow(selectedCustomerGuid);
                    bool? resultCus = editCustomer.ShowDialog();
                    break;
                case System.Type t when t == typeof(Supplier) && selectedItem != null:
                    string selectedSupplier = ((Supplier)selectedItem).SupId.ToString();
                    Guid selectedSupplierGuid = Guid.Parse(selectedSupplier);
                    EditSupplierWindow editSupplier = new EditSupplierWindow(selectedSupplierGuid);
                    bool? resultSup = editSupplier.ShowDialog();
                    break;
                case System.Type t when t == typeof(Product) && selectedItem != null:
                    string selectedProduct = ((Product)selectedItem).ProId.ToString();
                    Guid selectedProductGuid = Guid.Parse(selectedProduct);
                    EditProductWindow editProduct = new EditProductWindow(selectedProductGuid);
                    bool? resultProd = editProduct.ShowDialog();
                    break;
                case System.Type t when t == typeof(SupplierOrder) && selectedItem != null:
                    string selectedSupOrder = ((SupplierOrder)selectedItem).SoId.ToString();
                    Guid selectedSupOrderGuid = Guid.Parse(selectedSupOrder);
                    EditSupplierOrderWindow editSupOrder = new EditSupplierOrderWindow(selectedSupOrderGuid);
                    bool? resultSupOrder = editSupOrder.ShowDialog();
                    break;
                case System.Type t when t == typeof(NegoSoftShared.Models.Entities.Type) && selectedItem != null:
                    string selectedType = ((NegoSoftShared.Models.Entities.Type)selectedItem).TypId.ToString();
                    Guid selectedTypeGuid = Guid.Parse(selectedType);
                    EditTypeWindow editType = new EditTypeWindow(selectedTypeGuid);
                    bool? resultType = editType.ShowDialog();
                    break;
            }
            refreshDataGrid();
        }
        private async void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            switch (dataGridType)
            {
                case System.Type t when t == typeof(CustomerOrder) && selectedItem != null:
                    await DeleteCustomerOrder();
                    break;
                case System.Type t when t == typeof(Customer) && selectedItem != null:
                    await DeleteCustomer();
                    break;
                case System.Type t when t == typeof(Supplier) && selectedItem != null:
                    await DeleteSupplier(); ;
                    break;
                case System.Type t when t == typeof(Product) && selectedItem != null:
                    await DeleteProduct();
                    break;
                case System.Type t when t == typeof(SupplierOrder) && selectedItem != null:
                    await DeleteSupplierOrder();
                    break;
                case System.Type t when t == typeof(NegoSoftShared.Models.Entities.Type) && selectedItem != null:
                    await DeleteType();
                    break;
            }
            refreshDataGrid();
        }

        private async Task DeleteProduct()
        {
            try
            {
                string selectedProduct = ((Product)selectedItem).ProId.ToString();
                apiUrl = "https://localhost:7101/api/Product/" + selectedProduct;

                using var client = new HttpClient();
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string resultMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(resultMessage);
                    }
                    else
                    {
                        MessageBox.Show("Echec de la suppression");
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression du produit : {ex.Message}");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }

        private async Task DeleteType()
        {
            try
            {
                string selectedType = ((NegoSoftShared.Models.Entities.Type)selectedItem).TypId.ToString();
                apiUrl = "https://localhost:7101/api/Type/" + selectedType;

                using var client = new HttpClient();
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Famille produit supprimée avec succès");
                    }
                    else
                    {
                        MessageBox.Show("La famille produit n'a pas pus être supprimée car des produits lui sont associées.");
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression de la famille produit : {ex.Message}");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        private async void ButtonDetails(object sender, RoutedEventArgs e)
        {
            switch (dataGridType)
            {
                case System.Type t when t == typeof(CustomerOrder) && selectedItem != null:
                    string selectedCusOrder = ((CustomerOrder)selectedItem).CoId.ToString();
                    Guid selectedCusOrderGuid = Guid.Parse(selectedCusOrder);
                    CustomerOrderDetailsWindow customerOrderDetailsWindow = new CustomerOrderDetailsWindow(selectedCusOrderGuid);
                    bool? resultCusOrder = customerOrderDetailsWindow.ShowDialog();
                    break;
                case System.Type t when t == typeof(SupplierOrder) && selectedItem != null:
                    string selectedSupOrder = ((SupplierOrder)selectedItem).SoId.ToString();
                    Guid selectedSupOrderGuid = Guid.Parse(selectedSupOrder);
                    SupplierOrderDetailsWindow supplierOrderDetailsWindow = new SupplierOrderDetailsWindow(selectedSupOrderGuid);
                    bool? resultSupOrder = supplierOrderDetailsWindow.ShowDialog();
                    break;
            }
        }
        private async Task DeleteCustomer()
        {
            try
            {
                string selectedCustomer = ((Customer)selectedItem).CusId.ToString();
                apiUrl = "https://localhost:7101/api/Customer/" + selectedCustomer;
                using (var client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Client supprimé avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Le client n'a pas pu être supprimé car des commandes lui sont associées");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression du client : {ex.Message}");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async Task DeleteSupplier()
        {
            try
            {
                string selectedSupplier = ((Supplier)selectedItem).SupId.ToString();
                apiUrl = "https://localhost:7101/api/Supplier/" + selectedSupplier;
                using (var client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Fournisseur supprimé avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Le fournisseur n'a pas pu être supprimé car des commandes lui sont associées");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression du fournisseur : {ex.Message}");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task DeleteCustomerOrder()
        {
            try
            {
                string selectedOrder = ((CustomerOrder)selectedItem).CoId.ToString();
                apiUrl = "https://localhost:7101/api/CustomerOrder/" + selectedOrder;
                using (var client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Commande supprimée avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Echec de la suppression");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression de la commande : {ex.Message}");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task DeleteSupplierOrder()
        {
            try
            {
                string selectedOrder = ((SupplierOrder)selectedItem).SoId.ToString();
                apiUrl = "https://localhost:7101/api/SupplierOrder/" + selectedOrder;
                using (var client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Commande supprimée avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Echec de la suppression");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression de la commande : {ex.Message}");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dataTab_selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = dataTab.SelectedItem;
        }
    }

}


