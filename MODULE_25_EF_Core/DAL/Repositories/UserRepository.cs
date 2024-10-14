using EntityFrWorkBD;
using Microsoft.EntityFrameworkCore;
using MODULE25_EF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.DAL.Repositories
{
    public class UserRepository
    {
        private ELibraryContext _elContext;
        public UserRepository(ELibraryContext libContext)
        {
            _elContext = libContext;
        }

        public void AddUs(UserEntity user)
        {
            _elContext.Users.Add(user);
            _elContext.SaveChanges();
        }
        public void DeleteUser(UserEntity user)
        {
            if (user is not null)
            {
                _elContext.Remove(user);
                _elContext.SaveChanges();
            }
        }
        public IQueryable<UserEntity> SelectAll()
        {
            return from user in _elContext.Users
                   select user;
        }

        public UserEntity GetUsById(int id)
        {
            return _elContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool AcceptBook(UserEntity user, BookEntity book)
        {
            if (book.User is not null)
                return false;

            user.Books.Add(book);

            return true;
        }
        /// <summary>
        /// перепроверить,иногда выдает ошибку(непонятно!!!)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool ReturnBook(UserEntity user, BookEntity book)
        {
            if (book.User is null)
                return false;

            user.Books.Remove(book);
            return true;
        }

        public int GetBookCountUser(UserEntity user)
        {
            return user.Books.Count;
        }

    }
}
