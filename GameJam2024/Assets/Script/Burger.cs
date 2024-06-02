using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Burger : MonoBehaviour
{
    public bool isSupply;
    public bool hasBuns;
    public List<IngredientType> StartIngredients;
    public List<Ingredient> Contents;
    [SerializeField] SpriteRenderer TopBun;
    [SerializeField] SpriteRenderer BottomBun;
    public float Height;

    [SerializeField] GameObject Patty;
    [SerializeField] GameObject Ketchup;
    [SerializeField] GameObject Onion;
    [SerializeField] GameObject Lettuce;

    private void Start()
    {
        TopBun.gameObject.SetActive(false);
        BottomBun.gameObject.SetActive(false);
        foreach (var item in StartIngredients)
        {
            AddIngredientByType(item);
        }
        if (hasBuns)
            AddBuns();
        UpdateSprites();
    }

    public void AddIngredientByType(IngredientType type, bool isStartState = false)
    {
        switch (type)
        {
            case IngredientType.Patty:
                AddIngredient(Patty, isStartState);
                break;
            case IngredientType.Ketchup:
                AddIngredient(Ketchup, isStartState);
                break;
            case IngredientType.Onions:
                AddIngredient(Onion, isStartState);
                break;
            case IngredientType.Lettuce:
                AddIngredient(Lettuce, isStartState);
                break;
        }
    }

    public void Update()
    {
        // Debug code to test adding ingredients
        //if (Input.GetKeyDown(KeyCode.P))
        //    AddIngredient(Patty);

        //if (Input.GetKeyDown(KeyCode.K))
        //    AddIngredient(Ketchup);

        //if (Input.GetKeyDown(KeyCode.O))
        //    AddIngredient(Onion);

        //if (Input.GetKeyDown(KeyCode.L))
        //    AddIngredient(Lettuce);

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    AddBuns();
        //}
    }

    public void AddIngredient(GameObject ingredient, bool isStartState = false)
    {
        GameObject newIngredient = Instantiate(ingredient, transform);
        newIngredient.transform.localPosition = Vector3.zero;
        if (!isStartState)
            Contents.Add(newIngredient.GetComponent<Ingredient>());

        newIngredient.SetActive(!isSupply);
        UpdateSprites();
    }

    private void SetHeight()
    {
        if (hasBuns)
        {
            Height = TopBun.bounds.size.y + BottomBun.bounds.size.y;
        }
        foreach (Ingredient ingredient in Contents)
            Height += ingredient.GetHeight();
    }

    public void AddBuns()
    {
        hasBuns = true;
        TopBun.gameObject.SetActive(!isSupply);
        BottomBun.gameObject.SetActive(!isSupply);
        UpdateSprites();
    }

    public void UpdateSprites()
    {
        SetHeight();

        float offset = /*-Height / 2f*/0;
        //transform.localPosition = new Vector3(0, Height / 2f);

        if (hasBuns)
        {
            BottomBun.transform.localPosition = new(0, offset + BottomBun.bounds.extents.y);
            offset += BottomBun.bounds.size.y;

        }

        foreach (Ingredient ingredient in Contents)
        {
            ingredient.transform.localPosition = new(0, offset + ingredient.GetHeight() / 2f);
            offset += ingredient.GetHeight();
        }

        if (hasBuns)
        {
            TopBun.transform.localPosition = new(0, offset + TopBun.bounds.extents.y);
        }

        //float currentHeight = 0;

        //// Ingredients
        //foreach (Ingredient ingredient in Contents)
        //{
        //    currentHeight += ingredient.GetHeight();
        //    ingredient.transform.localPosition = new(0, currentHeight );
        //}

        //// Top Bun
        //currentHeight += TopBun.bounds.size.y;
        //TopBun.transform.localPosition = new(0, currentHeight);
    }
}