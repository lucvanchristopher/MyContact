using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace MyContact.Pages.Clients
{
    public class createModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        // add a new client in the database
        public void Onpost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];


            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "Please fill all the fields";
                return;
            }
            //save the new client into the database

            try
            {
                String conncetionString = "Data Source=MSI;Initial Catalog=MyContact;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(conncetionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Clients (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", clientInfo.name);
                        command.Parameters.AddWithValue("@Email", clientInfo.email);
                        command.Parameters.AddWithValue("@Phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@Address", clientInfo.address);
                        command.ExecuteNonQuery();
                    }
                }
            }
            //catch(Exception ex) 
            //{ 
            //    errorMessage = ex.Message;
            catch (Exception ex)
            {
                Console.WriteLine("Exception lors de l'insertion du client : " + ex.Message);
                errorMessage = "Une erreur s'est produite lors de la création du client.";


            }

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMessage = "Client added successfully";

            Response.Redirect("/Clients/Index");
        }
    }
}