namespace Lab4.Models;

public class NotebookEntryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public NotebookEntryDto()
    {
    }

    public NotebookEntryDto(NotebookEntry entry)
    {
        Id = entry.Id;
        Name = entry.Name;
        Surname = entry.Surname;
        PhoneNumber = entry.PhoneNumber;
        Email = entry.Email;
    }
}
