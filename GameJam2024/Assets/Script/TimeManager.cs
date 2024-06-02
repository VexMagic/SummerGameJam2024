using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    [SerializeField] private TextMeshProUGUI clock;
    float timer = 0f;
    [SerializeField] private int shiftHours;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (!AreCustomersStillHere() && timer >= shiftHours)
        {
            SceneManager.LoadScene("EndOfDay");
        }
    }

    private bool AreCustomersStillHere()
    {
        bool temp = false;
        foreach (var item in CustomerManager.Instance.spaces)
        {
            if (item.state == CustomerSpace.states.occupied)
            {
                temp = true;
            }
        }
        return temp;
    }
}
