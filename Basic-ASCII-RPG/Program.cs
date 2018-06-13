using System;

namespace Basic_ASCII_RPG
{
    class Program
    {
        private static ConsoleKey _input;
        private static readonly Player _player = new Player();
        private static readonly Mapping _map = new Mapping();

        internal static void Main(string[] args)
        {
            _map.SetPlayerStartPosition(_player);
            _map.PrintGameWindow(_player);

            GameLoop();
        }

        private static void GameLoop()
        {
            do
            {
                // Get any key the user presses
                _input = Console.ReadKey().Key;

                // Clear the console
                Console.Clear();

                // Update movement
                _player.Move(_input, _map);

                // Refresh the map
                _map.PrintGameWindow(_player);
            } while (_input != ConsoleKey.Escape);

            Console.WriteLine("\n\t    Press any key to exit...");
            Console.ReadKey();
        }
    }
}
