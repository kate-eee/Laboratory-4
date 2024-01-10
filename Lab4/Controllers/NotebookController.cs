using System;
using System.Collections.Generic;
using System.Linq;
using Lab4.Data;
using Lab4.Entities;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Lab4.Controllers;

[ApiController]
[Route("api/notebook")]
public class NotebookController : ControllerBase
{
    private readonly INotebook _notebook;
    private readonly NotebookContext _dbContext;

    public NotebookController(INotebook notebook, NotebookContext dbContext)
    {
        _notebook = notebook;
        _dbContext = dbContext;
        var dbContextOptions = new DbContextOptionsBuilder<NotebookContext>()
             .UseSqlite("Data Source=Notebook.db")
             .Options;

        using (dbContext = new NotebookContext(dbContextOptions))
        {
            dbContext.Database.EnsureCreated();
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<NotebookEntryDto>> Get()
    {
        var dtos = _dbContext.Archive.Select(entry => new NotebookEntryDto(entry));
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public ActionResult<NotebookEntryDto> Get(int id)
    {
        var entry = _dbContext.Archive.FirstOrDefault(e => e.Id == id);
        if (entry == null) return NotFound();
        return Ok(new NotebookEntryDto(entry));
    }

    [HttpPost]
    public ActionResult<NotebookEntryDto> Create([FromBody] NotebookEntryDto createDto)
    {
        try
        {
            var entry = _notebook.FindContact(createDto.Name, createDto.Surname, createDto.PhoneNumber, createDto.Email);

            if (entry != null) return Conflict("Contact with the same details already exists.");

            var newEntry = new NotebookEntry(createDto.Name, createDto.Surname, createDto.PhoneNumber, createDto.Email, _dbContext.Archive.Count() + 1);
            _dbContext.Archive.Add(newEntry);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = newEntry.Id }, new NotebookEntryDto(newEntry));
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create contact: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<NotebookEntryDto> Update(int id, [FromBody] NotebookEntryDto updateDto)
    {
        try
        {
            var existingEntry = _dbContext.Archive.FirstOrDefault(e => e.Id == id);

            if (existingEntry == null) return NotFound();

            existingEntry.Name = updateDto.Name;
            existingEntry.Surname = updateDto.Surname;
            existingEntry.PhoneNumber = updateDto.PhoneNumber;
            existingEntry.Email = updateDto.Email;

            _dbContext.SaveChanges();

            return Ok(new NotebookEntryDto(existingEntry));
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update contact: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var entryToRemove = _dbContext.Archive.FirstOrDefault(e => e.Id == id);

            if (entryToRemove == null) return NotFound();

            _dbContext.Archive.Remove(entryToRemove);
            _dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete contact: {ex.Message}");
        }
    }
}
