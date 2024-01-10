using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data;

public class NotebookContext : DbContext
{
    public DbSet<NotebookEntry> Archive { get; set; }
    
    public NotebookContext(DbContextOptions<NotebookContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotebookEntry>()
            .HasKey(e => e.Id);
    }
}