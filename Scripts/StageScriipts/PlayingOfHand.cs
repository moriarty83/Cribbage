using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingOfHand : MonoBehaviour
{

   
    //for Community
    public int playCount;
    public GameObject playCountTextUI;
    public GameObject playCountUIObject;

    public List<int> activeCardValues;
    public List<int> activeCountValues;
    public List<int> ActiveRuns;
    public List<Card> playedCards;

    public int totalCardsPlayed;

    public int playScore;

    public List<GameObject> Players;

    public bool lastCardScored;

    public bool stageOver;

    // Start is called before the first frame update
    void Start()
    {
        totalCardsPlayed = 0;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Players.Add(player);
        }
        //numberCardsPlayed = 0;
        playCount = 0;
        playCountTextUI.GetComponent<Text>().text = playCount.ToString();
        playCountUIObject.SetActive(false);

        lastCardScored = false;
        stageOver = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playCard(GameObject player)
    {
        GreenPlayer greenPlayerScript = player.GetComponent<GreenPlayer>();
        PlayerPlayCards playerPlayCardsScript = player.GetComponent<PlayerPlayCards>();
        //PlayCardVisuals();
        scorePlayedCards();
        greenPlayerScript.handScore += playScore;
        greenPlayerScript.scorePoints();
        totalCardsPlayed += 1;
        if(playCount == 31)
        {
            playerPlayCardsScript.goText.text = "31!";
            playerPlayCardsScript.goUI.SetActive(true);
        }
        playScore = 0;

        if(greenPlayerScript.totalScore > 120)
        {
            StartCoroutine(greenPlayerScript.endGameInSeconds(3));
            return;
        }



        if(CheckForEndOfStage() == true)
        {

            Debug.Log("End of Stage is True");
            if(playCount != 31)
            {
                playerPlayCardsScript.lastCardPlayed = true;
                if(greenPlayerScript.isAIPlayer == true)
                {
                    StartCoroutine(playerPlayCardsScript.CheckLastCard());
                }
                return;
            }
            if(playCount == 31)
            {
                playerPlayCardsScript.goText.text = "31!";
                playerPlayCardsScript.goUI.SetActive(true);
                StartCoroutine(StageIsOver()); 
            }
            return; 
        }

        if (checkEndOfRound() == true)
        {
            StartCoroutine(resetPlayRound());
            return;
        }

        if (CheckForEndOfStage() == false)
        {
            Debug.Log("End of Stage is False");
            updatePlayerTurn();
        }

    }

    public void updatePlayCountUI()
    {
        playCountTextUI.GetComponent<Text>().text = playCount.ToString();
    }

    public void scorePlayedCards()
    {
        FifteenPlayed();
        PairsPlayed();
        RunsPlayed();
        Score31();
        //Debug.Log("Play Score = " + playScore);
    }



    #region Scoreing Methods
    //Scores 2 points if count lands on 15.
    public int FifteenPlayed()
    {
        if (playCount == 15)
        {
            playScore += 2;
            return 2;
            //Debug.Log("Hit Fifteen!");
        }
        else return 0;
        
    }

    public int Score31()
    {
        if (playCount == 31)
        {
            playScore += 2;
            return 2;
            //Debug.Log("31 Scored");
        }
        else
            return 0;
    }

    //Scores points for pairs, three and four of a kind played.
    public int PairsPlayed()
    {
        int returnScore = new int();
        int lastValue = activeCardValues.Count-1;

        if (lastValue>0 && activeCardValues[lastValue] == activeCardValues[lastValue - 1])
        {
            playScore += 2;
            returnScore += 2;
            //Debug.Log("Pair Counted");

        }
        if (lastValue > 1 && activeCardValues[lastValue] == activeCardValues[lastValue - 1]
            && activeCardValues[lastValue] == activeCardValues[lastValue - 2])
        {
            playScore += 4;
            returnScore += 4;
            //Debug.Log("Three of a Kind Counted");
        }

        if (lastValue > 2 && activeCardValues[lastValue] == activeCardValues[lastValue - 1]
        && activeCardValues[lastValue] == activeCardValues[lastValue - 2]
        && activeCardValues[lastValue] == activeCardValues[lastValue - 3])
        {
            playScore += 6;
            returnScore += 6;
            //Debug.Log("Four of a Kind Counted");

        }
        return returnScore;

    }

    //Scores for hitting 31

    #region Counting Runs
    //Scores for Runs before 31
    public int RunsPlayed()
    {
        if (ActiveRuns.Count >= 8 && EightCardRun() == true)
        {
            playScore += 8;
            return 8;
        }

        if (ActiveRuns.Count >= 7 && SevenCardRun() == true)
        {
            playScore += 7;
            return 7;
        }

        if (ActiveRuns.Count >= 6 && SixCardRun() == true)
        {
            playScore += 6;
            return 6;
        }

        if (ActiveRuns.Count >= 5 && FiveCardRun() == true)
        {
            playScore += 5;
            return 5;
        }

        if (ActiveRuns.Count >= 4 && FourCardRun() == true)
        {
            playScore += 4;
            return 5;
        }

        if (ActiveRuns.Count >= 3 && ThreeCardRun() == true)
        {
            playScore += 3;
            return 3;
        }
        else
            return 0;
    }

    //Looks to see if last three cards played make a run.
    public bool ThreeCardRun()
    {
        Debug.Log("Runing ThreeCardRun");
        List<int> lastCards = new List<int>();
        int i = ActiveRuns.Count - 1;
        if (i >= 2)
        {
            lastCards.Add(ActiveRuns[i]);
            lastCards.Add(ActiveRuns[i - 1]);
            lastCards.Add(ActiveRuns[i - 2]);
            Debug.Log("Cards added to lastThreeCards");

            for (int k = 0; k < lastCards.Count; k++)
            {
                Debug.Log("Last Three Cards Value is " + lastCards[k]);
            }

            SortForRuns();
            int j = lastCards.Count - 1;
            if (lastCards[j] - 1 == lastCards[j - 1] && lastCards[j] - 2 == lastCards[j - 2])
            {
                Debug.Log("Three card run counted");
                return true;
            }
        }

        else
            Debug.Log("no three card runs counted");
        return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

        int SortFunct(int a, int b)
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
    }

    public bool FourCardRun()
    {
        List<int> lastCards = new List<int>();
        int i = ActiveRuns.Count - 1;
        if (i >= 3)
        {
            lastCards.Add(ActiveRuns[i]);
            lastCards.Add(ActiveRuns[i - 1]);
            lastCards.Add(ActiveRuns[i - 2]);
            lastCards.Add(ActiveRuns[i - 3]);

        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3])
        {
        return true;
        }
        else return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

        int SortFunct(int a, int b)
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
    }

    public bool FiveCardRun()
    {
        List<int> lastCards = new List<int>();
        int i = ActiveRuns.Count - 1;
        if (i >= 4)
        {
            lastCards.Add(ActiveRuns[i]);
            lastCards.Add(ActiveRuns[i - 1]);
            lastCards.Add(ActiveRuns[i - 2]);
            lastCards.Add(ActiveRuns[i - 3]);
            lastCards.Add(ActiveRuns[i - 4]);


        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4])
        {
            return true;
        }
        else return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

        int SortFunct(int a, int b)
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


    }

    public bool SixCardRun()
    {
        List<int> lastCards = new List<int>();
        int i = ActiveRuns.Count - 1;
        if (i >= 5)
        {
            lastCards.Add(ActiveRuns[i]);
            lastCards.Add(ActiveRuns[i - 1]);
            lastCards.Add(ActiveRuns[i - 2]);
            lastCards.Add(ActiveRuns[i - 3]);
            lastCards.Add(ActiveRuns[i - 4]);
            lastCards.Add(ActiveRuns[i - 5]);



        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4]
            && lastCards[j] - 5 == lastCards[j - 5])

        {
            return true;
        }
        else return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

        int SortFunct(int a, int b)
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


    }

    public bool SevenCardRun()
    {
        List<int> lastCards = new List<int>();
        int i = ActiveRuns.Count - 1;
        if (i >= 6)
        {
            lastCards.Add(ActiveRuns[i]);
            lastCards.Add(ActiveRuns[i - 1]);
            lastCards.Add(ActiveRuns[i - 2]);
            lastCards.Add(ActiveRuns[i - 3]);
            lastCards.Add(ActiveRuns[i - 4]);
            lastCards.Add(ActiveRuns[i - 5]);
            lastCards.Add(ActiveRuns[i - 6]);




        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4]
            && lastCards[j] - 5 == lastCards[j - 5]
            && lastCards[j] - 6 == lastCards[j - 6])


        {
            return true;
        }
        else return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

        int SortFunct(int a, int b)
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
    }

    public bool EightCardRun()
    {
        List<int> lastCards = new List<int>();
        int i = ActiveRuns.Count - 1;
        if (i >= 7)
        {
            lastCards.Add(ActiveRuns[i]);
            lastCards.Add(ActiveRuns[i - 1]);
            lastCards.Add(ActiveRuns[i - 2]);
            lastCards.Add(ActiveRuns[i - 3]);
            lastCards.Add(ActiveRuns[i - 4]);
            lastCards.Add(ActiveRuns[i - 5]);
            lastCards.Add(ActiveRuns[i - 6]);
            lastCards.Add(ActiveRuns[i - 7]);




        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4]
            && lastCards[j] - 5 == lastCards[j - 5]
            && lastCards[j] - 6 == lastCards[j - 6]
            && lastCards[j] - 7 == lastCards[j - 7])
        {
            return true;
        }
        else return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

        int SortFunct(int a, int b)
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
    }
    #endregion
    #endregion

    //HARD AI TESTING METHODS

    public int testScorePlayedCards(Card testCard)
    {
        Debug.Log("Running testScorePlayedCards");
        int testPlayCount = playCount;
        List<int> testActiveCardValues = new List<int>();
        for (int i = 0; i < activeCardValues.Count; i++)
        {
            testActiveCardValues.Add(activeCardValues[i]);
        }

        List<int> testActiveCountValues = new List<int>();
        for (int i = 0; i < activeCardValues.Count; i++)
        {
            testActiveCountValues.Add(activeCountValues[i]);
        }
        List<int> testActiveRuns = new List<int>();
        for (int i = 0; i < ActiveRuns.Count; i++)
        {
            testActiveRuns.Add(ActiveRuns[i]);
        }

        List<Card> testPlayedCards = playedCards;

        for (int i = 0; i < testActiveCardValues.Count; i++)
        {
            Debug.Log("Test Active Card Element " + i + " is " + testActiveCardValues[i]);
        }
        //Adds test card stuff to temporary lists/values
        testPlayCount += testCard.cardCountValue;
        testActiveCardValues.Add(testCard.cardValue);
        testActiveCountValues.Add(testCard.cardCountValue);
        testActiveRuns.Add(testCard.cardCountValue);
        playedCards.Add(testCard);


        int testScore = new int();
        testScore = testFifteenPlayed(testPlayCount) + testPairsPlayed(testActiveCardValues) +
        testRunsPlayed(testActiveRuns) + testScore31(testPlayCount);

        Debug.Log("testScore is " + testScore);

        return testScore;
        //Debug.Log("Play Score = " + playScore);


    }

    #region Scoreing Methods
    //Scores 2 points if count lands on 15.
    public int testFifteenPlayed(int testPlayCount)
    {
        if (testPlayCount == 15)
        {
            return 2;
            //Debug.Log("Hit Fifteen!");
        }
        else return 0;

    }

    public int testScore31(int testPlayCount)
    {
        if (testPlayCount == 31)
        {
            return 2;
            //Debug.Log("31 Scored");
        }
        else
            return 0;
    }

    //Scores points for pairs, three and four of a kind played.
    public int testPairsPlayed(List<int> testActiveCardValues)
    {
        int returnScore = new int();
        int lastValue = testActiveCardValues.Count - 1;

        if (lastValue > 0 && testActiveCardValues[lastValue] == testActiveCardValues[lastValue - 1])
        {
            returnScore += 2;
            //Debug.Log("Pair Counted");

        }
        if (lastValue > 1 && testActiveCardValues[lastValue] == testActiveCardValues[lastValue - 1]
            && testActiveCardValues[lastValue] == testActiveCardValues[lastValue - 2])
        {
            returnScore += 4;
            //Debug.Log("Three of a Kind Counted");
        }

        if (lastValue > 2 && testActiveCardValues[lastValue] == testActiveCardValues[lastValue - 1]
        && testActiveCardValues[lastValue] == testActiveCardValues[lastValue - 2]
        && testActiveCardValues[lastValue] == testActiveCardValues[lastValue - 3])
        {
            returnScore += 6;
            //Debug.Log("Four of a Kind Counted");

        }
        return returnScore;

    }

    //Scores for hitting 31

    #region Counting Runs
    //Scores for Runs before 31
    public int testRunsPlayed(List<int> testActiveRuns)
    {
        if (ActiveRuns.Count >= 8 && testEightCardRun(testActiveRuns) == true)
        {
            
            return 8;
        }

        if (testActiveRuns.Count >= 7 && testSevenCardRun(testActiveRuns) == true)
        {
            return 7;
        }

        if (testActiveRuns.Count >= 6 && testSixCardRun(testActiveRuns) == true)
        {
            return 6;
        }

        if (testActiveRuns.Count >= 5 && testFiveCardRun(testActiveRuns) == true)
        {
            
            return 5;
        }

        if (testActiveRuns.Count >= 4 && testFourCardRun(testActiveRuns) == true)
        {
            return 4;
        }

        if (testActiveRuns.Count >= 3 && testThreeCardRun(testActiveRuns) == true)
        {
            return 3;
        }

        else
            Debug.Log("No runs found");
            return 0;
    }

    //Looks to see if last three cards played make a run.
    public bool testThreeCardRun(List<int> testActiveRuns)
    {
        Debug.Log("Runing ThreeCardRun");
        List<int> lastCards = new List<int>();
        int i = testActiveRuns.Count - 1;
        if (i >= 2)
        {
            lastCards.Add(testActiveRuns[i]);
            lastCards.Add(testActiveRuns[i - 1]);
            lastCards.Add(testActiveRuns[i - 2]);
            Debug.Log("Cards added to lastThreeCards");

            for (int k = 0; k < lastCards.Count; k++)
            {
                Debug.Log("Last Three Cards Value is " + lastCards[k]);
            }

            SortForRuns();
            int j = lastCards.Count - 1;
            if (lastCards[j] - 1 == lastCards[j - 1] && lastCards[j] - 2 == lastCards[j - 2])
            {
                Debug.Log("Three card run counted");
                return true;
            }
        }

        else
            Debug.Log("no three card runs counted");
        return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }
    }

    public bool testFourCardRun(List<int> testActiveRuns)
    {
        List<int> lastCards = new List<int>();
        int i = testActiveRuns.Count - 1;
        if (i >= 3)
        {
            lastCards.Add(testActiveRuns[i]);
            lastCards.Add(testActiveRuns[i - 1]);
            lastCards.Add(testActiveRuns[i - 2]);
            lastCards.Add(testActiveRuns[i - 3]);
        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3])
        {
            Debug.Log("Four Card Run found");
            return true;
        }
        
        else
            Debug.Log("no four card runs counted");
        return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }
    }



    public bool testFiveCardRun(List<int> testActiveRuns)
    {
        List<int> lastCards = new List<int>();
        int i = testActiveRuns.Count - 1;
        if (i >= 4)
        {
            lastCards.Add(testActiveRuns[i]);
            lastCards.Add(testActiveRuns[i - 1]);
            lastCards.Add(testActiveRuns[i - 2]);
            lastCards.Add(testActiveRuns[i - 3]);
            lastCards.Add(testActiveRuns[i - 4]);


        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4])
        {
            Debug.Log("Five Card Run found");
            return true;
        }
        else
            Debug.Log("no Five card runs counted");
        return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }


    }

    public bool testSixCardRun(List<int> testActiveRuns)
    {
        List<int> lastCards = new List<int>();
        int i = testActiveRuns.Count - 1;
        if (i >= 5)
        {
            lastCards.Add(testActiveRuns[i]);
            lastCards.Add(testActiveRuns[i - 1]);
            lastCards.Add(testActiveRuns[i - 2]);
            lastCards.Add(testActiveRuns[i - 3]);
            lastCards.Add(testActiveRuns[i - 4]);
            lastCards.Add(testActiveRuns[i - 5]);

        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4]
            && lastCards[j] - 5 == lastCards[j - 5])

        {
            Debug.Log("Six Card Run found");
            return true;
        }
        else
            Debug.Log("no six card runs counted");
        return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

    }

    public bool testSevenCardRun(List<int> testActiveRuns)
    {
        List<int> lastCards = new List<int>();
        int i = testActiveRuns.Count - 1;
        if (i >= 6)
        {
            lastCards.Add(testActiveRuns[i]);
            lastCards.Add(testActiveRuns[i - 1]);
            lastCards.Add(testActiveRuns[i - 2]);
            lastCards.Add(testActiveRuns[i - 3]);
            lastCards.Add(testActiveRuns[i - 4]);
            lastCards.Add(testActiveRuns[i - 5]);
            lastCards.Add(testActiveRuns[i - 6]);
        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4]
            && lastCards[j] - 5 == lastCards[j - 5]
            && lastCards[j] - 6 == lastCards[j - 6])


        {
            Debug.Log("Seven Card Run found");
            return true;
        }
        else return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

        int SortFunct(int a, int b)
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
    }

    public bool testEightCardRun(List<int> ActiveRuns)
    {
        List<int> lastCards = new List<int>();
        int i = ActiveRuns.Count - 1;
        if (i >= 7)
        {
            lastCards.Add(ActiveRuns[i]);
            lastCards.Add(ActiveRuns[i - 1]);
            lastCards.Add(ActiveRuns[i - 2]);
            lastCards.Add(ActiveRuns[i - 3]);
            lastCards.Add(ActiveRuns[i - 4]);
            lastCards.Add(ActiveRuns[i - 5]);
            lastCards.Add(ActiveRuns[i - 6]);
            lastCards.Add(ActiveRuns[i - 7]);




        }

        SortForRuns();
        int j = lastCards.Count - 1;
        if (lastCards[j] - 1 == lastCards[j - 1]
            && lastCards[j] - 2 == lastCards[j - 2]
            && lastCards[j] - 3 == lastCards[j - 3]
            && lastCards[j] - 4 == lastCards[j - 4]
            && lastCards[j] - 5 == lastCards[j - 5]
            && lastCards[j] - 6 == lastCards[j - 6]
            && lastCards[j] - 7 == lastCards[j - 7])
        {
            return true;
        }
        else return false;

        void SortForRuns()
        {
            lastCards.Sort(SortFunct);
        }

    }

    int SortFunct(int a, int b)
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


    public void updatePlayerTurn()
    {
        if(stageOver == true)
        {
            Debug.Log("Stage Over, not updating turn");
            return;
        }

        if(GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage != RoundManager.Stage.PlayingOfHand)
        {
            Debug.Log("Not Playing of Hand Stage, not Updating Turn");
            return;
        }

        else
        Debug.Log("Updating Player Turn from Playing of Hand Script");
        //Makes list of players who have said go.
        List<GameObject> hasSaidGo = new List<GameObject>();
        for (int i = 0; i < Players.Count; i++)
        {
            if(Players[i].GetComponent<PlayerPlayCards>().saidGo == true)
            {
                hasSaidGo.Add(Players[i]);
                //Debug.Log(hasntSaidGo.Count + " players added to Hasn't Said Go");
            }
        }

        //If all players have said go, it resets the round.
        if(hasSaidGo.Count == Players.Count)
        {
             StartCoroutine(resetPlayRound());
            return;
        }

        //If all players haven't said go, it cyles player turns and stops on the
        //the first player who hasn't said go.
        if (hasSaidGo.Count != Players.Count)
        {
            NextPlayersTurn();
            for (int i = 0; i < Players.Count; i++)
            {

                if (Players[i].GetComponent<PlayerPlayCards>().isMyTurn == true && Players[i].GetComponent<PlayerPlayCards>().saidGo == true)
                {
                    NextPlayersTurn();
                }

                if (Players[i].GetComponent<PlayerPlayCards>().isMyTurn == true && Players[i].GetComponent<PlayerPlayCards>().saidGo == false)
                {
                    return;
                }

            }
        } 
    }


    public void NextPlayersTurn()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<PlayerPlayCards>().isMyTurn == true)
            {
                int j = (i + 1) % Players.Count;
                //Debug.Log("j = " + j);

                Players[j].GetComponent<PlayerPlayCards>().isMyTurn = true;
                Players[j].GetComponent<GreenPlayer>().yourTurnUI.SetActive(true);
                Players[j].GetComponent<GreenPlayer>().activePlayer = true;
                Players[i].GetComponent<PlayerPlayCards>().isMyTurn = false;
                Players[i].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);
                Players[i].GetComponent<GreenPlayer>().activePlayer = false;

                if (Players[j].GetComponent<PlayerPlayCards>().isMyTurn == true && Players[j].GetComponent<PlayerPlayCards>().saidGo == false)
                {
                    //TODO Check for "Time To Reset"
                    //checkForEndOfRound();
                    return;
                }

                //Debug.Log("Turn Updated to Player " + j);
                //checkForEndOfRound();
                return;
            }
        }
    }

    #endregion


    public bool checkEndOfRound()
    {
        Debug.Log("Checking for end of round");
        bool allGo = new bool();
        List<bool> saidGo = new List<bool>();

        allGo = true;
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<PlayerPlayCards>().saidGo == false)
            {
                Debug.Log("Not all players have said go, not end of stage");
                allGo = false;
            }

        }

        if (playCount == 31 && totalCardsPlayed < (Players.Count * 4))
        {
            Debug.Log("Hit 31 Time to reset the play round");
            return
                true;
        }

        if (allGo == true && totalCardsPlayed < (Players.Count * 4))
        {
            Debug.Log("Everybody has said go. Time to reset the play round");
            return
                true;
        }

        if(allGo == false)
        {
            return
                false;
        }
        else
            Debug.Log("No end round conditions met");
            return false;
    }

    public IEnumerator resetPlayRound() 
    {
        yield return new WaitForSeconds(2);
        playCount = 0;
        playScore = 0;


        playCountTextUI.GetComponent<Text>().text = playCount.ToString();
        for (int i = 0; i < activeCardValues.Count; i++)
        {
            activeCardValues.Remove(activeCardValues[i]);
            i--;
        }
        for (int i = 0; i < activeCountValues.Count; i++)
        {
            activeCountValues.Remove(activeCountValues[i]);
            i--;
        }
        for (int i = 0; i < ActiveRuns.Count; i++)
        {
            ActiveRuns.Remove(ActiveRuns[i]);
            i--;
        }
        for (int i = 0; i < playedCards.Count; i++)
        {
            playedCards.Remove(playedCards[i]);
            i--;
        }
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<PlayerPlayCards>().goUI.SetActive(false);
            Players[i].GetComponent<PlayerPlayCards>().saidGo = false;
            //Debug.Log("Reset HasntSaidGo");
        }

        updatePlayerTurn();

    }

    public void LastCardPlayed()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            
            Players[i].GetComponent<ButtonManager>().DeactivateButtons();
        }
     
    }

    public bool CheckForLastCard()
    {
        if (totalCardsPlayed == (Players.Count * 4) && playCount != 31)
        {
            return
                true;
        }
        else
            return
                false;
    }

    public bool CheckForEndOfStage()
    {
        if (totalCardsPlayed == (Players.Count * 4))
        {
            return
                true;
        }
        else
            return
                false;
    }


    

    IEnumerator EndRoundCheckAfterTwoSeconds()
    {
        //Debug.Log("waiting two seconds to check for end of round");
        yield return new WaitForSeconds(2);
        //Debug.Log("Done waiting, checking for end of round");
        //checkForEndOfRound();
    }

    //TODO - Bring UPdate Player turn method into this script so each script has its own copy.

    public IEnumerator StageIsOver()
    {
        //resetPlayRound();
        Debug.Log("Waiting to end Playing Stage");
        yield return new WaitForSeconds(2);
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<GreenPlayer>().activePlayer = false;
            Players[i].GetComponent<PlayerPlayCards>().goUI.SetActive(false);
        }
        playCountUIObject.SetActive(false);
        playCount = 0;
        stageOver = true; 
    }

    public void scoreLastCard(GameObject player)
    {
        Debug.Log("Scoring Last Card");
        lastCardScored = true;
        player.GetComponent<GreenPlayer>().handScore += 1;
        player.GetComponent<GreenPlayer>().scorePoints();
        player.GetComponent<GreenPlayer>().donePlaying = true;
        StartCoroutine(StageIsOver());
    }

    public void checkStageOverOnLoad()
    {
        Debug.Log("Checking Playing of Hand Stage over on load");
        for (int i = 0; i < Players.Count; i++)
        {
            if(lastCardScored == true)
            {
                stageOver = true;
                return;
            }
        }
    }

    public void checkPlayRoundOverOnLoad()
    {
        Debug.Log("Checking for end of round on load");
        if (checkEndOfRound() == true)
        {
            Debug.Log("End of Round is True On Load");
            StartCoroutine(resetPlayRound());
            return;
        }
    }

    public void endGame()
    {
        playCount = 0;
        playCountUIObject.SetActive(false);
        activeCardValues.Clear();
        activeCountValues.Clear();
        ActiveRuns.Clear();
        playedCards.Clear();
        totalCardsPlayed = 0;
        playScore = 0;
        lastCardScored =  false;
        stageOver = false;

        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<PlayerPlayCards>().endGame();
        }
    }

}

#endregion