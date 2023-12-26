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
        BGMPlay(bgmusic);
    }


    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject newObj = new GameObject(sfxName + "_Sound");
        AudioSource audioSource = newObj.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(newObj, clip.length);
        //KangSungwon Commit Test
    }

    public void BGMPlay(AudioClip bgm) //배경음 플레이 해주는 함수
    {

        // BGM 빠르게 하기
        if (GameManager.I.timeText.text == "20.00")
        {
            audioSource.pitch = 1.3f;
        }

        audioSource.clip = bgm;
        audioSource.loop = true;//배경음이니까 계속 나오도록
        audioSource.volume = 0.3f;
        audioSource.Play();

    }

    //URL Change Test
}
