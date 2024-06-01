using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    //ADD food reference for order
    public float customerTimer = 15f;
    bool receiveOrder = false;
    

    public GameObject exampleBurger;
    private GameObject desiredBurger;
    private int randomNum;
    private Burger burger;

    //public Transform transform;
    public CustomerSpace space;

    [SerializeField] List<Ingredient> customerContents = new List<Ingredient>();
    [SerializeField] SpriteRenderer TopBun;
    [SerializeField] SpriteRenderer BottomBun;
    public float Height;

    [SerializeField] GameObject Patty;
    [SerializeField] GameObject Ketchup;
    [SerializeField] GameObject Onion;
    [SerializeField] GameObject Lettuce;


    void Start()
    {
        customerTimer = 15f;
        desiredBurger = Instantiate(exampleBurger, transform);
        desiredBurger.SetActive(true);
        burger = desiredBurger.GetComponent<Burger>();
        CustomerOrder();

        if (space == null)
        {
            Debug.LogError("NO CUSTOMER SPACE AT START");
        }
    }


    void Update()
    {

        //    customerTimer -= Time.deltaTime;

        //    if (customerTimer < 0)
        //    {
        //        if (space != null) 
        //        {
        //            space.CustomerLeaves();
        //        }
        //        else
        //        {
        //            Debug.LogError("NO CUSTOMER SPACE WHEN RUNNING OUT OF TIME");
        //        }
        //        Destroy(gameObject);
        //    }

        customerTimer -= Time.deltaTime;

        if (customerTimer < 0)
        {
            if (space != null)
            {
                space.CustomerLeaves();
            }
            else
            {
                Debug.LogError("NO CUSTOMER SPACE WHEN RUNNING OUT OF TIME");
            }
            Destroy(gameObject);
        }
    }



    public void CustomerOrder()
    {
        randomNum = Random.Range(0, 3);
        //Generate a random order it wants fulfilled

        //Burger generateBurger = new Burger();

        switch (randomNum)
        {
            case 0:
                // Patty, Ketchup, Onion burger
                AddIngredientsToBurger(new List<GameObject> { Patty, Ketchup, Onion });
                break;
            case 1:
                // Patty, Ketchup, Onion, Lettuce burger
                AddIngredientsToBurger(new List<GameObject> { Patty, Ketchup, Onion, Lettuce });
                break;
            case 2:
                // Double-patty burger with lettuce
                AddIngredientsToBurger(new List<GameObject> { Patty, Patty, Lettuce });
                break;

        }
    }

    void AddIngredientsToBurger(List<GameObject> ingredients)
    {
        foreach (GameObject ingredient in ingredients)
        {
            burger.AddIngredient(ingredient);
            InternalOrderCheck(ingredient);
            burger.UpdateSprites();
        }
    }

    void InternalOrderCheck(GameObject ingredient)
    {
        //GameObject newIngredient = ingredient;
        customerContents.Add(ingredient.GetComponent<Ingredient>());
    }

    

    public void ReceiveOrder(Burger otherBurger)
    {
        //if(Compare(burger, otherBurger))
        //{
            
            
        //    if(space != null)
        //    {
        //        space.CustomerLeaves();
        //    }
        //    else
        //    {
        //        Debug.LogError("NO SPACE WHEN RECEIVING ORDER");
        //    }
        //    Destroy(gameObject);
        //}

        if (Compare(burger, otherBurger))
        {
            if (space != null)
            {
                space.CustomerLeaves();
            }
            else
            {
                Debug.LogError("NO SPACE WHEN RECEIVING ORDER");
            }
            Destroy(gameObject);
        }
    }

    bool Compare(Burger order, Burger received)
    {
        Dictionary<Ingredient, int> orderCount = CountIngredients(customerContents);
        Dictionary<Ingredient, int> receivedCount = CountIngredients(received.ingredients);

        foreach (Ingredient ingredient in orderCount.Keys)
        {
            if (!receivedCount.ContainsKey(ingredient) || receivedCount[ingredient] != orderCount[ingredient])
            {
                return false;
            }
        }
        return true;
    }
    Dictionary<Ingredient, int> CountIngredients(List<Ingredient> ingredients)
    {
        Dictionary<Ingredient, int> ingredientCount = new Dictionary<Ingredient, int>();
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredientCount.ContainsKey(ingredient))
            {
                ingredientCount[ingredient]++;
            }
            else
            {
                ingredientCount.Add(ingredient, 1);
            }
        }
        return ingredientCount;
    }




    public void CustomerLeaves()
    {
        if (burger != null)
        {
            Destroy(burger.gameObject);
        }
        else
        {
            Debug.LogError("Burger object is null.");
        }
    }
}
