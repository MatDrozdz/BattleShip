using BattleShip;

namespace BattleShip
{
    public static class BattleShipGame
    {
        /// <summary>
        /// Start Game
        /// </summary>
        public static void StartGame()
        {
            Console.WriteLine($"Hello in BattleShip Game. Please set your players:");
            string? player1Name = StringValidation("Please enter player 1 name: ");
            Player player1 = new Player(player1Name);
            string? player2Name = StringValidation("Please enter player 2 name: ");
            Player player2 = new Player(player2Name);

            bool p1Move;
            bool p2Move;
            while (!player1.IsWinner || !player2.IsWinner)
            {
                do
                {
                    if (IsWinner(player1, player2))
                    {
                        goto EndGame;
                    }
                    else
                    {
                        ShowPlayersBoards(player1, player2);
                        p1Move = PlayerMove(player1, player2);
                    }
                } while (!p1Move);
                do
                {
                    if (IsWinner(player1, player2))
                    {
                        goto EndGame;
                    }
                    else
                    {
                        ShowPlayersBoards(player1, player2);
                        p2Move = PlayerMove(player2, player1);
                    }
                } while (!p2Move);
            }
        EndGame:
            Console.WriteLine($"Thanks for playing :)");
        }

        /// <summary>
        /// Player put X and Y, then make a shoot
        /// </summary>
        /// <param name="player1">Player who make a shoot</param>
        /// <param name="player2">Player that is under attack  </param>
        /// <returns></returns>
        private static bool PlayerMove(Player player1, Player player2)
        {
            bool p1Move;
            int x, y;
            Console.WriteLine($"{ player1.PlayerName.ToUpper()} MOVE");
            x = TakeNumber($"Please put X coordinate", 1, 10);
            y = TakeNumber($"Please put Y coordinate", 1, 10);
            p1Move = player1.Shoot(x - 1, y - 1, player2.Board);
            return p1Move;
        }

        /// <summary>
        /// Check that number is numeric value, also is not out of range
        /// </summary>
        /// <param name="message">Inform what value is taken</param>
        /// <param name="minValue">Min range for value</param>
        /// <param name="maxValue">Max range for value</param>
        /// <returns></returns>
        static int TakeNumber(string message, int minValue, int maxValue)
        {
            int number;
            do
            {
                Console.Write($"{message} ({minValue}-{maxValue}): ");
                if (!int.TryParse(Console.ReadLine(), out number))
                    Console.WriteLine($"ERROR: PLEASE INSERT A NUMBER!\n");
                else
                    if (number < minValue || number > maxValue)
                    Console.WriteLine($"ERROR: YOUR NUMBER IS OUT OF RANGE!");
                else
                    return number;
            } while (true);
        }

        /// <summary>
        /// Check if any of the players is the winner. 
        /// </summary>
        /// <param name="p1">First player to check</param>
        /// <param name="p2">Second player to check</param>
        /// <returns></returns>
        static bool IsWinner(Player p1, Player p2)
        {
            if (p1.Board.ShipsLeft == 0)
            {
                Console.WriteLine($"{p2.PlayerName} is a winner!");
                Console.ReadLine();
                return true;
            }
            else
            {
                if (p2.Board.ShipsLeft == 0)
                {
                    Console.WriteLine($"{p1.PlayerName} is a winner!");
                    Console.ReadLine();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Show how many player ships are left in the game (by ship category).
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        static void ShowStats(List<Ship> listOfShips)
        {
            var list = listOfShips.ToList();

            var Destroyer = (from x in list where x.ShipCategory == ShipCategory.Destroyer select x).Count();
            var Submarine = (from x in list where x.ShipCategory == ShipCategory.Submarine select x).Count();
            var Cruiser = (from x in list where x.ShipCategory == ShipCategory.Cruiser select x).Count();
            var Battleship = (from x in list where x.ShipCategory == ShipCategory.BattleShip select x).Count();
            Console.WriteLine(
                              $"Destroyer<1>   {Destroyer}\n" +
                              $"Submarine<2>   {Submarine}\n" +
                              $"Cruiser<3>   {Cruiser}\n" +
                              $"Destroyer<4>   {Battleship}\n");
        }

        /// <summary>
        /// Show the boards of both players on the console. 
        /// </summary>
        /// <param name="player1">Player which board si show.</param>
        /// <param name="player2">Player which board si show.</param>
        static void ShowPlayersBoards(Player player1, Player player2)
        {
            Console.Clear();
            Console.WriteLine($"{player1.PlayerName.ToUpper()} BOARD\n");
            player1.Board.ShowBoard();
            Console.WriteLine();
            // player1.Board.ShowFieldStatus(player1.Board.FieldStatusBoard);                                // used to check that field status are right
            Console.WriteLine($"{player2.PlayerName.ToUpper()} BOARD\n");
            player2.Board.ShowBoard();
            // player2.Board.ShowFieldStatus(player2.Board.FieldStatusBoard);                               // used to check that field status are right
            Console.WriteLine($"\n{player1.PlayerName.ToUpper()} ships left: {player1.Board.ShipsLeft}");
            ShowStats(player1.Board.playerShips);
            Console.WriteLine($"\n{player2.PlayerName.ToUpper()} ships left: {player2.Board.ShipsLeft}");

            ShowStats(player2.Board.playerShips);
        }

        /// <summary>
        /// Check is string value is not null.
        /// </summary>
        /// <param name="message">Inform what value is taken</param>
        /// <returns></returns>
        static string StringValidation(string message)
        {
            bool isValid = false;
            string playerName;
            do
            {
                Console.WriteLine(message);
                playerName = Console.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(playerName))
                {
                    Console.Clear();
                    isValid = false;
                    Console.WriteLine($"Name/Surname is empty.Please enter again.");
                    Console.ReadKey();
                }
                else
                {
                    isValid = true;
                }
            }
            while (!isValid);
            return playerName;
        }
    }
}