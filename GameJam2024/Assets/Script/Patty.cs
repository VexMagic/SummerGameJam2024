using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patty : Ingredient
{
    public PattyState State;
    public float CookedProcentage;
    [SerializeField] Sprite[] Sprites;

    public void Update()
    {
        IngredientSprite.sprite = Sprites[(int)CookedProcentage];
        State = (PattyState)(int)CookedProcentage;
    }

    protected override void Initalize()
    {
        Type = IngredientType.Patty;
        base.Initalize();
    }
}

public enum PattyState
{
    Raw,
    Finished,
    Burnt
}