using LibraryMind.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMind.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Books> Book { get; set; }
        public DbSet<Readers> Reader { get; set; }
        public DbSet<Lendings> Lending { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>().ToTable("Book");
            modelBuilder.Entity<Readers>().ToTable("Reader");
            modelBuilder.Entity<Lendings>().ToTable("Lending");
        }
    }
}
