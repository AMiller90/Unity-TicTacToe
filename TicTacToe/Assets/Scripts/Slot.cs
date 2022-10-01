using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Slot
{
    public char character;
    public int xPosition;
    public int yPosition;

    public Slot()
    {
    }

    public Slot(int xPos, int yPos, char character = ' ')
    {
        this.character = character;
        this.xPosition = xPos;
        this.yPosition = yPos;
    }
}
