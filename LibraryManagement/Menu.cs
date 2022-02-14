using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LibraryManagement
{
    public class Menu
    {
        private string _Title { get; set; }

        private readonly List<MenuItem> _Items;

        public Menu(string title)
        {
            _Items = new List<MenuItem>();
            _Title = title;
        }

        public void AddMenuItem(int id, string description)
        {
            var item = new MenuItem(id, description);
            _Items.Add(item);          
        }

        public int Execute()
        {
            do
            {
                Render();
                Console.Write("Choose a option. => " );
                int id = int.Parse(Console.ReadLine());
                var isInMenu = _Items.FirstOrDefault(x => x.Id == id);
                if (isInMenu == null)
                {
                    Console.WriteLine("Wrong choose.");
                    Thread.Sleep(2000);
                }
                else
                {
                    return id;
                }                
            }             
            while (true);
        }

        private void Render()
        {
            Console.Clear();
            Console.WriteLine(_Title);
            foreach (var item in _Items) 
            {
                System.Console.WriteLine(item.Text);
            }
        }
    }
}
