using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUIComponent : MonoBehaviour
{
    [SerializeField] private RawImage borderSprite;
    [SerializeField] private TMPro.TMP_Text characterDisplayText;
    public Slot Slot { get; private set; }

    public void Initialize(Slot slot, Texture2D borderSprite, string text = "")
    {
        this.Slot = slot;
        //this.borderSprite.texture = borderSprite;
        this.characterDisplayText.text = text;
    }

    public void OnClickBehaviour()
    {
        if (this.Slot.character != ' ')
            return;
        
        GameController.Instance.ProcessMove(this.Slot);
    }

    public void UpdateSlot(char character)
    {
        this.Slot.character = character;
        characterDisplayText.text = character.ToString();
    }
}
