using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour
{
    [SerializeField] AudioSource Speaker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If game is paused, pause music
        if (Time.timeScale == 0 && Speaker.isPlaying)
            Speaker.Pause();

        // Turn music back on if paused
        else if (Time.timeScale > 0 && Speaker.isPlaying!)
            Speaker.UnPause();
    }
}
