using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;
    public GameObject plusTimeText;

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
        if (elapsedTime >= 60)
        {
            audioManager.instance.audioSource.pitch = 1.3f;
        }
    }

    public void IncreaseTime(float plusTime) 
    {
        elapsedTime += plusTime;
        GameObject newPlusTime = Instantiate(plusTimeText);
        newPlusTime.transform.parent = GameObject.Find("Canvas").transform;
        plusTimeText.GetComponent<Text>().text = "+" + plusTime.ToString();
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