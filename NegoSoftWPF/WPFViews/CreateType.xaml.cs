using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;


namespace NegoSoftWPF.WPFViews
{
    public partial class CreateType : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public CreateType()
        {
            InitializeComponent();
        }
        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypeLibelleTextBox.Text == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs !");
                return;
            }
            else
            {
                var typeViewModel = new TypeViewModel
                {
                    TypLibelle = TypeLibelleTextBox.Text,
                };

                try
                {
                    var response = await _httpClient.PostAsJsonAsync("https://localhost:7101/api/type", typeViewModel);
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
}
