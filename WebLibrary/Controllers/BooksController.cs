using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.DataModel;
using Application;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Application.Logic _context;

        public BooksController(WebLibraryContext context)
        {
            _context = new Application.Logic(context);
        } 

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.GetBookList();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {

            if (id != book.Id)
            {
                return BadRequest();
            }

            var result = await _context.ChangeBook(id,book);

            if (result == null)
            {

                return NotFound();
            }
            else
            {
                if (result.Value)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            
            return await _context.AddBook(book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _context.DeleteBook(id);
            if (result == null) 
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
