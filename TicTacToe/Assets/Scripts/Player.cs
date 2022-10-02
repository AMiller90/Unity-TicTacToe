namespace DefaultNamespace
{
    public abstract class Player 
    {
        // Character representing placement in a slot	
        public char playerChar;
        // Determines if it is this players turn or not
        public bool myTurn;
        // Reference to the coordinates selected for character placement on the board
        public int[] coords;
        // Abstract function for a player taking their turn
        public abstract int[] TakeTurn(Slot slot);
    }
}