using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCaller : MonoSingleton<FrameCaller>
{
    private Queue<(int frames, Action action)> framewisecalling = new Queue<(int, Action)>();
    private const int MaxFrames = 100; // Maximum frames cap (adjust as needed)
    private void Start()
    {
        Application.runInBackground = true;
        Application.targetFrameRate = 45;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public void Enqueue(int frames, Action callback)
    {
        if (callback == null)
        {
            return;
        }
        if (frames < 1 || frames > MaxFrames)
        {
            frames = MaxFrames;
        }
        StartCoroutine(CallAfterFrames(frames, callback));
    }

    private IEnumerator CallAfterFrames(int frames, Action action)
    {
        for (int i = 0; i < frames; i++)
        {
            yield return null; // Wait for the next frame
        }
        action?.Invoke(); // Call the action
    }
}
