using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatchData : MonoBehaviour
{
    public Image fishSprite;
    public TMP_Text fishname;
    public TMP_Text rare;

    public void setFishData(Fish fishGot)
    {
        fishSprite.sprite = fishGot.appearance;
        fishname.text = "You caught a " + fishGot.name + " fish!";
        rare.text = "That's a rarity level " + fishGot.rarity.ToString() + " fish!";
    }
}
