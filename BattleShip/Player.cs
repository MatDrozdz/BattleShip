using BattleShip;

namespace BattleShip
{
    public class Player
    {
        public bool IsWinner { get; set; }
        public string PlayerName { get; set; }
        public GameBoard Board { get; set; }
        public Player(string name)
        {
            PlayerName = name;
            Board=new GameBoard();
            IsWinner = false;
        }

        /// <summary>
        /// Take X, Y coordinate and make a shot to enemy player board. Check that X,Y field stats is Empty, Missed, Hit or Ship.
        /// </summary>
        /// <param name="x">Shot X coordinate.</param>
        /// <param name="y">Shot Y coordinate</param>
        /// <param name="enemyPlayerBoard">Enemy board that is attacked.</param>
        /// <returns></returns>
        public bool Shoot(int x, int y,GameBoard enemyPlayerBoard)
        {
            bool isMissed = false;

            if (enemyPlayerBoard.ShipsLeft == 0 && enemyPlayerBoard.playerShips.Count==0)
            {
                this.IsWinner = true;
                return isMissed= true;
            }
            else
            {
                if (enemyPlayerBoard.FieldStatusBoard[x, y] == FieldStatus.Miss || enemyPlayerBoard.FieldStatusBoard[x, y] == FieldStatus.Hit)
                {
                    Console.WriteLine($"You already hit this field!!");
                    Console.Read();
                    return isMissed = true;
                }
                else if (enemyPlayerBoard.FieldStatusBoard[x, y] == FieldStatus.Empty)
                {
                    enemyPlayerBoard.FieldStatusBoard[x, y] = FieldStatus.Miss;
                    enemyPlayerBoard.field[x, y] = 'O';
                    Console.WriteLine($"You MISS!");
                    Console.ReadKey();
                    return isMissed = true;
                }
                else if (enemyPlayerBoard.FieldStatusBoard[x, y] == FieldStatus.Ship)
                {

                    enemyPlayerBoard.FieldStatusBoard[x, y] = FieldStatus.Hit;
                    enemyPlayerBoard.field[x, y] = 'X';

                    for (int i = 0; i < enemyPlayerBoard.playerShips.Count; i++)
                    {
                        var ship = enemyPlayerBoard.playerShips[i];
                        if (ship.IsOnField(x, y) == true)
                        {
                            ship.ShipPartsLeft -= 1;

                            if (ship.ShipPartsLeft == 0)
                            {
                                ship.IsSink = true;
                                enemyPlayerBoard.SurroundShip(ship.ShipStartingX, ship.ShipStartingY, ship.ShipOrient, ship.len);
                                enemyPlayerBoard.ShipsLeft -= 1;
                                enemyPlayerBoard.playerShips.Remove(ship);
                                Console.WriteLine($"Ship was SINK!!");
                                Console.ReadLine();
                                return isMissed = false;
                            }
                            Console.WriteLine($"You HIT but not SINK!");
                            Console.ReadLine();
                            return isMissed = false;
                        }
                    }
                }
            }
            return isMissed = false; ;
        }      
    }
}
