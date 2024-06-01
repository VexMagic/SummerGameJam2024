using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    //ADD food reference for order
    public float customerTimer = 15f;
    bool receiveOrder = false;


    // Update is called once per frame
    void Update()
    {
        customerTimer -= Time.deltaTime;

        if (customerTimer < 0 )
        {
            CheckOrder();
        }
    }



    public void CustomerOrder()
    {
        //Generate a random order it wants fulfilled
    }

    public void CheckOrder()
    {
        if(receiveOrder)
        {
            //happy customer, +points

        }
        else
        {
            //Angry customer, -points
        }

        Destroy(gameObject);

        //Called to see if the order the player is holding checks out with its own
    }
}
