using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

//waitcode
//  101     host抄收自己手上的--第一張牌
//  102     host抄收自己手上的--第二張牌
public class host : MonoBehaviour
{
    delegate void voidDelegate();     //宣告一個無參數無回傳的「委派」類型
    voidDelegate method;              //宣告一個變數是委派
    public int message;
    int waitResponseCode;           //目前我們正在等待的code
    List<int> cardLibrary = new List<int>();
    main main;

    int playerNum;
    Timer aTimer;
    // Use this for initialization
    void Start()
    {

        main = GameObject.Find("main").GetComponent<main>();
        createCardsDouble();
        shuffleCards();
        displayCardsLibrary();
        sendnMessage(main, cardLibrary[0], 101, dealCard);    //發第一張牌給main，訊息為牌庫第一張牌碼,抄收碼101，抄收成功執行從牌庫移除牌

    }
    // Update is called once per frame
    void Update()
    {
        refreshDisplayCardsLibrary();
    }
    void dealCard()
    {
        cardLibrary.RemoveAt(0);
    }

    //傳送訊息（對象,訊息編碼,抄收編碼,抄收後續)
    void sendnMessage(main target, int n, int waitcode, voidDelegate method)
    {
        target.message = n;     //傳送訊息---fixme:這邊要請小八修改
        setWaitCode(waitcode);  //設定等待對方回覆的編碼----fixme:等待編碼應該要包含對方的ID ---安全考量
        setAfterCopyMethod(method);   //試定收到成功抄收碼之後的後續行為
        waitResponse(500);      //間隔0.5秒等待
    }
    void waitResponse(int n)
    {
        aTimer = new Timer(n);
        aTimer.Elapsed += new ElapsedEventHandler(checkMessage);
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    void setWaitCode(int n)
    {
        waitResponseCode = n;
    }
    void setAfterCopyMethod(voidDelegate m)
    {
        method = m;
    }
    void checkMessage(object sender, System.Timers.ElapsedEventArgs e)
    {
        string t = "still wait";
        if (message == waitResponseCode)
        {
            t = "got correct response";
            method();
            stopTimer();
        }
        Debug.Log(t);
    }

    void stopTimer()
    {
        aTimer.Enabled = false;
    }
    void createCardsDouble()
    {
        createCardsSingle();
        createCardsSingle();
    }
    void createCardsSingle()
    {
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 13; j++)
            {
                cardLibrary.Add(i * 13 + j);
            }
        }
    }
    void shuffleCards()
    {
        for (var i = 0; i < cardLibrary.Count; i++)
        {
            int r = Random.Range(0, 51);
            int temp = cardLibrary[r];
            cardLibrary[r] = cardLibrary[i];
            cardLibrary[i] = temp;
        }
    }
    void destoryDisplayedCards()
    {
        var displayedCards = GameObject.FindGameObjectsWithTag("cardsLibrary");
        if (displayedCards.Length > 0)
        {
            foreach (var i in displayedCards)
            {
                DestroyImmediate(i);
            }
        }
    }
    void displayCardsLibrary()
    {
        GameObject card = GameObject.Find("card");
        Sprite[] cardsSprite = Resources.LoadAll<Sprite>(@"poker");
        int count = 0;
        for (var i = 0; i < cardLibrary.Count; i++)
        {
            GameObject k = Instantiate(card);
            k.transform.tag = "cardsLibrary";
            int spriteIndex = decode2Index(cardLibrary[i]);
            k.GetComponent<SpriteRenderer>().sprite = cardsSprite[spriteIndex];
            k.name = k.GetComponent<SpriteRenderer>().sprite.name;
            k.transform.position += new Vector3(count * 2, 0, 0);
            count++;
        }
    }
    void refreshDisplayCardsLibrary()
    {
        destoryDisplayedCards();
        displayCardsLibrary();
    }
    string decode2Sting(int n)
    {
        string[] suit = { "club", "diamond", "heart", "spade" };
        string nSuit = suit[n / 100];
        string num = (n % 100).ToString("00");
        return nSuit + num;
    }
    int decode2Index(int n)
    {
        return n / 100 * 13 + n % 100;
    }
}
