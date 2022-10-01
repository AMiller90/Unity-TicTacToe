using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUIComponent : MonoBehaviour
{
    [SerializeField] private RawImage borderSprite;
    [SerializeField] private TMPro.TMP_Text characterDisplayText;
    private Slot slot;

    public void Initialize(Slot slot, Texture2D borderSprite, string text = "")
    {
        this.slot = slot;
        //this.borderSprite.texture = borderSprite;
        this.characterDisplayText.text = text;
    }

    public void OnClickBehaviour()
    {
        if (this.slot.character != ' ')
            return;

        this.slot.character = GameController.Instance.currentPlayer.playerChar;
        this.characterDisplayText.text = GameController.Instance.currentPlayer.playerChar.ToString();
    }
}
