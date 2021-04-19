using AutoMapper;
using BookStore.Api.DTO;
using BookStore.Api.Validators;
using BookStore.Core.Models;
using BookStore.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Api.Controllers
{
    [Route("api/book")]
    //[Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            this._bookService = bookService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("get-all-books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllWithAuthors();
            var bookResources = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(books);

            return BuildObjectResult(HttpStatusCode.OK, true, value: bookResources);
        }


        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById([FromRoute] int id)
        {
            var book = await _bookService.GetBookById(id);
            var bookRes = _mapper.Map<Book, BookDTO>(book);
            return BuildObjectResult(HttpStatusCode.OK, true, value: bookRes);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] SaveBookDTO saveBook)
        {
            try
            {
                var validator = new SaveBookResourceValidator();
                var validationResult = await validator.ValidateAsync(saveBook);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var bookCreation = _mapper.Map<SaveBookDTO, Book>(saveBook);
                var newBook = await _bookService.Create(bookCreation);
                var book = await _bookService.GetBookById(newBook.Id);
                var bookRes = _mapper.Map<Book, BookDTO>(book);

                return BuildObjectResult(HttpStatusCode.OK, true, value: bookRes);
            }
            catch (Exception ex)
            {
                return BuildObjectResult(HttpStatusCode.BadRequest, false, ex.Message);
            }

        }


        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult<BookDTO>> UpdateBook([FromRoute] int id, [FromBody] SaveBookDTO saveBook)
        {
            try
            {
                var validator = new SaveBookResourceValidator();
                var validationResult = await validator.ValidateAsync(saveBook);

                var requestIsInvalid = id == 0 || !validationResult.IsValid;

                if (requestIsInvalid)
                    return BadRequest(validationResult.Errors);

                var bookToBeUpdate = await _bookService.GetBookById(id);

                if (bookToBeUpdate == null)
                    return NotFound();

                var book = _mapper.Map<SaveBookDTO, Book>(saveBook);

                await _bookService.Update(bookToBeUpdate, book);

                var updatedBook = await _bookService.GetBookById(id);
                var updatedBookResource = _mapper.Map<Book, BookDTO>(updatedBook);


                return BuildObjectResult(HttpStatusCode.OK, true, value: updatedBookResource);
            }
            catch (Exception e)
            {
                return BuildObjectResult(HttpStatusCode.BadRequest, false, e.Message);
            }

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {

            try
            {
                if (id == 0)
                    return BadRequest();

                var book = await _bookService.GetBookById(id);

                if (book == null)
                    return NotFound();

                await _bookService.Delete(book);

                return BuildObjectResult(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return BuildObjectResult(HttpStatusCode.BadRequest, false, ex.Message);
            }

        }

        [HttpPatch]
        [Route("change-book-name/{id}")]
        public async Task<ActionResult<BookDTO>> ChangeBook([FromRoute] int id, [FromBody] SaveBookDTO saveBook)
        {
            try
            {
                var validator = new SaveBookResourceValidator();
                var validationResult = await validator.ValidateAsync(saveBook);

                var requestIsInvalid = id == 0 || !validationResult.IsValid;

                if (requestIsInvalid)
                    return BadRequest(validationResult.Errors);

                var bookToBeUpdate = await _bookService.GetBookById(id);

                if (bookToBeUpdate == null)
                    return NotFound();

                var book = _mapper.Map<SaveBookDTO, Book>(saveBook);

                await _bookService.Update(bookToBeUpdate, book);

                var updatedBook = await _bookService.GetBookById(id);
                var updatedBookResource = _mapper.Map<Book, BookDTO>(updatedBook);


                return BuildObjectResult(HttpStatusCode.OK, true, value: updatedBookResource);
            }
            catch (Exception e)
            {
                return BuildObjectResult(HttpStatusCode.BadRequest, false, e.Message);
            }

        }

        private ObjectResult BuildObjectResult(HttpStatusCode statusCode, bool success, string message = null, object value = null)
        {
            return new ObjectResult(new
            {
                StatusCode = (int)statusCode,
                Success = success,
                Message = message,
                Value = value
            })
            {
                StatusCode = (int)statusCode
            }; ;
        }

    }
}
