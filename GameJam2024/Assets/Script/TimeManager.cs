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
    [SerializeField] private float secondsPerHour;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (CanSpawnCustomers())
        {
            timer += Time.fixedDeltaTime / secondsPerHour;
            if (timer % 1 > 0.5f)
                clock.text = ((int)timer + 10) + ":30";
            else
                clock.text = ((int)timer + 10) + ":00";
        }

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

    public bool CanSpawnCustomers()
    {
        return timer < shiftHours;
    }
}
