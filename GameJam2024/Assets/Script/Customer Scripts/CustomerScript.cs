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

    public Transform transform;
    public CustomerSpace space;

    [SerializeField] List<Ingredient> customerContents;
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
        int randomNum = 0;
        desiredBurger = Instantiate(exampleBurger, transform);
        desiredBurger.SetActive(true);
        burger = desiredBurger.GetComponent<Burger>();
        CustomerOrder();
    }


    // Update is called once per frame
    void Update()
    {
        customerTimer -= Time.deltaTime;

        if (customerTimer < 0 )
        {
            if (space != null)
            {
                space.CustomerLeaves();
                Destroy(gameObject);
            }
            
            //lose points?
        }
    }



    public void CustomerOrder()
    {
        randomNum = Random.Range(0, 2);
        //Generate a random order it wants fulfilled

        //Burger generateBurger = new Burger();

        switch (randomNum)
        {
            case 0:
                //Patty, Ketchup, Onion burger
                burger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                burger.UpdateSprites();

                burger.AddIngredient(Ketchup);
                InternalOrderCheck(Ketchup);
                burger.UpdateSprites();

                burger.AddIngredient(Onion);
                InternalOrderCheck(Onion);
                burger.UpdateSprites();
                break;
            case 1:
                //Patty, Ketchup, Onion, Lettuce burger
                burger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                burger.UpdateSprites();

                burger.AddIngredient(Ketchup);
                InternalOrderCheck(Ketchup);
                burger.UpdateSprites();

                burger.AddIngredient(Onion);
                InternalOrderCheck(Onion);
                burger.UpdateSprites();

                burger.AddIngredient(Lettuce);
                InternalOrderCheck(Lettuce);
                burger.UpdateSprites();
                break;
            case 2:
                //Double-patty burger with lettuce
                burger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                burger.UpdateSprites();

                burger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                burger.UpdateSprites();

                burger.AddIngredient(Lettuce);
                InternalOrderCheck(Lettuce);
                burger.UpdateSprites();
                break;
        }
    }

    void InternalOrderCheck(GameObject ingredient)
    {
        GameObject newIngredient = ingredient;
        customerContents.Add(newIngredient.GetComponent<Ingredient>());
    }

    

    public void ReceiveOrder(Burger otherBurger)
    {
        if(Compare(burger, otherBurger))
        {
            //points?
            space.CustomerLeaves();
            Destroy(gameObject);
        }
    }

    bool Compare(Burger order, Burger received)
    {
        int orderPatties = 0;
        int orderLettuce = 0;
        int orderKetchup = 0;
        int orderOnion = 0;

        int receivedPatties = 0;
        int receivedLettuce = 0;
        int receivedKetchup = 0;
        int receivedOnion = 0;

        foreach (Ingredient i in customerContents)
        {
            if(i == Patty){ orderPatties++; }
            else if(i == Lettuce){ orderLettuce++; }
            else if(i ==  Ketchup){ orderKetchup++; }
            else if(i == Onion){ orderOnion++; }
        }

        

        foreach(Ingredient i in received.ingredients)
        {
            if (i == Patty){ receivedPatties++; }
            else if (i == Lettuce){ receivedLettuce++; }
            else if (i == Ketchup){ receivedKetchup++; }
            else if (i == Onion){ receivedOnion++; }
        }

        if(orderPatties == receivedPatties && orderLettuce == receivedLettuce && orderKetchup == receivedKetchup && orderOnion == receivedOnion)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
