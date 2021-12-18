using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fish
{
    public string name;
    public Sprite appearance;
    public float strength;
    public int rarity;

    public Fish(string newName, Sprite newAppearance, float newStrength, int newRarity)
    {
        name = newName;
        appearance = newAppearance;
        strength = newStrength;
        rarity = newRarity;
    }
}
