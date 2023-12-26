using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    public AudioSource audioSource;
    public AudioClip bgmusic;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
    }


    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject newObj = new GameObject(sfxName + "_Sound");
        AudioSource audioSource = newObj.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(newObj, clip.length);
    }
}
