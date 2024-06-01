using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    //public GameObject spacePrefab;
    List<CustomerSpace> spaces = new List<CustomerSpace>();
    public CustomerSpace space1;
    public CustomerSpace space2;
    public CustomerSpace space3;


    //public int numOfSpaces = 3;

    public float timerFloat = 10f;

    private void Start()
    {
        if(spaces == null || spaces.Count <= 0)
        {
            if (space1 != null) { spaces.Add(space1); }
            if (space2 != null) { spaces.Add(space2); }
            if (space3 != null) { spaces.Add(space3); }
        }


    }

    // Update is called once per frame
    void Update()
    {
        timerFloat -= Time.deltaTime;

        if( timerFloat <= 0 )
        {
            timerFloat = 10f;
            SearchThroughSpaces();
        }


    }

    void SearchThroughSpaces()
    {
        //bool found = false;
        //foreach (var space in spaces)
        //{
        //    if (!found && space.state == CustomerSpace.states.empty)
        //    {
        //        space.NewCustomer();
        //        found = true;
        //        break;
        //    }
            
        //}

        foreach (var space in spaces)
        {
            if (space.state == CustomerSpace.states.empty)
            {
                space.NewCustomer();
                break; 
            }
        }
    }
}
