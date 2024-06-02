using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;

    public List<CustomerSpace> spaces = new List<CustomerSpace>();
    public float timerFloat = 30f;

    private void Awake()
    {
        Instance = this;
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
