using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIEFSample.Data;
using WebAPIEFSample.DTOs;
using WebAPIEFSample.Models;
using WebAPIEFSample.Models.Interfaces;


/*
 * Actividad 8: Creando mi propio API (Actividad grupal)
Grupo 1
Aneurys Jose Baez Hernandez
Jose Gabriel Polanco Ramos
Miguel Angel Romero Ureña
*/
namespace WebAPIEFSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly WebAPIEFSampleContext _context;

        public BooksController(WebAPIEFSampleContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            return await _context.Book.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook   ([FromRoute] int id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var BookDto = new BookToListDto();
            BookDto.Title = book.Title;
            BookDto.Author = book.Author;
            BookDto.Pages = book.Pages;

            return Ok(BookDto);
        }

        [HttpGet ("ejemplo")]
        public async Task<ActionResult> Ejemplo ([FromQuery] int id)
        {
            try
            {
                HttpClient cliente = new HttpClient();
                cliente.BaseAddress = new Uri("https://powerful-bayou-95420.herokuapp.com/api/v1/");
                var result = await cliente.GetAsync("students");
                var student = await result.Content.ReadAsStringAsync();
                return Ok(student);

            }
            catch( Exception ex)
            {
                return BadRequest(ex.ToString());

            }
        }




        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book BookDto)
        {
            if (id != BookDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(BookDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookCreateDto BookDto)
        {
            var bookToCreate = new Book();
            bookToCreate.Id = BookDto.Id;
            bookToCreate.Title = BookDto.Title;
            bookToCreate.Author = BookDto.Author;
            bookToCreate.Pages = BookDto.Pages;


            _context.Book.Add(bookToCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = bookToCreate.Id }, bookToCreate);
        }



        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
