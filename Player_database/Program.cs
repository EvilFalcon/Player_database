using System;
using System.Collections.Generic;

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
                        database.AddPlayer();
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
        private static int _ids = 0;
        private string _name;
        private bool _isBanned = false;
        private int _level = 1;

        public Player(string name)
        {
            _name = name;
            UniqueNumber = ++_ids;
        }

        public int UniqueNumber { get; private set; }

        public void Unblock()
        {
            _isBanned = false;
        }

        public void Block()
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


        public void AddPlayer() 
        {
            Player player = CreatePlayer();
            _players.Add(player);
        }

        public void DeleteAccount()
        {
            if (TryGetPlayer(out Player foundPlayer))
            {
                _players.Remove(foundPlayer);
            }
        }

        public void EditStatus()
        {
            const int CommandUnblockUser = 1;
            const int CommandBlockUser = 2;

            if (TryGetPlayer(out Player foundPlayer))
            {

                Console.WriteLine($"{CommandUnblockUser} Разблокировать пользователя ");
                Console.WriteLine($"{CommandBlockUser} Заблокировать пользователя");

                int userInput = GetUniqueNumber();
                if (CommandUnblockUser == userInput)
                {
                    foundPlayer.Unblock();
                }
                else if (CommandBlockUser == userInput)
                {
                    foundPlayer.Block();
                }
                else
                {
                    Console.WriteLine("Неверный ввод");
                }
            }
            else
            {
                Console.WriteLine("игрок не найден");
            }
        }

        private Player CreatePlayer()
        {

            Console.WriteLine("Ведите имя :");
            string name = Console.ReadLine();

            Player player = new Player(name);

            return player;
        }

        private bool TryGetPlayer(out Player foundPlayer)
        {
            int uniqueNumber = GetUniqueNumber();

            foreach (var player in _players)
            {
                if (player.UniqueNumber == uniqueNumber)
                {
                    foundPlayer = player;
                    return true;
                }
            }

            foundPlayer = null;
            return false;
        }

        private int GetUniqueNumber()
        {
            int userNumber = 0;
            bool userInputIsCorrect = false;

            while (userInputIsCorrect == false)
            {
                if (int.TryParse(Console.ReadLine(), out userNumber))
                {
                    userInputIsCorrect = true;
                }
                else
                {
                    Console.WriteLine("Неверный ввод числа");
                }
            }

            return userNumber;
        }
    }
}
