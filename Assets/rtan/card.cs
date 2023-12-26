using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flips;

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
            Name.text = "김동환";
        else if (Type == 2 || Type == 3)
            Name.text = "김승현";
        else if (Type == 4 || Type == 5)
            Name.text = "김철우";
        else if (Type == 6 || Type == 7)
            Name.text = "강성원";
        else if (Type == 8 || Type == 9)
            Name.text = "박지훈";
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
                //audioSource.PlayOneShot(flips);

                anim.enabled = true;
                anim.SetBool("isOpen", true);
                audioManager.instance.SFXPlay("flips", flips);
                Invoke("flip", 0f);
                
                transform.DOScale(originalScale, 0.2f).OnComplete(() =>
                {
                    isFlip = false;
                });
            });
            if (GameManager.I.firstCard == null)
            {
            GameManager.I.countDownCheck = true; // kim
            GameManager.I.firstCard = gameObject;
            GameManager.I.firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 164 / 255f, 0, 255f); // kim   
            }
            else
            {
            GameManager.I.secondCard = gameObject;
            GameManager.I.isMatched();
            }
        }
    }

    public void CountDown() // kim
    {
        if (GameManager.I.firstCard != null && GameManager.I.secondCard == null)
        {
            Invoke("closeCardInvoke", 0f);
            GameManager.I.countDownCheck = false;
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
