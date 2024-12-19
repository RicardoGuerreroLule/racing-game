using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardVals = new int[53];
    int currentCard;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void getCardVal()
    {
        int val = 0;
        for(int i = 0; i < cardSprites.Length; i++)
        {
            val = i;
            //count cards
            val %= 13;

            if(val > 10 || val == 0)
            {
                val = 10;
            } // face cards
            cardVals[i] = val++;
        }
        currentCard = 1;
    }
    public void shuffleCards()
    {
        for(int i = cardSprites.Length -1; i > 0; i--)
        {
            int temp = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[temp];
            cardSprites[temp] = face;

            int value = cardVals[i];
            cardVals[i] = cardVals[temp];
            cardVals[temp] = value;
        }
    }
    public int dealCard()
    {
        //script.SetSprite
        return 0;
    }
}
