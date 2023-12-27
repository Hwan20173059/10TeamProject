using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Animator ExpAnimator;
    public bool aniPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(aniPlay == true)
        {
            ExpAnimator.enabled = true;
        }
        else
        {
            ExpAnimator.enabled = false;
        }
    }

    public void destroyInvoke()
    {
        Invoke("destroyBomb", 0.5f);
    }
    public void destroyBomb()
    {
        aniPlay = false;
        Destroy(gameObject);
    }


}
