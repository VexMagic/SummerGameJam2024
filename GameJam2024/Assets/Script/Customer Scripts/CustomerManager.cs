using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    //public GameObject spacePrefab;
    List<CustomerSpace> spaces = new List<CustomerSpace>();
    public CustomerSpace space1 = null;
    public CustomerSpace space2 = null;
    public CustomerSpace space3 = null;


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

        if( timerFloat < 0 )
        {
            timerFloat = 60f;

            SearchThroughSpaces();
        }


    }

    void SearchThroughSpaces()
    {
        bool found = false;
        foreach (var space in spaces)
        {
            if (!found && space.state == CustomerSpace.states.empty)
            {
                space.NewCustomer();
                break;
            }
        }
    }
}
