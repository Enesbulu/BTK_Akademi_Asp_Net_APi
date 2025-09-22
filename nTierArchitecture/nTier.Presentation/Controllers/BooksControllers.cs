using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using nTier.Entites.Models;
using nTier.Services.Contracts;

namespace nTier.Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager services)
        {
            _manager = services;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.BookService.GetAllBooks(false);
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _manager.BookService.GetOneBookById(id, false);

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

                _manager.BookService.CreateOneBook(book);

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
                if (book is null) return BadRequest();   //400

                _manager.BookService.UpdateOneBook(id, book, false);

                return NoContent(); //204
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
                _manager.BookService.DeleteOneBook(id, false);
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
                var entity = _manager.BookService.GetOneBookById(id, true);
                if (entity == null) return NotFound();  //404

                bookPatch.ApplyTo(entity);
                _manager.BookService.UpdateOneBook(id, entity, true);


                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

