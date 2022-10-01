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

        this.Slot.character = GameController.Instance.currentPlayer.playerChar;
        this.characterDisplayText.text = GameController.Instance.currentPlayer.playerChar.ToString();
        GameController.Instance.currentPlayer.coords = new[] {Slot.xPosition, Slot.yPosition};
        Debug.Log("" + GameController.Instance.currentPlayer.coords[0]+ " " + GameController.Instance.currentPlayer.coords[1]);
        GameController.Instance.ProcessMove();
    }
}
