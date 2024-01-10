using System.Collections.Generic;
using Lab4.Models;

namespace Lab4.Entities;

public interface INotebook
{
    public ICollection<NotebookEntry> GetArchive();
    public void AddContact(
        string name,
        string surname,
        string phoneNumber,
        string email);

    public NotebookEntry? FindContactByName(string name);

    public NotebookEntry? FindContactBySurname(string surname);

    public NotebookEntry? FindContactByPhoneNumber(string phoneNumber);

    public NotebookEntry? FindContactByEmail(string email);

    public NotebookEntry? FindContact(
        string name,
        string surname,
        string phoneNumber,
        string email);
}