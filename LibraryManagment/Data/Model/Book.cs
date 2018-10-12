using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Data.Model
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title{ get; set; }

        public virtual Author Author { get; set; }
        public Guid AuthorId { get; set; }

        public virtual Customer Borrower { get; set; }
        public Guid BorrowerId { get; set; }
    }
}