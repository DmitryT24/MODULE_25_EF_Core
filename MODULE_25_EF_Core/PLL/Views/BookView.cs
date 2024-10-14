using EntityFrWorkBD;
using MODULE25_EF.BLL;
using MODULE25_EF.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MODULE25_EF.PLL.Views
{
    class BookView
    {
        public void ShowBookMenu()
        {
            var isPause = true;
            while (isPause)
            {

                Console.WriteLine(" --- Электронная библиотека -> Список книг: ---");
                Console.WriteLine("     Выберите одно из действий(введите номер):");
                Console.WriteLine("      0 - Выйти в главное меню.");
                Console.WriteLine("      1 - Список всех книг.");
                Console.WriteLine("      2 - Поиск книги по Id.");
                Console.WriteLine("      3 - Список книг определенного жанра и вышедших между определенными годами.");
                Console.WriteLine("      4 - Количество книг определенного автора в библиотеке.");
                Console.WriteLine("      5 - Количество книг определенного жанра в библиотеке.");
                Console.WriteLine("      6 - Проверка наличия книги определенного автора и с определенным названием в библиотеке.");
                Console.WriteLine("      7 - Проверка наличия книги на руках у пользователя.");
                Console.WriteLine("      8 - Количество книг на руках у пользователя.");
                Console.WriteLine("      9 - Последняя вышедшая книга.");
                Console.WriteLine("      10 - Список всех книг, отсортированного в алфавитном порядке по названию.");
                Console.WriteLine("      11 - Список всех книг, отсортированного в порядке убывания года их выхода.");
                Console.WriteLine("      12 - Обновление года публикации книги.");

                switch (Console.ReadLine())
                {
                    case "0":
                        isPause = false;
                        break;
                    case "1":
                        GetListBook();
                        break;
                    case "2":
                        SearchBookById();
                        break;
                    case "3":
                        GetBooksByGenreYears();
                        break;
                    case "4":
                        GetBooksByAuthor();
                        break;
                    case "5":
                        GetBooksByGenre();
                        break;
                    case "6":
                        BookAuthorIsInLibrary();
                        break;
                    case "7":
                        BookIsOnUser();
                        break;
                    case "8":
                        UserBooks();
                        break;
                    case "9":
                        LastPublishedBook();
                        break;
                    case "10":
                        SelectAllOrderedByName();
                        break;
                    case "11":
                        SelectAllOrderedDescByPublishYear();
                        break;
                    case "12":
                        UpdateYearByBookId();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Не корректно введены данные, попробуйте снова!");
                        break;
                }
            }
        }

        public void GetListBook()
        {
            Console.Clear();
            Console.WriteLine("       ");
            Console.WriteLine("      Список книг:");
            try
            {
                using (var db = new ELibraryContext())
                {
                    if (db is null) Console.WriteLine("!!!!!!!!!!!!!!!!!!");
                    BookRepository bookRepository = new BookRepository(db);
                    var books = bookRepository.SelectAllBook();
                    if (books is not null)
                    {
                        foreach (var book in books)
                        {
                            Console.Write($"    ID: {book.Id} ");
                            Console.Write($"    Наименование: {book.TitleBook} ");
                            Console.Write($"    Год создания: {book.Year} ");
                            Console.Write($"    Автор:  ");

                            foreach (var a in book.Authors)
                                Console.Write($"{a.Name}");

                            Console.WriteLine();
                            Console.WriteLine("       ");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" --> Упс! Библиотека пуста <--");
                    }
                }
            }
            catch
            {
                throw new MyExceptions();
            }
        }
        public void SearchBookById()
        {
            int bookId;
            Console.Write(" Введите Id книги: ");
            while (!int.TryParse(Console.ReadLine(), out bookId))
            {
                Console.WriteLine("Ошибка! Введите целое число!");
            }
            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);

                var book = bookRepository.SelectById(bookId);
                if (book is not null)
                {
                    Console.Write($"    ID: {book.Id} ");
                    Console.Write($"    Наименование: {book.TitleBook} ");
                    Console.Write($"    Год создания: {book.Year} ");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(" --> Упс! Книга ненайдена! <--");
                }
            }
        }

        public void SelectById()
        {
            Console.WriteLine("Введите Id книги:");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAllBook();

                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Наименование: {book.TitleBook} ");
                    Console.Write($"Год создания: {book.Year} ");
                    Console.WriteLine();
                }
            }

        }
        public void UpdateYearByBookId()
        {
            Console.WriteLine("Обновление года публикации книги.");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAllBook();
                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Наименование: {book.TitleBook} ");
                    Console.Write($"Год создания: {book.Year.ToString()} ");
                    Console.WriteLine();
                }
                int bookId;
                Console.Write("Введите Id книги:");
                while (!int.TryParse(Console.ReadLine(), out bookId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                Console.WriteLine("Введите год издания:");
                int year;
                while (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }

                bookRepository.UpdateYearById(bookId, year);
            }
        }

        public void GetBooksByGenreYears()
        {
            Console.Clear();
            Console.WriteLine("Список всех книг определенного жанра и вышедших между определенными годами.");

            using (var db = new ELibraryContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();
                foreach (var genre in genres)
                {
                    Console.Write($"ID: {genre.Id} ");
                    Console.Write($"Имя: {genre.Name} ");
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
                Console.WriteLine("Введите год издания, начало:");
                int yearFrom;
                while (!int.TryParse(Console.ReadLine(), out yearFrom))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                Console.WriteLine("Введите год издания, конец:");
                int yearTo;
                while (!int.TryParse(Console.ReadLine(), out yearTo))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }

                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    var books = bookRepository.GetBooksByGenreYears(selectedGenre, yearFrom, yearTo);
                    if (books != null)
                    {
                        foreach (var book in books)
                        {
                            Console.Write($"ID: {book.Id} ");
                            Console.Write($"Наименование: {book.TitleBook} ");
                            Console.Write($"Год создания: {book.Year} ");
                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        public void GetBooksByAuthor()
        {
            Console.WriteLine("Список всех книг определенного автора.");

            using (var db = new ELibraryContext())
            {
                AuthorRepository authorRepository = new AuthorRepository(db);
                var authors = authorRepository.SelectAll();
                foreach (var author in authors)
                {
                    Console.Write($"ID: {author.Id} ");
                    Console.Write($"Имя: {author.Name} ");
                    Console.WriteLine();
                }
                int authorId;
                Console.Write("Введите Id автора:");
                while (!int.TryParse(Console.ReadLine(), out authorId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedAuthor = authorRepository.SelectByIdAuthor(authorId);
                if (selectedAuthor == null)
                {
                    Console.WriteLine($"Автор с Id {authorId} отсуствует!");
                    return;
                }

                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    Console.WriteLine($"{bookRepository.GetBooksByAuthorInLibrary(selectedAuthor)} книг(и) с автором {selectedAuthor.Name}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void GetBooksByGenre()
        {
            Console.WriteLine("Список всех книг определенного жанра.");

            using (var db = new ELibraryContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();
                foreach (var genre in genres)
                {
                    Console.Write($"ID: {genre.Id} ");
                    Console.Write($"Имя: {genre.Name} ");
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
                try
                {
                    Console.WriteLine($"{bookRepository.GetBooksByGenreInLibrary(selectedGenre)} книг(и) с жанром {selectedGenre.Name}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void BookAuthorIsInLibrary()
        {
            Console.WriteLine("Список книг для проверки:");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                AuthorRepository authorRepository = new AuthorRepository(db);

                var books = bookRepository.SelectAllBook();
                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Наименование: {book.TitleBook} ");
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
                    Console.WriteLine($"Книга с Id {bookId} отсуствует!");
                    return;
                }
                //выбираем автора
                var authors = authorRepository.SelectAll();
                foreach (var author in authors)
                {
                    Console.Write($"ID: {author.Id} ");
                    Console.Write($"Имя: {author.Name} ");
                    Console.WriteLine();
                }
                int authorId;
                Console.Write("Введите Id автора:");
                while (!int.TryParse(Console.ReadLine(), out authorId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedAuthor = authorRepository.SelectByIdAuthor(authorId);
                if (selectedAuthor == null)
                {
                    Console.WriteLine($"Автор с Id - {bookId} отсуствует!");
                    return;
                }

                try
                {
                    var exists = bookRepository.BookByAuthorIsInLibrary(selectedAuthor, selectedBook) switch
                    {
                        true => "на руках у пользователя",
                        false => "в библиотеке",
                        _ => $"книга - {selectedBook.TitleBook} автора - {selectedAuthor.Name} не найдена!"
                    };
                    Console.WriteLine($"Книга - {selectedBook.TitleBook} {exists}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public void BookIsOnUser()
        {
            Console.WriteLine();
            Console.WriteLine("Список книг для проверки:");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);

                var books = bookRepository.SelectAllBook();
                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Наименование: {book.TitleBook} ");
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
                    Console.WriteLine($"Книга с Id {bookId} отсуствует!");
                    return;
                }

                try
                {
                    var filteredBooks = bookRepository.BookIsOnUser(selectedBook);
                    string exists;
                    if (filteredBooks.Count() > 0)
                        exists = "на руках у пользователя";
                    else
                        exists = "в библиотеке";
                    Console.WriteLine($"Книга - {selectedBook.TitleBook} {exists}.");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public void UserBooks()
        {
            Console.WriteLine("Кол-во книг на руках у пользователя:");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                UserRepository userRepository = new UserRepository(db);

                var users = userRepository.SelectAll();
                foreach (var user in users)
                {
                    Console.Write($"ID: {user.Id} ");
                    Console.Write($"Имя: {user.Name} ");
                    Console.WriteLine();
                }
                int userId;
                Console.Write("Введите Id автора:");
                while (!int.TryParse(Console.ReadLine(), out userId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedUser = userRepository.GetUsById(userId);
                if (selectedUser == null)
                {
                    Console.WriteLine($"Пользователь с Id {userId} отсуствует!");
                    return;
                }

                try
                {
                    Console.WriteLine($"{bookRepository.UserBooks(selectedUser)} книг на руках у - {selectedUser.Name}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void LastPublishedBook()
        {
            Console.WriteLine("Последняя изданная книга:");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    var books = bookRepository.LastPublishedBook();
                    foreach (var book in books)
                    {
                        Console.Write($"ID: {book.Id} ");
                        Console.Write($"Наименование: {book.TitleBook} ");
                        Console.Write($"Год создания: {book.Year} ");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void SelectAllOrderedByName()
        {
            Console.WriteLine("Список всех книг в алфавитном порядке:");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAllOrderedByName();

                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Наименование: {book.TitleBook} ");
                    Console.Write($"Год создания: {book.Year} ");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

        }

        public void SelectAllOrderedDescByPublishYear()
        {
            Console.WriteLine("Список всех книг в обратном порядке по году выпуска:");

            using (var db = new ELibraryContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    var books = bookRepository.SelectAllOrderedDescByPublishYear();

                    foreach (var book in books)
                    {
                        Console.Write($"ID: {book.Id} ");
                        Console.Write($"Наименование: {book.TitleBook} ");
                        Console.Write($"Год создания: {book.Year} ");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)//=======================================================
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
