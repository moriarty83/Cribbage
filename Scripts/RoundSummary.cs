using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSummary : MonoBehaviour
{
    public bool summaryStageOver;
    //public GameObject[] playerName = new GameObject[2];
    public GameObject RoundSummaryGameObject;
    public GameObject GameSummaryGameObject;

    public Text[] playerNameText = new Text[2];
    public Text[] playerSummaryText = new Text[2];

    public Text[] gamePlayerNameText = new Text[2];
    public Text[] gamePlayerSummaryText = new Text[2];
    public GameObject[] playerTrophy = new GameObject[2];

    public GameObject mainCamera;
    public Vector3 menuCameraPosition = new Vector3(291, 79.1f, 475.8f);
    public Quaternion menuCameraRotation = Quaternion.Euler(15.257f, 0f, 0f);

    //public GameObject mainCamera;

    public Text winnerText;
    

    public GameObject[] players = new GameObject[2];

    //Game Summaries Variables
    public int[] gameScore = new int[2];
    public int[] gamePlayingOfHand = new int[2];
    public int[] gameHandScore = new int[2];
    public int[] gameCribScore = new int[2];

    public float[] gamePlayingPercentage = new float[2];
    public float[] gameHandPercentage = new float[2];
    public float[] gameCribPercentage = new float[2];

    //Round Summaries Variables
    public int[] roundTotal = new int[2];

    public int[] roundPlayingOfHand = new int[2];
    public int[] roundHandScore = new int[2];
    public int[] roundCribScore = new int[2];

    //Playing of Hand Breakdown Variables
    public int[] roundRightJackFlipped = new int[2];

    //Hand Breakdown Variables
    public int[] roundHandPairs = new int[2];
    public int[] roundHandFifteens = new int[2];

    public int[] roundHand3CardRuns = new int[2];
    public int[] roundHand4CardRuns = new int[2];
    public int[] roundHand5CardRuns = new int[2];

    public int[] roundHand5CardFlushes = new int[2];
    public int[] roundHand4CardFlushes = new int[2];

    public int[] roundHandRightJack = new int[2];

    //Crib Breakdown variables

    public int[] roundCribPairs = new int[2];
    public int[] roundCribFifteens = new int[2];

    public int[] roundCrib3CardRuns = new int[2];
    public int[] roundCrib4CardRuns = new int[2];
    public int[] roundCrib5CardRuns = new int[2];

    public int[] roundCrib5CardFlushes = new int[2];
    public int[] roundCrib4CardFlushes = new int[2];

    public int[] roundCribRightJack = new int[2];

    public List<Component> playerScript = new List<Component>();

    // Start is called before the first frame update
    void Start()
    {
        /*RoundSummaryGameObject = this.gameObject;
        GameSummaryGameObject = GameObject.Find("GameSummaryUI");

        players = GameObject.FindGameObjectsWithTag("Player");
        playerNameText[0] = GameObject.Find("Player0Name").GetComponent<Text>();
        playerSummaryText[0] = GameObject.Find("Player0Summary").GetComponent<Text>();

        playerNameText[1] = GameObject.Find("Player1Name").GetComponent<Text>();
        playerSummaryText[1] = GameObject.Find("Player1Summary").GetComponent<Text>();

        gamePlayerNameText[0] = GameObject.Find("GamePlayer0Name").GetComponent<Text>();
        gamePlayerSummaryText[0] = GameObject.Find("GamePlayer0Summary").GetComponent<Text>();

        gamePlayerNameText[1] = GameObject.Find("GamePlayer1Name").GetComponent<Text>(); 
        gamePlayerSummaryText[1] = GameObject.Find("GamePlayer1Summary").GetComponent<Text>(); 

        winnerText = GameObject.Find("WinnerText").GetComponent<Text>(); */
        //players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            playerScript.Add(players[i].GetComponent<GreenPlayer>());
        }
        RoundSummaryGameObject.SetActive(false);
        GameSummaryGameObject.SetActive(false);

        mainCamera = GameObject.Find("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateSummaryValues()
    {
        UpdateEndGameStats();
        for (int i = 0; i < players.Length; i++)
        {
            //Update Total Score
            gameScore[i] = players[i].GetComponent<GreenPlayer>().totalScore;

            //Update Playing, Hand, Crib Summaries
            roundPlayingOfHand[i] = players[i].GetComponent<GreenPlayer>().roundPlayingOfHandScore;
            roundHandScore[i] = players[i].GetComponent<GreenPlayer>().roundCountingHandScore;
            roundCribScore[i] = players[i].GetComponent<GreenPlayer>().roundCountingCribScore;

            //Updates Game Count Totals
            /*gamePlayingOfHand[i] += roundPlayingOfHand[i];
            gameHandScore[i] += roundHandScore[i];
            gameCribScore[i] += roundCribScore[i];*/


            //Update Hand Breakdown
            roundHandFifteens[i] = players[i].GetComponent<GreenPlayer>().roundHandFifteens;
            roundHandPairs[i] = players[i].GetComponent<GreenPlayer>().roundHandPairs;

            roundHand5CardRuns[i] = players[i].GetComponent<GreenPlayer>().roundHand5Runs;
            roundHand4CardRuns[i] = players[i].GetComponent<GreenPlayer>().roundHand4Runs;
            roundHand3CardRuns[i] = players[i].GetComponent<GreenPlayer>().roundHand3Runs;

            roundHand5CardFlushes[i] = players[i].GetComponent<GreenPlayer>().roundHand5Flushes;
            roundHand4CardFlushes[i] = players[i].GetComponent<GreenPlayer>().roundHand4Flushes;

            roundHandRightJack[i] = players[i].GetComponent<GreenPlayer>().roundHandRightJack;

            //Update Crib Breakdown

            roundCribFifteens[i] = players[i].GetComponent<GreenPlayer>().roundCribFifteens;
            roundCribPairs[i] = players[i].GetComponent<GreenPlayer>().roundCribPairs;

            roundCrib5CardRuns[i] = players[i].GetComponent<GreenPlayer>().roundCrib5Runs;
            roundCrib4CardRuns[i] = players[i].GetComponent<GreenPlayer>().roundCrib4Runs;
            roundCrib3CardRuns[i] = players[i].GetComponent<GreenPlayer>().roundCrib3Runs;

            roundCrib5CardFlushes[i] = players[i].GetComponent<GreenPlayer>().roundCrib5Flushes;
            roundCrib4CardFlushes[i] = players[i].GetComponent<GreenPlayer>().roundCrib4Flushes;

            roundCribRightJack[i] = players[i].GetComponent<GreenPlayer>().roundCribRightJack;
        }
    }

    public void printRoundSummary()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<GreenPlayer>().activePlayer = true;
        }
        //Playing of Hand Summary Variable
        string[] playingRightJackFlippedText = new string[2];

        //Hand Summary Text Variables
        string[] handPairsText = new string[2];
        string[] handFifteensText = new string[2];

        string[] handRunsText = new string[2];
        string[] handFlushesText = new string[2];

        string[] handRightJackText = new string[2];

        //Crib summary text variables
        string[] cribPairsText = new string[2];
        string[] cribFifteensText = new string[2];

        string[] cribRunsText = new string[2];
        string[] cribFlushesText = new string[2];

        string[] cribRightJackText = new string[2];

        RoundSummaryGameObject.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {
            //Checks to see if the right Jack was Flipped
            if (roundRightJackFlipped[i] > 0)
            {
                playingRightJackFlippedText[i] = "\n\tFlipped a Jack";
            }
            else
            {
                playingRightJackFlippedText[i] = "";
            }

            //Checks if there were Runs in the hand
            if (roundHand5CardRuns[i] > 0)
            {
                handRunsText[i] = "\n\tRuns - " + roundHand5CardRuns[i] + " Five Card";
            }

            if (roundHand4CardRuns[i] > 0)
            {
                handRunsText[i] = "\n\tRuns - " + roundHand4CardRuns[i] + " Four Card";
            }
            if (roundHand3CardRuns[i] > 0)
            {
                handRunsText[i] = "\n\tRuns - " + roundHand3CardRuns[i] + " Three Card";
            }
            if (roundHand3CardRuns[i] == 0 && roundHand4CardRuns[i] == 0 && roundHand5CardRuns[i] == 0)
            {
                handRunsText[i] = "\n\tRuns - 0";
            }


            //Checks if there were any Flushes in the Hand
            if (roundHand5CardFlushes[i] > 0)
            {
                handFlushesText[i] = "\n\tFlushes - " + roundHand5CardFlushes[i] + " Five Card";
            }

            if (roundHand4CardFlushes[i] > 0)
            {
                handFlushesText[i] = "\n\tFlushes - " + roundHand4CardFlushes[i] + " Four Card";
            }
            if(roundHand4CardFlushes[i] == 0 && roundHand5CardFlushes[i] == 0)
            {
                handFlushesText[i] = "\n\tFlushes - 0";
            }

            //Checks if the hand had the right jack.
            if (roundHandRightJack[i] > 0)
            {
                handRightJackText[i] = "\n\tRight Jack - 1";
            }
            else
                handRightJackText[i] = "\n\tRight Jack - 0";

            //Checks if there were Runs in the Crib
            if (roundCrib5CardRuns[i] > 0)
            {
                cribRunsText[i] = "\n\tRuns - " + roundCrib5CardRuns[i] + " Five Card";
            }

            if (roundCrib4CardRuns[i] > 0)
            {
                cribRunsText[i] = "\n\tRuns - " + roundCrib4CardRuns[i] + " Four Card";
            }
            if (roundCrib3CardRuns[i] > 0)
            {
                cribRunsText[i] = "\n\tRuns - " + roundCrib3CardRuns[i] + " Three Card" ;
            }
            else
            {
                cribRunsText[i] = "\n\tRuns - 0";
            }

            //Checks if there were any Flushes in the Hand
            if (roundCrib5CardFlushes[i] > 0)
            {
                cribFlushesText[i] = "\n\tFlushes - " + roundCrib5CardFlushes[i] + " Five Card" ;
            }

            if (roundCrib4CardFlushes[i] > 0)
            {
                cribFlushesText[i] = "\n\tFlushes - " + roundCrib4CardFlushes[i] + " Four Card";
            }
            else
                cribFlushesText[i] = "\n\tFlushes - 0";

            //Checks if the Crib had the right jack.
            if (roundCribRightJack[i] > 0)
            {
                cribRightJackText[i] = "\n\tRight Jack - 1";
            }
            else
            {
                cribRightJackText[i] = "\n\tRight Jack - 0";
            }

            //Populates Summary Text

            //Populates Summary for Dealer
            if (players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                //Displays Player Color and Total Score.
                if (players[i].GetComponent<GreenPlayer>().isAIPlayer == false)
                {
                    playerNameText[i].text = "You - " + players[i].GetComponent<GreenPlayer>().playerColor;
                }
                if (players[i].GetComponent<GreenPlayer>().isAIPlayer == true)
                {
                    playerNameText[i].text = "Opponent - " + players[i].GetComponent<GreenPlayer>().playerColor;
                }

                playerSummaryText[i].text = ("Total Score: " + gameScore[i] +
                 "\nRound Score: " + (roundHandScore[i] + roundPlayingOfHand[i] + roundCribScore[i]) +
                "\nPlaying Hand: " + roundPlayingOfHand[i] + " Points. \n\n" +
                "Counting Hand: " + roundHandScore[i] + " Points.\n" +

                    "\tFifteens - " + roundHandFifteens[i] + "\n" +
                    "\tPairs - " + roundHandPairs[i] +
                    handRunsText[i] +
                    handFlushesText[i] +
                    handRightJackText[i] + "\n" +
                    "\n" +
                "Counting Crib: " + roundCribScore[i] + " Points.\n" +
                    "\tFifteens - " + roundCribFifteens[i] + "\n" +
                    "\tPairs - " + roundCribPairs[i] +
                    cribRunsText[i] +
                    cribFlushesText[i] +
                    cribRightJackText[i]
                );

            }

            //Populates Summary for non-Dealer
            if (players[i].GetComponent<GreenPlayer>().isDealer == false)
            {

                if (players[i].GetComponent<GreenPlayer>().isAIPlayer == false)
                {
                    playerNameText[i].text = "You - " + players[i].GetComponent<GreenPlayer>().playerColor;
                }
                if (players[i].GetComponent<GreenPlayer>().isAIPlayer == true)
                {
                    playerNameText[i].text = "Opponent - " + players[i].GetComponent<GreenPlayer>().playerColor;
                }
                //Displays Player Color and Total Score.
                //playerNameText[i].text = "Total Score: " + gameScore[i];
                playerSummaryText[i].text = ("Total Score: " + gameScore[i] +
                 "\nRound Score: " + (roundHandScore[i] + roundPlayingOfHand[i] + roundCribScore[i]) +
                "\nPlaying Hand: " + roundPlayingOfHand[i] + " Points. \n\n" +
                "Counting Hand: " + roundHandScore[i] + " Points. \n" +
                "\tFifteens - " + roundHandFifteens[i] + "\n" +
                "\tPairs - " + roundHandPairs[i] +
                handRunsText[i] +
                handFlushesText[i] +
                handRightJackText[i]);
            }
        }
    }

    public void resetSummary()
    {
        RoundSummaryGameObject.SetActive(false);
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<GreenPlayer>().resetSummaryValues();
        }
    }

    public void SummaryStageEndCheck()
    {
        List<GameObject> donePlayers = new List<GameObject>();
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<GreenPlayer>().donePlaying == true)
            {
                donePlayers.Add(players[i]);
            }
            if(donePlayers.Count == players.Length)
            {
                summaryStageOver = true;
            }
        }
    }


    public void GameOverShowSummary()
    {
        moveMainCamera();
        GameSummaryGameObject.SetActive(true);
        UpdateEndGameStats();
        //moveMainCamera();
        PrintEndSummary();
    }

    public void UpdateEndGameStats()
    {
        for (int i = 0; i < players.Length; i++)
        {
            gameScore[i] = players[i].GetComponent<GreenPlayer>().totalScore;
            gamePlayingOfHand[i] = players[i].GetComponent<GreenPlayer>().gamePlayingOfHandScore;
            gameHandScore[i] = players[i].GetComponent<GreenPlayer>().gameCountingHandScore;
            gameCribScore[i] = players[i].GetComponent<GreenPlayer>().gameCountingCribScore;
            updateEndGamePercentages();
        }
    }

    public void updateEndGamePercentages()
    {
        Debug.Log("Calculating Game End Percentages");
        for (int i = 0; i < players.Length; i++)
        {
            if(gameScore[i] > 0)
            {
                Debug.Log("Gamescore is " + gameScore[i]);
                gamePlayingPercentage[i] = 100 * gamePlayingOfHand[i] / gameScore[i];
                gameHandPercentage[i] = 100 * gameHandScore[i] / gameScore[i];
                gameCribPercentage[i] = 100 * gameCribScore[i] / gameScore[i];
            }
            if (gameScore[i] == 0)
            {
                Debug.Log("Gamestcore is 0");
                gamePlayingPercentage[i] = 0;
                gameHandPercentage[i] = 0;
                gameCribPercentage[i] = 0;
            }
        }
    }

    public void updateGameHandPercentage()
    {
        for (int i = 0; i < players.Length; i++)
        {
            gameHandPercentage[i] = gameHandScore[i] / gameScore[i];
        }
    }

    public void moveMainCamera()
    {        
        mainCamera.transform.position = menuCameraPosition;
        mainCamera.transform.rotation = menuCameraRotation;
    }

    public void PrintEndSummary()
    {
        UpdateEndGameStats();

        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<GreenPlayer>().totalScore > 120 && players[i].GetComponent<GreenPlayer>().isAIPlayer == false)
            {

                    winnerText.text = "You Win!";
                    winnerText.color = players[i].GetComponent<GreenPlayer>().myColor;
                    playerTrophy[i].SetActive(true);
                }
            if (players[i].GetComponent<GreenPlayer>().totalScore <= 120 && players[i].GetComponent<GreenPlayer>().isAIPlayer == false)
            {
                
                    winnerText.text = "You Lose";
                    winnerText.color = players[i].GetComponent<GreenPlayer>().myColor;
                    playerTrophy[i].SetActive(true);
                
            }

            if (players[i].GetComponent<GreenPlayer>().totalScore < 121)
            {

                playerTrophy[i].SetActive(false);
            }

            gamePlayerNameText[i].color = players[i].GetComponent<GreenPlayer>().myColor;

            if (players[i].GetComponent<GreenPlayer>().isAIPlayer == false)
            {
                gamePlayerNameText[i].text = "Your Summary";
            }
            if (players[i].GetComponent<GreenPlayer>().isAIPlayer == true)
            {
                gamePlayerNameText[i].text = "Opponent Summary";
            }

            gamePlayerSummaryText[i].color = players[i].GetComponent<GreenPlayer>().myColor;

            gamePlayerSummaryText[i].text =
                "Total Score - " + gameScore[i] + "\n" +
                "\n" +
                "Playing Points - " + gamePlayingOfHand[i] + " / " + gamePlayingPercentage[i] + "% \n" + 
                "\n" +
                "Hand Points - " + gameHandScore[i] + " / " + gameHandPercentage[i] + "% \n" +
                "\n" +
                "Crib Points - " + gameCribScore[i] + " / " + gameCribPercentage[i] + "%";
        }
    }



}
