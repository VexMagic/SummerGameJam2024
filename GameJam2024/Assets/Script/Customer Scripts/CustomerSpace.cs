using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpace : MonoBehaviour
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
            //GameObject newCustomerObject = Instantiate(referenceCustomer, transform);

            //CustomerScript newCustomer = newCustomerObject.GetComponent<CustomerScript>();

            //currentCustomer = newCustomer;
            //state = states.occupied;

            GameObject newCustomerObject = Instantiate(referenceCustomer, transform);
            CustomerScript newCustomer = newCustomerObject.GetComponent<CustomerScript>();
            newCustomer.space = this; 

            currentCustomer = newCustomer;
            state = states.occupied;
        }
        
    }

    public void CustomerLeaves()
    {
        //if(currentCustomer != null && state == states.occupied)
        //{
        //    currentCustomer.CustomerLeaves();
        //    currentCustomer = null;
        //    state = states.empty;
        //}

        if (currentCustomer != null && state == states.occupied)
        {
            currentCustomer.CustomerLeaves();
            currentCustomer = null;
            state = states.empty;
        }

    }

    public states States
    {
        get { return state; }
        set { state = value; }
    }
}
