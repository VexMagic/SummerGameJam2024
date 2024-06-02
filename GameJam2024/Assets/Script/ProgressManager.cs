using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;
    [SerializeField] private GameObject progressBar;

    List<GameObject> barObjectList = new List<GameObject>();
    List<ProgressBar> progressBarList = new List<ProgressBar>();

    private void Awake()
    {
        instance = this;
    }

    public ProgressBar CreateBar(Vector2 pos, float time, float startTime = 0, bool alwaysGreen = false)
    {
        Debug.Log("bar");
        GameObject barObject = Instantiate(progressBar, transform);
        ProgressBar tempBar = barObject.GetComponent<ProgressBar>();
        tempBar.SetPosition(pos, time, startTime, alwaysGreen);

        barObjectList.Add(barObject);
        progressBarList.Add(tempBar);
        return tempBar;
    }

    public void RemoveBar(ProgressBar bar)
    {
        barObjectList.Remove(bar.gameObject);
        progressBarList.Remove(bar);
    }
}
