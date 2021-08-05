using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card 
{

    public string cardSuit;
    public int cardValue;
    public int cardOwnedBy;
    public Sprite cardSprite;
    public int cardCountValue;
    public bool played;

    public void AssignCountValue()
    {
        played = false;

        if (cardValue < 10)
        {
            cardCountValue = cardValue;
        }

        if(cardValue >= 10)
        {
            cardCountValue = 10;
        }
    }
    
     
}
