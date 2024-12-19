using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum blackJackStates { start, player1Draw, dealerDraw, player1Turn, dealerTurn }
public class blackjack : MonoBehaviour
{
    public blackJackStates state;
    public GameObject p1;
    public GameObject dealer;

    public Text dealerC;
    public Text p1C;

    public int dealerCount;
    public int p1Count;

    List<int> cards = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        state = blackJackStates.start;
        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator startGame()
    {
        //this will generate a deck
        //card #11 = Jack, card #12 = Queen, card #13 = King
        /*for(int i = 0; i < 53; i++){
            cards.Add(i);
        }*/
        yield return new WaitForSeconds(2f);
        if (state == blackJackStates.start)
        {
            playerTurn(cards);
            state = blackJackStates.player1Draw;
        }
    }

    IEnumerator playerTurn(List<int> cards)
    {
        if (state == blackJackStates.player1Draw)
        {
            //select 1 random card
            int firstCard = drawCard();
            cards.Add(firstCard);
            cards.Add(drawSecond(firstCard));
        }

        int combinedVal = cards[0] + cards[1];
        p1C.text = combinedVal + "";

        //returning cards for actual player turn
        yield return combinedVal;
    }

    public int drawCard()
    {
        int rand = UnityEngine.Random.Range(0, 52);
        return rand;
    }

    public int drawSecond(int firstCard)
    {
        int rand = UnityEngine.Random.Range(0, 52);
        if (firstCard + rand > 21)
        {
            drawSecond(firstCard);
        }
        return rand;
    }
}
