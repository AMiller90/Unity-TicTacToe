namespace DefaultNamespace
{
    /// <summary>
    /// Child class to represent a real player
    /// </summary>
    public class RealPlayer : Player
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RealPlayer()
        {
            this.myTurn = false;
            this.playerChar = ' ';
            this.coords = new int[2];
        }
        
        /// <summary>
        /// RealPlayer overloaded constructor
        /// </summary>
        /// <param name="character">the character to set</param>
        public RealPlayer(char character)
        {
            // Set the turn to false
            this.myTurn = false;
            // Set the character to the passed in character
            this.playerChar = character;
            // Initialize the coordinates
            this.coords = new int[2];
        }
        
        /// <summary>
        /// Function overloaded to handle real player functionality
        /// </summary>
        /// <param name="slot">the slot that is chosen by the player</param>
        /// <returns>Returns the players coords that have been chosen</returns>
        public override int[] TakeTurn(Slot slot)
        {
            this.coords = new[] {slot.xPosition, slot.yPosition};
            return this.coords;
        }
    }
}