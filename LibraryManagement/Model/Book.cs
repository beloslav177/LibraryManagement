namespace Library.Model
{
    public class Book
    {
        public int Id { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorName => AuthorFirstName + " " + AuthorLastName;

        public string BookName { get; set; }

        public User User { get; set; }
    }
}
