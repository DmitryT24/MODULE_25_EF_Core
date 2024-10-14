using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.PLL.Views
{
    internal class MainView
    {
        public void ShowMainMenu()
        {
            var isPause = true;
            while (isPause)
            {
                Console.Clear();
                Console.WriteLine("---Электронная библиотека---");
                Console.WriteLine(" Выберите одно из действий(введите номер):");
                Console.WriteLine("     0 - Выйти из библиотеки!");
                Console.WriteLine("     1 - Посетители!");
                Console.WriteLine("     2 - Список книг!");
                Console.WriteLine("     3 - Авторы книг!");
                Console.WriteLine("     4 - Литературные жанры!");

                switch (Console.ReadLine())
                {
                    case "0":
                        {
                            isPause = false;
                            Console.WriteLine("Досвидания, ждем вас снова!");
                            break;
                        }
                    case "1":
                        {
                            Program._userView.ShowUserMenu();
                            break;
                        }
                    case "2":
                        {
                            Program._bookView.ShowBookMenu();
                            break;
                        }
                    case "3":
                        {
                            Program._authorView.ShowMenuAuthor();
                            break;
                        }
                    case "4":
                        {
                            Program._genreView.ShowMenuGenre();
                            break;
                        }
                    default:
                        Console.WriteLine("Не корректно введены данные, попробуйте снова!");
                        break;
                }
            }
        }

    }
}
