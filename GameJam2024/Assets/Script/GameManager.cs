using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float TotalTime;
    public int Points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GivePoints(int points)
    {
        Debug.Log($"Old Score: {Points} | New Score {Points + points}");
        Points += points;
    }
}
