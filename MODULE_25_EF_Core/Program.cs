using EntityFrWorkBD;
using MODULE25_EF.DAL.Entities;
using MODULE25_EF.PLL.Views;
using System.Collections.Generic;

namespace MODULE25_EF
{

    internal class Program
    {

        public static ELibraryContext? _elContext;
        public static GenreView? _genreView;
        public static BookView? _bookView;
        public static MainView? _mainView;
        public static UserView? _userView;
        public static AuthorView? _authorView;

        static void Main(string[] args)
        {
            Console.WriteLine("Вход в электронную библиотеку! Выберите действие из списка!");
            Console.WriteLine("Чтобы проинициализировать БД введите - 0");
            Console.WriteLine("Чтобы войти в электронную библиотеку введите - 1");

            try
            {
                switch (Console.ReadLine())
                {
                    case "0":
                        FormingDataqq();
                        break;
                    case "1":
                        _mainView = new MainView();
                        _userView = new UserView();
                        _bookView = new BookView();
                        _genreView = new GenreView();
                        _authorView = new AuthorView();
                        _mainView.ShowMainMenu();
                        break;
                    default:
                        Console.WriteLine("Не корректно введены данные, попробуйте снова!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static BookEntity FormingdataUs(string titleBook, int year, string nameAuthor, GenreEntity genre)
        {
            AuthorEntity author = new AuthorEntity() { Name = nameAuthor };
            BookEntity book = new BookEntity() { TitleBook = titleBook, Year = year };

            book.Genre = genre;

            author.Books.Add(book);
            book.Authors.Add(author);

            if (_elContext is not null)
            {
                _elContext.Books.AddRange(book);
                _elContext.Authors.AddRange(author);
                _elContext.Genres.AddRange(genre);
            }
            else
            {
                throw new Exception("Ошибка БД!");
            }
            return book;
        }

        enum EnUser { MikeS, StepB, PetrS, IlyaS, ViktorM }
        enum EnGenre { Program, Roman, Povest, Rasskaz }
        public static void FormingDataqq()
        {
            Console.WriteLine(" Укажите название вашего сервера(Data Source=)");
            Console.Write(" Например - 'DESKTOP-QK3UCEL\\SQLEXPRESS' :");
            string _dataSourceUs = Console.ReadLine();
            Console.WriteLine("  Ожидание...");
            using (var db = new ELibraryContext(true, _dataSourceUs))
            {
                _elContext = db;

                UserEntity[] userEntity = new[]
                 {
                      new UserEntity { Name = "Михаил Сладкоешкин" },
                      new UserEntity { Name = "Степан Борода" },
                      new UserEntity { Name = "Петр Сыроешькин" },
                      new UserEntity { Name = "Илья Соседкин" },
                      new UserEntity { Name = "Виктор Мельников" }
                  };

                GenreEntity[] genreBook = new[]
                {
                 new GenreEntity { Name = "Программирование" },
                 new GenreEntity { Name = "Роман"  },
                 new GenreEntity { Name = "Повесть"},
                 new GenreEntity { Name = "Рассказы"}

                };

                // db.Users.AddRange(userEntity);
                // db.SaveChanges();
                var book =
                    FormingdataUs("Совершенный код", 2019, "Стив Макконнелл", genreBook[(int)EnGenre.Program]);
                var book1 =
                    FormingdataUs("Программирование на С++", 2018, "Стивен Прата", genreBook[(int)EnGenre.Roman]);
                var book2 = FormingdataUs("Собачье сердце", 1925, "Михаил Булгаков", genreBook[(int)EnGenre.Povest]);
                FormingdataUs("Мёртвые души", 1842, "Николай Гоголь", genreBook[(int)EnGenre.Roman]);
                FormingdataUs("Рассказы", 1885, "Антон Чехов", genreBook[(int)EnGenre.Rasskaz]);
                //FormingdataUs("Отцы и дети", 1861, "Иван Тургенев", genreBook[(int)EnGenre.Roman]);
                // FormingdataUs("Ася", 1857, "Иван Тургенев", genreBook[(int)EnGenre.Povest]);

                AuthorEntity author_IvanT = new AuthorEntity() { Name = "Иван Тургенев" };
                BookEntity book_OiD = new BookEntity() { TitleBook = "Отцы и дети", Year = 1861 };

                book_OiD.Genre = genreBook[(int)EnGenre.Roman];

                author_IvanT.Books.Add(book_OiD);
                book_OiD.Authors.Add(author_IvanT);

                if (_elContext is not null)
                {
                    // _elContext.Books.AddRange(book_OiD);
                    _elContext.Authors.AddRange(author_IvanT);
                    _elContext.Genres.AddRange(genreBook[(int)EnGenre.Povest]);
                }


                BookEntity book_Asya = new BookEntity() { TitleBook = "Ася", Year = 1857 };

                book_Asya.Genre = genreBook[(int)EnGenre.Povest];

                author_IvanT.Books.Add(book_Asya);
                book_Asya.Authors.Add(author_IvanT);

                if (_elContext is not null)
                {
                    _elContext.Books.AddRange(new[] { book_OiD, book_Asya });
                    // _elContext.Authors.AddRange(author_IvanT);
                    _elContext.Genres.AddRange(genreBook[(int)EnGenre.Povest]);
                }

                /* */

                userEntity[(int)EnUser.MikeS].Books.Add(book);
                userEntity[(int)EnUser.MikeS].Books.Add(book2);
                userEntity[(int)EnUser.IlyaS].Books.Add(book1);
                db.Users.AddRange(userEntity);
                db.SaveChanges();

                Console.WriteLine("The End!");
            }
        }
    }


}
/*
 * 
 * "Михаил Булгаков","Мастер и Маргарита","1940"
"Михаил Булгаков","Собачье сердце","1925"
"Николай Гоголь","Мёртвые души","1842"
"Александр Пушкин","Евгений Онегин","1837"	
"Федор Достоевский","Преступление и наказание","1866"	
"Александр Пушкин","Повести Белкина","1831"	
"Лев Толстой","Война и мир","1868"	
"Федор Достоевский","Село Степанчиково и его обитатели","1859" 	
"Николай Гоголь","Ревизор","1836"	
"Иван Тургенев","Отцы и дети","1861" 	
"Антон Чехов","Палата №6","1892"
"Иван Тургенев","Рудин","1855"	
"Антон Чехов","Рассказы","1885"
 */