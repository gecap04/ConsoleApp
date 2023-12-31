namespace Contacts.Interfaces;

public interface IContactService
{
    bool AddContactToList(IContact contact);
    IEnumerable<IContact> GetContactsFromList();
    IContact? GetContactFromList(string email);
    bool DeleteContactFromList(string email);
}


