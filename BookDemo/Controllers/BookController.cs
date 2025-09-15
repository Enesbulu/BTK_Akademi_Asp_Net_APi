using BookDemo.Data;
using BookDemo.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]   //Hepsini Get
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;
            return Ok(books);
        }

        [HttpGet("{id:int}")]   //Bir Tanesini Getir
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = ApplicationContext.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
            if (book == null)
                return NotFound();  //404
            return Ok(book);
        }

        [HttpPost]  //Oluştur
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();    //400

                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]   //Güncelle
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            //check book?
            var entity = ApplicationContext.Books.Find(b => b.Id == id);

            if (entity == null)
                return NotFound();  //404

            //check id
            if (id != book.Id)
                return BadRequest();    //400

            ApplicationContext.Books.Remove(entity);
            book.Id = entity.Id;
            ApplicationContext.Books.Add(book);

            return Ok(book);
        }

        [HttpDelete]    //Hepsini Sil
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204
        }


        [HttpDelete("{id:int}")]    //Bir Tanesini sil
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            var entity = ApplicationContext.Books.Find(b => b.Id == id);
            ////check book?
            if (entity is null)
                return NotFound(new
                {
                    status = StatusCode(404),
                    message = $"{id} numaralı kitap bulunamadı!"
                });
            //check id
            if (id != entity.Id)
                return BadRequest();
            ApplicationContext.Books.Remove(entity);
            return NoContent(); //204
        }

        [HttpPatch("{id:int}")] //Kısmı olarak güncelleme
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var entity = ApplicationContext.Books.Find(b => b.Id.Equals(id));

            if (entity is null)
                return NotFound();  //404
            if (id != entity.Id)
                return BadRequest();

            bookPatch.ApplyTo(entity);  //400

            return NoContent(); //204

        }
    }
}
