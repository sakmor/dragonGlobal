﻿using System.Collections;
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
        host = GameObject.Find("host").GetComponent<host>();
        GameObject player01 = GameObject.Find("player01");
        cardA = player01.transform.Find("cardLeft").gameObject;
        cardB = player01.transform.Find("cardRight").gameObject;
        cardsSprite = Resources.LoadAll<Sprite>(@"poker");


    }

    // Update is called once per frame
    void Update()
    {
        contorl();
        receiveHost();
    }
    //waitcode
    void receiveHost()// fixme:檢查是否有host的訊息要請小八改一下
    {
        if (message != 0)
        {
            cardLeft = message;

        }
    }
    void sendHost(int n)
    {
        host.message = n;
    }

    void receiveCard(int card, int cardValue)
    {

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
            sendHost(101);
            displayCard();
        }
    }






}
