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
            // Rien � faire ici pour la m�thode GET
        }

        public void OnPost()
        {

            // R�cup�ration des donn�es du formulaire
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            try{
                // Cha�ne de connexion � la base de donn�es
                string connectionString = "Data Source=MSI;Initial Catalog=MyContact;Integrated Security=True;Encrypt=True;TrustServerCertificate=true;";

                // Ouverture de la connexion � la base de donn�es
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Requ�te SQL avec des param�tres pour �viter les injections SQL
                    string sql = "INSERT INTO Clients (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address);";

                    // Cr�ation de la commande avec les param�tres
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Assignation des valeurs aux param�tres
                        command.Parameters.AddWithValue("@Name", clientInfo.name);
                        command.Parameters.AddWithValue("@Email", clientInfo.email);
                        command.Parameters.AddWithValue("@Phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@Address", clientInfo.address);

                        // Ex�cution de la commande
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Nombre de lignes affect�es : " + rowsAffected);

                        // Affichage d'un message de succ�s
                        successMessage = "Client ajout� avec succ�s!";
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                errorMessage = "Une erreur s'est produite lors de l'op�ration de modification : " + ex.Message;
            }
        }
    }
}
