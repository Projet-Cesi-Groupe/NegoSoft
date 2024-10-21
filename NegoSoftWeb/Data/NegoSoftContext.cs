using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Models.Entities;

namespace NegoSoftWeb.Data
{
    public class NegoSoftContext : IdentityDbContext<User>
    {
        public NegoSoftContext(DbContextOptions<NegoSoftContext> options)
            : base(options)
        {
        }

        public DbSet<NegoSoftShared.Models.Entities.Type> Types { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderDetails> CustomerOrderDetails { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<SupplierOrderDetails> SupplierOrderDetails { get; set; }

        //enable sensitive data logging
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuration des relations des tables de la base de données

            // Configuration des relations pour CustomerOrder
            modelBuilder.Entity<CustomerOrder>()
                .HasOne(co => co.Customer) // Une commande client est associée à un client
                .WithMany(c => c.CustomerOrders) // Un client peut passer plusieurs commandes
                .HasForeignKey(co => co.CoCustomerId)   // La clé étrangère du client de la commande
                .OnDelete(DeleteBehavior.Restrict);  // Quand un client est supprimé, on ne supprime pas automatiquement ses commandes

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(co => co.Address) // Une commande client est associée à une adresse
                .WithMany(a => a.CustomerOrders) // Une adresse peut être associée à plusieurs commandes client
                .HasForeignKey(co => co.CoAddressId) // La clé étrangère de l'adresse de la commande
                .OnDelete(DeleteBehavior.Restrict);  // Quand une adresse est supprimée, on ne supprime pas automatiquement les commandes associées

            // Configuration des relations pour SupplierOrder
            modelBuilder.Entity<SupplierOrder>()
                .HasOne(so => so.Supplier) // Une commande fournisseur est associée à un fournisseur
                .WithMany(s => s.SupplierOrders) // Un fournisseur peut passer plusieurs commandes
                .HasForeignKey(so => so.SoSupplierId)  // La clé étrangère du fournisseur de la commande
                .OnDelete(DeleteBehavior.Restrict);  // Quand un fournisseur est supprimé, on ne supprime pas automatiquement ses commandes

            modelBuilder.Entity<SupplierOrder>()
                .HasOne(so => so.Address) // Une commande fournisseur est associée à une adresse
                .WithMany(a => a.SupplierOrders) // Une adresse peut être associée à plusieurs commandes fournisseur
                .HasForeignKey(so => so.SoAddressId) // La clé étrangère de l'adresse de la commande
                .OnDelete(DeleteBehavior.Restrict);  // Quand une adresse est supprimée, on ne supprime pas automatiquement les commandes associées

            // Configuration des relations pour Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier) // Un produit est associé à un fournisseur
                .WithMany(s => s.Products) // Un fournisseur peut proposer plusieurs produits
                .HasForeignKey(p => p.ProSupplierId) // La clé étrangère du fournisseur du produit
                .OnDelete(DeleteBehavior.Restrict);  // Quand un fournisseur est supprimé, on ne supprime pas automatiquement ses produits

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Type) // Un produit est associé à un type
                .WithMany(t => t.Products) // Un type peut être associé à plusieurs produits
                .HasForeignKey(p => p.ProTypeId) // La clé étrangère du type du produit
                .OnDelete(DeleteBehavior.Restrict);  // Quand un type est supprimé, on ne supprime pas automatiquement les produits associés

            // Configuration des relations pour CustomerOrderDetails (lignes de commande client)
            modelBuilder.Entity<CustomerOrderDetails>()
                .HasOne(cod => cod.CustomerOrder) // Un détail de commande client est associé à une commande client
                .WithMany(co => co.CustomerOrderDetails) // Une commande client peut contenir plusieurs détails de commande client
                .HasForeignKey(cod => cod.CodOrderId) // La clé étrangère de la commande client du détail de commande client
                .OnDelete(DeleteBehavior.Restrict);  // Quand une commande client est supprimée, on ne supprime pas automatiquement ses détails de commande client

            modelBuilder.Entity<CustomerOrderDetails>()
                .HasOne(cod => cod.Product) // Un détail de commande client est associé à un produit
                .WithMany(p => p.CustomerOrderDetails) // Un produit peut être associé à plusieurs détails de commande client
                .HasForeignKey(cod => cod.CodProductId) // La clé étrangère du produit du détail de commande client
                .OnDelete(DeleteBehavior.Restrict);  // Quand un produit est supprimé, on ne supprime pas automatiquement ses détails de commande client

            // Configuration des relations pour SupplierOrderDetails (lignes de commande fournisseur)
            modelBuilder.Entity<SupplierOrderDetails>()
                .HasOne(sod => sod.SupplierOrder) // Un détail de commande fournisseur est associé à une commande fournisseur
                .WithMany(so => so.SupplierOrderDetails) // Une commande fournisseur peut contenir plusieurs détails de commande fournisseur
                .HasForeignKey(sod => sod.SodOrderId) // La clé étrangère de la commande fournisseur du détail de commande fournisseur
                .OnDelete(DeleteBehavior.Restrict);  // Quand une commande fournisseur est supprimée, on ne supprime pas automatiquement ses détails de commande fournisseur

            modelBuilder.Entity<SupplierOrderDetails>()
                .HasOne(sod => sod.Product) // Un détail de commande fournisseur est associé à un produit
                .WithMany(p => p.SupplierOrderDetails) // Un produit peut être associé à plusieurs détails de commande fournisseur
                .HasForeignKey(sod => sod.SodProductId) // La clé étrangère du produit du détail de commande fournisseur
                .OnDelete(DeleteBehavior.Restrict);  // Quand un produit est supprimé, on ne supprime pas automatiquement ses détails de commande fournisseur

            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.DefaultAddress) // Un fournisseur a une adresse par défaut
                .WithMany(a => a.Suppliers) // Une adresse peut être associée à plusieurs fournisseurs
                .HasForeignKey(s => s.SupDefaultAddressId) // La clé étrangère de l'adresse par défaut du fournisseur
                .OnDelete(DeleteBehavior.Restrict); // Quand un fournisseur est supprimé, on ne supprime pas automatiquement son adresse par défaut

            // Configuration des relations pour Customer
            modelBuilder.Entity<Customer>()
                .HasOne<User>() // Un Customer a un User
                .WithMany() //  Un User peut être associé à plusieurs Customer
                .HasForeignKey(c => c.CusUserId); // Clé étrangère dans Customer

        }
    }
}
