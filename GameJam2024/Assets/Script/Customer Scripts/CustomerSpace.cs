using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpace : InteractionArea
{
    [SerializeField]
    CustomerScript currentCustomer;
    public GameObject referenceCustomer;
    
    //public Transform transform;
    public enum states { empty, occupied };
    public states state;

    public CustomerSpace()
    {
        currentCustomer = null;
        state = states.empty;
    }

    public override bool PlaceBurger()
    {
        if (currentCustomer != null)
        {
            if (currentCustomer.ReceiveOrder(PlayerMovement.instance.GetCurrentBurger()))
            {
                Destroy(PlayerMovement.instance.GetCurrentBurger().gameObject);
            }
            else
            {
                base.PlaceBurger();
            }

            return true;
        }

        return false;
    }

    //Holds a spot that can have a customer present. 
    //These should be interactable with the player to deliver food to.


    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case states.empty:

                break;

            case states.occupied:

                break;
        }
    }

    public void NewCustomer()
    {
        if (state == states.empty)
        {
            GameObject newCustomerObject = Instantiate(referenceCustomer, transform);
            CustomerScript newCustomer = newCustomerObject.GetComponent<CustomerScript>();
            newCustomer.space = this;
            currentCustomer = newCustomer;
            state = states.occupied;
            newCustomer.progressBar = ProgressManager.instance.CreateBar(transform.position, newCustomer.customerTimer);
        }
        
    }

    public void CustomerLeaves()
    {
        if (currentCustomer != null && state == states.occupied)
        {
            currentCustomer.CustomerLeaves(); // This will call Destroy on itself
            currentCustomer = null;
            state = states.empty; // Ensure the state is set to empty
        }
    }

    public states States
    {
        get { return state; }
        set { state = value; }
    }
}
