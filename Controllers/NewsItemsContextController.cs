using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsItems.Data;
using NewsItems.Model;

namespace NewsItems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class NewsItemsContextController(NewsItemsContext context) : ControllerBase
    {
        private readonly NewsItemsContext _context = context;

        // GET: api/NewsItemsControllerDb
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsItem>>> GetNewsItem()
        {
            return await _context.NewsItem.ToListAsync();
        }

        // GET: api/NewsItemsControllerDb/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsItem>> GetNewsItem(int? id)
        {
            var newsItem = await _context.NewsItem.FindAsync(id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return newsItem;
        }

        // PUT: api/NewsItemsControllerDb/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsItem(int? id, NewsItem newsItem)
        {
            if (id != newsItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NewsItemsControllerDb
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsItem>> PostNewsItem(NewsItem newsItem)
        {
            _context.NewsItem.Add(newsItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsItem", new { id = newsItem.Id }, newsItem);
        }

        // DELETE: api/NewsItemsControllerDb/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsItem(int? id)
        {
            var newsItem = await _context.NewsItem.FindAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            _context.NewsItem.Remove(newsItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsItemExists(int? id)
        {
            return _context.NewsItem.Any(e => e.Id == id);
        }
    }
}
