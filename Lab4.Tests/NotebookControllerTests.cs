using Lab4.Controllers;
using Lab4.Data;
using Lab4.Entities;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace Lab4.Tests;

public class NotebookControllerTests
{
    [Fact]
    public void Get_ReturnsOkResultWithListOfNotebookEntryDtos()
    {
        var dbContextOptions = new DbContextOptionsBuilder<NotebookContext>()
            .UseInMemoryDatabase(databaseName: "Get_ReturnsOkResultWithListOfNotebookEntryDtos")
            .Options;

        using (var dbContext = new NotebookContext(dbContextOptions))
        {
            dbContext.Archive.Add(new NotebookEntry { Id = 1, Name = "John", Surname = "Doe", PhoneNumber = "123", Email = "john@example.com" });
            dbContext.SaveChanges();
        }

        using (var dbContext = new NotebookContext(dbContextOptions))
        {
            var notebook = new Notebook(dbContext.Archive.ToList());
            var controller = new NotebookController(notebook, dbContext);
            
            var result = controller.Get();
            
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var dtos = Assert.IsType<Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<NotebookEntryDto>>(okResult.Value);
            Assert.Single(dtos);
        }
    }

    [Fact]
    public void Get_WithValidId_ReturnsOkResultWithNotebookEntryDto()
    {
        var dbContextOptions = new DbContextOptionsBuilder<NotebookContext>()
            .UseInMemoryDatabase(databaseName: "Get_WithValidId_ReturnsOkResultWithNotebookEntryDto")
            .Options;

        using (var dbContext = new NotebookContext(dbContextOptions))
        {
            dbContext.Archive.Add(new NotebookEntry { Id = 1, Name = "John", Surname = "Doe", PhoneNumber = "123", Email = "john@example.com" });
            dbContext.SaveChanges();
        }

        using (var dbContext = new NotebookContext(dbContextOptions))
        {
            var notebook = new Notebook(dbContext.Archive.ToList());
            var controller = new NotebookController(notebook, dbContext);
            
            var result = controller.Get(1);
            
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var dto = Assert.IsType<NotebookEntryDto>(okResult.Value);
            Assert.Equal(1, dto.Id);
        }
    }
}