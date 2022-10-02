using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    public class AIPlayer : Player
    {
        private int boardSize;
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