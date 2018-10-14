using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagment.Data.Interfaces;
using LibraryManagment.Data.Model;
using LibraryManagment.ViewModel;
using Microsoft.AspNetCore.Mvc;
namespace LibraryManagment.Controllers
{
    public class AuthorController: Controller
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [Route("Author")]
        public IActionResult List()
        {
            var authors = _authorRepository.GetAllWithBooks();
            if (authors.Count() == 0)
                return View("Empty");
            return View(authors);
        }

        public IActionResult Update(Guid id)
        {
            var author = _authorRepository.GetById(id);

            if (author == null)
                return NotFound();

            return View(author);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            _authorRepository.Update(author);

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            var viewVM = new CreateAuthorViewModel
            {
                Referer = Request.Headers["Referer"].ToString()
            };
            return View(viewVM);
        }

        [HttpPost]
        public IActionResult Create(CreateAuthorViewModel authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }

            _authorRepository.Create(authorVM.Author);

            if (!String.IsNullOrEmpty(authorVM.Referer))
            {
                return Redirect(authorVM.Referer); 
            }

            return RedirectToAction("List");
        }

        public IActionResult Delete(Guid id)
        {
            var author = _authorRepository.GetById(id);
            _authorRepository.Delete(author);
            return RedirectToAction("List");
        }
    }
}
