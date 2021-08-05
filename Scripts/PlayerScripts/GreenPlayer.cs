using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

[Serializable]

public class GreenPlayer : MonoBehaviour
{
    public bool isAIPlayer;
    public bool aIProceed;
    public bool aIRunning;
    public GameObject aIContinueButton;
    public Deck gameDeck;
    public BasicAI playerAI;

    //public bool waitingToPlay;
    public bool donePlaying;
    public bool activePlayer;
    public int playerNumber;
    public bool isDealer;

    public string playerColor;
    GameObject roundManagerObject;
    public RoundManager roundManagerScript;
    public RoundSummary roundSummaryScript;
    public SaveLoadGameState gameState;
    public GreenPlayer thisGreenPlayer;
    public Tutorial tutorial;
    public SaveData lifetimeStats;

    public GameObject lifetimeStatsEraseConfirm;

    public int handScore = 0;
    public int totalScore = 0;
    public int pegNumber;
    public List<GameObject> holes;
    public GameObject[] pegs = new GameObject[3];
    public bool[] moveNext = new bool[3];

    public Vector3[] startHoleVector = new Vector3[3];
    public Vector3[] liftPegTo = new Vector3[3];
    public Vector3[] lowerPegFrom = new Vector3[3];
    public Vector3[] targetPegVector = new Vector3[3];

    public List<Card> deckOfCards;
    [SerializeField] public List<Card> playerHand;
    public List<int> runValues;
    public List<GameObject> cardImagesUI = new List<GameObject>(6);
    public Transform handUIParent;
    public GameObject yourTurnUI;
    public GameObject yourTurnBorderUI;
    public GameObject countSummaryUI;
    public Sprite nullSprite;

    public int playHandScore;
    public Scorer scorer;
    public GameObject summaryObject;

    public GameObject speechBubble;
    public GameObject speechBubbleText;

    public Color32 playerGreen = new Color32(0, 155, 0, 255);

    public List<GameObject> aIUnplayedCards = new List<GameObject>();

    public GameObject borderObject;
    public GameObject donePlayingButton;
    public Material turtleSkin;
    public Color32 myColor;

    //public bool allowedToScore;

    public List<Card> crib;

    public bool counted;
    public bool scored;

    //Round Summary Variables
    //Totals
    public int roundPlayingOfHandScore;
    public int roundCountingHandScore;
    public int roundCountingCribScore;

    //Right Jack flipped
    public int roundCuttingRightJack;

    //Counting Hand
    public int roundHandPairs;
    public int roundHandFifteens;

    public int roundHand5Runs;
    public int roundHand4Runs;
    public int roundHand3Runs;

    public int roundHand5Flushes;
    public int roundHand4Flushes;

    public int roundHandRightJack;
    //Counting Crib
    public int roundCribPairs;
    public int roundCribFifteens;

    public int roundCrib5Runs;
    public int roundCrib4Runs;
    public int roundCrib3Runs;

    public int roundCrib5Flushes;
    public int roundCrib4Flushes;

    public int roundCribRightJack;

    //Game Summary Variables
    //Totals
    public int gamePlayingOfHandScore;
    public int gameCountingHandScore;
    public int gameCountingCribScore;
    //Breakdown
    public int gameRightJackFlipped;

    public int gameTotalFifteens;
    public int gameTotalPairs;

    public int gameTotal5Runs;
    public int gameTotal4Runs;
    public int gameTotal3Runs;

    public int gameTotalFlushes;

    public int gameRightJack;


    

    private void Awake()
    {
        lifetimeStats = new SaveData();
        lifetimeStats.loadData(lifetimeStats, thisGreenPlayer);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        roundManagerObject = GameObject.Find("RoundManager");
        if (isAIPlayer == true)
        {
            playerAI = new BasicAI();
            
            playerAI.findme();

            
            /*foreach (GameObject card in GameObject.FindGameObjectsWithTag("UnplayedCard"))
            {
                aIUnplayedCards.Add(card);
            }*/

        }

        for (int i = 0; i < aIUnplayedCards.Count; i++)
        {
            aIUnplayedCards[i].SetActive(false);
        }

        /*
        SetmyColor();
        borderObject.GetComponent<Image>().color = myColor;
        scorer = GameObject.Find("RoundManager").GetComponent<Scorer>();
        //myColor = "Green";

        foreach (GameObject hole in GameObject.FindGameObjectsWithTag(playerColor))
        {
            holes.Add(hole);
        }
        holes.Add(GameObject.Find("VictoryHole"));
        

        pegs = GameObject.FindGameObjectsWithTag(playerColor + "Peg");
        moveNext[0] = true;
        moveNext[1] = false;
        moveNext[2] = false;

        handScore = 0;

        deckOfCards = GameObject.Find("Deck").GetComponent<Deck>().deck;
        */

    }

    // Update is called once per frame
    void Update()
    {
        borderObject.GetComponent<Image>().color = myColor;

        if (tutorial.tutorialOn == true && tutorial.tutorialGotIt == true || tutorial.tutorialOn == false)
        {
            //Debug.Log("tutorialOn is " + tutorial.tutorialOn);
            if (activePlayer == true && isAIPlayer == true && aIRunning == false && donePlaying == false)
            {

                if (roundManagerScript.activeStage == RoundManager.Stage.Discard && playerAI.aIDifficulty == BasicAI.difficulty.hard)
                {
                    playerAI.DoAI();
                }
                else
                    StartCoroutine(doAIInASecond());
                //playerAI.DoAI();
            }
        }
    }

    public void startGame()
    {
        lifetimeStats.gameInProgress = 1;
        lifetimeStats.gamesPlayed += 1;
        lifetimeStats.saveData(lifetimeStats, thisGreenPlayer);
        roundManagerObject = GameObject.Find("RoundManager");
        if (isAIPlayer == true)
        {
            playerAI = new BasicAI();
            playerAI.findme();

            for (int i = 0; i < aIUnplayedCards.Count; i++)
            {
                aIUnplayedCards[i].SetActive(false);
            }
        }

        SetmyColor();
        scorer = GameObject.Find("RoundManager").GetComponent<Scorer>();
        //myColor = "Green";

        foreach (GameObject hole in GameObject.FindGameObjectsWithTag(playerColor))
        {
            holes.Add(hole);
        }

    
        holes.Add(GameObject.Find("VictoryHole"));
        holes = holes.OrderBy(holes => holes.name).ToList();


        pegs = GameObject.FindGameObjectsWithTag(playerColor + "Peg");
        moveNext[0] = true;
        moveNext[1] = false;
        moveNext[2] = false;

        handScore = 0;

        deckOfCards = GameObject.Find("Deck").GetComponent<Deck>().deck;

    }

    public void playerCreated()
    {
        roundManagerObject = GameObject.Find("RoundManager");
        if (isAIPlayer == true)
        {
            playerAI = new BasicAI();
            playerAI.findme();
        }


        SetmyColor();
        borderObject.GetComponent<Image>().color = myColor;
        scorer = GameObject.Find("RoundManager").GetComponent<Scorer>();

        foreach (GameObject hole in GameObject.FindGameObjectsWithTag(playerColor))
        {
            holes.Add(hole);
        }
        holes.Add(GameObject.Find("VictoryHole"));

        pegs = GameObject.FindGameObjectsWithTag(playerColor + "Peg");
        moveNext[0] = true;
        moveNext[1] = false;
        moveNext[2] = false;

        handScore = 0;

        deckOfCards = GameObject.Find("Deck").GetComponent<Deck>().deck;
    }

    public void dealPlayerHand() {
        handScore = 0;
        for (int i = 0; i < deckOfCards.Count; i++)
        {
            if(playerNumber == 1)
            {
                playerHand = gameDeck.playerOneCards;
            }

            if (playerNumber == 2)
            {
                playerHand = gameDeck.playerTwoCards;
            }

            /*if (deckOfCards[i].cardOwnedBy == playerNumber && !playerHand.Contains(deckOfCards[i]))
            {
                playerHand.Add(deckOfCards[i]);
            }
            //resetToggles();
            */
        }

        for (int i = 0; i < playerHand.Count; i++)
        {
            playerHand[i].played = false;
        }

        SortHand();

        for (int i = 0; i < playerHand.Count; i++)
        {
            cardImagesUI[i].GetComponent<Image>().sprite = playerHand[i].cardSprite;
            cardImagesUI[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

    }

    public void scorePoints()
    {
        aIContinueButton.SetActive(false);
        Debug.Log("Scoring " + handScore + " Points");

        
        
        if (handScore > 0 /*&& donePlaying == false || GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Discard*/)
        {
            updateGameCounts(handScore);
            roundManagerObject.GetComponent<ScoringUI>().displayScore(myColor, handScore);

            lifetimeStats.updateLifetimeStats(thisGreenPlayer, roundManagerScript, handScore);
            lifetimeStats.saveData(lifetimeStats, thisGreenPlayer);

            if(roundManagerScript.activeStage == RoundManager.Stage.CountCrib || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                roundManagerObject.GetComponent<StageCountingHand>().updatePlayerTurn();
            }


            //totalScore += playHandScore;
            int startHoleInt = totalScore;
            totalScore += handScore;

            if (totalScore > 121)
            {
                totalScore = 121;
            }


            handScore = 0;
            playHandScore = 0;


            if (moveNext[0] == true)
            {
                Debug.Log("Moving Peg 0");
                if (pegs[2].GetComponent<Peg>().inPosition == false)
                {
                    StartCoroutine(waitASecond());
                }
                pegs[0].GetComponent<Peg>().startingPosition = pegs[0].GetComponent<Peg>().transform.position;
                pegs[0].GetComponent<Peg>().targetPosition = holes[totalScore].transform.position;
                //pegs[0].GetComponent<Peg>().updateRaiseLowerTargets();

                moveNext[0] = false;
                moveNext[1] = true;
                if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage != RoundManager.Stage.PlayingOfHand)
                {
                    donePlaying = true;
                    if (roundManagerScript.activeStage == RoundManager.Stage.CountHands)
                    {
                        roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCountingStage();
                    }
                    if (roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                    {
                        roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCribStage();
                    }

                }
                //Debug.Log("Switched Done Playing");
                if (checkEndOfGame() == true)
                {
                    StartCoroutine(endGameInSeconds(2));
                    return;
                }

                if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands && totalScore < 121)
                {
                    scored = true;
                    GameObject.Find("RoundManager").GetComponent<StageCountingHand>().updatePlayerTurn();
                }


                return;
            }
            if (moveNext[1] == true)
            {
                Debug.Log("Moving Peg 1");
                if (pegs[0].GetComponent<Peg>().inPosition == false)
                {
                    StartCoroutine(waitASecond());
                }
                pegs[1].GetComponent<Peg>().startingPosition = pegs[1].GetComponent<Peg>().transform.position;
                pegs[1].GetComponent<Peg>().targetPosition = holes[totalScore].transform.position;
                pegs[1].GetComponent<Peg>().updateRaiseLowerTargets();


                moveNext[1] = false;
                moveNext[2] = true;


                if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage != RoundManager.Stage.PlayingOfHand)
                {
                    donePlaying = true;
                    if (roundManagerScript.activeStage == RoundManager.Stage.CountHands)
                    {
                        roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCountingStage();
                    }
                    if (roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                    {
                        roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCribStage();
                    }
                }
                //Debug.Log("Switched Done Playing");
                if (checkEndOfGame() == true)
                {
                    StartCoroutine(endGameInSeconds(2));
                    return;
                }

                if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands && totalScore < 121)
                {
                    scored = true;
                    GameObject.Find("RoundManager").GetComponent<StageCountingHand>().updatePlayerTurn();
                }

                StartCoroutine(waitASecond());

                return;
            }
            if (moveNext[2] == true)
            {
                Debug.Log("Moving Peg 2");
                if (pegs[1].GetComponent<Peg>().inPosition == false)
                {
                    StartCoroutine(waitASecond());
                }
                pegs[2].GetComponent<Peg>().startingPosition = pegs[2].GetComponent<Peg>().transform.position;
                pegs[2].GetComponent<Peg>().targetPosition = holes[totalScore].transform.position;
                pegs[2].GetComponent<Peg>().updateRaiseLowerTargets();


                moveNext[2] = false;
                moveNext[0] = true;
                if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage != RoundManager.Stage.PlayingOfHand)
                {
                    donePlaying = true;
                    if (roundManagerScript.activeStage == RoundManager.Stage.CountHands)
                    {
                        roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCountingStage();
                    }
                    if (roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                    {
                        roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCribStage();
                    }
                }

                //Debug.Log("Switched Done Playing");
                if (checkEndOfGame() == true)
                {
                    StartCoroutine(endGameInSeconds(2));
                    return;
                }

                if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands && totalScore < 121)
                {
                    scored = true;
                    donePlaying = true;
                    roundManagerObject.GetComponent<StageCountingHand>().updatePlayerTurn();

                }

                StartCoroutine(waitASecond());

                return;
            }


        }
        if(handScore == 0 && roundManagerObject.GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands || handScore == 0 && roundManagerObject.GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountCrib)
        {
            Debug.Log("Scoring zero.");
            roundManagerObject.GetComponent<ScoringUI>().displayScore(myColor, handScore);
            donePlaying = true;
            if(roundManagerScript.activeStage == RoundManager.Stage.CountHands)
            {
                roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCountingStage();
            }

            if (roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCribStage();
            }

            GameObject.Find("RoundManager").GetComponent<StageCountingHand>().updatePlayerTurn();
            
        }

        if (isDealer == false && GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands )
        {
            GameObject.Find("RoundManager").GetComponent<StageCountingHand>().updatePlayerTurn();
        }
        return;
    }

    public void countHand()
    {
        if (handScore == 0 && activePlayer == true && counted == false)
        {
            counted = true;
            countSummaryUI.SetActive(true);

            handScore += scorer.ScoreHand(playerHand, this,
                out int fifteens,
                out int pairs,
                out int fiveRuns,
                out int fourRuns,
                out int threeRuns,
                out int fourFlushes,
                out int fiveFlushes,
                out int rightJack);
            //roundCountingHandScore += handScore;
            roundHandFifteens += fifteens;
            roundHandPairs += pairs;
            roundHand5Runs += fiveRuns;
            roundHand4Runs += fourRuns;
            roundHand3Runs += threeRuns;
            roundHand4Flushes += fourFlushes;
            roundHand5Flushes += fiveFlushes;
            roundHandRightJack += rightJack;

            gameTotalFifteens += fifteens;
            gameTotalPairs += pairs;
            gameTotal5Runs += fiveRuns;
            gameTotal4Runs += fourRuns;
            gameTotal3Runs += threeRuns;
            gameTotalFlushes += (fourFlushes + fiveFlushes);
            gameRightJack += rightJack;

            counted = true;
            if(isAIPlayer == true)
            {
                aIProceed = false;
                aIContinueButton.SetActive(true); 
            }
        }
    }

    public void scoreHand()
    {
        if(roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
        {
            roundManagerObject.GetComponent<Scorer>().clearCountSummary();
        }
        countSummaryUI.SetActive(false);
        Debug.Log("Score Hand Running");
        if(scored == true)
        {
            return;
        }
            //Debug.Log(roundHandFifteens + "Hand Fifteens Counted");

            scorePoints();

            if (checkEndOfGame() == true)
            {
                Debug.Log("End of game true");
                StartCoroutine(endGameInSeconds(3));
            }

            if (checkEndOfGame() == true)
            {
                GameObject.Find("RoundManager").GetComponent<StageCountingHand>().checkForEndOfCountingStage();
            }

        counted = true;
        scored = true;
        donePlaying = true;

        roundManagerObject.GetComponent<StageCountingHand>().checkForEndOfCountingStage();
    }






    public void countCrib()
    {
        if (scored == true || counted == true)
        {
            return;
        }

        counted = true;
        countSummaryUI.SetActive(true);

        Debug.Log("Counting Crib");
        /*for (int i = 0; i < GameObject.Find("Deck").GetComponent<Deck>().crib.Count; i++)
        {
            crib.Add(GameObject.Find("Deck").GetComponent<Deck>().crib[i]);
        }*/
        handScore = 0;
        handScore += GameObject.Find("RoundManager").GetComponent<Scorer>().ScoreHand(crib, this,
            out int fifteens,
            out int pairs,
            out int fiveRuns,
            out int fourRuns,
            out int threeRuns,
            out int fourFlushes,
            out int fiveFlushes,
            out int rightJack);
        counted = true;

        //roundCountingCribScore += handScore;
        roundCribFifteens += fifteens;
        roundCribPairs += pairs;
        roundCrib5Runs += fiveRuns;
        roundCrib4Runs += fourRuns;
        roundCrib3Runs += threeRuns;
        roundCrib4Flushes += fourFlushes;
        roundCrib5Flushes += fiveFlushes;
        roundCribRightJack += rightJack;

        //gameCountingCribScore += handScore;

        gameTotalFifteens += fifteens;
        gameTotalPairs += pairs;
        gameTotal5Runs += fiveRuns;
        gameTotal4Runs += fourRuns;
        gameTotal3Runs += threeRuns;
        gameTotalFlushes += (fourFlushes + fiveFlushes);
        gameRightJack += rightJack;

        if(isAIPlayer == true)
        {
            aIContinueButton.SetActive(true);
        }

        this.GetComponent<ButtonManager>().updateButtons();
    }

    public void scoreCrib()
    {
        countSummaryUI.SetActive(false);
        if (counted == false)
        {
            return;
        }
        scorePoints();
        scored = true;
        donePlaying = true;
        if (checkEndOfGame() == true)
        {
            StartCoroutine(endGameInSeconds(3));
            return;
        }

        if (checkEndOfGame() == false)
        {
            StartCoroutine(GameObject.Find("RoundManager").GetComponent<StageCountingHand>().WaitToSwithCribDoneBool());
        }
    }

    private void updateGameCounts(int score)
    {
        RoundManager roundMgr = GameObject.Find("RoundManager").GetComponent<RoundManager>();

        if(roundMgr.activeStage == RoundManager.Stage.PlayingOfHand || roundMgr.activeStage == RoundManager.Stage.Cut)
        {
            gamePlayingOfHandScore += score;
            roundPlayingOfHandScore += score;
        }

        if(roundMgr.activeStage == RoundManager.Stage.CountHands)
        {
            gameCountingHandScore += score;
            roundCountingHandScore += score;
        }

        if (roundMgr.activeStage == RoundManager.Stage.CountCrib)
            {
                gameCountingCribScore += score;
                roundCountingCribScore += score;
            }
    }

    /*public void Played()
    {
        waitingToPlay = false;
        activePlayer = false;
        donePlaying = true;
    }*/

    #region Sort Hand
    public void SortHand()
    {
        playerHand.Sort(SortFunct);
    }

    private int SortFunct(Card a, Card b)
    {
        if (a.cardValue < b.cardValue)
        {
            return -1;
        }

        else if (a.cardValue > b.cardValue)
        {
            return 1;
        }

        return 0;
    }

    public void SortForRuns()
    {
        runValues.Sort(sortInts);
    }

    private int sortInts(int a, int b)
    {
        if (a < b)
        {
            return -1;
        }

        else if (a > b)
        {
            return 1;
        }

        return 0;
    }

    #endregion

    public void UpdateHand()
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            if (playerHand[i].cardOwnedBy == 10)
            {
                playerHand.Remove(playerHand[i]);

                i--;
            }
        }
    }
    #region Reset Hand

    public void ResetHand()
    {
        ActivateHandObjects();
        EraseUISprites();
        ClearPlayerHand();
        for (int i = 0; i < cardImagesUI.Count; i++)
        {
            ColorBlock cb1 = cardImagesUI[i].GetComponent<Toggle>().colors;
            cardImagesUI[i].GetComponent<Toggle>().isOn = false;
            cb1.normalColor = Color.white;
            cb1.highlightedColor = Color.white;
        }

        //UpdateHand();
    }


    public void ActivateHandObjects()
    {
        int children = handUIParent.childCount;

        for (int i = 0; i < children; i++)
        {
            handUIParent.GetChild(i).gameObject.SetActive(true);
            if (!cardImagesUI.Contains(handUIParent.GetChild(i).gameObject))
            {
                cardImagesUI.Add(handUIParent.GetChild(i).gameObject);
            }
        }
    }

    public void EraseUISprites()
    {
        int children = handUIParent.childCount;

        for (int i = 0; i < children; i++)
        {
            cardImagesUI[i].GetComponent<Image>().sprite = nullSprite;
            cardImagesUI[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);


        }
    }
    private void populateRunValues()
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            runValues.Add(playerHand[i].cardValue);
        }
    }

    private void clearRunValues()
    {
        for (int i = 0; i < runValues.Count; i++)
        {
            runValues.Remove(runValues[i]);
            i--;
        }
    }

    public void ClearPlayerHand()
    {

        for (int i = 0; i < playerHand.Count; i++)
        {
            playerHand[i].cardOwnedBy = 0;
            playerHand.Remove(playerHand[i]);
            i--;
        }
    }

    public void updateDonePlaying()
    {
        GameObject.Find("RoundManager").GetComponent<RoundManager>().updateDonePlaying();
    }

    public void changeDonePlaying()
    {
        if (donePlaying == true)
        {
            donePlaying = false;
            return;
        }

        if (donePlaying == false)
        {
            donePlaying = true;
        }
    }

    public void isDonePlaying()
    {
        donePlaying = true;
    }

    public void SetmyColor()
    {
        if (playerColor == "Green")
        {
            myColor = playerGreen;
            if (isAIPlayer == true)
            {
                turtleSkin.color = new Color32(0, 200, 0, 0);
            }
        }
        if (playerColor == "Red")
        {
            myColor = Color.red;
            if (isAIPlayer == true)
            {
                turtleSkin.color = new Color32(200, 0, 0, 0);
            }
        }
        if (playerColor == "Blue")
        {
            myColor = Color.blue;
            if (isAIPlayer == true)
            {
                turtleSkin.color = new Color32(0, 0, 200, 0);
            }
        }



        //Debug.Log(playerColor);
    }

    public void resetToggles()
    {
        List<Toggle> handToggles = new List<Toggle>();
        {
            for (int i = 0; i < cardImagesUI.Count; i++)
            {
                handToggles.Add(cardImagesUI[i].GetComponent<Toggle>());
            }

            for (int i = 0; i < handToggles.Count; i++)
            {
                ColorBlock cb1 = handToggles[i].colors;
                {
                    handToggles[i].isOn = false;
                    cb1.normalColor = Color.white;
                    cb1.highlightedColor = Color.white;
                }
                handToggles[i].GetComponent<Toggle>().colors = cb1;
            }
        }
    }

    public void clearCrib()
    {
        for (int i = 0; i < crib.Count; i++)
        {
            crib.Remove(crib[i]);
            i--;
        }
    }
    #endregion

    IEnumerator waitASecond()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("running Wait a Second");
    }

    public void endOfRound()
    {

    }

    public void rightJackFlipped()
    {
        roundCuttingRightJack += 1;
        handScore += 2;
        scorePoints();
    }

    IEnumerator doAIInASecond()
    {
        
        aIRunning = true;
        Debug.Log("AI Waiting");
        yield return new WaitForSeconds(2);
        playerAI.DoAI();
    }


    public void resetSummaryValues() {
        //Round Summary Variables
        //Totals
        roundPlayingOfHandScore = 0;
        roundCountingHandScore = 0;
        roundCountingCribScore = 0;

        //Right Jack flipped
        roundCuttingRightJack = 0;

        //Counting Hand
        roundHandPairs = 0;
        roundHandFifteens = 0;

        roundHand5Runs = 0;
        roundHand4Runs = 0;
        roundHand3Runs = 0;

        roundHand5Flushes = 0;
        roundHand4Flushes = 0;

        roundHandRightJack = 0;
        //Counting Crib
        roundCribPairs = 0;
        roundCribFifteens = 0;

        roundCrib5Runs = 0;
        roundCrib4Runs = 0;
        roundCrib3Runs = 0;

        roundCrib5Flushes = 0;
        roundCrib4Flushes = 0;

        roundCribRightJack = 0;
    }

    public void resetGameSummaryValues()
    {
        gamePlayingOfHandScore = 0;
        gameCountingHandScore = 0;
        gameCountingCribScore = 0;
    //Breakdown
        gameRightJackFlipped = 0;

        gameTotalFifteens = 0;
        gameTotalPairs = 0;

        gameTotal5Runs = 0;
        gameTotal4Runs = 0;
        gameTotal3Runs = 0;

        gameTotalFlushes = 0;

        gameRightJack = 0;

}


    public void gameOver()
    {
        activePlayer = false;
    }

    public void endGame()
    {
        lifetimeStats.updateWinLoss(this.gameObject);
        lifetimeStats.saveData(lifetimeStats, thisGreenPlayer);
        totalScore = 0;
        handScore = 0;
        activePlayer = false;
        aIRunning = false;
        aIProceed = false;
        playerColor = "";
        myColor = Color.white;

        for (int i = 0; i < holes.Count; i++)
        {
            holes.Remove(holes[i]);
            i--;
        }

        for (int i = 0; i < pegs.Length; i++)
        {
            pegs[i] = null;
        }

        resetSummaryValues();
        resetGameSummaryValues();

    }

    public IEnumerator endGameInSeconds(int seconds)
    {
        Debug.Log("Waiting to end game");
        yield return new WaitForSeconds(seconds);
        roundManagerObject.GetComponent<RoundManager>().EndGame();
    }

    public bool checkEndOfGame()
    {
        if (totalScore > 120)
        {
            StartCoroutine(endGameInSeconds(3));
            return true;
        }
        else
            return false;
    }

    public void turnOffSpeechBubble()
    {
        Debug.Log("Deactivating speech bubble");
        speechBubble.SetActive(false);
    }

    public void forfeitGame()
    {
        lifetimeStats.updateWinLoss(this.gameObject);
        lifetimeStats.saveData(lifetimeStats, thisGreenPlayer);
    }

    public void resetLifetimeStats()
    {
        lifetimeStats.resetStats(lifetimeStats, thisGreenPlayer);
        lifetimeStatsEraseConfirm.SetActive(false);
    }

    public void confirmResetLifetimeStats()
    {
        lifetimeStatsEraseConfirm.SetActive(true);
    }

    public void cancelEraseLifetimeStats()
    {
        lifetimeStatsEraseConfirm.SetActive(false);
    }


    public void setCardImageUIColors()
    {
        for (int i = 0; i < cardImagesUI.Count; i++)
        {
            cardImagesUI[i].GetComponent<Image>().color = roundManagerObject.GetComponent<SaveLoadGameState>().currentGameState.humanCardImagesUIColor[i];
        }
    }

    public void aISetEasy()
    {
        playerAI.aIDifficulty = BasicAI.difficulty.easy;
        PlayerPrefs.SetString("aIDifficulty", "easy");
    }

    public void aISetMedium()
    {
        playerAI.aIDifficulty = BasicAI.difficulty.medium;
        PlayerPrefs.SetString("aIDifficulty", "medium");
    }

    public void aISetHard()
    {
        playerAI.aIDifficulty = BasicAI.difficulty.hard;
        PlayerPrefs.SetString("aIDifficulty", "hard");
    }

    public void aIContinue()
    {
        aIProceed = true;
        activePlayer = true;
        aIContinueButton.SetActive(false);
        roundManagerObject.GetComponent<Scorer>().clearCountSummary();
        countSummaryUI.SetActive(false);
        
        scorePoints();
        
    }





}