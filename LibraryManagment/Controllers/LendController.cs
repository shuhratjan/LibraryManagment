using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagment.Data.Interfaces;
using LibraryManagment.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers
{
    public class LendController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public LendController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }
        [Route("Lend")]
        public IActionResult List()
        {
            var availableBooks = _bookRepository.FindWithAuthor(x => x.BorrowerId == Guid.Empty);
            if (availableBooks.Count() == 0)
                return View("Empty");

            return View(availableBooks);
        }

        public IActionResult LendBook(Guid id)
        {
            var lendVM = new LendViewModel()
            {
                Book = _bookRepository.GetById(id),
                Customers = _customerRepository.GetAll()
            };
            return View(lendVM);
        }

        [HttpPost]
        public IActionResult LendBook(LendViewModel lendVM)
        {
            var book = _bookRepository.GetById(lendVM.Book.BookId);
            var customer = _customerRepository.GetById(lendVM.Book.BorrowerId);
            book.Borrower = customer;
            _bookRepository.Update(book);

            return RedirectToAction("List");    
        }
    }
}
