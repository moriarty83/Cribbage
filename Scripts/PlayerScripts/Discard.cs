using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;




public class Discard : MonoBehaviour
{
    public Scorer scorer;
    public List<GameObject> toggles;
    public List<GameObject> toDiscard;
    public bool hasDiscarded;
    public Deck gameDeck;

    //This is the object with the Card List for the player hand.
    public GameObject handObject;

    public GameObject deckObject;
    public GreenPlayer myGreenPlayer;

    public List<Card> testCards;
    public List<int> testQuantities;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().enabled = false;
        }

        scorer = GameObject.Find("RoundManager").GetComponent<Scorer>();

        //toggle1.onValueChanged.AddListener(OnToggle1ValueChanged);
        //toggle2.onValueChanged.AddListener(OnToggle2ValueChanged);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().enabled = false;
        }
    }

    public void enterDiscartStage()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().enabled = true;
            toggles[i].GetComponent<Toggle>().onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if(GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Discard)
        { 
        
        for (int i = 0; i < toggles.Count; i++)
        {


            ColorBlock cb1 = toggles[i].GetComponent<Toggle>().colors;
            if (toggles[i].GetComponent<Toggle>().isOn == true && toDiscard.Count < 2 && !toDiscard.Contains(toggles[i]))
            {
                //cb1.normalColor = Color.gray;
                //cb1.highlightedColor = Color.gray;
                    toggles[i].GetComponent<Image>().color = Color.gray;
                if (!toDiscard.Contains(toggles[i]))
                {
                    toDiscard.Add(toggles[i]);
                }
                    return;
            }

            if (toggles[i].GetComponent<Toggle>().isOn == true && toDiscard.Count >= 2 && !toDiscard.Contains(toggles[i]))
            {
                    //cb1.normalColor = Color.white;
                    //cb1.highlightedColor = Color.white;
                    
                    toDiscard.Add(toggles[i]);
                    toggles[i].GetComponent<Image>().color = Color.gray;
                    toggles[i].GetComponent<Toggle>().isOn = true;

                    toDiscard[0].GetComponent<Image>().color = Color.white;
                    toDiscard[0].GetComponent<Toggle>().isOn = false;
                    

                    return;
            }

            

                if (toggles[i].GetComponent<Toggle>().isOn == false && toDiscard.Contains(toggles[i]))
                {
                    //cb1.normalColor = Color.white;
                    //cb1.highlightedColor = Color.white;
                        toggles[i].GetComponent<Image>().color = Color.white;

                        toDiscard.Remove(toggles[i]);
                    return;
                }

            

            toggles[i].GetComponent<Toggle>().colors = cb1;

            }

        }
    }

    public void DiscardToCrib()
    {
        if (hasDiscarded == false)
        {

            if (toDiscard.Count > 1)
            {
                for (int i = 0; i < toDiscard.Count; i++)
                {
                    toDiscard[i].SetActive(false);
                }

            }

            if (toDiscard.Count > 1)
            {
                for (int i = 0; i < toggles.Count; i++)
                {

                    if (toggles[i].GetComponent<Toggle>().isOn == true)
                    {
                        Debug.Log("discarding toggles element " + i);
                        handObject.GetComponent<GreenPlayer>().playerHand[i].cardOwnedBy = 10;
                        deckObject.GetComponent<Deck>().crib.Add(handObject.GetComponent<GreenPlayer>().playerHand[i]);
                        //this.GetComponent<GreenPlayer>().cardImagesUI.Remove(toggles[i]);
                        //this.GetComponent<GreenPlayer>().playerHand.Remove(this.GetComponent<GreenPlayer>().playerHand[i]);


                    }
                    toggles[i].GetComponent<Toggle>().isOn = false;
                }
                removeListeners();
                hasDiscarded = true;
                this.GetComponent<GreenPlayer>().donePlaying = true;

                if(myGreenPlayer.isAIPlayer == true)
                {
                    gameDeck.GetComponent<Deck>().cribCards[2].SetActive(true);
                    gameDeck.GetComponent<Deck>().cribCards[3].SetActive(true);
                }
                if (myGreenPlayer.isAIPlayer == false)
                {
                    gameDeck.GetComponent<Deck>().cribCards[0].SetActive(true);
                    gameDeck.GetComponent<Deck>().cribCards[1].SetActive(true);
                }

            }
        }
        //GameObject.Find("GreenPlayer").GetComponent<GreenPlayer>().UpdateHand();
        //GameObject.Find("GreenPlayer").GetComponent<GreenPlayer>().UpdateHand();

    }

    public void removeListeners()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }

    public void turnTogglesOff()
    {

        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().enabled = true;
            toggles[i].GetComponent<Toggle>().isOn = false;

            ColorBlock cb2 = toggles[i].GetComponent<Toggle>().colors;
            
            //cb2.normalColor = Color.white;
            //cb2.highlightedColor = Color.white;
            toggles[i].GetComponent<Image>().color = Color.white;

        }
    }



    public void AISelectToDiscard()
    {
        List<int> tempToggles = new List<int>();
        List<int> toTurnOn = new List<int>();

        for (int i = 0; i < toggles.Count; i++)
        {
            tempToggles.Add(i);
        }

        this.GetComponent<GreenPlayer>().aIUnplayedCards[0].SetActive(false);
        this.GetComponent<GreenPlayer>().aIUnplayedCards[1].SetActive(false);


        int x1 = Random.Range(0, tempToggles.Count /*- 1*/);

        tempToggles.Remove(x1);

        int x2 = Random.Range(0, tempToggles.Count /*- 1*/);

        tempToggles.Remove(x2);

        toggles[x1].GetComponent<Toggle>().isOn = true;
        toggles[x2].GetComponent<Toggle>().isOn = true;

    }

    public void AIMediumSelectToDiscard()
    {
        List<Card> currentHand = this.GetComponent<GreenPlayer>().playerHand;

        List<int> possibleScores = new List<int>();

        var possibleCardCombos = Combinations<Card>.GetCombinations(currentHand, 4);

        this.GetComponent<GreenPlayer>().aIUnplayedCards[0].SetActive(false);
        this.GetComponent<GreenPlayer>().aIUnplayedCards[1].SetActive(false);

        for (int i = 0; i < possibleCardCombos.Count; i++)
        {
            possibleScores.Add(scorer.scoreFourCards(possibleCardCombos[i]));
        }


        int highestScore = possibleScores.Max();
        int handToSelect = possibleScores.IndexOf(highestScore);

 
        Debug.Log("The highest definite score is: " + highestScore +
                "This is in Card Combo number " + possibleScores.IndexOf(highestScore));

        for (int i = 0; i < currentHand.Count; i++)
        {
            if (!possibleCardCombos[handToSelect].Contains(currentHand[i]))
            {
                toggles[i].GetComponent<Toggle>().isOn = true;
                Debug.Log("Discarding this card");
            }
            if (possibleCardCombos[handToSelect].Contains(currentHand[i]))
            {
                Debug.Log("Keeping this card");
                
            }
            //else toggles[i].GetComponent<Toggle>().isOn = true;
        }
    }

    //AI Hard Difficulty Select
    public void AIHardSelectToDiscard()
    {
        List<Card> currentHand = this.GetComponent<GreenPlayer>().playerHand;

        List<int> handPotentialScore = new List<int>();

        var possibleCardCombos = Combinations<Card>.GetCombinations(currentHand, 4);

        Debug.Log("Hard Discard line 260");

        for (int i = 0; i < testQuantities.Count; i++)
        {
            testQuantities[i] = 4;
        }

        for (int i = 0; i < currentHand.Count; i++)
        {
            if(currentHand[i].cardCountValue == testCards[currentHand[i].cardCountValue].cardCountValue)
            {
                testQuantities[(currentHand[i].cardCountValue - 1)] -= 1;
            }
        }

        for (int i = 0; i < possibleCardCombos.Count; i++)
        {
            handPotentialScore.Add(handPotential(possibleCardCombos[i]));
            Debug.Log("HandPotential Score i is" + handPotentialScore[i]);
        }

        int highestScore = handPotentialScore.Max();
        int handToSelect = handPotentialScore.IndexOf(highestScore);


        Debug.Log("The highest hand potential score is: " + highestScore +
                "This is in Card Combo number " + handPotentialScore.IndexOf(highestScore));

        for (int i = 0; i < currentHand.Count; i++)
        {
            if (!possibleCardCombos[handToSelect].Contains(currentHand[i]))
            {
                toggles[i].GetComponent<Toggle>().isOn = true;
                Debug.Log("Discarding this card");
            }
            if (possibleCardCombos[handToSelect].Contains(currentHand[i]))
            {
                Debug.Log("Keeping this card");

            }
            //else toggles[i].GetComponent<Toggle>().isOn = true;
        }
        this.GetComponent<GreenPlayer>().aIUnplayedCards[0].SetActive(false);
        this.GetComponent<GreenPlayer>().aIUnplayedCards[1].SetActive(false);
    }

    public int handPotential(List<Card> handCombo)
    {
        Debug.Log("running hand potential formula");
        int potentialSum = new int();

        if (scorer.CountFourCardFlush(handCombo, out int fourCardFlushCounted) > 0)
        {
            Debug.Log("combo has a flus");
            for (int i = 0; i < gameDeck.deck.Count; i++)
            {
                Debug.Log("handCombo is " + handCombo.Count + "Elements long.");
                if (!myGreenPlayer.playerHand.Contains(gameDeck.deck[i]))
                {
                    handCombo.Add(gameDeck.deck[i]);
                    Debug.Log("handComob is " + handCombo.Count + " Elements Long.");
                    potentialSum += scorer.fiveCardPotentialScoreWithFlush(handCombo);
                    handCombo.Remove(handCombo[4]);
                }
            }
        }

        if (scorer.CountFourCardFlush(handCombo, out int fourCardFlushCounted_2) == 0)
        {
            Debug.Log("Combo donesn't have a flus");
            for (int i = 0; i < testCards.Count; i++)
            {
                Debug.Log("Running nonflush card from Discard" + testCards[i].cardCountValue);
                    handCombo.Add(testCards[i]);
                    Debug.Log("handComob is " + handCombo.Count + " Elements Long.");
                    Debug.Log("Five Card hand potential without multiplication is " + scorer.fiveCardPotentialScoreWithoutFlush(handCombo));
                    potentialSum += (scorer.fiveCardPotentialScoreWithoutFlush(handCombo)*(testQuantities[handCombo[4].cardCountValue-1]));
                Debug.Log("card potential sum with multiplication" + potentialSum);
                handCombo.Remove(handCombo[4]);
            }
        }
        Debug.Log("hand potentialSum of card combo is :" + potentialSum);
        return potentialSum;
    }


    public void resumeDiscard()
    {
        if(GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage != RoundManager.Stage.Discard)
        {
            return;
        }

        enterDiscartStage();
        List<Card> playerhand = new List<Card>();
        playerhand = this.GetComponent<GreenPlayer>().playerHand;

        List<GameObject> uIObjects = new List<GameObject>();
        uIObjects = this.GetComponent<GreenPlayer>().cardImagesUI;

        if(hasDiscarded == false)
        {
            for (int i = 0; i < uIObjects.Count; i++)
            {
                uIObjects[i].SetActive(true);
            }
        }
    }

    /*
    public int helpfulCardCount(List<Card> cardCombo, int baseScore, out int betterCardsCount)
    {
        Deck gameDeck = GameObject.Find("Deck").GetComponent<Deck>();

        for (int i = 0; i < gameDeck.deck.Count; i++)
        {
            cardCombo.Add(gameDeck.deck[i]);

            
        }
    }
    */
}