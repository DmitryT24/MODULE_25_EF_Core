using EntityFrWorkBD;
using MODULE25_EF.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.PLL.Views
{
    internal class AuthorView
    {
        public void ShowMenuAuthor()
        {
            var isPause = true;
            while (isPause)
            {
                Console.WriteLine(" Выберете действие:");
                Console.WriteLine("     0. Выйти из в основное меню.");
                Console.WriteLine("     1. Список Авторов в библиотеке.");

                switch (Console.ReadLine())
                {
                    case "0":
                        {
                            isPause = false;
                            break;
                        }
                    case "1":
                        {
                            ShowAllAuthors();
                            break;
                        }
                    default:
                        Console.WriteLine("Не корректно введены данные, попробуйте снова!");
                        break;
                }
            }
        }
        public void ShowAllAuthors()
        {
            Console.WriteLine("Список всех Авторов в библиотеке:");
            Console.WriteLine("Ожидание...");
            using (var db = new ELibraryContext())
            {
                AuthorRepository authorRepository = new AuthorRepository(db);
                var authors = authorRepository.SelectAll();

                foreach (var author in authors)
                {
                    Console.Write($"ID: {author.Id} ");
                    Console.Write($"Имя автора: {author.Name} ");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
