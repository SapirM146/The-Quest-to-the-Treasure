using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    float oldFixedDeltaTime;

    private void Start()
    {
        oldFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        if (Time.timeScale < 1)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            if (Time.timeScale >= 1)
            {
                Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime;
                Debug.Log("done scaling");
            }
        }
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime *= slowdownFactor;
    }
}
