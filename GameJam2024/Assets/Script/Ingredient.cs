using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientType Type;
    public SpriteRenderer IngredientSprite;
    public float Offset;

    void Awake()
    {
        Initalize();
    }

    protected virtual void Initalize()
    {
        IngredientSprite = GetComponent<SpriteRenderer>();
    }

    public float GetHeight()
    {
        return IngredientSprite.bounds.size.y;
    }
}


public enum IngredientType
{
    Patty,
    Ketchup,
    Onions,
    Lettuce
}