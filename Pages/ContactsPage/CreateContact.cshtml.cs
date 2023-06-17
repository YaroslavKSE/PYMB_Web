using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PYMB_Web.Pages.ContacsPage;
using System.Data.SqlClient;

namespace PYMB_Web.Pages.ContactsPage
{
    public class CreateContactModel : PageModel
    {
        public Contact contact = new Contact();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() { 
        contact.Name = Request.Form["name"];
        contact.Surname = Request.Form["surname"];
        contact.Email = Request.Form["email"];
        contact.Phone = Request.Form["phone"];

            try
            {
                string connectionName = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Contacts;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "INSERT INTO CONTACTS (name, surname, email, phone) VALUES (@name, @surname, @email, @phone);";

                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@name", contact.Name);
                        sqlCommand.Parameters.AddWithValue("@surname", contact.Surname);
                        sqlCommand.Parameters.AddWithValue("@email", contact.Email);
                        sqlCommand.Parameters.AddWithValue("@phone", contact.Phone);

                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            if(contact == null){
                errorMessage = "Something went wrong, pleace try again";
                return;
            }
            contact = new Contact();
            successMessage = "New contact created!";
            Response.Redirect("/ContactsPage/Index");
        }
    }
}
