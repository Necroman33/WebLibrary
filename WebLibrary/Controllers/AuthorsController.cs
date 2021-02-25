using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.DataModel;
using Library.Logic.Models;
using Library.Logic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly Library.Logic.AuthorLogic AuthorLogic;

        public AuthorsController(LibraryContext context)
        {
            AuthorLogic = new Library.Logic.AuthorLogic(context);
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authors = await AuthorLogic.GetAuthorList();
            var authorDto = new List<AuthorDto>();
            foreach(Author author in authors)
            {
                authorDto.Add(DtoConvert.AuthorDtoFromAuthor(author));
            }
            return authorDto;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await AuthorLogic.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return DtoConvert.AuthorDtoFromAuthor(author);
        }

        // GET: api/Authors/ByFIO/Aleksandr
        [HttpGet("ByFIO/{fio}")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorыByFIO(string fio)
        {
            var authors = await AuthorLogic.GetAuthorsByFIO(fio);
            var authorDto = new List<AuthorDto>();
            foreach (Author author in authors)
            {
                authorDto.Add(DtoConvert.AuthorDtoFromAuthor(author));
            }
            return authorDto;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDto author)
        {
            author.Id = id;
            var result = await AuthorLogic.ChangeAuthor(id, author);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorDto author)
        {
            var addedAuthor = await AuthorLogic.AddAuthor(author);
            return DtoConvert.AuthorDtoFromAuthor(addedAuthor);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await AuthorLogic.DeleteAuthor(id);
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
