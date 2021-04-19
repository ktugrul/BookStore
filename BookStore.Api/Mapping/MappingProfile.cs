using AutoMapper;
using BookStore.Api.DTO;
using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<Author, AuthorDTO>();

            CreateMap<BookDTO, Book>();
            CreateMap<AuthorDTO, Author>();


            CreateMap<SaveBookDTO, Book>();
            CreateMap<SaveAuthorDTO, Author>();
        }
                
    }
}
