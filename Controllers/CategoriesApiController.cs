using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectMedii_Anime___Manga_Tracking_.Data;
using ProiectMedii_Anime___Manga_Tracking_.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProiectMedii_Anime___Manga_Tracking_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ProiectMedii_Anime___Manga_Tracking_Context _context;
        public CategoriesApiController(ProiectMedii_Anime___Manga_Tracking_Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }

        // GET: api/<CategoriesApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CategoriesApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriesApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
