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
    public class GenresController : ControllerBase
    {
        private readonly Library.Logic.GenreLogic GenreLogic;

        public GenresController(LibraryContext context)
        {
            GenreLogic = new Library.Logic.GenreLogic(context);
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<IEnumerable<GenreDto>> GetGenres()
        {
            var genres = await GenreLogic.GetGenreList();
            var genreDto = new List<GenreDto>();
            foreach (Genre genre in genres)
            {
                genreDto.Add(DtoConvert.GenreDtoFromGenre(genre));
            }
            return genreDto;
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenre(int id)
        {
            var genre = await GenreLogic.GetGenreById(id);

            if (genre == null)
            {
                return NotFound();
            }

            return DtoConvert.GenreDtoFromGenre(genre);
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            var result = await GenreLogic.ChangeGenre(id, genre.GenreName);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<GenreDto> PostGenre(Genre genre)
        {
            var addedGenre = await GenreLogic.AddGenre(genre.GenreName);
            return DtoConvert.GenreDtoFromGenre(addedGenre); ;
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var result = await GenreLogic.DeleteGenre(id);
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
