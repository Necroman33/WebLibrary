using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.DataModel;
using Library.Logic;
using Library.Logic.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly Library.Logic.TagLogic TagLogic;

        public TagsController(LibraryContext context)
        {
            TagLogic = new Library.Logic.TagLogic(context);
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<IEnumerable<TagDto>> GetTags()
        {
            var tags = await TagLogic.GetTagList();
            var tagDto = new List<TagDto>();
            foreach (Tag tag in tags)
            {
                tagDto.Add(DtoConvert.TagDtoFromTag(tag));
            }
            return tagDto;
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetTag(int id)
        {
            var tag = await TagLogic.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return DtoConvert.TagDtoFromTag(tag);
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            var result = await TagLogic.ChangeTag(id, tag.TagName);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<TagDto> PostTag(Tag tag)
        {
            var addedTag = await TagLogic.AddTag(tag.TagName);
            return DtoConvert.TagDtoFromTag(addedTag); ;
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var result = await TagLogic.DeleteTag(id);
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
