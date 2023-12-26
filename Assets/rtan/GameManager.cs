using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public card Card;
    public int difficult;

    public Text timeText;
    public GameObject card;
    public GameObject endTxt;
    public GameObject end; // ������ ��� �ؽ�Ʈ �˾�â
    public GameObject failTxt;
    public GameObject minusTxt;
    float time;

    public AudioSource audioSource;
    public AudioClip match;
    public AudioClip mismatch;

    public Animator lastDence;

    public static GameManager I;

    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject Explosion;

    public bool countDownCheck = false; // kim �۾����� �߰�
    float countDown = 5.0f;
    public Text countDownTxt; // ��

    int trial = 0;// �õ� Ƚ��
    int chance = 0;
    int cardNumb = 0;
    bool startcheck = false;
    int maxNumb = 0;
    int[] rtans;


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
            rtans = new int[12] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };

            rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
            maxNumb = 12;

        }
        else if(difficult == 1) 
        {
            rtans = new int[16] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

            rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
            maxNumb = 16;

        }
        else if (difficult == 2)
        {
            rtans = new int[20] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };

            rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
            maxNumb = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        chance++;

        if (chance == 20 && cardNumb < maxNumb && startcheck == false)
        {
            cardNumb++;
            CardSet(cardNumb);
            chance = 0;

        }
        else if (cardNumb >= maxNumb && chance == 20 && startcheck == false)
        {
            TimerManager.instance.StartTimer();
            startcheck = true;
        }

        timeText.text = TimerManager.instance.elapsedTime.ToString("N2");

        if(TimerManager.instance.elapsedTime > 90)//timermanager�� �ð��� üũ�Ͽ� 60�ʰ� ������ ���а� �� - ���� 60���ΰ� 90�ʷ� ����(������)
        {
            failTxt.SetActive(true);
            TimerManager.instance.StopTimer();
        }
        // 90�� ���� 60�� ���ĺ��� �÷��̾�� �ð� ��� ǥ��
        if (TimerManager.instance.elapsedTime >= 60)
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

    public void CardSet(int i)
    {
        i = i - 1;
        GameObject newCard = Instantiate(card);
        newCard.transform.parent = GameObject.Find("Cards").transform;
        newCard.GetComponent<card>().Type = rtans[i];
        newCard.transform.position = new Vector3(2.5f, 3.5f, 0);

        float x = (i % 4) * 1.4f - 2.1f;
        float y = (i / 4) * 1.4f - 4.0f;
        newCard.GetComponent<card>().finalX = x;
        newCard.GetComponent<card>().finalY = y;


        string rtanName = "rtan" + rtans[i].ToString();

        newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
    }

    public void BombPlay(float x, float y)
    {
        GameObject newBomb = Instantiate(Explosion);
        Explosion.transform.position = new Vector3(x, y, 0);
        Destroy(newBomb);
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        trial++;

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
                TimerManager.instance.timeStop = true;
                Invoke("endgame", 1.0f);
            }
        }
        else
        {
            audioManager.instance.SFXPlay("mismatch", mismatch);
            TimerManager.instance.IncreaseTime(3.0f);//Ʋ���� �ð��� �����Ǵ� ��
     
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 164 / 255f, 0, 255f); // ������ ī�� �� ��ȭ
            secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 164 / 255f, 0, 255f);
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
            countDownCheck = false; // kim
            countDown = 5.0f;
        }

        firstCard = null;
        secondCard = null;
    }

    void endgame()//���������� �� ���߰� �����ϸ� ��� ���� ǥ��
    {
        float finalTime = 30f - time;

        if (finalTime < 0)
        {
            finalTime = 0;
            endTxt.GetComponent<Text>().text = "�õ��� Ƚ�� : " + trial + "\n" + "���� �ð� : " + finalTime + "\n" +
    "���� : " + 0;
        }
        else
        {
            endTxt.GetComponent<Text>().text = "�õ��� Ƚ�� : " + trial + "\n" + "���� �ð� : " + finalTime + "\n" +
    "���� : " + (trial * 3 + Mathf.Sqrt(finalTime));
        }

        end.SetActive(true);
        TimerManager.instance.StopTimer();
    }

}
