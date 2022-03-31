namespace BattleShip
{

    public class Ship
    {
        public int Id { get; set; }
        public bool IsSink = false;
        public ShipCategory ShipCategory { get; set; }
        public int ShipPartsLeft { get; set; }
        public int len { get; set; }
        public ShipOrientation ShipOrient { get; set; }
        public int ShipStartingX { get; set; }
        public int ShipStartingY { get; set; }

        public List<ShipCoords> shipCoords = new List<ShipCoords>();

        public Ship(int id, int numberOfParts, List<ShipCoords> coords,ShipOrientation orient, int shipXstart, int shipYstart,ShipCategory sCategory)
        {
            Id = id;
            ShipPartsLeft = numberOfParts;
            shipCoords = coords;
            len = numberOfParts;
            ShipOrient = orient;
            ShipStartingX = shipXstart;
            ShipStartingY = shipYstart;
            ShipCategory = sCategory;
        }
     
        /// <summary>
        /// Check that on X,Y field is placed a ship or part of a ship.
        /// </summary>
        /// <param name="x">X coordinate to check</param>
        /// <param name="y">Y coordinate to check</param>
        /// <returns></returns>
        public bool IsOnField(int x, int y) 
        {
            foreach (var item in shipCoords)
            {
                if (item.X == x && item.Y == y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
