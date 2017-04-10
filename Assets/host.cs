using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

//waitcode
//  101     host抄收自己手上的--第一張牌
//  102     host抄收自己手上的--第二張牌

//流程
/*  1--- 發牌
        
    2--- 問牌

 */
public class host : MonoBehaviour
{
    List<handCards> playersCard = new List<handCards>();
    int randomNum, playerNum;
    List<int> cardLibrary = new List<int>();
    main main;
    void Start()
    {
        playerNum = 8;
        createCardsSingle();
        shuffleCards();
        dealCard(playerNum);
        displayPlayerHandsCard();
    }

    // Update is called once per frame
    void Update()
    {
        refreshDisplayCardsLibrary();
    }
    void dealCard(int n)
    {
        for (int i = 0; i < playerNum; i++)
        {
            handCards temp;
            temp.left = cardLibrary[0];
            temp.right = cardLibrary[1];
            playersCard.Add(temp);
            cardLibrary.Remove(2);
        }
    }

    void removeCardLibrary()
    {
        cardLibrary.RemoveAt(randomNum);
    }

    void setRandomNum()
    {
        randomNum = Random.Range(0, cardLibrary.Count);
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
    void displayPlayerHandsCard()
    {
        Sprite[] cardsSprite = Resources.LoadAll<Sprite>(@"poker");
        for (int i = 1; i <= playerNum; i++)
        {
            GameObject nowPlayer = GameObject.Find("player0" + i);
            nowPlayer.transform.Find("cardLeft").gameObject.GetComponent<SpriteRenderer>().sprite = cardsSprite[cardLibrary[0]];
            nowPlayer.transform.Find("cardRight").gameObject.GetComponent<SpriteRenderer>().sprite = cardsSprite[cardLibrary[1]];
            cardLibrary.RemoveAt(1);
            cardLibrary.RemoveAt(0);
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
    public struct handCards
    {
        public int left, right;
    }
}