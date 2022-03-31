namespace BattleShip
{

    public class GameBoard
    {
        private static int boardSize = 10;
        public char[,] field = new char[10, 10];
        private int[] shipsToPlace = new int[] { 4, 3, 2, 1 };
        public FieldStatus[,] FieldStatusBoard = new FieldStatus[10, 10];
        public List<Ship> playerShips = new List<Ship>();
        public int ShipsLeft { get; set; } = 10;

        public GameBoard()
        {
            FillField();
            CreateFieldStatusBoard();
        }
       
        /// <summary>
        /// Fill display board.
        /// </summary>
        private void FillField()
        {
            for (int x = 0; x < boardSize; x++)
                for (int y = 0; y < boardSize; y++)
                    field[x, y] = '-';

        }

        /// <summary>
        /// Create status field board and fill it with Empty status.Also fill this board with random placed ships.
        /// </summary>
        private void CreateFieldStatusBoard()
        {
            for (int x = 0; x < boardSize; x++)
                for (int y = 0; y < boardSize; y++)
                    FieldStatusBoard[x, y] = FieldStatus.Empty;
            FillBoardWithTheShips(FieldStatusBoard);
        }

        /// <summary>
        /// Display game board on console.
        /// </summary>
        public void ShowBoard()
        {
            for (int x = 0; x < boardSize; x++)
            {
                Console.Write("  " + (x + 1));
            }
            Console.WriteLine();
            for (int y = 0; y < boardSize; y++)
            {
                Console.Write(y + 1 + "  ");
                for (int x = 0; x < boardSize; x++)
                    Console.Write(field[x, y] + "  ");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Testing method that is used to show on console all fields with the status. Was used to check, that after create FieldStatus Board, all fields are Empty,
        /// then every time player take a shoot, this status was change in correct way.
        /// </summary>
        /// <param name="FieldStatusBoard">Field Status board to check that status are correct.</param>
        public void ShowFieldStatus(FieldStatus[,] FieldStatusBoard)
        {
            for (int x = 0; x < boardSize; x++)
                Console.Write(" " + (x + 1));
            Console.WriteLine();
            for (int y = 0; y < boardSize; y++)
            {
                Console.Write(y + 1);
                for (int x = 0; x < boardSize; x++)
                    Console.Write(FieldStatusBoard[x, y] + " ");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Fill Status Board with random placed Ships.
        /// </summary>
        /// <param name="FieldStatus">Status board that is fill with the Ships</param>
        private void FillBoardWithTheShips(FieldStatus[,] FieldStatus)
        {
            int shipId = 0;


            CreateShip(FieldStatus, shipId, 4);
            shipId = +1;
            for (int i = 0; i < 2; i++)
            {
                CreateShip(FieldStatus, shipId,3);
                shipId++;
            }
            for (int i = 0; i < 3; i++)
            {
                CreateShip(FieldStatus, shipId, 2);
                shipId++;
            }
            for (int i = 0; i < 4; i++)
            {
                CreateShip(FieldStatus, shipId, 1);
                shipId++;
            }
        }

        /// <summary>
        /// Create random placed Ship and after that change right Status Field to Ship.
        /// </summary>
        /// <param name="fStatus">Status field board used to change field status.</param>
        /// <param name="shipId">Ship Id</param>
        /// <param name="shipLength">Ship length</param>
        private void CreateShip(FieldStatus[,] fStatus, int shipId, int shipLength)
        {
            bool isPlaced = false;
            do
            {
                Random shipOientation = new Random();
                Random shipStartPosiotion = new Random();
                ShipOrientation orient;
                int shipX;
                int shipY;
                int shipXStart = -1;
                int shipYStart = -1;
                int maxRange = boardSize - shipLength;
                ShipCategory sCategory= (ShipCategory)shipLength;
                List<ShipCoords> coords = new List<ShipCoords>();

                do
                {
                    orient = (ShipOrientation)shipOientation.Next(0, 2);
                    shipX = shipStartPosiotion.Next(0, maxRange);
                    shipY = shipStartPosiotion.Next(0, maxRange);
                } while (CanPutShip(shipX, shipY, shipLength, orient) != true);

                if (orient == ShipOrientation.Horizontal)
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (i == 0)
                        {
                            shipXStart = shipX;
                            shipYStart = shipY;
                        }
                        fStatus[shipX + i, shipY] = FieldStatus.Ship;
                        coords.Add(new ShipCoords(shipX + i, shipY));
                    }
                    isPlaced = true;
                    playerShips.Add(new Ship(shipId, shipLength, coords, orient, shipXStart, shipYStart,sCategory));
                }
                else
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (i == 0)
                        {
                            shipXStart = shipX;
                            shipYStart = shipY;
                        }
                        fStatus[shipX, shipY + i] = FieldStatus.Ship;
                        coords.Add(new ShipCoords(shipX, shipY + i));
                    }
                    isPlaced = true;
                    playerShips.Add(new Ship(shipId, shipLength, coords, orient, shipXStart, shipYStart, sCategory));
                }
            }
            while (isPlaced == false);
        }

        /// <summary>
        /// Depend on ship orientation, ship X,Y position and length check is possible to put ship on the field (X,Y).
        /// </summary>
        /// <param name="x">X coordintate.</param>
        /// <param name="y">Y coorditante</param>
        /// <param name="length">Ship length</param>
        /// <param name="orientation">Ship orientation</param>
        /// <returns></returns>
        private bool CanPutShip(int x, int y, int length, ShipOrientation orientation)
        {
            if (orientation == ShipOrientation.Vertical)
            {
                if (x < 0 || x >= boardSize-1 || y < 0 || y + length > boardSize-1) return false;
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + length; j++)
                    {
                        if (i >= 0 && i < boardSize-1 && j >= 0 && j < boardSize-1)
                        {
                            if (FieldStatusBoard[i, j] != FieldStatus.Empty) return false;
                        }
                    }
                }
            }
            else
            {
                if (x < 0 || x + length > boardSize-1 || y < 0 || y >= boardSize-1) return false;
                for (int i = x - 1; i <= x + length; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i < boardSize-1 && j >= 0 && j < boardSize-1)
                        {
                            if (FieldStatusBoard[i, j] != FieldStatus.Empty) return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Surround sink ship with Miss field status.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="orientatnion">Ship orientation</param>
        /// <param name="length">Ship length</param>
        public void SurroundShip(int x, int y, ShipOrientation orientatnion, int length)   
        {
            if (orientatnion == ShipOrientation.Horizontal)
            {
                for (int i = x - 1; i <= x + length; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i < boardSize && j >= 0 && j < boardSize)
                        {
                            if (FieldStatusBoard[i, j] == FieldStatus.Empty)
                            {
                                FieldStatusBoard[i, j] = FieldStatus.Miss;
                                field[i, j] = 'O';
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + length; j++)
                    {
                        if (i >= 0 && i < boardSize && j >= 0 && j < boardSize)
                        {
                            if (FieldStatusBoard[i, j] == FieldStatus.Empty)
                            {
                                FieldStatusBoard[i, j] = FieldStatus.Miss;
                                field[i, j] = 'O';
                            }
                        }
                    }
                }
            }
        }
    }


}
