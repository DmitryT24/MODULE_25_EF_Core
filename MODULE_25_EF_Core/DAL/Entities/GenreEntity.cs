using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.DAL.Entities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
