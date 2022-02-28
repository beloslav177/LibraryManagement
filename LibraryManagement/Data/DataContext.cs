using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class DataContext : DbContext
    {
        private const string connectionString = @"Server=.\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;";

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, AuthorFirstName = "Marcel", AuthorLastName = "Proust", BookName = "In Search of Lost Time" },
                new Book { Id = 2, AuthorFirstName = "James", AuthorLastName = "Joyce", BookName = "Ulysses" },
                new Book { Id = 3, AuthorFirstName = "Miguel", AuthorLastName = "de Cervantes", BookName = "Don Quixote" },
                new Book { Id = 4, AuthorFirstName = "Gabriel", AuthorLastName = "Garcia Marquez", BookName = "One Hundred Years of Solitude" }
            );
        }
    }
}
