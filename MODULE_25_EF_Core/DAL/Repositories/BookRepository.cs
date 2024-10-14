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
    internal class BookRepository
    {
        private ELibraryContext _elContext;
        public BookRepository(ELibraryContext libContext)
        {
            _elContext = libContext;
        }
        /// <summary>
        /// Добавление книги
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(BookEntity book)
        {
            _elContext.Add(book);
            _elContext.SaveChanges();
        }
        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="book"></param>
        public void Delete(BookEntity book)
        {
            if (book != null)
            {
                _elContext.Books.Remove(book);
                _elContext.SaveChanges();
            }
        }
        /// <summary>
        /// выбор книги по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookEntity SelectById(int id)
        {
            return (from book in _elContext.Books
                    where book.Id == id
                    select book).FirstOrDefault();
        }

        /// <summary>
        /// Сортируем и получаем список книг по Годам
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookEntity> GetBooksSortedYear() => _elContext.Books.OrderByDescending(b => b.Year).ToList();

        /// <summary>
        /// Сортируем и получаем список книг по Названию
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookEntity> GetBooksSortedByTitle() => _elContext.Books.OrderBy(b => b.TitleBook).ToList();
        /// <summary>
        /// Выводим все книги
        /// </summary>
        /// <returns></returns>
        public IQueryable<BookEntity> SelectAllBook() => from book in _elContext.Books
                                                         select book;
        /// <summary>
        /// Изменение года книги по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year"></param>
        public void UpdateYearById(int id, int year)
        {
            BookEntity book = SelectById(id);
            book.Year = year;
            _elContext.SaveChanges();
        }
        /// <summary>
        /// Получение Списка книг жанра в диапазоне гг.  
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="yearFrom"></param>
        /// <param name="yearTo"></param>
        /// <returns></returns>
        public IQueryable<BookEntity> GetBooksByGenreYears(GenreEntity genre, int yearFrom, int yearTo)
        {
            return _elContext.Books.
                        Where(b => b.Year >= yearFrom &&
                              b.Year <= yearTo)
                        .Join(_elContext.Genres
                                .Where(g => g.Id == genre.Id), b => b.Id, g => g.Id,
                              (b, g) => b);
        }
        /// <summary>
        /// Количество книг данного автора
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public int GetBooksByAuthorInLibrary(AuthorEntity author)
        {
            return _elContext.Books.Where(b => b.Authors.Contains(author) && b.UserId == null).Count();
        }
        /// <summary>
        /// Количество книг данного жанра в бибилиотеке 
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public int GetBooksByGenreInLibrary(GenreEntity genre)
        {
            return _elContext.Books.Where(b => b.Genre == genre && b.UserId == null).Count();
        }
        /// <summary>
        /// Есть ли данная книга автора в библиотеке
        /// </summary>
        /// <param name="author"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool? BookByAuthorIsInLibrary(AuthorEntity author, BookEntity book)
        {
            if (_elContext.Books.Where(b => b.TitleBook == book.TitleBook
                                            && b.Authors.Any(a => a == author)
                                            ).Count() == 0)
                return null;

            if (_elContext.Books.Where(b => b.TitleBook == book.TitleBook
                                        && b.Authors.Any(a => a == author)
                                        && b.UserId == null
                                        ).Count() > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Количество взятых книг 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public IQueryable<BookEntity> BookIsOnUser(BookEntity book)
        {
            return _elContext.Books.Where(b => b == book && b.UserId != null);
        }
        /// <summary>
        /// Количество книг на руках у пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UserBooks(UserEntity user)
        {
            return _elContext.Books.Where(b => b.User == user).Count();
        }
        /// <summary>
        /// Последняя вышедшая книга
        /// </summary>
        /// <returns></returns>
        public IQueryable<BookEntity> LastPublishedBook() =>
            _elContext.Books.OrderByDescending(b => b.Year).Take(1);
        /// <summary>
        /// Сортировка по Названию книг
        /// </summary>
        /// <returns></returns>
        public IQueryable<BookEntity> SelectAllOrderedByName() =>
            _elContext.Books.OrderBy(n => n.TitleBook);
        /// <summary>
        /// Сортировка книг по годам
        /// </summary>
        /// <returns></returns>
        public IQueryable<BookEntity> SelectAllOrderedDescByPublishYear() =>
             _elContext.Books.OrderByDescending(n => n.Year);

    }
}
