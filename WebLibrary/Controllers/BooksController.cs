using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.DataModel;
using Library.Logic;
using Library.Models;

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
            return await BookLogic.GetBookList();
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

            return book;
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
        public async Task<ActionResult<BookDto>> PostBook(BookDto book)
        {
            return await BookLogic.AddBook(book);
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
