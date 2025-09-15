using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Modals;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryDbContext _context;

        public BooksController(RepositoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

                if (book == null)
                    return NotFound();
                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();    //400
                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                //check book
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

                if (entity == null)
                    return BadRequest();    //404

                if (id != book.Id)
                    return BadRequest();    //400

                entity.Title = book.Title;
                entity.Price = book.Price;
                _context.SaveChanges();
                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBooks([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

                if (entity == null) return BadRequest();
                _context.Books.Remove(entity);
                _context.SaveChanges();
                return NoContent();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                //check entity
                var entity = _context.Books.Where(b => b.Equals(id)).SingleOrDefault();
                if (entity == null) return BadRequest();

                bookPatch.ApplyTo(entity);
                _context.SaveChanges();
                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
