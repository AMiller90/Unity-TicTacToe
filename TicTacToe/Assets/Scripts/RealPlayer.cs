using UnityEngine;

namespace DefaultNamespace
{
    public class RealPlayer : Player
    {
        // RealPlayer constructor - Takes a character argument for placement character
        public RealPlayer(char character)
        {
            // Set the turn to false
            this.myTurn = false;
            // Set the character to the passed in character
            this.playerChar = character;
            // Set the number of wins to 0
            this.wins = 0;
            // Initialize the coordinates
            this.coords = new int[2];
        }
        
        public override int[] TakeTurn(Slot slot)
        {
            this.coords = new[] {slot.xPosition, slot.yPosition};
            Debug.Log("HUMANCOORDS" + " " + this.coords[0] + " " + this.coords[1]);
            return this.coords;
        }
    }
}