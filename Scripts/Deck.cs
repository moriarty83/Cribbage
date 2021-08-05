using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]




public class Deck : MonoBehaviour
{

    public List<Card> deck;
    public List<Card> playerOneCards;
    public List<Card> playerTwoCards; 
    public List<Card> crib;
    public List<GameObject> Players;
    public List<GameObject> cribCards;
    public GameObject fifthCardParent;
    public GameObject fifthCard;
    public Sprite cardBackSprite;
    public Animator anim;

    

    private void Start()
    {
        for (int i = 0; i < GameObject.Find("RoundManager").GetComponent<PlayerManager>().Players.Count; i++)
        {
            Players.Add(GameObject.Find("RoundManager").GetComponent<PlayerManager>().Players[i]);
        }
        

        anim = this.GetComponent<Animator>();
    }

    


    public void shuffleAndDeal()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<GreenPlayer>().ResetHand();
        }
        foreach (Card card in deck)
        {
            if (card.cardValue < 10)
            {
                card.cardCountValue = card.cardValue;
            }

            if (card.cardValue >= 10)
            {
                card.cardCountValue = 10;
            }

        }

        for (int i = 0; i < deck.Count; i++)
        {
            deck[i].cardOwnedBy = 0;
        }

        Shuffler.Shuffle<Card>(deck);


        deck[0].cardOwnedBy = 1;
        deck[1].cardOwnedBy = 1;
        deck[2].cardOwnedBy = 1;
        deck[3].cardOwnedBy = 1;
        deck[4].cardOwnedBy = 1;
        deck[5].cardOwnedBy = 1;

        deck[6].cardOwnedBy = 2;
        deck[7].cardOwnedBy = 2;
        deck[8].cardOwnedBy = 2;
        deck[9].cardOwnedBy = 2;
        deck[10].cardOwnedBy = 2;
        deck[11].cardOwnedBy = 2;

        deck[12].cardOwnedBy = 9;



        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].cardOwnedBy == 1)
            {
                playerOneCards.Add(deck[i]);
            }
        }

        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].cardOwnedBy == 2)
            {
                playerTwoCards.Add(deck[i]);
            }
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<GreenPlayer>().dealPlayerHand();
            Players[i].GetComponent<GreenPlayer>().donePlaying = true;
            if(Players[i].GetComponent<GreenPlayer>().isAIPlayer == true)
            {
                for (int j = 0; j < Players[i].GetComponent<GreenPlayer>().aIUnplayedCards.Count; j++)
                {
                    Players[i].GetComponent<GreenPlayer>().aIUnplayedCards[j].SetActive(true);
                }
            }
        }



        GameObject.Find("RoundManager").GetComponent<StageDeal>().doneDealing();
    }

    public void RevealFifthCard()
    {
        
        GameObject.Find("FifthCard").GetComponent<SpriteRenderer>().sprite = deck[12].cardSprite;
        anim.SetBool("Cutting", true);

        

    }

    public void resetAnimation()
    {
        anim.SetBool("Cutting", false);
    }

    public void AddFifthCardToHands()
    {
        for (int i = 0; i < GameObject.Find("RoundManager").GetComponent<PlayerManager>().Players.Count; i++)
        {
            GameObject.Find("RoundManager").GetComponent<PlayerManager>().Players[i].GetComponent<GreenPlayer>().playerHand.Add(deck[12]);
        }
    }

    public void ResetRound()
    {
        resetAnimation();
        GameObject.Find("FifthCard").GetComponent<SpriteRenderer>().sprite = cardBackSprite;
        for (int i = 0; i < deck.Count; i++)
        {
            deck[i].cardOwnedBy = 0;
            deck[i].played = false;
        }
        for (int i = 0; i < GameObject.Find("RoundManager").GetComponent<PlayerManager>().Players.Count; i++)
        {
            GameObject.Find("RoundManager").GetComponent<PlayerManager>().Players[i].GetComponent<PlayerPlayCards>().clearPlayedCards();
            Players[i].GetComponent<GreenPlayer>().ResetHand();
        }

        for (int i = 0; i < playerOneCards.Count; i++)
        {
            playerOneCards.Remove(playerOneCards[i]);
            i--;
        }
        for (int i = 0; i < playerTwoCards.Count; i++)
        {
            playerTwoCards.Remove(playerTwoCards[i]);
            i--;
        }

        for (int i = 0; i < cribCards.Count; i++)
        {
            cribCards[i].SetActive(false);
        }

    }


    public void ActivateCrib()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].cardOwnedBy == 9 && !crib.Contains(deck[i]))
            {
                crib.Add(deck[i]);
                Debug.Log("Crib Activated");
            }
        }


    }

    public void RevealCrib()
    {

        //Adds the Communal Card to the Crib
        List<Card> crib10 = new List<Card>();
        for (int i = 0; i < crib.Count; i++)
        {
            if (crib[i].cardOwnedBy == 10)
            {
                crib10.Add(crib[i]);
            }
            Debug.Log("Crib10 is " + crib10.Count + " elements long");
        }

        //Displayes the Crib on the Table
        for (int k = 0; k < Players.Count; k++)
        {
            if (Players[k].GetComponent<GreenPlayer>().isDealer == true)
            {
                for (int j = 0; j < crib10.Count; j++)
                {
                    Players[k].GetComponent<PlayerPlayCards>().playedCardObects[j].GetComponent<SpriteRenderer>().sprite = crib10[j].cardSprite;
                    Debug.Log("Sprite for Crib10 element " + j + " displayed");
                }

                for (int i = 0; i < crib.Count; i++)
                {
                    Players[k].GetComponent<GreenPlayer>().crib.Add(crib[i]);
                }
            }
        }

        //Adds the Deck crib to the Dealer's Crib


    }

    public void clearCrib()
    {
        for (int i = 0; i < crib.Count; i++)
        {
            crib.Remove(crib[i]);
            i--;
        }
    }

    public void ScoreJack()
    {
        if(deck[12].cardValue == 11)
        {
            Debug.Log("Jack Flipped");
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].GetComponent<GreenPlayer>().isDealer == true)
                {
                    Players[i].GetComponent<GreenPlayer>().handScore += 2;
                    Players[i].GetComponent<GreenPlayer>().scorePoints();
                    Players[i].GetComponent<GreenPlayer>().roundCuttingRightJack += 1;
                }
            }
        }
    }
}

