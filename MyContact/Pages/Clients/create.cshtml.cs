using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace MyContact.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            // Rien à faire ici pour la méthode GET
        }

        public void OnPost()
        {

            // Récupération des données du formulaire
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            try{
                // Chaîne de connexion à la base de données
                string connectionString = "Data Source=MSI;Initial Catalog=MyContact;Integrated Security=True;Encrypt=True;TrustServerCertificate=true;";

                // Ouverture de la connexion à la base de données
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Requête SQL avec des paramètres pour éviter les injections SQL
                    string sql = "INSERT INTO Clients (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address);";

                    // Création de la commande avec les paramètres
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Assignation des valeurs aux paramètres
                        command.Parameters.AddWithValue("@Name", clientInfo.name);
                        command.Parameters.AddWithValue("@Email", clientInfo.email);
                        command.Parameters.AddWithValue("@Phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@Address", clientInfo.address);

                        // Exécution de la commande
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Nombre de lignes affectées : " + rowsAffected);

                        // Affichage d'un message de succès
                        successMessage = "Client ajouté avec succès!";
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                errorMessage = "Une erreur s'est produite lors de l'opération de modification : " + ex.Message;
            }
        }
    }
}
