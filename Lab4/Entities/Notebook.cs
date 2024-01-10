using System;
using System.Collections.Generic;
using System.Linq;
using Lab4.Models;

namespace Lab4.Entities;

public class Notebook : INotebook
{
    private readonly List<NotebookEntry> _archive;

    public Notebook(List<NotebookEntry>? archive = null)
    {
        _archive = archive ?? new List<NotebookEntry>();
    }

    public ICollection<NotebookEntry> GetArchive() => _archive;

    public void AddContact(
        string name,
        string surname,
        string phoneNumber,
        string email)
    {
        _archive.Add(new NotebookEntry(name, surname, phoneNumber, email, _archive.Count + 1));
    }

    public NotebookEntry? FindContactByName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        return _archive.FirstOrDefault(entry => entry.Name == name);
    }

    public NotebookEntry? FindContactBySurname(string surname)
    {
        if (surname is null) throw new ArgumentNullException(nameof(surname));

        return _archive.FirstOrDefault(entry => entry.Surname == surname);
    }

    public NotebookEntry? FindContactByPhoneNumber(string phoneNumber)
    {
        if (phoneNumber is null) throw new ArgumentNullException(nameof(phoneNumber));

        return _archive.FirstOrDefault(entry => entry.PhoneNumber == phoneNumber);
    }

    public NotebookEntry? FindContactByEmail(string email)
    {
        if (email is null) throw new ArgumentNullException(nameof(email));

        return _archive.FirstOrDefault(entry => entry.Email == email);
    }

    public NotebookEntry? FindContact(
        string name,
        string surname,
        string phoneNumber,
        string email)
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (surname is null) throw new ArgumentNullException(nameof(surname));
        if (phoneNumber is null) throw new ArgumentNullException(nameof(phoneNumber));
        if (email is null) throw new ArgumentNullException(nameof(email));

        return _archive.FirstOrDefault(entry => entry.Name == name
                                            && entry.Surname == surname
                                            && entry.PhoneNumber == phoneNumber
                                            && entry.Email == email);
    }
}