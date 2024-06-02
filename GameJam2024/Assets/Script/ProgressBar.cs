using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressCircle;
    private float time;
    private float timer;

    const float positionMultiplyer = 120f;

    public void SetPosition(Vector2 pos, float time, float startTime)
    {
        transform.localPosition = pos * positionMultiplyer;
        timer = startTime;
        this.time = time;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        float progress = timer / time;
        progressCircle.fillAmount = 1 - progress;
        progressCircle.color = new Color(progress, 1 - progress, 0);

        if (timer >= time)
            Done();
    }

    public void Done()
    {
        ProgressManager.instance.RemoveBar(this);
        Destroy(gameObject);
    }
}
