namespace Contacts.Interfaces;

    public interface IContact
    {
        Guid Id { get; set; }

        string Name { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        string Address { get; set; }

       
        
}
