using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<LibraryService> StartLibrary();
        Task<LibraryService> StopLibrary();
    }
}
