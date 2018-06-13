namespace Basic_ASCII_RPG
{
    public class Player : Entity
    {
        public override char TileSymbol => '@';
        
        public Player()
        {
            // Start at _board[0, 1, 1] (_board[z, y, x])
            StartX = 1;
            StartY = 1;
            StartZ = 0;

            // Update current position
            // X = East/West
            // Y = North/South
            // Z = Up/Down
            X = StartX;
            Y = StartY;
            Z = StartZ;

            // Update "forward" positions
            NextX = X + 1;
            NextY = Y + 1;
            NextZ = Z + 1;

            // Update "backward" positions
            PreviousX = X - 1;
            PreviousY = Y - 1;
            PreviousZ = Z - 1;

            // Set level/leveling criteria
            Level = 1;
            Experience = 0;
            ExperienceToLevel = 50;

            // Provide starting stats for the player
            HP = 20;
            MP = 10;
            Attack = 3;
            Defense = 1;
            Strength = 4;
            Dexterity = 2;
            Agility = 3;
            Vitality = 4;
            Wisdom = 2;
            Intelligence = 1;
        }
    }
}
