using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using BookProject;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> lBooks;

        public BookController()
        {
            lBooks = DB_Books.BookList;
        }


        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return lBooks;
        }

        // GET api/<BookController>/5
        [HttpGet("{isbn13}")]
        public IActionResult Get(string isbn13)
        {
            var book = DB_Books.GetAllBooks(isbn13);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound(new {message = " Id not Found!"});
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post([FromBody] Book newBook)
        {
            
            if (!BookExists(newBook.Isbn))
            {
                lBooks.Add(newBook);
                return CreatedAtAction("Get", new { Isbn13 = newBook.Isbn },newBook);
            }
            else
            {
                return NotFound(new { message = "Isbn13 is duplicate" });
            }
           
        }

        // PUT api/<BookController>/5
        [HttpPut("{Isbn13}")]
        public IActionResult Put(string Isbn13, [FromBody] Book updatedBook)
        {
            if (Isbn13 != updatedBook.Isbn)
            {
                return BadRequest();
            }

            Book currentBook = DB_Books.GetAllBooks(Isbn13);

            if (currentBook != null)
            {
                currentBook.Title = updatedBook.Title;
                currentBook.Author = updatedBook.Author;
                currentBook.PageAmount = updatedBook.PageAmount;
                currentBook.Isbn = updatedBook.Isbn;
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{isbn13}")]
        public IActionResult Delete(string isbn13)
        {
            var book = DB_Books.GetAllBooks(isbn13);
            if (book != null)
            {
                lBooks.Remove(book);
            }
            else
            {
                return NotFound();
            }

            return Ok(book);
        }


        // Helper method

        private bool BookExists(string isbn13)
        {
            return lBooks.Any(b => b.Isbn == isbn13);
        }
    }
}
