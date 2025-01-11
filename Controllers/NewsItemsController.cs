using System.Security.Cryptography;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NewsItems.Data;
using NewsItems.Exception;
using NewsItems.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsItems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class NewsItemsController(INewsMessageRepository newsMessageRepository) : ControllerBase
    {
        readonly INewsMessageRepository messageRepository = newsMessageRepository;

        /// <summary>
        /// Returns all NewsItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<NewsItem>> Get()
        {
            return messageRepository.Get();
        }

        // GET api/<NewsItemController>/5
        [HttpGet("{id}")]
        public ActionResult<NewsItem> Get(int id)
        {
            try
            {
                return messageRepository.Get(id);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new NewsItem
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// {
        /// "title": "test",
        ///"message": "test",
        ///"dateTime": "2025-01-11T14:06:07.288Z"
        ///}
        /// </remarks>
        /// <param name="value"></param>
        /// <response code="201">If the item is created succesfully</response>
        /// <response code="409">If the item already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(NewsItem), 200)]
        [ProducesResponseType(typeof(NewsItem), 409)]
        public ActionResult Post([FromBody] NewsItem value)
        {
            try
            {
                value.Id = RandomNumberGenerator.GetInt32(10000);
                messageRepository.Add(value);
                Response.Headers.Append("Location", $"/{value.Id.Value}");
                return Created($"{Request.GetDisplayUrl()}/{value.Id.Value}", value);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return Conflict();
            }
        }

        // PUT api/<NewsItemController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] NewsItem value)
        {
            try
            {
                messageRepository.Update(id, value);
                return NoContent();
            }
            catch (ExceptionInvalidParameters e) 
            { 
                Console.Error.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
            catch (ExceptionNewsItemNotFound e)
            {
                Console.Error.WriteLine(e.Message);
                return NotFound(e.Message);
            }
        }

        // DELETE api/<NewsItemController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                messageRepository.Delete(id);
                return Ok();
            }
            catch (ExceptionNewsItemNotFound ex)
            {
                Console.Error.WriteLine(ex.Message);
                return NotFound();
            }
        }
    }
}
