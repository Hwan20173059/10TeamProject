using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flips;
    public AudioSource audioSource;

    public GameObject NameText;
    public GameObject FailText;

    public int Type = 0;
    public Text Name;

    Vector3 originalScale;
    Vector3 targetScale;
    bool isFlip = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            transform.DOKill();
        }
        if (Type == 0 || Type == 1)
            Name.text = "±èµ¿È¯";
        else if (Type == 2 || Type == 3)
            Name.text = "±è½ÂÇö";
        else if (Type == 4 || Type == 5)
            Name.text = "±èÃ¶¿ì";
        else if (Type == 6 || Type == 7)
            Name.text = "°­¼º¿ø";
        else if (Type == 8 || Type == 9)
            Name.text = "¹ÚÁöÈÆ";
    }

    public void openCard()
    {
        if (Time.timeScale > 0 && !isFlip)
        {
            anim.enabled = false;
            isFlip = true;

            originalScale = transform.localScale;
            targetScale = new Vector3(0, originalScale.y, originalScale.z);

            transform.DOScale(targetScale, 0.2f).OnComplete(() =>
            {
                audioSource.PlayOneShot(flips);

                anim.enabled = true;
                anim.SetBool("isOpen", true);
                Invoke("flip", 0f);
                
                transform.DOScale(originalScale, 0.2f).OnComplete(() =>
                {
                    isFlip = false;
                });
            });
            if (GameManager.I.firstCard == null)
            {
                GameManager.I.firstCard = gameObject;
            }
            else
            {
                GameManager.I.secondCard = gameObject;
                GameManager.I.isMatched();
            }
        }
    }

    void flip()
    {
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);
    }

    public void destroyCard()
    {
        NameText.SetActive(true);
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        FailText.SetActive(true);
        Invoke("closeCardInvoke", 0.5f);
    }

    void closeCardInvoke()
    {
        anim.enabled = false;
        isFlip = true;

        originalScale = transform.localScale;
        targetScale = new Vector3(0, originalScale.y, originalScale.z);

        transform.DOScale(targetScale, 0.2f).OnComplete(() =>
        {
            anim.enabled = true;
            FailText.SetActive(false);
            anim.SetBool("isOpen", false);
            transform.Find("back").gameObject.SetActive(true);
            transform.Find("front").gameObject.SetActive(false);

            transform.DOScale(originalScale, 0.2f).OnComplete(() =>
            {
                isFlip = false;
            });
        });
    }
}
