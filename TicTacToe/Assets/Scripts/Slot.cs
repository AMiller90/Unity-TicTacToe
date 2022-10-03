public class Slot
{
    /// <summary>
    /// Reference to the character
    /// </summary>
    public char character;
    
    /// <summary>
    /// Reference to the x position of the slot
    /// </summary>
    public int xPosition;
    
    /// <summary>
    /// Reference to the y position of the slot
    /// </summary>
    public int yPosition;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Slot()
    {
        this.character = ' ';
    }

    /// <summary>
    /// Slot overloaded constructor
    /// </summary>
    /// <param name="xPos">the x position to set</param>
    /// <param name="yPos">the y position to set</param>
    /// <param name="character">the character to set</param>
    public Slot(int xPos, int yPos, char character = ' ')
    {
        this.character = character;
        this.xPosition = xPos;
        this.yPosition = yPos;
    }
}
