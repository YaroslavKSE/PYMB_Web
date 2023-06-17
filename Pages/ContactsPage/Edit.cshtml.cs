using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PYMB_Web.Pages.ContacsPage;
using System.Data.SqlClient;

namespace PYMB_Web.Pages.ContactsPage
{
    public class EditModel : PageModel
    {
        public Contact contact = new Contact();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
            var id = Request.Query["id"];
            try
            {
                string connectionName = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Contacts;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "SELECT * FROM CONTACTS WHERE id=@id;";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@id", id);
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                contact.Id = Convert.ToInt32(reader["id"]);
                                contact.Name = reader.GetString(1);
                                contact.Surname = reader.GetString(2);
                                contact.Email = reader.GetString(3);
                                contact.Phone = reader.GetString(4);

                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            contact.Name = Request.Form["id"];
            contact.Name = Request.Form["name"];
            contact.Surname = Request.Form["surname"];
            contact.Email = Request.Form["email"];
            contact.Phone = Request.Form["phone"];

            if (contact == null)
            {
                errorMessage = "Something went wrong, pleace try again";
                return;
            }

            try
            {
                string connectionName = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Contacts;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "UPDATE CONTACTS SET name=@name, surname=@surname, email=@email, phone=@phone WHERE id=@id";

                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@name", contact.Name);
                        sqlCommand.Parameters.AddWithValue("@surname", contact.Surname);
                        sqlCommand.Parameters.AddWithValue("@email", contact.Email);
                        sqlCommand.Parameters.AddWithValue("@phone", contact.Phone);
                        sqlCommand.Parameters.AddWithValue("@id", contact.Id);

                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            contact = new Contact();
            successMessage = "Contact updated!";
            Response.Redirect("/ContactsPage/Index");
        }
    }
}

