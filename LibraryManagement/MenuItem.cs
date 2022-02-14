namespace LibraryManagement
{
    public class MenuItem
    {
        public int Id { get; set; }

        private string _Description;

        public MenuItem(int id, string description)
        {
            Id = id;
            _Description = description;
        }

        public string Text => $"{Id}:  {_Description}";
    }
}
