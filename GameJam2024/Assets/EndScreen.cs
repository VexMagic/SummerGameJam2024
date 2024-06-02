using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    private void Start()
    {
        score.text = "Score: " + GameManager.Instance.Points;
        GameManager.Instance.Points = 0;
    }
}
