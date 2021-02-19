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
    public class SeriesController : ControllerBase
    {
        private readonly Library.Logic.SeriesLogic SeriesLogic;

        public SeriesController(LibraryContext context)
        {
            SeriesLogic = new Library.Logic.SeriesLogic(context);
        }

        // GET: api/Series
        [HttpGet]
        public async Task<IEnumerable<SeriesDto>> GetSeries()
        {
            var series = await SeriesLogic.GetSeriesList();
            var seriesDto = new List<SeriesDto>();
            foreach (Series seri in series)
            {
                seriesDto.Add(DtoConvert.SeriesDtoFromSeries(seri));
            }
            return seriesDto;
        }

        // GET: api/Series/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeriesDto>> GetSeries(int id)
        {
            var series = await SeriesLogic.GetSeriesById(id);

            if (series == null)
            {
                return NotFound();
            }

            return DtoConvert.SeriesDtoFromSeries(series);
        }

        // PUT: api/Series/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeries(int id, Series series)
        {
            var result = await SeriesLogic.ChangeSeries(id, series.SeriesName);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Series
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<SeriesDto> PostSeries(Series series)
        {
            var addedSeries = await SeriesLogic.AddSeries(series.SeriesName);
            return DtoConvert.SeriesDtoFromSeries(addedSeries); ;
        }

        // DELETE: api/Series/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var result = await SeriesLogic.DeleteSeries(id);
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
