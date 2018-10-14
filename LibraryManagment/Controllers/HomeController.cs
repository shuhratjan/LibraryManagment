using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagment.Models;
using LibraryManagment.Data.Interfaces;
using LibraryManagment.ViewModel;

namespace LibraryManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorRepository _authorRepositroy;
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public HomeController(IAuthorRepository authorRepositroy,
                              IBookRepository bookRepository,
                              ICustomerRepository customerRepository)
        {
            _authorRepositroy = authorRepositroy;
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            var homeVM = new HomeViewModel()
            {
                AuthorCount = _authorRepositroy.Count(x => true),
                BookCount = _bookRepository.Count(x => true),
                CustomerCount = _customerRepository.Count(x => true),
                LendBookCount = _bookRepository.Count(x => x.Borrower != null)
            };
            return View(homeVM);
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
