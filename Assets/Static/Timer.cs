using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float currentTime = 0f;
    private float requiredTime;

    public Timer(float requiredTime)
    {
        this.requiredTime = requiredTime;
    }

    public float UpdateTimer(bool isCounting, float deltaTime)
    {
        if (isCounting)
            currentTime += deltaTime;
        else
            currentTime = 0f;
        return currentTime;
    }

    public bool IsTimeReached()
    {
        return currentTime >= requiredTime;
    }

    public void Reset()
    {
        currentTime = 0f;
    }

    public float GetElapsedTime()
    {
        return currentTime;
    }
}