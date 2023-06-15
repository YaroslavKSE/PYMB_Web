using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PYMB_Web.Pages.ContacsPage
{
    public class IndexModel : PageModel
    {
        private List<Contact> contactsList = new List<Contact>();

        public void OnGet()
        {
            try
            {
                string connectionName = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Contacts;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "SELECT * FORM CONTACTS";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while(reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string name = reader.GetString(1);
                                string surname = reader.GetString(2);
                                string email = reader.GetString(3);
                                string phone = reader.GetString(4);

                                Contact contact = new Contact(id, name, surname, email, phone);
                                contactsList.Add(contact);
                            }
                        }
                    }

                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }
}
