using Contacts.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Contacts.Services;
public class ContactService : IContactService
{

    /// <summary>
    /// Add a contact to list
    /// </summary>
    /// <param name="contact">a contact of type IContact</param>
    /// <returns>Return true if successful, or fales if it fails or contact already exists</returns>
    
    private readonly IFileService _fileService = new FileService();
    private List<IContact> _contacts = [];
    private readonly string _filePath = @"c:\Education\Projects\contacts.json";


    public bool AddContactToList(IContact contact)
    {
        try
        {
            

            if (!_contacts.Any(x => x.Email == contact.Email))
            {
                _contacts.Add(contact);

                string json = JsonConvert.SerializeObject(_contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                var result = _fileService.SaveContentToFile(_filePath, json);
                return result;
            }

            

        }
        catch (Exception ex) { Debug.WriteLine("ContactService - AddContactToList:: " + ex.Message); }
        return false;
    }

    public IContact? GetContactFromList(string email)
    {
        try
        {
            GetContactsFromList();

            var contact = _contacts.FirstOrDefault(x => x.Email == email);
            return contact;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - GetContactFromList:: " + ex.Message);
            return null;
        }
    }

    public IEnumerable<IContact> GetContactsFromList()
    {
        try
        {
            var content = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
                _contacts = JsonConvert.DeserializeObject<List<IContact>>(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
                return _contacts;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - GetContactsFromList:: " + ex.Message); }
        return null!;
    }

    public bool DeleteContactFromList(string email)
    {
        try
        {
            var contactToRemove = _contacts.FirstOrDefault(x => x.Email == email);

            if (contactToRemove != null)
            {
                _contacts.Remove(contactToRemove);

                string json = JsonConvert.SerializeObject(_contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                var result = _fileService.SaveContentToFile(_filePath, json);
                return result;
            }
            else
            {
                Debug.WriteLine($"Contact with email '{email}' not found.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - DeleteContactFromList:: " + ex.Message);
            return false;
        }
    }







}
