using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Book
    {
        public int Id { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string BookName { get; set; }

        public bool IsBorrowed { get; set; } = false;

        public string UserNameOfBorrowed { get; set; }

        public User User { get; set; }
    }
}
