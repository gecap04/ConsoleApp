using Contacts.Interfaces;
using Contacts.Models;
using Contacts.Services;

namespace Contacts.ConsoleApp.Services;

public interface IMenuService
{
    void MainMenu();
  

}

internal class MenuService : IMenuService
{

    private static readonly IContactService _contactService = new ContactService();

    public void MainMenu()
    {
        while (true)
        {
            DisplayMenuTitle("MENU OPTIONS");
            Console.WriteLine($"{"1.",-4} Add New Contact");
            Console.WriteLine($"{"2.",-4} View Contact List");
            Console.WriteLine($"{"3.",-4} View Specific Contact");
            Console.WriteLine($"{"4.",-4} Delete Contact");
            Console.WriteLine($"{"0.",-4} Exit Option");
            Console.WriteLine();
            Console.Write("Enter Menu Option: ");
            var option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    AddContactMenu();
                    break;
                case "2":
                    ShowAllContactsMenu(); 
                    break;
                case "3":
                    GetContactFromList();
                    break;
                case "4":
                    DeleteContactMenu();
                    break;

                case "0":
                    ShowExitApplication();
                    break;
                default:
                    Console.WriteLine("\nInvalid Input. Please try again.");
                    break;

            }

            Console.ReadKey();
        }
    }

    private void ShowExitApplication ()
    {
        Console.Clear();
        Console.Write("Are you sure you wnat to close this application? (y/n): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals( "y", StringComparison.OrdinalIgnoreCase))
            Environment.Exit(0);
           
    }

    public static void AddContactMenu()
    {
        IContact contact = new Contact();

        Console.Write("Insert first name: ");
        contact.Name = Console.ReadLine()!;

        Console.Write("Insert last name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("Insert phone number: ");
        contact.PhoneNumber = Console.ReadLine()!;

        Console.Write("Insert email: ");
        contact.Email = Console.ReadLine()!;

        Console.Write("Insert address: ");
        contact.Address = Console.ReadLine()!;

        _contactService.AddContactToList(contact);
    }

    public static void ShowAllContactsMenu()
    {
        var contacts = _contactService.GetContactsFromList();
        foreach (var contact in contacts)
        {
            if (contact is IContact c)
            {
                Console.WriteLine($"{c.Name} {c.LastName}");
                Console.WriteLine($" {c.PhoneNumber}");
                Console.WriteLine($" {c.Email}");
                Console.WriteLine($" {c.Address}");
                Console.WriteLine();
            }
                
        }
    }

    public static void GetContactFromList()
    {
        Console.Write("Insert email to view specific contact: ");
        string emailToFind = Console.ReadLine() ?? "";

        var contact = _contactService.GetContactFromList(emailToFind);

        if (contact != null)
        {
            Console.WriteLine($"Contact found:");
            Console.WriteLine($"Name: {contact.Name} {contact.LastName}");
            Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Address: {contact.Address}");
        }
        else
        {
            Console.WriteLine($"Contact with email '{emailToFind}' not found.");
        }
    }




    public static void DeleteContactMenu()
    {
        Console.Write("Insert email to delete the contact: ");
        string emailToDelete = Console.ReadLine() ?? "";

        bool deleteResult = _contactService.DeleteContactFromList(emailToDelete);
    }






    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"## {title} ##");
        Console.WriteLine();
    }

}
