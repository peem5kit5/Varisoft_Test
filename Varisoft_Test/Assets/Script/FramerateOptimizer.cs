using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class FramerateOptimizer : MonoBehaviour
{
    [Header("Frame Settings")]
    public float TargetFrameRate = 60.0f;

    private int MaxRate = 9999;
    private float currentFrameTime;

    public void Init()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxRate;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
    }

    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / TargetFrameRate;
            var t = Time.realtimeSinceStartup;

            var _sleepTime = currentFrameTime - t - 0.01f;

            if (_sleepTime > 0)
                Thread.Sleep((int)(_sleepTime * 1000));

            while (t < currentFrameTime)
                t = Time.realtimeSinceStartup;
        }
    }
}
