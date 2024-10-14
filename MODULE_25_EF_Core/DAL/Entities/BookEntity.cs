using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.DAL.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string? TitleBook { get; set; }
        public int Year { get; set; }
        public int? UserId { get; set; }
        public UserEntity User { get; set; }
        /// <summary>
        /// может содержать сразу несколько авторов
        /// </summary>
        public List<AuthorEntity> Authors { get; set; } = new List<AuthorEntity>();
        /// <summary>
        /// жанр
        /// и фантастика или детектив и т. п .
        /// Считаем что у книги не более 1 жанра
        /// </summary>
        public int? GenreId;
        public GenreEntity Genre { get; set; }
    }
}
