using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    //ADD food reference for order
    public float customerTimer = 15f;
    bool receiveOrder = false;

    public Burger desiredBurger;
    private int randomNum;

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
        int randomNum = Random.Range(0, 2);
        desiredBurger = GetComponent<Burger>();
        CustomerOrder();
    }


    // Update is called once per frame
    void Update()
    {
        customerTimer -= Time.deltaTime;

        if (customerTimer < 0 )
        {
            Destroy(gameObject);
            //lose points?
        }
    }



    public void CustomerOrder()
    {
        //Generate a random order it wants fulfilled

        Burger generateBurger = new Burger();

        if(generateBurger != null)
        {
            if (randomNum == 0)
            {
                //Patty, Ketchup, Onion burger
                generateBurger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                generateBurger.AddIngredient(Ketchup);
                InternalOrderCheck(Ketchup);
                generateBurger.AddIngredient(Onion);
                InternalOrderCheck(Onion);

                desiredBurger = generateBurger;

            }
            else if (randomNum == 1)
            {
                //Patty, Ketchup, Onion, Lettuce burger
                generateBurger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                generateBurger.AddIngredient(Ketchup);
                InternalOrderCheck(Ketchup);
                generateBurger.AddIngredient(Onion);
                InternalOrderCheck(Onion);
                generateBurger.AddIngredient(Lettuce);
                InternalOrderCheck(Lettuce);

                desiredBurger = generateBurger;
            }
            else if (randomNum == 2)
            {
                //Double-patty burger with lettuce
                generateBurger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                generateBurger.AddIngredient(Patty);
                InternalOrderCheck(Patty);
                generateBurger.AddIngredient(Lettuce);
                InternalOrderCheck(Lettuce);
            }
        }
        

    }

    void InternalOrderCheck(GameObject ingredient)
    {
        GameObject newIngredient = ingredient;
        customerContents.Add(newIngredient.GetComponent<Ingredient>());
    }

    

    public void ReceiveOrder(Burger burger)
    {
        if(Compare(desiredBurger, burger))
        {
            //points?
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
