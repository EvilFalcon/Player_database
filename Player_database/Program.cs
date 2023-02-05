using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Player_database
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandCreateAccount = "1";
            const string CommandDeleteAccount = "2";
            const string CommandShowInfoAccount = "3";
            const string CommandBanned = "4";
            const string CommandExitProgram = "5";

            Database database = new Database();

            bool isProgramWork = true;


            while (isProgramWork)
            {
                Console.Clear();

                Console.WriteLine($"{CommandCreateAccount} Добавить пользователя \n" +
                    $"{CommandDeleteAccount} Удалить пользователя\n" +
                    $"{CommandShowInfoAccount} Вывести информацию всех игроков\n" +
                    $"{CommandBanned} Изменить статус игрока\n" +
                    $"{CommandExitProgram} Выход из программы");

                switch (Console.ReadLine())
                {
                    case CommandCreateAccount:
                        database.CreatePlayer();
                        break;

                    case CommandDeleteAccount:
                        database.DeleteAccount();
                        break;

                    case CommandShowInfoAccount:
                        database.ShowPlayers();
                        break;
                    case CommandBanned:
                        database.EditStatus();
                        break;

                    case CommandExitProgram:
                        isProgramWork = false;
                        break;
                }

                Console.Write("нажмите любую клавишу");
                Console.ReadKey();
            }
        }
    }

    class Player
    {
        private static int _id = 0;
        private string _name;
        private bool _isBanned = false;
        private int _level = 1;

        public Player(string name)
        {
            _name = name;
            UniqueNumber = ++_id;
        }

        public int UniqueNumber { get; private set; }

        public void UnblockUser()
        {
            _isBanned = false;
        }

        public void BlockUser()
        {
            _isBanned = true;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"id: {UniqueNumber} ,имя игрока {_name},статус {_isBanned},уровень игрока {_level}");
        }
    }

    class Database
    {
        private List<Player> _players;

        public Database()
        {
            _players = new List<Player>();
        }

        public void ShowPlayers()
        {
            foreach (Player player in _players)
            {
                player.ShowInfo();
            }
        }

        public void CreatePlayer()
        {

            Console.WriteLine("ведите имя :");
            string name = Console.ReadLine();

            Player player = new Player(name);

            _players.Add(player);
        }

        public void DeleteAccount()
        {
            Console.WriteLine("ведите индивидуальный номер игрока для его удаления");
            Player foundPlayer = FindPlayer();

            if (foundPlayer != null)
            {
                _players.Remove(foundPlayer);
            }

        }

        public void EditStatus()
        {
            const int CommandUnblockUser = 1;
            const int CommandBlockUser = 2;


            Console.Write("Ведите уникальный номер пользователя :");
            Player foundPlayer = FindPlayer();

            Console.WriteLine($"{CommandUnblockUser} Разблокировать пользователя ");
            Console.WriteLine($"{CommandBlockUser} Заблокировать пользователя");
            int userInput = InputUniqueNumber();

            if (CommandUnblockUser == userInput)
            {
                foundPlayer.UnblockUser();
            }
            else if (CommandBlockUser == userInput)
            {
                foundPlayer.BlockUser();
            }
            else
            {
                Console.WriteLine("Неверный ввод");
            }
        }

        private Player FindPlayer()
        {
            Player foundPlayer = null;
            int uniqueNumber = InputUniqueNumber();

            foreach (var player in _players)
            {
                if (player.UniqueNumber == uniqueNumber)
                {
                    foundPlayer = player;
                }
            }

            return foundPlayer;
        }

        private int InputUniqueNumber()
        {
            int userNumber = 0;
            bool userInputIsCorrect = false;
            while (userInputIsCorrect == false)
            {
                if (int.TryParse(Console.ReadLine(), out userNumber))
                {
                    userInputIsCorrect = true;
                }
            }

            return userNumber;
        }
    }
}
