using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgmusic;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // BGM 빠르게 하기
        if (GameManager.I.timeText.text == "20.00")
        {
            audioSource.pitch = 1.3f;
        }
    }
}
