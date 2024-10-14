using EntityFrWorkBD;
using MODULE25_EF.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.PLL.Views
{
    internal class GenreView
    {
        public void ShowMenuGenre()
        {
            var isPause = true;
            while (isPause)
            {
                Console.WriteLine(" Выберете действие:");
                Console.WriteLine("     0. Выйти из в основное меню.");
                Console.WriteLine("     1. Список жанров.");
                Console.WriteLine("     2. Добавить жанр.");

                switch (Console.ReadLine())
                {
                    case "0":
                        {
                            isPause = false;
                            break;
                        }
                    case "1":
                        {
                            ShowGenre();
                            break;
                        }
                    case "2":
                        {
                            AddBookGenre();
                            break;
                        }
                    default:
                        Console.WriteLine("Не корректно введены данные, попробуйте снова!");
                        break;
                }
            }
        }
        public void ShowGenre()
        {
            Console.WriteLine("         Жанры:");

            using (var db = new ELibraryContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();

                foreach (var genre in genres)
                {
                    Console.Write($"ID: {genre.Id} ");
                    Console.Write($"Имя: {genre.Name}");
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
        public void AddBookGenre()
        {
            Console.WriteLine("Добавление жанра");

            using (var db = new ELibraryContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();

                foreach (var genre in genres)
                {
                    Console.WriteLine($"ID: {genre.Id}");
                    Console.WriteLine($"Имя: {genre.Name}");
                    Console.WriteLine();
                }
                int genreId;
                Console.Write("Введите Id жанра:");
                while (!int.TryParse(Console.ReadLine(), out genreId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedGenre = genreRepository.SelectById(genreId);
                if (selectedGenre == null)
                {
                    Console.WriteLine($"Жанр с Id {genreId} отсуствует!");
                    return;
                }

                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAllBook();

                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Наименование: {book.TitleBook} ");
                    Console.Write($"Год создания: {book.Year}");
                    Console.WriteLine();
                }
                int bookId;
                Console.Write("Введите Id книги:");
                while (!int.TryParse(Console.ReadLine(), out bookId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedBook = bookRepository.SelectById(bookId);
                if (selectedBook == null)
                {
                    Console.WriteLine($"Книга с Id {bookId} отсуствует !");
                    return;
                }

                try
                {
                    genreRepository.AddBookGenre(selectedBook, selectedGenre);
                    Console.WriteLine($"Для книги {selectedBook.TitleBook} добавлен жанр {selectedGenre.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
