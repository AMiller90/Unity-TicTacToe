using Random = System.Random;

namespace DefaultNamespace
{
    /// <summary>
    /// Child class to represent an ai player
    /// </summary>
    public class AIPlayer : Player
    {
        /// <summary>
        /// Reference to the size of the board
        /// </summary>
        private int boardSize;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AIPlayer()
        {
            this.myTurn = false;
            this.playerChar = ' ';
            this.coords = new int[2];
        }
        
        /// <summary>
        /// AIPlayer overloaded constructor
        /// </summary>
        /// <param name="character">the character to set</param>
        /// <param name="boardSize">the boardSize to set</param>
        public AIPlayer(char character, int boardSize)
        {
            // Set the turn to false
            this.myTurn = false;
            // Set the character to the passed in character
            this.playerChar = character;
            // Initialize the coordinates
            this.coords = new int[2];
            // Reference to the size of the board
            this.boardSize = boardSize;
        }
        
        /// <summary>
        /// Function overloaded to handle ai player functionality
        /// </summary>
        /// <param name="slot">the slot that is being sent</param>
        /// <returns>Returns the ai coords that have been chosen</returns>
        public override int[] TakeTurn(Slot slot)
        {
            bool findEmptySlot = true;
            while (findEmptySlot)
            {
                Random rnd = new Random();
                var coords = new[] {rnd.Next(this.boardSize), rnd.Next(this.boardSize)};

                if (GameController.Instance.SlotIsEmpty(coords))
                {
                    findEmptySlot = false;
                    this.coords = coords;
                }
                    
            }
            return this.coords;
        }
    }
}