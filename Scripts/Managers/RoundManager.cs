using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundManager: MonoBehaviour
{
    public enum Stage { Deal, Discard, Cut, PlayingOfHand, CountHands, CountCrib, Summary, GameOver};

    public int numberOfPlayers;

    public List<GameObject> players;

    public Stage activeStage;

    public List<GameObject> UIButtons;

    public List<bool> donePlaying;

    public GameObject SummaryUIObject;

    public GameObject activeStageUIParent;

    public GameObject activeStageIcon;

    public GameObject activeStageText;

    public GameObject dealerUI;

    public GreenPlayer humanGreenPlayer;

    public List<Sprite> stageIcons;

    public Tutorial gameTutorial;

    public bool stageOver;

    public bool cutting;

    private void Awake()
    {
        activeStageUIParent.SetActive(false);

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player);
        }


        //Transition to startGame function.
        /*populateDonePlaying();
        players[Random.Range(0, players.Count)].GetComponent<GreenPlayer>().isDealer = true;

        activeStage = Stage.Deal;
        startDealStage();
        newStageStarted();
        */
    }

    private void Update() 
    {
        if (activeStage == Stage.Deal && this.GetComponent<StageDeal>().dealStageOver == true)
        {
            NextStage();
        }
        if (allPlayersDonePlaying() == true && activeStage == Stage.Discard)
        {
            NextStage();
        }
        if (activeStage == Stage.PlayingOfHand && this.GetComponent<PlayingOfHand>().stageOver == true)
        {
            NextStage();
            //StartCoroutine(NextStageInThreeSeconds());
        }

        if(activeStage == Stage.CountHands && this.GetComponent<StageCountingHand>().countingStageOver == true)
        {
            NextStage();
        }
        if(activeStage == Stage.CountCrib && this.GetComponent<StageCountingHand>().cribStageOver == true)
        {
            NextStage();
        }
        if (activeStage == Stage.Summary && SummaryUIObject.GetComponent<RoundSummary>().summaryStageOver == true)
        {
            EndRound();
        }
    }

    public void startGame()
    {
        populateDonePlaying();
        players[Random.Range(0, players.Count)].GetComponent<GreenPlayer>().isDealer = true;
        //this.GetComponent<SaveLoadGameState>().gameInProgress = true;
        PlayerPrefs.SetInt("GameInProgress", 1);
        //gameTutorial.startGame();
        activeStage = Stage.Deal;
        activeStageUIParent.SetActive(true);
        startDealStage();
        newStageStarted();
    }

    public void NextStage()
    {
        endCurrentStage();

        if(activeStage == Stage.Summary)
        {
            activeStage = Stage.Deal;
            newStageStarted();
        }
        else
            activeStage += 1;
        

        newStageStarted();

            if(activeStage == Stage.Deal)
            {
            startDealStage();
            }
            if(activeStage == Stage.Discard)
            {
            startDiscardStage();
            }

            if(activeStage == Stage.Cut)
            {
            activeStageText.GetComponent<Text>().text = "The Cut";
                startCutStage();
            }

            if (activeStage == Stage.PlayingOfHand)
            {
            startPlayingOfHandStage();
            }

            if (activeStage == Stage.CountHands)
            {
                activeStageText.GetComponent<Text>().text = "The Count";
                activeStageIcon.GetComponent<Image>().sprite = stageIcons[3];

                GameObject.Find("Deck").GetComponent<Deck>().AddFifthCardToHands();

                for (int i = 0; i < players.Count; i++)
                {
                    /*if (GameObject.Find("Deck").GetComponent<Deck>().deck[12].cardValue == 11 &&
                        players[i].GetComponent<GreenPlayer>().isDealer == true)
                {
                    players[i].GetComponent<PlayerPlayCards>().isMyTurn = true;
                    this.GetComponent<PlayingOfHand>().updatePlayerTurn();
                }*/
         
                }
                this.GetComponent<StageCountingHand>().enterCountingStage();
            }

            if (activeStage == Stage.CountCrib)
            {
            activeStageText.GetComponent<Text>().text = "The Crib";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[4];

            this.GetComponent<StageCountingHand>().enterCountCribStage();
            }

        if (activeStage == Stage.Summary)
        {
            activeStageText.GetComponent<Text>().text = "End of Round";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[5];

            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<GreenPlayer>().donePlaying = false;
                players[i].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);

            }
            SummaryUIObject.GetComponent<RoundSummary>().updateSummaryValues();
            SummaryUIObject.GetComponent<RoundSummary>().printRoundSummary();

        }



        Debug.Log("Active stage is: " + activeStage);


    }

    public bool allPlayersDonePlaying()
    {
        updateDonePlaying();
        for (int i = 0; i < donePlaying.Count; i++)
        {
            if (donePlaying[i] == false)
            {
                //Debug.Log("Not everybody is done playing");
                return false;
            }
        }
        Debug.Log("Everybody is done playing");
        return true;
    }

    public void updateDonePlaying()
    {
        for (int i = 0; i < donePlaying.Count; i++)
        {
            donePlaying.Remove(donePlaying[i]);
            i--;
        }
        populateDonePlaying();
    }

    public void populateDonePlaying()
    {
        for (int i = 0; i < players.Count; i++)
        {
            donePlaying.Add(players[i].GetComponent<GreenPlayer>().donePlaying);
        }
    }

    public void clearDonePlaying()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<GreenPlayer>().donePlaying = false;
        }
    }

    public void newStageStarted()
    {
        stageOver = false;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<ButtonManager>().updateButtons();

        }
        clearDonePlaying();
        updateDonePlaying();
    }

    public void EndRound()
    {

            for (int i = 0; i < players.Count; i++)
            {
            //if (players[i].GetComponent<GreenPlayer>().donePlaying == true)
            players[i].GetComponent<GreenPlayer>().activePlayer = false;

            players[i].GetComponent<PlayerPlayCards>().lastCardPlayed = false;
            players[i].GetComponent<PlayerPlayCards>().aINumberCardsPlayed = 0;

            updateDonePlaying();
                GameObject.Find("Deck").GetComponent<Deck>().clearCrib();
                

                
                for (int k = 0; k < players.Count; k++)
                {
                    players[k].GetComponent<GreenPlayer>().resetToggles();
                    players[k].GetComponent<PlayerPlayCards>().clearTogglesList();
                    players[k].GetComponent<GreenPlayer>().clearCrib();
                    

                    players[k].GetComponent<GreenPlayer>().ResetHand();
                    players[k].GetComponent<Discard>().hasDiscarded = false;
                    players[k].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);

                    this.GetComponent<PlayingOfHand>().totalCardsPlayed = 0;
                }
                Debug.Log("Round Ended. Active stage is " + activeStage); 
            }

            GameObject.Find("Deck").GetComponent<Deck>().ResetRound();
            SummaryUIObject.GetComponent<RoundSummary>().resetSummary();
            this.GetComponent<StageDeal>().dealStageOver = false;
            this.GetComponent<PlayingOfHand>().stageOver = false;
            this.GetComponent<StageCountingHand>().countingStageOver = false;
            this.GetComponent<StageCountingHand>().cribStageOver = false;
            StartCoroutine(this.GetComponent<PlayingOfHand>().resetPlayRound());

            NextStage();
    }



    IEnumerator NextStageInThreeSeconds()
    {
        Debug.Log("waiting started");
        yield return new WaitForSeconds(3);
        Debug.Log("waiting finished");
        GameObject.Find("RoundManager").GetComponent<RoundManager>().NextStage();
        cutting = false;
    }


    //this method should reset all player variables that should be false/blank
    //at the beginning of a stage
    public void endCurrentStage()
    {
        for (int i = 0; i < players.Count; i++)
        {
            //Updates PlayerPlayCards Script
            players[i].GetComponent<PlayerPlayCards>().saidGo = false;
            players[i].GetComponent<PlayerPlayCards>().isMyTurn = false;
            players[i].GetComponent<PlayerPlayCards>().canPlayCard = false;
            players[i].GetComponent<PlayerPlayCards>().lastToGo = false;
            players[i].GetComponent<PlayerPlayCards>().lastCardButton.SetActive(false);



            //Updates Greenplayer Script
            players[i].GetComponent<GreenPlayer>().activePlayer = false;
            players[i].GetComponent<GreenPlayer>().donePlaying = false;
            players[i].GetComponent<GreenPlayer>().counted = false;
            players[i].GetComponent<GreenPlayer>().scored = false;

            //Updates StageCountingSTage Script
            this.GetComponent<StageCountingHand>().countingStageOver = false;
            this.GetComponent<StageCountingHand>().cribStageOver = false;

            //Updates RoundSummary Script
            SummaryUIObject.GetComponent<RoundSummary>().summaryStageOver = false;
        }

        updateDonePlaying();
        gameTutorial.tutorialNextStage();
    }

    void updateDealer()
    {
        for (int i = 0; i < players.Count; i++)
        {

            if (players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                int j = (i + 1) % players.Count;
                Debug.Log("Player number" + j + " should be dealer");
                players[j].GetComponent<GreenPlayer>().isDealer = true;
                players[i].GetComponent<GreenPlayer>().isDealer = false;
                return;
            }
        }
    }
    
    public void startDealStage()
    {
        activeStageText.GetComponent<Text>().text = "The Deal";
        activeStageIcon.GetComponent<Image>().sprite = stageIcons[0];
        updateDealer();
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                players[i].GetComponent<GreenPlayer>().activePlayer = true;
            }
        }
        newStageStarted();
    }

    public void startDiscardStage()
    {
        activeStageText.GetComponent<Text>().text = "The Discard";
        activeStageIcon.GetComponent<Image>().sprite = stageIcons[1];
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<GreenPlayer>().activePlayer = true;
        }
    }

    public void startCutStage()
    {
        
        if (gameTutorial.tutorialOn == false || gameTutorial.tutorialGotIt == true && cutting == false)
        {
            cutting = true;
            GameObject.Find("Deck").GetComponent<Deck>().RevealFifthCard();
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<GreenPlayer>().donePlaying = true;
                Debug.Log("done playing set true");
            }
            updateDonePlaying();
            StartCoroutine(NextStageInThreeSeconds());
        }
    }

    public void startPlayingOfHandStage()
    {
        this.GetComponent<PlayingOfHand>().lastCardScored = false;
        this.GetComponent<PlayingOfHand>().playCount = 0;
        this.GetComponent<PlayingOfHand>().updatePlayCountUI();
        activeStageText.GetComponent<Text>().text = "The Play";
        activeStageIcon.GetComponent<Image>().sprite = stageIcons[2];

        this.GetComponent<PlayingOfHand>().playCountUIObject.SetActive(true);
        GameObject.Find("Deck").GetComponent<Deck>().ScoreJack();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerPlayCards>().startPlayRound();
            players[i].GetComponent<PlayerPlayCards>().populateTogglesList();
            players[i].GetComponent<PlayerPlayCards>().aINumberCardsPlayed = 0;
            if (players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                players[i].GetComponent<PlayerPlayCards>().isMyTurn = true;
                this.GetComponent<PlayingOfHand>().updatePlayerTurn();
            }
        }
    }

    public void updateActiveStageUI()
    {
        if (activeStage == Stage.Deal)
        {
            activeStageText.GetComponent<Text>().text = "The Deal";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[0];
        }
        if (activeStage == Stage.Discard)
        {
            activeStageText.GetComponent<Text>().text = "The Discard";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[1];
        }

        if (activeStage == Stage.Cut)
        {
            activeStageText.GetComponent<Text>().text = "The Cut";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[1];
        }

        if (activeStage == Stage.PlayingOfHand)
        {
            activeStageText.GetComponent<Text>().text = "The Play";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[2];
        }

        if (activeStage == Stage.CountHands)
        {
            activeStageText.GetComponent<Text>().text = "The Count";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[3];
        }

        if (activeStage == Stage.CountCrib)
        {
            activeStageText.GetComponent<Text>().text = "The Crib";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[4];
        }

        if (activeStage == Stage.Summary)
        {
            activeStageText.GetComponent<Text>().text = "End of Round";
            activeStageIcon.GetComponent<Image>().sprite = stageIcons[5];
        }
    }

    

    public void EndGame()
    {
        PlayerPrefs.SetInt("GameInProgress", 0);

        humanGreenPlayer.lifetimeStats.gamesPlayed += 1;
        humanGreenPlayer.lifetimeStats.updateWinLoss(humanGreenPlayer.gameObject);

        //this.GetComponent<SaveLoadGameState>().gameInProgress = false;
        this.GetComponent<PlayingOfHand>().playCountUIObject.SetActive(false);
        SummaryUIObject.GetComponent<RoundSummary>().GameOverShowSummary();
        dealerUI.SetActive(false);
        this.GetComponent<ScoreUI>().endGame();
        this.GetComponent<GameMenu>().disableHumanPlayerChildren();
        this.GetComponent<GameMenu>().disableAIPlayerChildren();
        activeStageUIParent.SetActive(false);
        activeStage = Stage.GameOver;

        List<GameObject> pegs = new List<GameObject>();

        foreach (GameObject peg in GameObject.FindGameObjectsWithTag("BluePeg"))
        {
            pegs.Add(peg);
        }
        foreach (GameObject peg in GameObject.FindGameObjectsWithTag("RedPeg"))
        {
            pegs.Add(peg);
        }
        foreach (GameObject peg in GameObject.FindGameObjectsWithTag("GreenPeg"))
        {
            pegs.Add(peg);
        }

        for (int i = 0; i < pegs.Count; i++)
        {
            pegs[i].GetComponent<Peg>().pegToStartingPosition();
        }

    }

    public void gameOver()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<GreenPlayer>().endGame();
            players[i].GetComponent<PlayerPlayCards>().endGame();

            //if (players[i].GetComponent<GreenPlayer>().donePlaying == true)
            players[i].GetComponent<GreenPlayer>().activePlayer = false;
            updateDonePlaying();
            GameObject.Find("Deck").GetComponent<Deck>().clearCrib();


                for (int k = 0; k < players.Count; k++)
                {
                    players[k].GetComponent<GreenPlayer>().resetToggles();
                    players[k].GetComponent<PlayerPlayCards>().clearTogglesList();
                    players[k].GetComponent<GreenPlayer>().clearCrib();


                    players[k].GetComponent<GreenPlayer>().ResetHand();
                    players[k].GetComponent<Discard>().hasDiscarded = false;
                    players[k].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);

                    this.GetComponent<PlayingOfHand>().totalCardsPlayed = 0;
                }
                Debug.Log("Round Ended. Active stage is " + activeStage);
        }

        GameObject.Find("Deck").GetComponent<Deck>().ResetRound();
        SummaryUIObject.GetComponent<RoundSummary>().resetSummary();
        this.GetComponent<StageDeal>().dealStageOver = false;
        this.GetComponent<PlayingOfHand>().endGame();
        this.GetComponent<StageCountingHand>().countingStageOver = false;
        this.GetComponent<StageCountingHand>().cribStageOver = false;

        this.GetComponent<PlayingOfHand>().playCountUIObject.SetActive(false);


        this.GetComponent<GameMenu>().mainMenu();
        
    }

   
}
