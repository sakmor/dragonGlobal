using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    Sprite[] cardsSprite;
    public int message;
    int cardLeft, cardRight;
    host host;
    GameObject cardA, cardB, cardC;

    // Use this for initialization
    void Start()
    {
        // host = GameObject.Find("host").GetComponent<host>();
    }

    // Update is called once per frame
    void Update()
    {
        contorl();
    }
    void displayCard()
    {
        int spriteIndex = decode2Index(cardLeft);
        cardA.GetComponent<SpriteRenderer>().sprite = cardsSprite[spriteIndex];
        spriteIndex = decode2Index(cardRight);
        cardB.GetComponent<SpriteRenderer>().sprite = cardsSprite[spriteIndex];
    }
    int decode2Index(int n)
    {
        return n / 100 * 13 + n % 100;
    }

    void contorl()
    {
        if (Input.GetKeyUp("space"))
        {
            displayCard();
        }
    }

    void requestHandCard()
    {

    }






}
