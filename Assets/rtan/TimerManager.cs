using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;

    public float elapsedTime { get; private set; }

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }
    void Start()
    {
        elapsedTime = 0.0f;
    }

    void Update() 
    {
        elapsedTime += Time.deltaTime;
    }

    public void IncreaseTime(float plusTime) 
    {
        elapsedTime += plusTime;
    }

    public void DecreaseTime(float minusTime) 
    {
        elapsedTime -= minusTime;
    }

    public void StopTimer() 
    {
        Time.timeScale = 0.0f;
    }
}