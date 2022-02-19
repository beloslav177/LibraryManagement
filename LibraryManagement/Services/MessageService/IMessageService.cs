using System;

namespace LibraryManagement.Services.MessageService
{
    public interface IMessageService
    {
        void Exist(string message);
        void NotExist(string message);
        void IsBorrowing(string message);
        void IsNotBorrowing(string message);
        void Error(Exception ex);
        void PressAny();
    }
}
