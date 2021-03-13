using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.DataModel;
using Library.Logic;
using Library.Models;
using System;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Library.Logic.BookLogic BookLogic;

        public BooksController(LibraryContext context)
        {
            BookLogic = new Library.Logic.BookLogic(context);
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetBooks()
        {
            var books = await BookLogic.GetBookList();
            var booksDto = new List<BookDto>();
            foreach (Book book in books)
            {
                booksDto.Add(DtoConvert.BookDtoFromBook(book));
            }
            return booksDto;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await BookLogic.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return DtoConvert.BookDtoFromBook(book);
        }

        // GET: api/Books/ByTitle/Harry Potter
        [HttpGet("ByTitle/{title}")]
        public async Task<IEnumerable<BookDto>> GetBookByTitle(string title)
        {
            var books = await BookLogic.GetBookByPharam(title,"title");
            var booksDto = new List<BookDto>();
            foreach (Book book in books)
            {
                booksDto.Add(DtoConvert.BookDtoFromBook(book));
            }
            return booksDto;
        }
        // GET: api/Books/ByAuthor/Petya
        [HttpGet("ByAuthor/{Author}")]
        public async Task<IEnumerable<BookDto>> GetBookByAuthor(string author)
        {
            var books = await BookLogic.GetBookByPharam(author, "author");
            var booksDto = new List<BookDto>();
            foreach (Book book in books)
            {
                booksDto.Add(DtoConvert.BookDtoFromBook(book));
            }
            return booksDto;
        }
        // GET: api/Books/BySeries/school
        [HttpGet("BySeries/{Series}")]
        public async Task<IEnumerable<BookDto>> GetBookBySeries(string Series)
        {
            var books = await BookLogic.GetBookByPharam(Series,"series");
            var booksDto = new List<BookDto>();
            foreach (Book book in books)
            {
                booksDto.Add(DtoConvert.BookDtoFromBook(book));
            }
            return booksDto;
        }
        // GET: api/Books/ByPublicationYear/2001
        [HttpGet("ByPublicationYear/{PublicationYear}")]
        public async Task<IEnumerable<BookDto>> GetBookByPublicationDate(string PublicationYear)
        {
            var books = await BookLogic.GetBookByPharam(PublicationYear, "publicationYear");
            var booksDto = new List<BookDto>();
            foreach (Book book in books)
            {
                booksDto.Add(DtoConvert.BookDtoFromBook(book));
            }
            return booksDto;
        }
        // GET: api/Books/ByGenre/Horror
        [HttpGet("ByGenre/{Genre}")]
        public async Task<IEnumerable<BookDto>> GetBookByGenre(string genre)
        {
            var books = await BookLogic.GetBookByPharam(genre, "genre");
            var booksDto = new List<BookDto>();
            foreach (Book book in books)
            {
                booksDto.Add(DtoConvert.BookDtoFromBook(book));
            }
            return booksDto;
        }
        // GET: api/Books/ByTag/marked
        [HttpGet("ByTag/{Tag}")]
        public async Task<IEnumerable<BookDto>> GetBookByTag(string tag)
        {
            var books = await BookLogic.GetBookByPharam(tag, "tag");
            var booksDto = new List<BookDto>();
            foreach (Book book in books)
            {
                booksDto.Add(DtoConvert.BookDtoFromBook(book));
            }
            return booksDto;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDto book)
        {
            book.Id = id;
            var result = await BookLogic.ChangeBook(id, book);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<BookDto> PostBook(BookDto book)
        {
            var addedBook = await BookLogic.AddBook(book);
            return DtoConvert.BookDtoFromBook(addedBook);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await BookLogic.DeleteBook(id);
            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

    }
}
