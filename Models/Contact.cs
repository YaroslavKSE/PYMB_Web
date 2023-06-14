namespace PYMB_Web.Models
{
    public class Contact
    {
        public Contact(int id, string name, string surname, string address, string email)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Address = address;
            Email = email;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
