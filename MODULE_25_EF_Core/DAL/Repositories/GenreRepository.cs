using EntityFrWorkBD;
using MODULE25_EF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.DAL.Repositories
{
    public class GenreRepository
    {
        private ELibraryContext _elContext;
        public GenreRepository(ELibraryContext db)
        {
            _elContext = db;
        }

        public GenreEntity SelectById(int id)
        {
            return (from genre in _elContext.Genres
                    where genre.Id == id
                    select genre).FirstOrDefault();
        }
        public IQueryable<GenreEntity> SelectAll()
        {
            return from genre in _elContext.Genres
                   select genre;
        }
        public void AddBookGenre(BookEntity book, GenreEntity genre)
        {
            book.Genre = genre;
            _elContext.SaveChanges();
        }
    }
}
