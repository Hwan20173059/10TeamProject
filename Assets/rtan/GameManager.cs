using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public card Card;
    public int difficult;

    public Text timeText;
    public GameObject card;
    public GameObject endTxt;
    public GameObject failTxt;
    float time;

    public AudioSource audioSource;
    public AudioClip match;
    public AudioClip mismatch;

    public static GameManager I;

    public GameObject firstCard;
    public GameObject secondCard;


    public Animator lastDence;

    public bool countDownCheck = false; // kim 작업내용 추가
    float countDown = 5.0f;
    public Text countDownTxt; // 끝


    void Awake()
    {
        I = this;
        difficult = Random.Range(0, 3);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        if(difficult == 0 )
        {
            int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };

            rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

            for (int i = 0; i < 12; i++)
            {
                Card.Type = rtans[i];
                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("Cards").transform;

                float x = (i % 4) * 1.4f - 2.1f;
                float y = (i / 4) * 1.4f - 2.0f;
                newCard.transform.position = new Vector3(x, y, 0);

                string rtanName = "rtan" + rtans[i].ToString();

                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
            }
        }
        else if(difficult == 1) 
        {
            int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

            rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

            for (int i = 0; i < 16; i++)
            {
                Card.Type = rtans[i];
                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("Cards").transform;

                float x = (i / 4) * 1.4f - 2.1f;
                float y = (i % 4) * 1.4f - 3.0f;
                newCard.transform.position = new Vector3(x, y, 0);

                string rtanName = "rtan" + rtans[i].ToString();

                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
            }
        }
        else if (difficult == 2)
        {
            int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };

            rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

            for (int i = 0; i < 20; i++)
            {
                Card.Type = rtans[i];
                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("Cards").transform;

                float x = (i % 4) * 1.4f - 2.1f;
                float y = (i / 4) * 1.4f - 4.0f;                
                newCard.transform.position = new Vector3(x, y, 0);

                string rtanName = "rtan" + rtans[i].ToString();

                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = TimerManager.instance.elapsedTime.ToString("N2");

        if(time > 30)
        {
            failTxt.SetActive(true);
            TimerManager.instance.StopTimer();
        }

        // 20초 부터 붉은 숫자와 깜빡임
        if (time >= 20.0f)
        {
            lastDence.SetBool("emg", true);
        }


        if (countDownCheck)
        {
            countDown -= Time.deltaTime;
            countDownTxt.text = countDown.ToString("N1");
            if (countDown <= 0.0f)
            {
                firstCard.GetComponent<card>().CountDown();
                firstCard = null;
                countDown = 5.0f;
            }
        }


    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioManager.instance.SFXPlay("match", match);

            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
            countDownCheck = false;  // kim 
            countDown = 5.0f;

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            Debug.Log(cardsLeft);
            if (cardsLeft == 2)
            {
                Invoke("endgame", 1.0f);
            }
        }
        else
        {
            audioManager.instance.SFXPlay("mismatch", mismatch);
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 164 / 255f, 0, 255f); // 뒤집힌 카드 색 변화
            secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 164 / 255f, 0, 255f);
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
            countDownCheck = false; // kim
            countDown = 5.0f;
        }

        firstCard = null;
        secondCard = null;
    }

    void endgame()
    {
        endTxt.SetActive(true);
        TimerManager.instance.StopTimer();
    }

}
