using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientType Type;
    public SpriteRenderer IngredientSprite;
    [SerializeField] private float size;

    void Awake()
    {
        //Initalize();
    }

    protected virtual void Initalize()
    {
        IngredientSprite = GetComponent<SpriteRenderer>();
    }

    public float GetHeight()
    {
        return size * 0.125f;
    }

    protected virtual void Update()
    {
        IngredientSprite.sortingOrder = (int)(transform.parent.transform.position.y * -10);
    }
}


public enum IngredientType
{
    Patty,
    Ketchup,
    Onions,
    Lettuce
}