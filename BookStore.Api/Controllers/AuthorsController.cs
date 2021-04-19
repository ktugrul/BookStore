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
    [Route("api/author")]
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            this._authorService = authorService;
            this._mapper = mapper;
        }


        [HttpGet]
        [Route("get-all-books")]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            var author = await _authorService.GetAllAuthors();
            var authorResource = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDTO>>(author);

            return BuildObjectResult(HttpStatusCode.OK, true, value: authorResource);
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorService.GetAuthorById(id);
            var authorResource = _mapper.Map<Author, AuthorDTO>(author);

            return BuildObjectResult(HttpStatusCode.OK, true, value: authorResource);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor([FromBody] SaveAuthorDTO saveAuthorResource)
        {
            try
            {
                var validator = new SaveAuthorResourceValidator();
                var validationResult = await validator.ValidateAsync(saveAuthorResource);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var authorToCreate = _mapper.Map<SaveAuthorDTO, Author>(saveAuthorResource);

                var newAuthor = await _authorService.Create(authorToCreate);

                var author = await _authorService.GetAuthorById(newAuthor.Id);

                var authorResource = _mapper.Map<Author, AuthorDTO>(author);

                return BuildObjectResult(HttpStatusCode.OK, true, value: authorResource);
            }
            catch (Exception ex)
            {
                return BuildObjectResult(HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult<AuthorDTO>> UpdateAuthor([FromRoute] int id, [FromBody] SaveAuthorDTO saveAuthorResource)
        {
            try
            {
                var validator = new SaveAuthorResourceValidator();
                var validationResult = await validator.ValidateAsync(saveAuthorResource);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var authorToBeUpdated = await _authorService.GetAuthorById(id);

                if (authorToBeUpdated == null)
                    return NotFound();

                var artist = _mapper.Map<SaveAuthorDTO, Author>(saveAuthorResource);

                await _authorService.Update(authorToBeUpdated, artist);

                var updatedAuthor = await _authorService.GetAuthorById(id);

                var updatedAuthorResource = _mapper.Map<Author, AuthorDTO>(updatedAuthor);

                return BuildObjectResult(HttpStatusCode.OK, true, value: updatedAuthorResource);
            }
            catch (Exception ex)
            {
                return BuildObjectResult(HttpStatusCode.BadRequest, false, ex.Message);
            }
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            try
            {
                var author = await _authorService.GetAuthorById(id);

                await _authorService.Delete(author);

                return BuildObjectResult(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return BuildObjectResult(HttpStatusCode.BadRequest, false, ex.Message);
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
