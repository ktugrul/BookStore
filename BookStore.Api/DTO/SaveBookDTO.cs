using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.DTO
{
    public class SaveBookDTO
    {
        public string Name { get; set; }
        public int  AuthorId { get; set; }
    }
}
