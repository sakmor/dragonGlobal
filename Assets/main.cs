using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    Sprite[] cardsSprite;
    public int hostMessage;
    int cardLeft, cardRight;
    bool waitHost;
    GameObject cardA, cardB, cardC;

    // Use this for initialization
    void Start()
    {
        Debug.Log(waitHost);
        GameObject player01 = GameObject.Find("player01");
        cardA = GameObject.Find("mycard");
        // cardB = player01.transform.Find("cardRight").gameObject;
        cardsSprite = Resources.LoadAll<Sprite>(@"poker");


    }

    // Update is called once per frame
    void Update()
    {
        contorl();
        receiveHost();
    }
    void receiveHost()
    {
    }
    void actionByhostMessage()
    {
        int actionType = hostMessage % 1000;
        switch (actionType)
        {
            case 1:
                //host發牌
                break;
            case 2:
                break;
            case 3:
                break;

        }

    }
    void receiveCard(int a, int b)
    {
        cardLeft = a;
        cardRight = b;
        displayCard();
    }
    void displayCard()
    {
        int spriteIndex = decode2Index(cardLeft);
        GameObject.Find("mycard").transform.position += Vector3.up;
        cardA.GetComponent<SpriteRenderer>().sprite = cardsSprite[spriteIndex];
        // spriteIndex = decode2Index(cardRight);
        // cardB.GetComponent<SpriteRenderer>().sprite = cardsSprite[spriteIndex];
    }
    int decode2Index(int n)
    {
        return n / 100 * 13 + n % 100;
    }



    void contorl()
    {

        if (Input.GetKeyUp("space"))
        {

        }
        if (Input.GetKeyUp("f"))
        {

        }
    }






}
