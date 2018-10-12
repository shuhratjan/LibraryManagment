using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagment.Data.Model;

namespace LibraryManagment.Data.Interfaces
{
    public interface IAuthorRepository: IRepository<Author>
    {
        IEnumerable<Author> GetAllWithBooks();
        Author GetWithBooks(Guid id);
    }
}
