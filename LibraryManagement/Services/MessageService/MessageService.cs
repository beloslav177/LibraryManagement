using System;

namespace LibraryManagement.Services.MessageService
{
    public class MessageService : IMessageService
    {
        public void Exist(string message)
        {
            Console.WriteLine($"\n {message} is already exist in Library.");
        }

        public void NotExist(string message)
        {
            Console.WriteLine($"\n {message} does'nt exist in Library.");
        }

        public void IsBorrowing(string message)
        {
            Console.WriteLine($"\n{message} is already borrowed, so it's not possible delete.");
        }
         
        public void IsNotBorrowing(string message)
        {
            Console.WriteLine($"\n{message} book is not borrowed.");
        }

        public void Error(Exception ex)
        {
            Console.WriteLine("\nMessage ---\n{0}", ex.Message);
        }

        public void PressAny()
        {
            Console.WriteLine("\n#####################\nPress Any to Continue.");
            Console.ReadKey();
        }
    }
}
