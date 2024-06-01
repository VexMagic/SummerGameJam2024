using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patty : Ingredient
{
    public PattyState State;
    public float CookedProcentage;
    [SerializeField] Sprite[] Sprites;

    protected override void Initalize()
    {
        Type = IngredientType.Patty;
        base.Initalize();
    }

    public void Cook(float cookingRate)
    {
        CookedProcentage += cookingRate;
        CookedProcentage = Mathf.Clamp(CookedProcentage, 0f, 2f);
        IngredientSprite.sprite = Sprites[Mathf.FloorToInt(CookedProcentage)];
        State = (PattyState)Mathf.FloorToInt(CookedProcentage);
    }
}

public enum PattyState
{
    Raw,
    Finished,
    Burnt
}