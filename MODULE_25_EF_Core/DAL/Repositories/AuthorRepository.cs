using EntityFrWorkBD;
using MODULE25_EF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.DAL.Repositories
{
    public class AuthorRepository
    {
        public ELibraryContext _elContext;
        public AuthorRepository(ELibraryContext db)
        {
            _elContext = db;
        }

        public IQueryable<AuthorEntity> SelectAll()
        {
            return from author in _elContext.Authors
                   select author;
        }

        public AuthorEntity SelectByIdAuthor(int id)
        {
            IQueryable<AuthorEntity> authorEntities = from author in _elContext.Authors
                                                      where author.Id == id
                                                      select author;
            return authorEntities.FirstOrDefault();
        }

        /**/
    }
}
