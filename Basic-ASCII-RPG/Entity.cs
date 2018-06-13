using System;

namespace Basic_ASCII_RPG
{
    public class Entity
    {
        public virtual char TileSymbol => ' ';

        private bool _isOnStairUp;
        private bool _isOnStairDown;

        #region Leveling properties

        public int Level { get; set; }

        public int Experience { get; set; }

        public int ExperienceToLevel { get; set; }

        #endregion

        #region Movement properties

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int StartX { get; set; }

        public int StartY { get; set; }

        public int StartZ { get; set; }

        public int NextX { get; set; }

        public int NextY { get; set; }

        public int NextZ { get; set; }

        public int PreviousX { get; set; }

        public int PreviousY { get; set; }

        public int PreviousZ { get; set; }

        #endregion

        #region Stat properties

        public int HP { get; set; }

        public int MP { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Agility { get; set; }

        public int Vitality { get; set; }

        public int Wisdom { get; set; }

        public int Intelligence { get; set; }

        #endregion

        public void SetPosition(int z, int y, int x)
        {
            X = x;
            Y = y;
            Z = z;

            NextX = x + 1;
            NextY = y + 1;
            NextZ = z + 1;

            PreviousX = x - 1;
            PreviousY = y - 1;
            PreviousZ = z - 1;
        }

        public virtual void Move(ConsoleKey input, Mapping map)
        {
            switch (input)
            {
                // North
                case ConsoleKey.W:
                    PerformMove(input, map, Z, PreviousY, X);
                    break;

                // South
                case ConsoleKey.S:
                    PerformMove(input, map, Z, NextY, X);
                    break;

                // West
                case ConsoleKey.A:
                    PerformMove(input, map, Z, Y, PreviousX);
                    break;

                // East
                case ConsoleKey.D:
                    PerformMove(input, map, Z, Y, NextX);
                    break;
            }
        }

        protected virtual void PerformMove(ConsoleKey input, Mapping map, int targetZ, int targetY, int targetX)
        {
            var targetTile = map.Board[targetZ, targetY, targetX];

            switch (targetTile)
            {
                // Do nothing, you can't pass through a wall
                // TODO - Implement a turn system and have walking into a wall take a turn
                case Mapping.Wall:
                    break;

                // Proceed to the target tile
                case Mapping.Floor:
                    // Swap current tile based on your target tile
                    if (_isOnStairDown)
                    {
                        map.Board[Z, Y, X] = Mapping.StairDown;
                        map.Board[targetZ, targetY, targetX] = TileSymbol;
                        _isOnStairDown = false;
                    }
                    else if (_isOnStairUp)
                    {
                        map.Board[Z, Y, X] = Mapping.StairUp;
                        map.Board[targetZ, targetY, targetX] = TileSymbol;
                        _isOnStairUp = false;
                    }
                    else
                    {
                        map.Board[Z, Y, X] = Mapping.Floor;
                        map.Board[targetZ, targetY, targetX] = TileSymbol;
                    }

                    // Update position variables to accurately reflect your current position
                    switch (input)
                    {
                        case ConsoleKey.W:
                            NextY = Y;
                            Y = PreviousY;
                            PreviousY--;
                            break;
                        case ConsoleKey.S:
                            PreviousY = Y;
                            Y = NextY;
                            NextY++;
                            break;
                        case ConsoleKey.A:
                            NextX = X;
                            X = PreviousX;
                            PreviousX--;
                            break;
                        case ConsoleKey.D:
                            PreviousX = X;
                            X = NextX;
                            NextX++;
                            break;
                    }
                    break;

                // Proceed to next floor's StairDown, if there is a next floor
                case Mapping.StairUp:
                    if (Z + 1 < Mapping.BoardSizeZ)
                    {
                        map.Board[Z, Y, X] = Mapping.Floor;
                        // Iterate over the NEXT floor's map to find the StairDown character
                        for (var z = Z + 1; z == Z + 1;)
                        {
                            for (var y = 0; y < Mapping.BoardSizeY; y++)
                            {
                                for (var x = 0; x < Mapping.BoardSizeX; x++)
                                {
                                    if (map.Board[z, y, x] == Mapping.StairDown)
                                    {
                                        _isOnStairDown = true;
                                        map.Board[z, y, x] = TileSymbol;
                                        SetPosition(z, y, x);
                                    }
                                }
                            }
                        }
                    }
                    break;

                // Proceed to the previous floor's StairUp, if there is a previous floor
                case Mapping.StairDown:
                    if (Z - 1 >= 0)
                    {
                        map.Board[Z, Y, X] = Mapping.Floor;
                        // Iterate over the PREVIOUS floor's map to find the StairDown character
                        for (var z = Z - 1; z == Z - 1;)
                        {
                            for (var y = 0; y < Mapping.BoardSizeY; y++)
                            {
                                for (var x = 0; x < Mapping.BoardSizeX; x++)
                                {
                                    if (map.Board[z, y, x] == Mapping.StairUp)
                                    {
                                        _isOnStairUp = true;
                                        map.Board[z, y, x] = TileSymbol;
                                        SetPosition(z, y, x);
                                    }
                                }
                            }
                        }
                    }
                    break;

                default:
                    // Do nothing when encountering an unknown tile (Redundant)
                    break;
            }
        }
    }
}
