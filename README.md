# NegoSoft

# Instructions de Configuration

Pour configurer l'application, vous devez créer un fichier `.env` à la racine du projet et y ajouter les variables d'environnement nécessaires. Suivez les étapes ci-dessous pour configurer correctement votre environnement.

Il est nécessaire d'ajouter ce fichier sur le projet NegoSoftAPI et NegoSoftWeb.

## Étapes à suivre

1. **Créer le fichier `.env`**

   À la racine de votre projet, créez un fichier nommé `.env`. Ce fichier ne doit pas être suivi par Git (il est inclus dans `.gitignore` pour éviter de partager des informations sensibles).

   ```bash
   touch .env
   
# .env à créer
Ajouter et compléter cette ligne.
```bash
DB_CONNECTION_STRING="ConnexionString"
```
2. **Créer la base de données**
   
Pour créer la base de données, vous devez exécuter les migrations.
Utilisez la commande suivante dans le terminal :
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
Si vous avez installé les outils Entity Framework Core, vous pouvez également
utiliser le gestionnaire de packages pour ajouter une migration :
```bash
Add-Migration InitialCreate
```
Ensuite, appliquez les migrations en utilisant :
```bash
Update-Database
```
Assurez-vous d'avoir installé les outils Entity Framework Core et
que la chaîne de connexion est correcte avant d'exécuter les commandes.
