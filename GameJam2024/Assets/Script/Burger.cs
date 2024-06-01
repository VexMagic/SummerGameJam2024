using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    [SerializeField] List<Ingredient> Contents;
    [SerializeField] SpriteRenderer TopBun;
    [SerializeField] SpriteRenderer BottomBun;
    public float Height;

    [SerializeField] GameObject Patty;
    [SerializeField] GameObject Ketchup;
    [SerializeField] GameObject Onion;
    [SerializeField] GameObject Lettuce;

    private void Start()
    {
        Height = TopBun.bounds.size.y + BottomBun.bounds.size.y;
        UpdateSprites();
    }

    public void Update()
    {
        // Debug code to test adding ingredients
        if (Input.GetKeyDown(KeyCode.P))
            AddIngredient(Patty);

        if (Input.GetKeyDown(KeyCode.K))
            AddIngredient(Ketchup);

        if (Input.GetKeyDown(KeyCode.O))
            AddIngredient(Onion);

        if (Input.GetKeyDown(KeyCode.L))
            AddIngredient(Lettuce);

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (Ingredient ingredient in Contents)
                Height = TopBun.bounds.size.y + BottomBun.bounds.size.y;
        }
    }

    public void AddIngredient(GameObject ingredient)
    {
        GameObject newIngredient = Instantiate(ingredient, transform);
        newIngredient.transform.localPosition = Vector3.zero;
        Contents.Add(newIngredient.GetComponent<Ingredient>());
        Height += newIngredient.GetComponent<Ingredient>().GetHeight();
        UpdateSprites();
    }

    public void UpdateSprites()
    {
        float offset = -Height / 2f;

        BottomBun.transform.localPosition = new(0, offset + BottomBun.bounds.extents.y);
        offset += BottomBun.bounds.size.y;

        foreach (Ingredient ingredient in Contents)
        {
            ingredient.transform.localPosition = new(0, offset + ingredient.GetHeight() / 2f);
            offset += ingredient.GetHeight();
        }

        TopBun.transform.localPosition = new(0, offset + TopBun.bounds.extents.y);

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

    public List<Ingredient> ingredients
    {
        get { return Contents; }
    }
}