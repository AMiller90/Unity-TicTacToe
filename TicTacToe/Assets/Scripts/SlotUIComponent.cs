using UnityEngine;

public class SlotUIComponent : MonoBehaviour
{
    /// <summary>
    /// Reference to the character display text
    /// </summary>
    [SerializeField] private TMPro.TMP_Text characterDisplayText;
    
    /// <summary>
    /// Reference to the Slot in this component
    /// </summary>
    public Slot Slot { get; private set; }

    /// <summary>
    /// Function used to initialize the Slot object and text
    /// </summary>
    /// <param name="slot">the slot to set</param>
    /// <param name="text">the text to set</param>
    public void Initialize(Slot slot, string text = "")
    {
        this.Slot = slot;
        this.characterDisplayText.text = text;
    }

    /// <summary>
    /// Function used to handle user click on slot
    /// </summary>
    public void OnClickBehaviour()
    {
        if (this.Slot.character != ' ')
            return;
        
        GameController.Instance.ProcessMove(this.Slot);
    }

    /// <summary>
    /// Function used to update the slot text
    /// </summary>
    /// <param name="character">the character to set</param>
    public void UpdateSlot(char character)
    {
        this.Slot.character = character;
        characterDisplayText.text = character.ToString();
    }
}
