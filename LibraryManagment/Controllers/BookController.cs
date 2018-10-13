using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagment.Data.Interfaces;
using LibraryManagment.Data.Model;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagment.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        public IActionResult List(Guid? authorId, Guid? borrowerId)
        {
            if(authorId==null && borrowerId == null)
            {
                //show all books
                var books = _bookRepository.GetAllWithAuthor();
                return CheckBooks(books);
            }
            else if (authorId != null)
            {
                var author = _authorRepository.GetWithBooks((Guid)authorId);
                if (author.Books.Count() == 0)
                {
                    return View("AuthorEmpty");
                }
                return View(author.Books);

            }
            else if(borrowerId!=null)
            {
                var books = _bookRepository
                    .FindWithAuthorAndBorrower(book => book.BorrowerId == borrowerId);
                return CheckBooks(books);
            }
            else
            {
                //throw exception
                throw new ArgumentException();
            }
        }

        public IActionResult CheckBooks(IEnumerable<Book> books)
        {
            if (books.Count() == 0)
            {
                return View("Empty");
            }
            return View(books);
        }
    }
}
