using LibraryManagment.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReturnController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }
        [Route("Return")]
        public IActionResult List()
        {
            var borrowedBooks = _bookRepository.FindWithAuthorAndBorrower(x => x.BorrowerId != Guid.Empty);

            if (borrowedBooks == null || borrowedBooks.ToList().Count() == 0)
            {
                return View("Empty");
            }

            return View(borrowedBooks);
        }

        public IActionResult ReturnBook(Guid bookId)
        {
            var book = _bookRepository.GetById(bookId);

            book.Borrower = null;
            book.BorrowerId = Guid.Empty;

            _bookRepository.Update(book);
            return RedirectToAction("List");
        }
    }
}
