namespace Lab4.Models;

public class NotebookEntry // запись
{
    public NotebookEntry()
    {
    }

    public NotebookEntry(string name, string surname, string phoneNumber, string email, int id)
    {
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Email = email;
        Id = id;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}