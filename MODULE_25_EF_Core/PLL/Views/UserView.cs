using EntityFrWorkBD;
using Microsoft.EntityFrameworkCore;
using MODULE25_EF.BLL;
using MODULE25_EF.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.PLL.Views
{
    internal class UserView
    {
        public void ShowUserMenu()
        {
            var isPause = true;
            while (isPause)
            {

                Console.WriteLine(" --- Электронная библиотека->Посетители: ---");
                Console.WriteLine("     Выберите одно из действий(введите номер):");
                Console.WriteLine("      0 - Выйти в главное меню.");
                Console.WriteLine("      1 - Список всех пользователей.");
                Console.WriteLine("      2 - Добавить нового пользователя ");
                Console.WriteLine("      3 - Удалить пользователя по Id ");
                Console.WriteLine("      4 - Удалить пользователя по Email ");
                Console.WriteLine("      5 - Обновить имя пользователя ");
                Console.WriteLine("      6 - Выдать книгу пользователю ");
                Console.WriteLine("      7 - Возврат книги пользователем ");


                switch (Console.ReadLine())
                {
                    case "0":
                        isPause = false;
                        break;
                    case "1":
                        GetAllUser();
                        break;
                    case "2":
                        AddUser();
                        break;
                    case "3":
                        DeleteUserById();
                        break;
                    case "4":
                        DeleteUserByEmail();
                        break;
                    case "5":
                        UpdateUserNameById();
                        break;
                    case "6":
                        UserTakeBook();
                        break;
                    case "7":
                        UserReturnBook();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Не корректно введены данные, попробуйте снова!");
                        break;
                }

            }
        }
        public void GetAllUser()
        {
            using (var db = new ELibraryContext())
            {
                UserRepository userRepository = new UserRepository(db);

                var users = userRepository.SelectAll();

                foreach (var user in users)
                {
                    Console.Write($"    ID: {user.Id} ");
                    Console.Write($"    Имя: {user.Name} ");
                    Console.Write($"    EMail: {user.Email} ");
                    Console.WriteLine();
                }
            }
        }
        public void AddUser()
        {
            try
            {
                Console.Write(" Введите имя нового пользователя: ");
                var name = Console.ReadLine();

                Console.Write(" Введите Email нового пользователя: ");
                var email = Console.ReadLine();
                Console.WriteLine("Ожидание...");

                if (!new EmailAddressAttribute().IsValid(email))
                    throw new MyExceptions();


                using (var db = new ELibraryContext())
                {
                    var user = new DAL.Entities.UserEntity { Name = name, Email = email };
                    db.Users.Add(user);
                    db.SaveChanges();
                    Console.WriteLine("");
                    Console.WriteLine(" Пользователь добавлен! ");
                    Console.WriteLine("");
                }
            }
            catch (MyExceptions myExp)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {myExp.Message}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Упс! - {ex.Message}");
                Console.WriteLine("");
            }
        }
        public void DeleteUserById()
        {
            try
            {
                using (var db = new ELibraryContext())
                {
                    UserRepository userRepository = new UserRepository(db);

                    var users = userRepository.SelectAll();
                    foreach (var _user in users)
                    {
                        Console.Write($"ID: {_user.Id} ");
                        Console.Write($"Имя: {_user.Name} ");
                        Console.WriteLine();
                    }
                    Console.WriteLine($"    Введите Id пользователя для удаления из списка: ");
                    bool result = int.TryParse(Console.ReadLine(), out var id);
                    Console.WriteLine("  Ожидание...");
                    if (!result)
                        throw new MyExceptions("Ошибка ввода id");

                    var user = db.Users.Where(user => user.Id == id).FirstOrDefault();
                    if (user == null)
                        throw new MyExceptions("Пользователь с указанными данными не найден!");
                    db.Users.Remove(user);
                    db.SaveChanges();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine($"  Пользователь с id {id} удален!");
                    Console.WriteLine("  ");
                }
            }
            catch (MyExceptions myExp)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {myExp.Message}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {ex.Message}");
                Console.WriteLine("");
            }
        }
        public void DeleteUserByEmail()
        {
            try
            {
                using (var db = new ELibraryContext())
                {
                    Console.WriteLine(" Список пользователей:");

                    UserRepository userRepository = new UserRepository(db);
                    var users = userRepository.SelectAll();

                    foreach (var _user in users)
                    {
                        Console.Write($"    ID: {_user.Id} ");
                        Console.Write($"    Имя: {_user.Name} ");
                        Console.Write($"    EMail: {_user.Email} ");
                        Console.WriteLine();
                    }
                    Console.Write($"    Введите Email пользователя для удаления: ");

                    var email = Console.ReadLine();

                    if (!new EmailAddressAttribute().IsValid(email))
                        throw new MyExceptions("    Ошибка Email адреса - EmailAddressAttribute!");

                    var user = db.Users.Where(user => user.Email == email).FirstOrDefault();
                    if (user == null)
                        throw new MyExceptions("Возможно вы некорректно ввели данные, попробуйте снова!");
                    db.Users.Remove(user);
                    db.SaveChanges();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine($"    Пользователь с адресом {email} удален! ");
                    Console.WriteLine("");
                }
            }
            catch (MyExceptions myExp)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {myExp.Message}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {ex.Message}");
                Console.WriteLine("");
            }
        }
        public void UpdateUserNameById()
        {
            try
            {
                using (var db = new ELibraryContext())
                {
                    Console.WriteLine(" Список пользователей:");

                    UserRepository userRepository = new UserRepository(db);
                    var users = userRepository.SelectAll();

                    foreach (var _user in users)
                    {
                        Console.Write($"    ID: {_user.Id} ");
                        Console.Write($"    Имя: {_user.Name} ");
                        Console.Write($"    EMail: {_user.Email} ");
                        Console.WriteLine();
                    }
                    Console.Write("Введите Id пользователя для обновления имени: ");

                    bool result = int.TryParse(Console.ReadLine(), out var id);
                    if (!result)
                        throw new MyExceptions("Некорректный Id пользователя!");

                    var user = db.Users.Where(user => user.Id == id).FirstOrDefault();
                    if (user == null)
                        throw new MyExceptions("Пользователь ненайден!");

                    Console.Write("Введите новое имя пользователя: ");
                    string newName = Console.ReadLine();

                    user.Name = newName;
                    db.SaveChanges();
                    Console.WriteLine("");
                    Console.WriteLine($"    Имя Пользователя с id = {id} обновлено на {newName}! ");
                    Console.WriteLine("");
                }
            }
            catch (MyExceptions myExp)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {myExp.Message}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Возникло исключение {ex.Message}");
                Console.WriteLine("");
            }
        }
        public void UserTakeBook()
        {
            try
            {
                using (var db = new ELibraryContext())
                {
                    Console.WriteLine(" Список пользователей:");

                    UserRepository userRepository = new UserRepository(db);
                    var users = userRepository.SelectAll();

                    foreach (var _user in users)
                    {
                        Console.Write($"    ID: {_user.Id} ");
                        Console.Write($"    Имя: {_user.Name} ");
                        Console.Write($"    EMail: {_user.Email} ");
                        Console.WriteLine();
                    }
                    Console.WriteLine("");
                    Console.WriteLine(" Список Книг:");

                    if (db is null) Console.WriteLine("!!!!!!!!!!!!!!!!!!");
                    BookRepository bookRepository = new BookRepository(db);
                    var books = bookRepository.SelectAllBook();
                    if (books is not null)
                    {
                        foreach (var _book in books)
                        {
                            Console.Write($"    ID: {_book.Id} ");
                            Console.Write($"    Наименование: {_book.TitleBook} ");
                            Console.Write($"    Год создания: {_book.Year} ");
                            Console.Write($"    Автор:  ");

                            foreach (var a in _book.Authors)
                                Console.Write($"{a.Name}");

                            Console.WriteLine();
                            Console.WriteLine("       ");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" --> Упс! Библиотека пуста <--");
                    }

                    Console.Write("Введите Id пользователя, который хочет взять книгу: ");
                    bool resultUserId = int.TryParse(Console.ReadLine(), out int userId);
                    if (!resultUserId)
                        throw new MyExceptions("Некорректный Id пользователя!");

                    Console.Write("Введите Id книги, которую хочет взять пользователь");
                    bool resultBookId = int.TryParse(Console.ReadLine(), out int bookId);
                    if (!resultBookId)
                        throw new MyExceptions("Некорректный Id книги!");

                    Console.WriteLine("Ожидание...");

                    var user = db.Users.Include(u => u.Books).Where(user => user.Id == userId).FirstOrDefault();
                    if (user == null)
                        throw new MyExceptions($"Пользователь c id {userId} ненайден!");

                    var book = db.Books.Where(book => book.Id == bookId).FirstOrDefault();
                    if (book == null)
                        throw new MyExceptions($"Книга с id {bookId} ненайдена!");

                    var filteredBooks = bookRepository.BookIsOnUser(book);

                    if (filteredBooks.Count() > 0)
                        throw new MyExceptions($"Упс! Книга с id {bookId} -  {book.TitleBook} уже взята!");


                    user.Books.Add(book);
                    db.SaveChanges();
                    Console.WriteLine("Готово!");
                    Console.WriteLine("");
                }
            }
            catch (MyExceptions myExp)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {myExp.Message}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Возникло исключение {ex.Message}");
                Console.WriteLine("");
            }

        }
        public void UserReturnBook()
        {
            try
            {


                //Console.Write("Введите Id книги, которую хочет вернуть пользователь: ");
                //bool resultBookId = int.TryParse(Console.ReadLine(), out int bookId);
                //if (!resultBookId)
                //    throw new WrongIdException();

                using (var db = new ELibraryContext())
                {
                    Console.WriteLine(" Список пользователей:");

                    UserRepository userRepository = new UserRepository(db);
                    var users = userRepository.SelectAll();

                    foreach (var _user in users)
                    {
                        Console.Write($"    ID: {_user.Id} ");
                        Console.Write($"    Имя: {_user.Name} ");
                        Console.Write($"    EMail: {_user.Email} ");
                        Console.WriteLine();
                    }

                    Console.Write("Введите Id пользователя, который хочет вернуть книгу: ");
                    bool resultUserId = int.TryParse(Console.ReadLine(), out int userId);
                    if (!resultUserId)
                        throw new MyExceptions("Некорректно введен Id");

                    Console.WriteLine("Список книг на руках у пользователя: ");


                    var user = db.Users.Include(u => u.Books).Where(user => user.Id == userId).FirstOrDefault();
                    if (user == null)
                        throw new MyExceptions("Пользователь c указанным Id не найден");

                    foreach (var _book in user.Books)
                    {
                        Console.Write($"    ID: {_book.Id} ");
                        Console.Write($"    Имя: {_book.TitleBook} ");
                        Console.WriteLine();
                    }

                    Console.Write("Введите Id книги, которую хочет вернуть пользователь: ");
                    bool resultBookId = int.TryParse(Console.ReadLine(), out int bookId);
                    if (!resultBookId)
                        throw new MyExceptions("Некорректно введен Id книги!");

                    var book = db.Books.Where(book => book.Id == bookId).FirstOrDefault();
                    if (book == null)
                        throw new MyExceptions("Не удалось найти книгу у пользователя! Попробуйте заново!");

                    Console.WriteLine();
                    if (!user.Books.Contains(book))
                        Console.WriteLine("Пользователь не брал данную книгу!");

                    user.Books.Remove(book);

                    db.SaveChanges();
                    Console.WriteLine("Пользователь вернул книгу в библиотеку!");
                    Console.WriteLine();
                }
            }
            catch (MyExceptions myExp)
            {
                Console.WriteLine("");
                Console.WriteLine($"Внимание! - {myExp.Message}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Возникло исключение {ex.Message}");
                Console.WriteLine("");
            }

        }
    }
}
