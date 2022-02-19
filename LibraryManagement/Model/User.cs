﻿namespace Library.Model
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set;}

        public bool IsBorrowing { get; set; } = false;

        public string UserName => FirstName + " " + LastName;
    }
}
