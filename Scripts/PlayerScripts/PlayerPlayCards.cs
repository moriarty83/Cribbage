using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerPlayCards : MonoBehaviour
{
    public List<GameObject> togglesOn;
    public List<GameObject> toggles;
    public Transform handUIObject;
    public List<GameObject> playedCardObects;
    public Sprite nullSprite;
    public int numberCardsPlayed;
    public bool saidGo;
    public bool lastToGo;
    public GameObject goUI;
    public Text goText;
    public bool canPlayCard;
    public RoundManager roundManager;
    public PlayingOfHand playingOfHandManager;

    public GameObject lastCardButton;

    public int aINumberCardsPlayed;

    public bool isMyTurn;
    public bool lastCardPlayed;

    // Start is called before the first frame update
    void Start()
    {
        playingOfHandManager = GameObject.Find("RoundManager").GetComponent<PlayingOfHand>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        numberCardsPlayed = 0;
        lastCardButton.SetActive(false);
        //isMyTurn = this.GetComponent<GreenPlayer>().isDealer;

    }

    public void playerCreated()
    {
        playingOfHandManager = GameObject.Find("RoundManager").GetComponent<PlayingOfHand>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        numberCardsPlayed = 0;
        lastCardButton.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (roundManager.activeStage == RoundManager.Stage.PlayingOfHand)
        {
            for (int i = 0; i < toggles.Count; i++)
            {
                ColorBlock cb1 = toggles[i].GetComponent<Toggle>().colors;

                //Makes card gray and adds it to the "Toggles On" List, prepping it to be discarded.
                if (toggles[i].GetComponent<Toggle>().isOn == true && togglesOn.Count < 1)
                {
                    //cb1.normalColor = Color.gray;
                    //cb1.highlightedColor = Color.gray;
                    toggles[i].GetComponent<Image>().color = Color.gray;

                    //Debug.Log("Ready to Play Toggle #" + i);

                    if (!togglesOn.Contains(toggles[i]))
                    {
                        togglesOn.Add(toggles[i]);

                    }
                }

                if (toggles[i].GetComponent<Toggle>().isOn == true && togglesOn.Count == 1 && !togglesOn.Contains(toggles[i]))
                {
                    togglesOn[0].GetComponent<Toggle>().isOn = false;
                    togglesOn[0].GetComponent<Image>().color = Color.white;
                    togglesOn.Remove(togglesOn[0]);
                    togglesOn.Add(toggles[i]);
                    //Debug.Log("Ready to Play Toggle #" + i);
                    toggles[i].GetComponent<Image>().color = Color.gray;
                    Debug.Log("Picking new card");

                }

                //If another card is already selected, keeps the toggle colored white
                //and keeps it from being added to "TogglesOn" list.
                //and turns it off again immediately.
                if (toggles[i].GetComponent<Toggle>().isOn == true && togglesOn.Count >= 1 && !togglesOn.Contains(toggles[i]))
                {
                    //cb1.normalColor = Color.white;
                    //cb1.highlightedColor = Color.white;
                    toggles[i].GetComponent<Image>().color = Color.white;

                    toggles[i].GetComponent<Toggle>().isOn = false;
                }

                
                //If the card being clicked is already selected, it makes it white and removes it
                //fomr the "TogglesOn" list.
                if (toggles[i].GetComponent<Toggle>().isOn == false && togglesOn.Contains(toggles[i]))
                    {
                        //cb1.normalColor = Color.white;
                        //cb1.highlightedColor = Color.white;
                    toggles[i].GetComponent<Image>().color = Color.white;

                    togglesOn.Remove(toggles[i]);
                    }

                
                toggles[i].GetComponent<Toggle>().colors = cb1;
            }

        }
    }


    public void populateTogglesList()
    {
        int children = handUIObject.childCount;

        for (int i = 0; i < children; i++)
        {
            if (handUIObject.GetChild(i).gameObject.activeInHierarchy == true && !toggles.Contains(handUIObject.GetChild(i).gameObject))
            {
                toggles.Add(handUIObject.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().onValueChanged.AddListener(OnToggleValueChanged);
        }
        //Debug.Log("Toggles populated");
    }

    public void removeListeners()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }

    public void PlayCardVisuals()
    {

        for (int i = 0; i < toggles.Count; i++)
        {
            ColorBlock cb1 = toggles[i].GetComponent<Toggle>().colors;

            Debug.Log("i is element " + i);

            if (toggles[i].GetComponent<Toggle>().isOn == true && this.GetComponent<GreenPlayer>().playerHand[i].played == false)
            {

                //Checks to see if card can be played, i.e. if card value doesn't exceed 31.
                if ((GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount + this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue) <= 31)
                {
                    canPlayCard = true;
                    this.gameObject.GetComponent<GreenPlayer>().playerHand[i].played = true;

                    //Adds card value to the count value.
                    GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount += this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue;

                    //Adds the card value to the list of card values for scoring purposes
                    GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCardValues.Add(this.GetComponent<GreenPlayer>().playerHand[i].cardValue);

                    //Adds the count value (jacks, kings and queens = 10) the list if count values.
                    GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCountValues.Add(this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue);

                    //Adds the card value to the ActiveRuns list in the Playing of Hand Script.
                    GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().ActiveRuns.Add(this.GetComponent<GreenPlayer>().playerHand[i].cardValue);

                    //Updates the UI with the current count of the round.
                    GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCountTextUI.GetComponent<Text>().text = GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount.ToString();

                    //Removes the played card from the TogglesOn list.
                    togglesOn.Remove(toggles[i]);

                    //This might not be needed anymore.
                    GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playedCards.Add(this.GetComponent<GreenPlayer>().playerHand[i]);


                    //Adjust UI to Put cards on table.
                    playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().enabled = true;
                    playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<GreenPlayer>().playerHand[i].cardSprite;
                    numberCardsPlayed += 1;


                    toggles[i].GetComponent<Toggle>().isOn = false;
                    toggles[i].GetComponent<Toggle>().enabled = false;
                    toggles[i].GetComponent<Image>().color = Color.clear;

                    //Debug.Log("Can Play card");



                    GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCard(this.gameObject);


                    /*if (GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().totalCardsPlayed < (GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().Players.Count * 4))
                    {
                        GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playScore = 0;
                        GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().updatePlayerTurn();
                    }*/
                    
                }
            }
            else
                canPlayCard = false;
        }
    }

    /*public void checkFor31()
    {
        if (GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount == 31)
        {
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().totalCardsPlayed += 1;
            //goUI.GetComponent<Text>().text = "31!";
            //goUI.SetActive(true);
            StartCoroutine(GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().resetPlayRound());
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().updatePlayerTurn();

            return;

        }
    }*/

    public void checkForLastCard()
    {
        if (GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().totalCardsPlayed == (GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().Players.Count * 4))
        {
            this.gameObject.GetComponent<GreenPlayer>().handScore += 1;
            lastCardPlayed = true;
            lastCardButton.SetActive(true);
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().LastCardPlayed();
            //this.gameObject.GetComponent<GreenPlayer>().scorePoints();
        }
    }


    public void PlayingOver()
    {
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.PlayingOfHand)
            for (int i = 0; i < toggles.Count; i++)
            {
                toggles[i].GetComponent<Image>().color = Color.white;
                //Debug.Log("Sprite color set");
            }
        else
            return;
    }


    public void clearPlayedCards()
    {
        for (int i = 0; i < playedCardObects.Count; i++)
        {
            playedCardObects[i].GetComponent<SpriteRenderer>().enabled = true;
            playedCardObects[i].GetComponent<SpriteRenderer>().sprite = nullSprite;

        }
        //Debug.Log("Played cards cleared");

    }

    public void playCard()
    {
        if (isMyTurn == true)
        {
            PlayCardVisuals();
            
        }
        else
            return;
    }


    public void go()
    {
        Debug.Log("Saying go - regular");
        List<Card> unplayedCards = new List<Card>();
        List<Card> unplayedAndCantPlay = new List<Card>();
        List<GameObject> hasSaidGo = new List<GameObject>();

        int currentPlayScore = GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount;

        //Adds cards to Unplayed Cards List
        for (int i = 0; i < this.GetComponent<GreenPlayer>().playerHand.Count; i++)
        {
            if (this.GetComponent<GreenPlayer>().playerHand[i].played == false)
            {
                unplayedCards.Add(this.GetComponent<GreenPlayer>().playerHand[i]);
            }
        }

        //Adds cards to a list of cards that Can't be played.
        for (int i = 0; i < unplayedCards.Count; i++)
        {
            if(unplayedCards[i].cardCountValue + currentPlayScore > 31)
            {
                unplayedAndCantPlay.Add(unplayedCards[i]);
            }
        }

        //Returns if player still has cards to play.
        if(unplayedCards.Count != unplayedAndCantPlay.Count)
        {
            return;
        }

        //Looks to see how many other players had said go.
        foreach (GameObject player in GameObject.Find("RoundManager").GetComponent<RoundManager>().players)
        {
            if (player.GetComponent<PlayerPlayCards>().saidGo == true)
            {
                hasSaidGo.Add(player);
            }
        }

        //Action for when the number of unplayed cards is the same of cards you can't play which is allow Go.
        if (unplayedCards.Count == unplayedAndCantPlay.Count)
        {
            Debug.Log("Human saying go");
            saidGo = true;
            hasSaidGo.Add(this.gameObject);
            goText.GetComponent<Text>().text = "Go";
            goUI.SetActive(true);
            //Debug.Log("Is allowed to Click Go");

            //If the number of players who has said go is the same as the number of players in the game.
            //The player gets one point.
            if (hasSaidGo.Count == GameObject.Find("RoundManager").GetComponent<RoundManager>().players.Count)
            {
                this.GetComponent<GreenPlayer>().handScore += 1;

                this.GetComponent<GreenPlayer>().scorePoints();
                lastToGo = true;
                StartCoroutine(GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().resetPlayRound());

                //GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().checkForEndOfRound();

                return;
            }

         GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().updatePlayerTurn();

        }
    }

    public void turnTogglesOff()
    {

        for (int i = 0; i < toggles.Count; i++)
        {
            ColorBlock cb1 = toggles[i].GetComponent<Toggle>().colors;
            {
                
                toggles[i].GetComponent<Toggle>().isOn = false;
                //cb1.normalColor = Color.white;
                //cb1.highlightedColor = Color.white;
                toggles[i].GetComponent<Image>().color = Color.white;

            }
            toggles[i].GetComponent<Toggle>().colors = cb1;
        }
    }

    public void clearTogglesList()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles.Remove(toggles[i]);
            i--;
        }
    }

    public void startPlayRound()
    {
        numberCardsPlayed = 0;
    }

    public bool canPlayACard()
    {
        List<int> playableCards = new List<int>();

        for (int i = 0; i < this.GetComponent<GreenPlayer>().playerHand.Count; i++)
        {
            if (this.GetComponent<GreenPlayer>().playerHand[i].played == false &&
                this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue + GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount <= 31)
            {
                playableCards.Add(i);
                //Debug.Log("Added Card " + i + " To playable Cards");
            }
        }
        if (playableCards.Count > 0 /*&& roundManager.GetComponent<PlayingOfHand>().totalCardsPlayed < (roundManager.GetComponent<PlayingOfHand>().Players.Count * 2)*/)
        {
            return true;
        }
        else
            return false;
    }

    public void AIPlayCard()
    {
        Debug.Log("Doing AIPlayercard");
        //Populates Unplayed Cards That Can Be Played
        List<int> playableCards = new List<int>();
        if (saidGo == true)
        {
            return;
        }

        for (int i = 0; i < this.GetComponent<GreenPlayer>().playerHand.Count; i++)
        {
            if (this.GetComponent<GreenPlayer>().playerHand[i].played == false &&
                this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue + GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount <= 31)
            {
                playableCards.Add(i);
                //Debug.Log("Added Card " + i + " To playable Cards");
            }
        }
        if (playableCards.Count == 0 /*&& roundManager.GetComponent<PlayingOfHand>().totalCardsPlayed < (roundManager.GetComponent<PlayingOfHand>().Players.Count * 4)*/)
        {
            Debug.Log("AI is Saying Go");
            AIGo();

            return;
        }

        if (playableCards.Count > 0)
        {

            //Randomly picks playable Card 
            int x1 = Random.Range(0, playableCards.Count);

            //Removes card from face down cards on table
            aINumberCardsPlayed += 1;
            this.GetComponent<GreenPlayer>().aIUnplayedCards[aINumberCardsPlayed + 1].SetActive(false);

            //Plays randomly selected card:

            this.gameObject.GetComponent<GreenPlayer>().playerHand[playableCards[x1]].played = true;

            //Adds card value to the count value.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount += this.GetComponent<GreenPlayer>().playerHand[playableCards[x1]].cardCountValue;

            //Adds the card value to the list of card values for scoring purposes
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCardValues.Add(this.GetComponent<GreenPlayer>().playerHand[playableCards[x1]].cardValue);

            //Adds the count value (jacks, kings and queens = 10) the list if count values.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCountValues.Add(this.GetComponent<GreenPlayer>().playerHand[playableCards[x1]].cardCountValue);

            //Adds the card value to the ActiveRuns list in the Playing of Hand Script.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().ActiveRuns.Add(this.GetComponent<GreenPlayer>().playerHand[playableCards[x1]].cardValue);

            //Updates the UI with the current count of the round.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCountTextUI.GetComponent<Text>().text = GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount.ToString();

            //Removes the played card from the TogglesOn list.
            togglesOn.Remove(toggles[playableCards[x1]]);

            //This might not be needed anymore.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playedCards.Add(this.GetComponent<GreenPlayer>().playerHand[playableCards[x1]]);


            //Adjust UI to Put cards on table.
            playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().enabled = true;
            playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<GreenPlayer>().playerHand[playableCards[x1]].cardSprite;
            numberCardsPlayed += 1;


            toggles[playableCards[x1]].GetComponent<Toggle>().isOn = false;
            toggles[playableCards[x1]].GetComponent<Toggle>().enabled = false;
            toggles[playableCards[x1]].GetComponent<Image>().color = Color.clear;

            //Debug.Log("Can Play card");

            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCard(this.gameObject);

            return;
        }
    }


        //MEDIUM DIFFICULTY AIPLAYCARDS: Plays highest value card possible.

        public void aIMediumPlayCard()
        {
            Debug.Log("Doing AIPlayercard");
            //Populates Unplayed Cards That Can Be Played
            List<int> playableCards = new List<int>();
            List<int> playableCountVales = new List<int>();
            if (saidGo == true)
            {
                return;
            }

            for (int i = 0; i < this.GetComponent<GreenPlayer>().playerHand.Count; i++)
            {
                if (this.GetComponent<GreenPlayer>().playerHand[i].played == false &&
                    this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue + GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount <= 31)
                {
                    playableCards.Add(i);
                    playableCountVales.Add(this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue);
                    //Debug.Log("Added Card " + i + " To playable Cards");
                }
            }
            if (playableCards.Count == 0 /*&& roundManager.GetComponent<PlayingOfHand>().totalCardsPlayed < (roundManager.GetComponent<PlayingOfHand>().Players.Count * 4)*/)
            {
                Debug.Log("AI is Saying Go");
                AIGo();

                return;
            }

        if (playableCards.Count > 0)
        {

            int highestPlayableElement = playableCountVales.IndexOf(playableCountVales.Max());
            int x1 = playableCards[highestPlayableElement];
            Debug.Log("x1 = " + x1);



            //Removes card from face down cards on table
            aINumberCardsPlayed += 1;
                this.GetComponent<GreenPlayer>().aIUnplayedCards[aINumberCardsPlayed + 1].SetActive(false);

                //Plays randomly selected card:

                this.gameObject.GetComponent<GreenPlayer>().playerHand[x1].played = true;

                //Adds card value to the count value.
                GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount += this.GetComponent<GreenPlayer>().playerHand[x1].cardCountValue;

                //Adds the card value to the list of card values for scoring purposes
                GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCardValues.Add(this.GetComponent<GreenPlayer>().playerHand[x1].cardValue);

                //Adds the count value (jacks, kings and queens = 10) the list if count values.
                GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCountValues.Add(this.GetComponent<GreenPlayer>().playerHand[x1].cardCountValue);

                //Adds the card value to the ActiveRuns list in the Playing of Hand Script.
                GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().ActiveRuns.Add(this.GetComponent<GreenPlayer>().playerHand[x1].cardValue);

                //Updates the UI with the current count of the round.
                GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCountTextUI.GetComponent<Text>().text = GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount.ToString();

                //Removes the played card from the TogglesOn list.
                togglesOn.Remove(toggles[x1]);

                //This might not be needed anymore.
                GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playedCards.Add(this.GetComponent<GreenPlayer>().playerHand[x1]);


                //Adjust UI to Put cards on table.
                playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().enabled = true;
                playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<GreenPlayer>().playerHand[x1].cardSprite;
                numberCardsPlayed += 1;


                toggles[x1].GetComponent<Toggle>().isOn = false;
                toggles[x1].GetComponent<Toggle>().enabled = false;
                toggles[x1].GetComponent<Image>().color = Color.clear;

                //Debug.Log("Can Play card");

                GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCard(this.gameObject);

                return;
            }

        }

    public void aIHardPlayCard()
    {
        GreenPlayer thisGreenPlayer = this.GetComponent<GreenPlayer>();

        Debug.Log("Doing AIPlayercard");
        //Populates Unplayed Cards That Can Be Played
        List<int> playableCards = new List<int>();
        List<int> playableCountVales = new List<int>();
        List<int> pointsIfPlayed = new List<int>();


        if (saidGo == true)
        {
            return;
        }

        for (int i = 0; i < this.GetComponent<GreenPlayer>().playerHand.Count; i++)
        {
            if (thisGreenPlayer.playerHand[i].played == false &&
                this.GetComponent<GreenPlayer>().playerHand[i].cardCountValue + GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount <= 31)
            {
                playableCards.Add(i);
                pointsIfPlayed.Add(playingOfHandManager.testScorePlayedCards(thisGreenPlayer.playerHand[i]));
                Debug.Log("PointsIfPlayed is now " + pointsIfPlayed.Count + "elements long");
                
                //Debug.Log("Added Card " + i + " To playable Cards");
            }
        }
        if (playableCards.Count == 0 /*&& roundManager.GetComponent<PlayingOfHand>().totalCardsPlayed < (roundManager.GetComponent<PlayingOfHand>().Players.Count * 4)*/)
        {
            Debug.Log("AI is Saying Go");
            AIGo();

            return;
        }

        if (playableCards.Count > 0)
        {

            int elementToPlay = new int();
            if (pointsIfPlayed.Max() > 0)
            {
                elementToPlay = pointsIfPlayed.IndexOf(pointsIfPlayed.Max());
            }
            if(pointsIfPlayed.Max() == 0)
            {
                elementToPlay = Random.Range(0, playableCards.Count);
            }
            int x1 = playableCards[elementToPlay];
            Debug.Log("x1 = " + x1);



            //Removes card from face down cards on table
            aINumberCardsPlayed += 1;
            this.GetComponent<GreenPlayer>().aIUnplayedCards[aINumberCardsPlayed + 1].SetActive(false);

            //Plays randomly selected card:

            this.gameObject.GetComponent<GreenPlayer>().playerHand[x1].played = true;

            //Adds card value to the count value.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount += this.GetComponent<GreenPlayer>().playerHand[x1].cardCountValue;

            //Adds the card value to the list of card values for scoring purposes
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCardValues.Add(this.GetComponent<GreenPlayer>().playerHand[x1].cardValue);

            //Adds the count value (jacks, kings and queens = 10) the list if count values.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().activeCountValues.Add(this.GetComponent<GreenPlayer>().playerHand[x1].cardCountValue);

            //Adds the card value to the ActiveRuns list in the Playing of Hand Script.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().ActiveRuns.Add(this.GetComponent<GreenPlayer>().playerHand[x1].cardValue);

            //Updates the UI with the current count of the round.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCountTextUI.GetComponent<Text>().text = GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount.ToString();

            //Removes the played card from the TogglesOn list.
            togglesOn.Remove(toggles[x1]);

            //This might not be needed anymore.
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playedCards.Add(this.GetComponent<GreenPlayer>().playerHand[x1]);


            //Adjust UI to Put cards on table.
            playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().enabled = true;
            playedCardObects[numberCardsPlayed].GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<GreenPlayer>().playerHand[x1].cardSprite;
            numberCardsPlayed += 1;


            toggles[x1].GetComponent<Toggle>().isOn = false;
            toggles[x1].GetComponent<Toggle>().enabled = false;
            toggles[x1].GetComponent<Image>().color = Color.clear;

            //Debug.Log("Can Play card");

            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCard(this.gameObject);

            return;
        }

    }

    public void AIGo()
    {
        Text goText = goUI.GetComponentInChildren<Text>();

        if (roundManager.GetComponent<PlayingOfHand>().CheckForEndOfStage() == true)
        {
            goText.text = "Last \n Card";
            goText.fontSize = 28;
            //goText.GetComponent<Text>().text = "Go";
            goUI.SetActive(true);
            return;
        }
        Debug.Log("Saying Go - AI");
        List<GameObject> hasSaidGo = new List<GameObject>();

        goText.text = "Go";
        goText.fontSize = 58;

        //Looks to see how many other players had said go.
        foreach (GameObject player in GameObject.Find("RoundManager").GetComponent<RoundManager>().players)
        {
            if (player.GetComponent<PlayerPlayCards>().saidGo == true)
            {
                hasSaidGo.Add(player);
            }
        }

        saidGo = true;
        hasSaidGo.Add(this.gameObject);
        goUI.SetActive(true);

        //If the number of players who has said go is the same as the number of players in the game...
        //But there are more cards to be played in the round/stage...
        //The player gets one point.
        if (hasSaidGo.Count == roundManager.players.Count &&
            playingOfHandManager.totalCardsPlayed != (roundManager.players.Count * 4))
        {
            goUI.SetActive(true);
            
            
            Debug.Log("Total played cards is " + playingOfHandManager.totalCardsPlayed + " resetting round");
            this.GetComponent<GreenPlayer>().handScore += 1;

            this.GetComponent<GreenPlayer>().scorePoints();

            lastToGo = true;
            this.GetComponent<GreenPlayer>().activePlayer = false;
            StartCoroutine(roundManager.GetComponent<PlayingOfHand>().resetPlayRound());

            return;
        }



        //If number of players who said go is the same as number of players in the game...
        //And if there are no more cards to be played

        /*if (hasSaidGo.Count == roundManager.players.Count &&
            playingOfHandManager.totalCardsPlayed == (roundManager.players.Count * 4))
        {
            Debug.Log("AI Go, Scoring last card");
            playingOfHandManager.scoreLastCard(this.gameObject);
            return;
        }
        */
        if (roundManager.GetComponent<PlayingOfHand>().CheckForEndOfStage() == false)
        {

            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().updatePlayerTurn();
        }
        else
            return;
        
    }



    public void endGame()
    {
           
        numberCardsPlayed = 0;
        saidGo = false;
        lastToGo = false;
        canPlayCard = false;


        aINumberCardsPlayed = 0;
        isMyTurn = false;
        lastCardPlayed = false;
        goUI.SetActive(false);

        lastCardButton.SetActive(false);

}

public IEnumerator CheckLastCard()
    {
        yield return new WaitForSeconds(2);
        if(lastCardButton.activeSelf)
        {
            this.gameObject.GetComponent<GreenPlayer>().scorePoints();
            GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().scoreLastCard(this.gameObject);
        }
    }

    private IEnumerator resetRoundDelay()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().resetPlayRound();

    }

}
