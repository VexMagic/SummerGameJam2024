using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressCircle;
    private float time;
    private float timer;
    private bool alwaysGreen;
    private bool paused;

    const float positionMultiplyer = 120f;

    public void SetPosition(Vector2 pos, float time, float startTime, bool alwaysGreen)
    {
        transform.localPosition = pos * positionMultiplyer;
        timer = startTime;
        this.time = time;
        this.alwaysGreen = alwaysGreen;
    }

    private void FixedUpdate()
    {
        if (paused)
            return;

        timer += Time.fixedDeltaTime;
        float progress = timer / time;
        progressCircle.fillAmount = 1 - progress;
        if (alwaysGreen)
            progressCircle.color = new Color(0, 1, 0);
        else
            progressCircle.color = new Color(progress, 1 - progress, 0);

        if (timer >= time)
            Done();
    }

    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }

    public void Done()
    {
        ProgressManager.instance.RemoveBar(this);
        Destroy(gameObject);
    }
}
