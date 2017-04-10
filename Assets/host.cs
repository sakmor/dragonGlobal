using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class host : MonoBehaviour
{
    List<int> cardLibrary = new List<int>();
    int playerNum;
    // Use this for initialization
    void Start()
    {
        createCardsDouble();
        shuffleCards();
        displayCardsLibrary();
        dealCardto();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void dealCardto()
    {
        // GameObject.Find("main").GetComponent<main>().receiveCard(cardLibrary[0], cardLibrary[1]);
        cardLibrary.RemoveAt(1);
        cardLibrary.RemoveAt(0);
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
