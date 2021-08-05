using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
[System.Serializable]

public class GameState
{


    public GreenPlayer humanGreenPlayer;
    public PlayerPlayCards humanPlayerPlayCards;
    public Discard humanDiscard;
    public ButtonManager humanButtonManager;
    public GameMenu gameMenu;
    public ScoreUI savedScoreUI;
    public Tutorial savedTutorial;
    public CameraManager gameCameraAngleManager;
    public Scorer gameScorer;

    public GreenPlayer aIGreenPlayer;
    public PlayerPlayCards aIPlayerPlayCards;
    public Discard aIDiscard;
    public ButtonManager aIButtonManager;

    public RoundManager roundManager;
    public RoundSummary roundSummary;
    public Deck gameDeck;
    public PlayingOfHand gamePlayingOfHand;
    public StageCountingHand gameStageCountingHand;

    //DECK VARIABLES
    public List<Card> savedDeck;

    public bool[] showCribCards = new bool[4];
    public List<Card> savedPlayerOneCards;
    public List<Card> savedPlayerTwoCards;
    public List<Card> savedCrib;
    public List<GameObject> savedDeckPlayers;


    public GameObject savedFifthCardParent;
    public Vector3 savedFifthCardParentPosition;
    public Vector3 savedFifthCardParentRotation;
    public GameObject savedFifthCard;
    public Sprite fifthCardFaceRenderer;


    //PLAYING OF HAND VARIABLES
    public int gameStatePlayingOfHandPlayCount;
    public GameObject gameStatePlayingOfHandPlayCountTextUI;
    public GameObject gameStatePlayingOfHandPlayCountUIObject;

    public List<int> gameStatePlayingOfHandActiveCardValues;
    public List<int> gameStatePlayingOfHandActiveCountValues;
    public List<int> gameStatePlayingOfHandActiveRuns;
    public List<Card> gameStatePlayingOfHandPlayedCards;

    public int gameStatePlayingOfHandTotalCardsPlayed;
    public int gameStatePlayingOfHandPlayScore;
    public List<GameObject> gameStatePlayingOfHandPlayers;
    public bool gameStatePlayingOfHandLastCardScored;
    public bool gameStatePlayingOfHandStageOver;

    //COUNTING HAND AND COUNTING CRIB STAGE VARIABLES
    public bool savedCountingStageOver;
    public bool savedCribStageOver;


    //ROUND MANAGER VARIABLES

    public RoundManager.Stage gameStateRoundManagerActiveStage;
    public List<GameObject> gameStateRoundManagerPlayers;
    public List<GameObject> gameStateRoundManagerUIButtons;
    public List<bool> gameStateRoundManagerDonePlaying;
    public GameObject gameStateRoundManagerSummaryUIObject;
    public GameObject gameStateRoundManagerActiveStageUIParent;
    public GameObject gameStateRoundManagerActiveStageIcon;
    public GameObject gameStateRoundManagerActiveStageText;
    public GameObject gameStateRoundManagerDealerUI;
    public List<Sprite> gameStateRoundManagerStageIcons;
    public bool gameStateRoundManagerStageOver;


    //ROUND SUMMARY VARIABLES
    public GameObject savedRoundSummaryGameObject;
    public bool savedRoundSummaryGameObjectActive;

    public GameObject savedGameSummaryGameObject;
    public bool savedGameSummaryObjectActive;

    public Text[] savedPlayerNameText = new Text[2];
    public Text[] savedPlayerSummaryText = new Text[2];

    public Text[] savedGamePlayerNameText = new Text[2];
    public Text[] savedGamePlayerSummaryText = new Text[2];
    public GameObject[] savedPlayerTrophy = new GameObject[2];

    public GameObject savedRoundSummaryMainCamera;
    public Vector3 savedMenuCameraPosition = new Vector3(0f, 78.1f, -60.5f);
    public Quaternion savedMenuCameraRotation = Quaternion.Euler(32f, 0f, 0f);

    public Vector3 savedCameraPosition;
    public Quaternion savedCameraRoation;


    //START MENU VARIABLES
    public Vector3 savedGamePlayCameraPosition = new Vector3(0f, 78.1f, -60.5f);
    public Quaternion savedGamePlayCameraRotation = Quaternion.Euler(32f, 0f, 0f);

    public GameObject savedStartMenuMainCamera;

    public GameObject savedMainMenuUI;

    public GameObject savedGameSummaryObjectUI;

    public GameObject savedDealerObjectUI;


    //CURRENT SCORE UI VARIABLES
    public GameObject[] savedCurrentScoreUIPlayers = new GameObject[2];
    public GameObject[] savedCurrentScoreUIPlayerText = new GameObject[2];

    public GameObject savedCurrentScoreUIScoreUIParent;

    private bool savedCurrentScoreUIGameOn;



    //TUTORIAL VARIABLES
    public bool savedTutorialOn;
    public GameObject savedTutorialPrompt;
    public bool savedTutorialPromptOn;

    //SCORER VARIABLES
    /*
    public bool scoredHandUIOn;

    public GameObject savedFifteensParent;
    public GameObject savedPairsParent;
    public GameObject savedRunsParent;
    public GameObject savedFlushesParent;
    public GameObject savedNibsParent;

    public string savedFifteensText;
    public string savedPairsText;
    public string savedRunsText;
    public string savedFlushesText;
    public string savedNibsText;
    public string savedTotalText;

    public List<GameObject> fifteensChildren;
    public List<GameObject> pairsChildren;
    public List<GameObject> runsChildren;
    public List<GameObject> flushesChildren;
    public List<GameObject> nibsChildren;

    public List<Sprite> fifteensSprites;
    public List<Sprite> pairsSprites;
    public List<Sprite> runsSprites;
    public List<Sprite> flushesSprites;
    public List<Sprite> nibsSprites;
    */


    #region Human GreenPlayer
    //HUMAN GREENPLAYER VARIABLES

    public bool humanDonePlaying;
    public bool humanActivePlayer;
    public int humanPlayerNumber;
    public bool humanIsDealer;
    public string humanPlayerColor;
    public int humanTotalScore = 0;
    public List<Card> humanDeckOfCards;

    public List<GameObject> humanHoles;
    public GameObject[] humanPegs = new GameObject[3];
    public Vector3[] humanPegsTargetPosition = new Vector3[3];
    public bool[] humanMoveNext = new bool[3];

    [SerializeField] public List<Card> humanPlayerHand;
    public List<GameObject> humanCardImagesUI;
    public Sprite[] humanCardImagesUISprite = new Sprite[6];
    public Color32[] humanCardImagesUIColor = new Color32[6];
    public bool[] humanCardImagesUIActive = new bool[6];

    public GameObject humanSummaryObject;
    public Scorer humanScorer;
    public GameObject humanSpeechBubble;
    public bool humanSpeechBubbleActive;
    public GameObject humanSpeechBubbleText;
    public Color32 humanMyColor;
    public List<Card> humanCrib;
    public bool humanCounted;
    public bool humanScored;

    public GameObject humanYourTurnUI;
    public bool humanYourTurnUIActive;


    public int humanRoundPlayingOfHandScore;
    public int humanRoundCountingHandScore;
    public int humanRoundCountingCribScore;


    public int humanRoundCuttingRightJack;


    public int humanRoundHandPairs;
    public int humanroundHandFifteens;

    public int humanRoundHand5Runs;
    public int humanRoundHand4Runs;
    public int humanRoundHand3Runs;

    public int humanRoundHand5Flushes;
    public int humanRoundHand4Flushes;

    public int humanRoundHandRightJack;

    public int humanRoundCribPairs;
    public int humanRoundCribFifteens;

    public int humanRoundCrib5Runs;
    public int humanRoundCrib4Runs;
    public int humanRoundCrib3Runs;

    public int humanRoundCrib5Flushes;
    public int humanRoundCrib4Flushes;
    public int humanRoundCribRightJack;

    //Game Summary Variables
    //Totals
    public int humanGamePlayingOfHandScore;
    public int humanGameCountingHandScore;
    public int humanGameCountingCribScore;
    //Breakdown
    public int humanGameRightJackFlipped;
    public int humanGameTotalFifteens;
    public int humanGameTotalPairs;
    public int humanGameTotal5Runs;
    public int humanGameTotal4Runs;
    public int humanGameTotal3Runs;
    public int humanGameTotalFlushes;
    public int humanGameRightJack;
    #endregion

    //HUMAN PLAYER LIFETIME STATS
    public SaveData humanLifetimeStats;

    //HUMAN PLAYERPLAYCARDS VARIABLES
    public List<GameObject> humanTogglesOn;
    public List<GameObject> humanToggles;
    public Transform humanHandUIObject;
    public List<GameObject> humanPlayedCardObects;
    public bool[] humanPlayedCardObectsSpriteRendererActive = new bool[4];
    public Sprite[] humanPlayedCardSprite = new Sprite[4];

    public GameObject humanCountSummaryUI;
    public Sprite humanNullSprite;
    public int humanNumberCardsPlayed;
    public bool humanSaidGo;
    public bool humanLastToGo;
    public GameObject humanGoUI;
    public bool humanGoUIActive;
    public Text humanGoText;
    public bool humanCanPlayCard;
    public RoundManager humanPlayerPlayCardsRoundManager;
    public PlayingOfHand humanPlayingOfHandManager;

    public GameObject humanLastCardButton;

    public bool humanIsMyTurn;
    public bool humanLastCardPlayed;



    //HUMAN DISCARD VARIABLES
    public List<GameObject> humanDiscardToggles;
    public List<GameObject> humanToDiscard;
    public bool humanHasDiscarded;

    //This is the object with the Card List for the player hand.
    public GameObject humanHandObject;

    public GameObject humanDeckObject;


    //AI VARIABLES
    //AI GREENPLAYER VARIABLES

    public bool aIDonePlaying;
    public bool aIActivePlayer;
    public int aIPlayerNumber;
    public bool aIIsDealer;
    public string aIPlayerColor;
    public int aITotalScore = 0;
    public List<Card> aIDeckOfCards;

    public List<GameObject> aIHoles;
    public GameObject[] aIPegs = new GameObject[3];
    public Vector3[] aIPegsTargetPosition = new Vector3[3];
    public bool[] aIMoveNext = new bool[3];

    [SerializeField] public List<Card> aIPlayerHand;
    public Scorer aIScorer;
    public List<GameObject> aICardImagesUI;

    public List<GameObject> aIUnplayedCards;
    public bool[] aIUnplayedCardsActive = new bool[6];

    public GameObject aISummaryObject;
    public GameObject aISpeechBubble;
    public bool aISpeechBubbleActive;
    public GameObject aISpeechBubbleText;
    public Color32 aIMyColor;
    public List<Card> aICrib;
    public bool aICounted;
    public bool aIScored;
    public BasicAI.difficulty aIDifficulty;

    public GameObject aIYourTurnUI;
    public bool aIYourTurnUIActive;


    public int aIRoundPlayingOfHandScore;
    public int aIRoundCountingHandScore;
    public int aIRoundCountingCribScore;


    public int aIRoundCuttingRightJack;


    public int aIRoundHandPairs;
    public int aIroundHandFifteens;

    public int aIRoundHand5Runs;
    public int aIRoundHand4Runs;
    public int aIRoundHand3Runs;

    public int aIRoundHand5Flushes;
    public int aIRoundHand4Flushes;

    public int aIRoundHandRightJack;

    public int aIRoundCribPairs;
    public int aIRoundCribFifteens;

    public int aIRoundCrib5Runs;
    public int aIRoundCrib4Runs;
    public int aIRoundCrib3Runs;

    public int aIRoundCrib5Flushes;
    public int aIRoundCrib4Flushes;
    public int aIRoundCribRightJack;

    //Game Summary Variables
    //Totals
    public int aIGamePlayingOfHandScore;
    public int aIGameCountingHandScore;
    public int aIGameCountingCribScore;
    //Breakdown
    public int aIGameRightJackFlipped;
    public int aIGameTotalFifteens;
    public int aIGameTotalPairs;
    public int aIGameTotal5Runs;
    public int aIGameTotal4Runs;
    public int aIGameTotal3Runs;
    public int aIGameTotalFlushes;
    public int aIGameRightJack;

    //AI PLAYER LIFETIME STATS
    public SaveData aILifetimeStats;

    //AI PLAYERPLAYCARDS VARIABLES
    public List<GameObject> aITogglesOn;
    public List<GameObject> aIToggles;
    public Transform aIHandUIObject;
    public List<GameObject> aIPlayedCardObects;
    public Sprite[] aIPlayedCardSprite = new Sprite[4];
    public bool[] aIPlayedCardObectsSpriteRendererActive = new bool[4];

    public Sprite aINullSprite;
    public int aINumberCardsPlayed;
    public int aIAINumberCardsPlayed;

    public bool aISaidGo;
    public bool aILastToGo;
    public GameObject aIGoUI;
    public bool aIGoUIActive;
    public Text aIGoText;
    public bool aICanPlayCard;
    public RoundManager aIPlayerPlayCardsRoundManager;
    public PlayingOfHand aIPlayingOfHandManager;

    public GameObject aILastCardButton;

    public bool aIIsMyTurn;
    public bool aILastCardPlayed;



    //AI DISCARD VARIABLES
    public List<GameObject> aIDiscardToggles;
    public List<GameObject> aIToDiscard;
    public bool aIHasDiscarded;

    //This is the object with the Card List for the player hand.
    public GameObject aIHandObject;

    public GameObject aIDeckObject;




    void Awake()
    {

    }



    public void updateGameState()
    {
        //UPDATE DECK
        savedDeck = gameDeck.deck;

        savedPlayerOneCards = gameDeck.playerOneCards;
        savedPlayerTwoCards = gameDeck.playerTwoCards;
        savedCrib = gameDeck.crib;
        savedDeckPlayers = gameDeck.Players;

        for(int i = 0; i < gameDeck.cribCards.Count; i++)
        {
            if (gameDeck.cribCards[i].activeInHierarchy == true)
            {
                showCribCards[i] = true;
            }
            else
                showCribCards[i] = false;
        }

        savedFifthCardParent = gameDeck.fifthCardParent;
        savedFifthCardParentPosition = gameDeck.fifthCardParent.transform.position;
        savedFifthCardParentRotation = gameDeck.fifthCardParent.transform.rotation.eulerAngles;
        savedFifthCard = gameDeck.fifthCard;
        fifthCardFaceRenderer = gameDeck.fifthCard.GetComponent<SpriteRenderer>().sprite;


        //UPDATE ROUND MANAGER
        gameStateRoundManagerActiveStage = roundManager.activeStage;
        gameStateRoundManagerPlayers = roundManager.players;
        gameStateRoundManagerUIButtons = roundManager.UIButtons;
        gameStateRoundManagerDonePlaying = roundManager.donePlaying;
        gameStateRoundManagerSummaryUIObject = roundManager.SummaryUIObject;
        gameStateRoundManagerActiveStageUIParent = roundManager.activeStageUIParent;
        gameStateRoundManagerActiveStageIcon = roundManager.activeStageIcon;
        gameStateRoundManagerActiveStageText = roundManager.activeStageText;
        gameStateRoundManagerDealerUI = roundManager.dealerUI;
        gameStateRoundManagerStageIcons = roundManager.stageIcons;
        gameStateRoundManagerStageOver = roundManager.stageOver;

        //UPDATE ROUND SUMMARY
        savedRoundSummaryGameObject = roundSummary.RoundSummaryGameObject;
        if (roundSummary.RoundSummaryGameObject.activeInHierarchy == true)
        {
            savedRoundSummaryGameObjectActive = true;
        }
        if (roundSummary.RoundSummaryGameObject.activeInHierarchy == false)
        {
            savedRoundSummaryGameObjectActive = false;
        }

        savedGameSummaryGameObject = roundSummary.GameSummaryGameObject;
        if (roundSummary.GameSummaryGameObject.activeInHierarchy == true)
        {
            savedGameSummaryObjectActive = true;
        }
        if (roundSummary.GameSummaryGameObject.activeInHierarchy == false)
        {
            savedGameSummaryObjectActive = false;
        }


        savedPlayerNameText = roundSummary.playerNameText;
        savedPlayerSummaryText = roundSummary.playerSummaryText;

        savedGamePlayerNameText = roundSummary.gamePlayerSummaryText;
        savedGamePlayerSummaryText = roundSummary.gamePlayerSummaryText;
        savedPlayerTrophy = roundSummary.playerTrophy;

        savedRoundSummaryMainCamera = roundSummary.mainCamera;
        savedMenuCameraPosition = roundSummary.menuCameraPosition;
        savedMenuCameraRotation = roundSummary.menuCameraRotation;

        savedCameraPosition = roundSummary.mainCamera.transform.position;
        savedCameraRoation = roundSummary.mainCamera.transform.rotation;

        //UPDATE PLAYING OF HAND
        gameStatePlayingOfHandPlayCount = gamePlayingOfHand.playCount;
        gameStatePlayingOfHandPlayCountTextUI = gamePlayingOfHand.playCountTextUI;
        gameStatePlayingOfHandPlayCountUIObject = gamePlayingOfHand.playCountUIObject;

        gameStatePlayingOfHandActiveCardValues = gamePlayingOfHand.activeCardValues;
        gameStatePlayingOfHandActiveCountValues = gamePlayingOfHand.activeCountValues;
        gameStatePlayingOfHandActiveRuns = gamePlayingOfHand.ActiveRuns;
        gameStatePlayingOfHandPlayedCards = gamePlayingOfHand.playedCards;

        gameStatePlayingOfHandTotalCardsPlayed = gamePlayingOfHand.totalCardsPlayed;

        gameStatePlayingOfHandPlayScore = gamePlayingOfHand.playScore;

        gameStatePlayingOfHandPlayers = gamePlayingOfHand.Players;

        gameStatePlayingOfHandLastCardScored = gamePlayingOfHand.lastCardScored;

        gameStatePlayingOfHandStageOver = gamePlayingOfHand.stageOver;

        //UPDATE COUNTING HAND AND COUNTING CRIB
        savedCountingStageOver = gameStageCountingHand.countingStageOver;
        savedCribStageOver = gameStageCountingHand.cribStageOver;

        //SAVE START MENU
        savedStartMenuMainCamera = gameMenu.mainCamera;

        savedMainMenuUI = gameMenu.mainMenuUI;

        savedGameSummaryObjectUI = gameMenu.gameSummaryObjectUI;

        savedDealerObjectUI = gameMenu.dealerObjectUI;

        //SAVE CURRENT SCORE UI
        savedCurrentScoreUIPlayers = savedScoreUI.players;
        savedCurrentScoreUIPlayerText = savedScoreUI.playerText;

        savedCurrentScoreUIScoreUIParent = savedScoreUI.scoreUIParent;

        savedCurrentScoreUIGameOn = savedScoreUI.gameOn;

        //UPDATE TUTORIAL VARIABLES
        savedTutorialOn = savedTutorial.tutorialOn;
        savedTutorialPrompt = savedTutorial.tutorialPrompt;
        if (savedTutorial.tutorialPrompt.activeInHierarchy == true)
        {
            savedTutorialPromptOn = true;
        }
        if (savedTutorial.tutorialPrompt.activeInHierarchy == false)
        {
            savedTutorialPromptOn = false;
        }



    //UPDATE HUMAN PLAYER GREENPLAYER
    humanDonePlaying = humanGreenPlayer.donePlaying;
        humanActivePlayer = humanGreenPlayer.activePlayer;
        humanPlayerNumber = humanGreenPlayer.playerNumber;
        humanIsDealer = humanGreenPlayer.isDealer;
        humanDeckOfCards = humanGreenPlayer.deckOfCards;

        humanPlayerColor = humanGreenPlayer.playerColor;
        humanTotalScore = humanGreenPlayer.totalScore;
        humanHoles = humanGreenPlayer.holes;
        humanPegs = humanGreenPlayer.pegs;
        for (int i = 0; i < humanPegs.Length; i++)
        {
            humanPegsTargetPosition[i] = humanPegs[i].GetComponent<Peg>().targetPosition;
        }

        humanMoveNext = humanGreenPlayer.moveNext;
        humanPlayerHand = humanGreenPlayer.playerHand;
        humanCardImagesUI = humanGreenPlayer.cardImagesUI;

        for (int i = 0; i < humanGreenPlayer.cardImagesUI.Count; i++)
        {
            humanCardImagesUISprite[i] = humanGreenPlayer.cardImagesUI[i].GetComponent<Image>().sprite;
            humanCardImagesUIColor[i] = humanGreenPlayer.cardImagesUI[i].GetComponent<Image>().color;
            if (humanGreenPlayer.cardImagesUI[i].activeInHierarchy == true)
            {
                humanCardImagesUIActive[i] = true;
            }
            if (humanGreenPlayer.cardImagesUI[i].activeInHierarchy == false)
            {
                humanCardImagesUIActive[i] = false;
            }

        }

        humanCountSummaryUI = humanGreenPlayer.countSummaryUI;
        humanScorer = humanGreenPlayer.scorer;
        humanSummaryObject = humanGreenPlayer.summaryObject;

        //Will need to save their active state
        humanSpeechBubble = humanGreenPlayer.speechBubble;
        if (humanSpeechBubble.activeInHierarchy == true)
        {
            humanSpeechBubbleActive = true;
        }
        else
            humanSpeechBubbleActive = false;

        humanMyColor = humanGreenPlayer.myColor;

        //public Color32 playerGreen = new Color32(0, 155, 0, 255);

        //public List<GameObject> aIUnplayedCards = new List<GameObject>();

        //public GameObject borderObject;
        //public GameObject donePlayingButton;
        //public Material turtleSkin;

        //public bool allowedToScore;

        humanCrib = humanGreenPlayer.crib;

        humanCounted = humanGreenPlayer.counted;
        humanScored = humanGreenPlayer.scored;

        humanYourTurnUI = humanGreenPlayer.yourTurnUI;
        if (humanGreenPlayer.yourTurnUI.activeInHierarchy == true)
        {
            humanYourTurnUIActive = true;
        }
        if (humanGreenPlayer.yourTurnUI.activeInHierarchy == false)
        {
            humanYourTurnUIActive = false;
        }

        //Round Summary Variables
        //Totals
        humanRoundPlayingOfHandScore = humanGreenPlayer.roundPlayingOfHandScore;
        humanRoundCountingHandScore = humanGreenPlayer.roundCountingHandScore;
        humanRoundCountingCribScore = humanGreenPlayer.roundCountingCribScore;

        //Right Jack flipped
        humanRoundCuttingRightJack = humanGreenPlayer.roundCuttingRightJack;

        //Counting Hand
        humanRoundHandPairs = humanGreenPlayer.roundHandPairs;
        humanroundHandFifteens = humanGreenPlayer.roundHandFifteens;

        humanRoundHand5Runs = humanGreenPlayer.roundHand5Runs;
        humanRoundHand4Runs = humanGreenPlayer.roundHand4Runs;
        humanRoundHand3Runs = humanGreenPlayer.roundHand3Runs;

        humanRoundHand5Flushes = humanGreenPlayer.roundHand5Flushes;
        humanRoundHand4Flushes = humanGreenPlayer.roundHand4Flushes;

        humanRoundHandRightJack = humanGreenPlayer.roundHandRightJack;
        //Counting Crib
        humanRoundCribPairs = humanGreenPlayer.roundCribPairs;
        humanRoundCribFifteens = humanGreenPlayer.roundCribFifteens;

        humanRoundCrib5Runs = humanGreenPlayer.roundCrib5Runs;
        humanRoundCrib4Runs = humanGreenPlayer.roundCrib4Runs;
        humanRoundCrib3Runs = humanGreenPlayer.roundCrib3Runs;

        humanRoundCrib5Flushes = humanGreenPlayer.roundCrib5Flushes;
        humanRoundCrib4Flushes = humanGreenPlayer.roundCrib4Flushes;
        humanRoundCribRightJack = humanGreenPlayer.roundCribRightJack;

        //Game Summary Variables
        //Totals
        humanGamePlayingOfHandScore = humanGreenPlayer.gamePlayingOfHandScore;
        humanGameCountingHandScore = humanGreenPlayer.gameCountingHandScore;
        humanGameCountingCribScore = humanGreenPlayer.gameCountingCribScore;
        //Breakdown
        humanGameRightJackFlipped = humanGreenPlayer.gameRightJackFlipped;
        humanGameTotalFifteens = humanGreenPlayer.gameTotalFifteens;
        humanGameTotalPairs = humanGreenPlayer.gameTotalPairs;
        humanGameTotal5Runs = humanGreenPlayer.gameTotal5Runs;
        humanGameTotal4Runs = humanGreenPlayer.gameTotal4Runs;
        humanGameTotal3Runs = humanGreenPlayer.gameTotal3Runs;
        humanGameTotalFlushes = humanGreenPlayer.gameTotalFlushes;
        humanGameRightJack = humanGreenPlayer.gameRightJack;

        //HUMAN LIFETIME STATS
        humanLifetimeStats = humanGreenPlayer.lifetimeStats;

        //UPDATE HUMAN PLAYER PLAYERPLAYCARDS
        humanTogglesOn = humanPlayerPlayCards.togglesOn;
        humanToggles = humanPlayerPlayCards.toggles;
        humanHandUIObject = humanPlayerPlayCards.handUIObject;
        humanPlayedCardObects = humanPlayerPlayCards.playedCardObects;
        for (int i = 0; i < humanPlayerPlayCards.playedCardObects.Count; i++)
        {
            humanPlayedCardSprite[i] = humanPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().sprite;
            if (humanPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled == true)
            {
                humanPlayedCardObectsSpriteRendererActive[i] = true;
            }
            if (humanPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled == false)
            {
                humanPlayedCardObectsSpriteRendererActive[i] = false;
            }

        }

        humanNullSprite = humanPlayerPlayCards.nullSprite;
        humanNumberCardsPlayed = humanPlayerPlayCards.numberCardsPlayed;
        humanSaidGo = humanPlayerPlayCards.saidGo;
        humanLastToGo = humanPlayerPlayCards.lastToGo;
        //make sure to set active state.
        humanGoUI = humanPlayerPlayCards.goUI;
        if (humanPlayerPlayCards.goUI.activeInHierarchy == true)
        {
            humanGoUIActive = true;
        }
        if (humanPlayerPlayCards.goUI.activeInHierarchy == false)
        {
            humanGoUIActive = false;
        }
        humanGoText = humanPlayerPlayCards.goText;
        humanCanPlayCard = humanPlayerPlayCards.canPlayCard;
        humanPlayerPlayCardsRoundManager = humanPlayerPlayCards.roundManager;
        humanPlayingOfHandManager = humanPlayerPlayCards.playingOfHandManager;

        humanLastCardButton = humanPlayerPlayCards.lastCardButton;

        humanIsMyTurn = humanPlayerPlayCards.isMyTurn;
        humanLastCardPlayed = humanPlayerPlayCards.lastCardPlayed;



        //HUMAN DISCARD VARIABLES


        humanDiscardToggles = humanDiscard.toggles;
        humanToDiscard = humanDiscard.toDiscard;
        humanHasDiscarded = humanDiscard.hasDiscarded;

        //This is the object with the Card List for the player hand.
        humanHandObject = humanDiscard.handObject;

        humanDeckObject = humanDiscard.deckObject;


        //UPDATE AI PLAYER GREENPLAYER
        aIDonePlaying = aIGreenPlayer.donePlaying;
        aIActivePlayer = aIGreenPlayer.activePlayer;
        aIPlayerNumber = aIGreenPlayer.playerNumber;
        aIIsDealer = aIGreenPlayer.isDealer;
        aIDeckOfCards = aIGreenPlayer.deckOfCards;
        aIDifficulty = aIGreenPlayer.playerAI.aIDifficulty;

        aIPlayerColor = aIGreenPlayer.playerColor;
        aITotalScore = aIGreenPlayer.totalScore;
        aIHoles = aIGreenPlayer.holes;
        aIPegs = aIGreenPlayer.pegs;
        for (int i = 0; i < aIPegs.Length; i++)
        {
            aIPegsTargetPosition[i] = aIPegs[i].GetComponent<Peg>().targetPosition;
        }

        aIMoveNext = aIGreenPlayer.moveNext;
        aIPlayerHand = aIGreenPlayer.playerHand;
        aICardImagesUI = aIGreenPlayer.cardImagesUI;

        aIUnplayedCards = aIGreenPlayer.aIUnplayedCards;
        for (int i = 0; i < aIGreenPlayer.aIUnplayedCards.Count; i++)
        {
            if (aIGreenPlayer.aIUnplayedCards[i].activeInHierarchy == true)
            {
                aIUnplayedCardsActive[i] = true;
            }
            if (aIGreenPlayer.aIUnplayedCards[i].activeInHierarchy == false)
            {
                aIUnplayedCardsActive[i] = false;
            }
        }

        aIScorer = aIGreenPlayer.scorer;
        aISummaryObject = aIGreenPlayer.summaryObject;

        //Will need to save their active state
        aISpeechBubble = aIGreenPlayer.speechBubble;
        if (aISpeechBubble.activeInHierarchy == true)
        {
            aISpeechBubbleActive = true;
        }
        else
            aISpeechBubbleActive = false;

        aIMyColor = aIGreenPlayer.myColor;

        //public Color32 playerGreen = new Color32(0, 155, 0, 255);

        //public List<GameObject> aIUnplayedCards = new List<GameObject>();

        //public GameObject borderObject;
        //public GameObject donePlayingButton;
        //public Material turtleSkin;

        //public bool allowedToScore;

        aICrib = aIGreenPlayer.crib;

        aICounted = aIGreenPlayer.counted;
        aIScored = aIGreenPlayer.scored;

        aIYourTurnUI = aIGreenPlayer.yourTurnUI;
        if (aIGreenPlayer.yourTurnUI.activeInHierarchy == true)
        {
            aIYourTurnUIActive = true;
        }
        if (aIGreenPlayer.yourTurnUI.activeInHierarchy == false)
        {
            aIYourTurnUIActive = false;
        }

        //Round Summary Variables
        //Totals
        aIRoundPlayingOfHandScore = aIGreenPlayer.roundPlayingOfHandScore;
        aIRoundCountingHandScore = aIGreenPlayer.roundCountingHandScore;
        aIRoundCountingCribScore = aIGreenPlayer.roundCountingCribScore;

        //Right Jack flipped
        aIRoundCuttingRightJack = aIGreenPlayer.roundCuttingRightJack;

        //Counting Hand
        aIRoundHandPairs = aIGreenPlayer.roundHandPairs;
        aIroundHandFifteens = aIGreenPlayer.roundHandFifteens;

        aIRoundHand5Runs = aIGreenPlayer.roundHand5Runs;
        aIRoundHand4Runs = aIGreenPlayer.roundHand4Runs;
        aIRoundHand3Runs = aIGreenPlayer.roundHand3Runs;

        aIRoundHand5Flushes = aIGreenPlayer.roundHand5Flushes;
        aIRoundHand4Flushes = aIGreenPlayer.roundHand4Flushes;

        aIRoundHandRightJack = aIGreenPlayer.roundHandRightJack;
        //Counting Crib
        aIRoundCribPairs = aIGreenPlayer.roundCribPairs;
        aIRoundCribFifteens = aIGreenPlayer.roundCribFifteens;

        aIRoundCrib5Runs = aIGreenPlayer.roundCrib5Runs;
        aIRoundCrib4Runs = aIGreenPlayer.roundCrib4Runs;
        aIRoundCrib3Runs = aIGreenPlayer.roundCrib3Runs;

        aIRoundCrib5Flushes = aIGreenPlayer.roundCrib5Flushes;
        aIRoundCrib4Flushes = aIGreenPlayer.roundCrib4Flushes;
        aIRoundCribRightJack = aIGreenPlayer.roundCribRightJack;

        //Game Summary Variables
        //Totals
        aIGamePlayingOfHandScore = aIGreenPlayer.gamePlayingOfHandScore;
        aIGameCountingHandScore = aIGreenPlayer.gameCountingHandScore;
        aIGameCountingCribScore = aIGreenPlayer.gameCountingCribScore;
        //Breakdown
        aIGameRightJackFlipped = aIGreenPlayer.gameRightJackFlipped;
        aIGameTotalFifteens = aIGreenPlayer.gameTotalFifteens;
        aIGameTotalPairs = aIGreenPlayer.gameTotalPairs;
        aIGameTotal5Runs = aIGreenPlayer.gameTotal5Runs;
        aIGameTotal4Runs = aIGreenPlayer.gameTotal4Runs;
        aIGameTotal3Runs = aIGreenPlayer.gameTotal3Runs;
        aIGameTotalFlushes = aIGreenPlayer.gameTotalFlushes;
        aIGameRightJack = aIGreenPlayer.gameRightJack;

        //AI LIFETIME STATS
        aILifetimeStats = aIGreenPlayer.lifetimeStats;

        //UPDATE AI PLAYER PLAYERPLAYCARDS
        aITogglesOn = aIPlayerPlayCards.togglesOn;
        aIToggles = aIPlayerPlayCards.toggles;
        aIHandUIObject = aIPlayerPlayCards.handUIObject;
        aIPlayedCardObects = aIPlayerPlayCards.playedCardObects;
        for (int i = 0; i < aIPlayerPlayCards.playedCardObects.Count; i++)
        {
            aIPlayedCardSprite[i] = aIPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().sprite;

            if (aIPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled == true)
            {
                aIPlayedCardObectsSpriteRendererActive[i] = true;
            }
            if (aIPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled == false)
            {
                aIPlayedCardObectsSpriteRendererActive[i] = false;
            }

        }
        aINullSprite = aIPlayerPlayCards.nullSprite;
        aINumberCardsPlayed = aIPlayerPlayCards.numberCardsPlayed;
        aIAINumberCardsPlayed = aIPlayerPlayCards.aINumberCardsPlayed;
        aISaidGo = aIPlayerPlayCards.saidGo;
        aILastToGo = aIPlayerPlayCards.lastToGo;
        //make sure to set active state.
        aIGoUI = aIPlayerPlayCards.goUI;
        if (aIPlayerPlayCards.goUI.activeInHierarchy == true)
        {
            aIGoUIActive = true;
        }
        if (aIPlayerPlayCards.goUI.activeInHierarchy == false)
        {
            aIGoUIActive = false;
        }
        aIGoText = aIPlayerPlayCards.goText;
        aICanPlayCard = aIPlayerPlayCards.canPlayCard;
        aIPlayerPlayCardsRoundManager = aIPlayerPlayCards.roundManager;
        aIPlayingOfHandManager = aIPlayerPlayCards.playingOfHandManager;

        aILastCardButton = aIPlayerPlayCards.lastCardButton;

        aIIsMyTurn = aIPlayerPlayCards.isMyTurn;
        aILastCardPlayed = aIPlayerPlayCards.lastCardPlayed;



        //AI DISCARD VARIABLES


        aIDiscardToggles = aIDiscard.toggles;
        aIToDiscard = aIDiscard.toDiscard;
        aIHasDiscarded = aIDiscard.hasDiscarded;

        //This is the object with the Card List for the player hand.
        aIHandObject = aIDiscard.handObject;

        aIDeckObject = aIDiscard.deckObject;


    }


    public void saveGameState(GameState data)
    {
        updateGameState();
        string json = JsonUtility.ToJson(data);

#if UNITY_EDITOR_OSX
        File.WriteAllText(Application.dataPath + "/savedGameState.txt", json);
#endif
#if UNITY_STANDALONE_OSX
        File.WriteAllText(Application.dataPath + "/savedGameState.txt", json);
#endif
#if UNITY_STANDALONE_WIN
        File.WriteAllText(Application.dataPath + "/savedGameState.txt", json);
#endif
#if UNITY_IOS
        File.WriteAllText(Application.persistentDataPath + "/savedGameState.txt", json);
#endif
#if UNITY_ANDROID
        File.WriteAllText(Application.persistentDataPath + "/savedGameState.txt", json);
#endif

        Debug.Log("Saved");
    }

    public void loadGameState()
    {
        //gameMenu.startGame();
        Debug.Log("Data Loading");

#if UNITY_STANDALONE_OSX

        if (File.Exists(Application.dataPath + "/savedGameState.txt"))
        {
#endif
#if UNITY_STANDALONE_WIN
        if (File.Exists(Application.dataPath + "/savedGameState.txt"))
        {
#endif
#if UNITY_IOS
        if (File.Exists(Application.persistentDataPath + "/savedGameState.txt"))
            {
#endif
#if UNITY_ANDROID
            if (File.Exists(Application.persistentDataPath + "/savedGameState.txt"))
            {
#endif


            

#if UNITY_STANDALONE_OSX
            string saveString = File.ReadAllText(Application.dataPath + "/savedGameState.txt");
#endif

#if UNITY_STANDALONE_WIN
            string saveString = File.ReadAllText(Application.dataPath + "/savedGameState.txt");
#endif
#if UNITY_IOS
            string saveString = File.ReadAllText(Application.persistentDataPath + "/savedGameState.txt");
#endif
#if UNITY_ANDROID
            string saveString = File.ReadAllText(Application.persistentDataPath + "/savedGameState.txt");
#endif
            GameState savedGameState = JsonUtility.FromJson<GameState>(saveString);

            //LOAD DECK
            gameDeck.deck = savedGameState.savedDeck;

            gameDeck.playerOneCards = savedGameState.savedPlayerOneCards;
            gameDeck.playerTwoCards = savedGameState.savedPlayerTwoCards;
            gameDeck.crib = savedGameState.savedCrib;
            gameDeck.Players = savedGameState.savedDeckPlayers;

            for (int i = 0; i < gameDeck.cribCards.Count; i++)
            {
                if (savedGameState.showCribCards[i] == true)
                {
                    gameDeck.cribCards[i].SetActive(true);
                }
                else
                    gameDeck.cribCards[i].SetActive(false);

            }


            gameDeck.fifthCardParent = savedGameState.savedFifthCardParent;
            gameDeck.fifthCardParent.transform.position = savedGameState.savedFifthCardParentPosition;
            gameDeck.fifthCardParent.transform.eulerAngles = savedGameState.savedFifthCardParentRotation;
            gameDeck.fifthCard = savedGameState.savedFifthCard;
            gameDeck.fifthCard.GetComponent<SpriteRenderer>().sprite = savedGameState.fifthCardFaceRenderer;

            //LOAD ROUND MANAGER STATE
            roundManager.activeStage = savedGameState.gameStateRoundManagerActiveStage;
            roundManager.players = savedGameState.gameStateRoundManagerPlayers;
            roundManager.updateActiveStageUI();


            roundManager.activeStage = savedGameState.gameStateRoundManagerActiveStage;
            roundManager.players = savedGameState.gameStateRoundManagerPlayers;
            roundManager.UIButtons = savedGameState.gameStateRoundManagerUIButtons;
            roundManager.donePlaying = savedGameState.gameStateRoundManagerDonePlaying;
            roundManager.SummaryUIObject = savedGameState.gameStateRoundManagerSummaryUIObject;
            roundManager.activeStageUIParent = savedGameState.gameStateRoundManagerActiveStageUIParent;
            roundManager.activeStageUIParent.SetActive(true);
            roundManager.activeStageIcon = savedGameState.gameStateRoundManagerActiveStageIcon;
            roundManager.activeStageText = savedGameState.gameStateRoundManagerActiveStageText;
            roundManager.dealerUI = savedGameState.gameStateRoundManagerDealerUI;
            roundManager.dealerUI.SetActive(true);
            roundManager.stageIcons = savedGameState.gameStateRoundManagerStageIcons;
            roundManager.stageOver = savedGameState.gameStateRoundManagerStageOver;





            //LOAD ROUND SUMMARY
            roundSummary.RoundSummaryGameObject = savedGameState.savedRoundSummaryGameObject;
            if (savedGameState.savedRoundSummaryGameObjectActive == true)
            {
                roundSummary.RoundSummaryGameObject.SetActive(true);
            }
            if (savedGameState.savedRoundSummaryGameObjectActive == false)
            {
                roundSummary.RoundSummaryGameObject.SetActive(false);
            }

            roundSummary.GameSummaryGameObject = savedGameState.savedGameSummaryGameObject;
            if (savedGameState.savedGameSummaryObjectActive == true)
            {
                Debug.Log("Game Summary Object Should be Active");
                roundSummary.GameSummaryGameObject.SetActive(true);
            }
            roundSummary.GameSummaryGameObject = savedGameState.savedGameSummaryGameObject;
            if (savedGameState.savedGameSummaryObjectActive == false)
            {
                Debug.Log("Game Summary Object Should be Inactive");
                roundSummary.GameSummaryGameObject.SetActive(false);
            }


            roundSummary.playerNameText = savedGameState.savedPlayerNameText;
            roundSummary.playerSummaryText = savedGameState.savedPlayerSummaryText;

            roundSummary.gamePlayerSummaryText = savedGameState.savedGamePlayerNameText;
            roundSummary.gamePlayerSummaryText = savedGameState.savedGamePlayerSummaryText;
            roundSummary.playerTrophy = savedGameState.savedPlayerTrophy;

            roundSummary.mainCamera = savedGameState.savedRoundSummaryMainCamera;
            roundSummary.menuCameraPosition = savedGameState.savedMenuCameraPosition;
            roundSummary.menuCameraRotation = savedGameState.savedMenuCameraRotation;

            //roundSummary.mainCamera.transform.position = savedGameState.savedCameraPosition;
            //roundSummary.mainCamera.transform.rotation = savedCameraRoation;

            //LOAD PLAYING OF HAND
            gamePlayingOfHand.playCount = savedGameState.gameStatePlayingOfHandPlayCount;
            gamePlayingOfHand.playCountTextUI = savedGameState.gameStatePlayingOfHandPlayCountTextUI;
            gamePlayingOfHand.playCountTextUI.GetComponent<Text>().text = savedGameState.gameStatePlayingOfHandPlayCount.ToString();
            gamePlayingOfHand.playCountUIObject = savedGameState.gameStatePlayingOfHandPlayCountUIObject;

            gamePlayingOfHand.activeCardValues = savedGameState.gameStatePlayingOfHandActiveCardValues;
            gamePlayingOfHand.activeCountValues = savedGameState.gameStatePlayingOfHandActiveCountValues;
            gamePlayingOfHand.ActiveRuns = savedGameState.gameStatePlayingOfHandActiveRuns;
            gamePlayingOfHand.playedCards = savedGameState.gameStatePlayingOfHandPlayedCards;

            gamePlayingOfHand.totalCardsPlayed = savedGameState.gameStatePlayingOfHandTotalCardsPlayed;

            gamePlayingOfHand.playScore = savedGameState.gameStatePlayingOfHandPlayScore;

            gamePlayingOfHand.Players = savedGameState.gameStatePlayingOfHandPlayers;

            gamePlayingOfHand.lastCardScored = savedGameState.gameStatePlayingOfHandLastCardScored;

            gamePlayingOfHand.stageOver = savedGameState.gameStatePlayingOfHandStageOver;

            //LOAD COUNTING HAND AND COUNTING CRIP
            gameStageCountingHand.countingStageOver = savedGameState.savedCountingStageOver;
            gameStageCountingHand.cribStageOver = savedGameState.savedCribStageOver;


            //LOAD GAME MENU
            gameMenu.mainCamera = savedGameState.savedStartMenuMainCamera;
            gameMenu.mainCamera.transform.position = new Vector3(0f, 78.1f, -60.5f);
            gameMenu.mainCamera.transform.rotation = Quaternion.Euler(37f, 0f, 0f);

            gameMenu.mainMenuUI = savedGameState.savedMainMenuUI;
            gameMenu.mainMenuUI.SetActive(false);

            savedGameSummaryObjectUI = gameMenu.gameSummaryObjectUI;
            //gameMenu.gameSummaryObjectUI.SetActive(false);

            savedDealerObjectUI = gameMenu.dealerObjectUI;

            gameMenu.enableHumanPlayerChildren();
            gameMenu.enableAIPlayerChildren();

            //LOAD TUTORIAL STATUS
            savedTutorial.tutorialOn = savedGameState.savedTutorialOn;
            if(savedTutorial.tutorialOn == true)
            {
                savedTutorial.tutorialUI.SetActive(true);
            }

            if (savedTutorial.tutorialOn == false)
            {
                savedTutorial.tutorialUI.SetActive(false);
            }

            savedTutorial.tutorialPrompt = savedGameState.savedTutorialPrompt;
            if(savedGameState.savedTutorialPromptOn == true)
            {
                savedTutorial.tutorialPrompt.SetActive(true);
                
            }
            if (savedGameState.savedTutorialPromptOn == false)
            {
                savedTutorial.tutorialPrompt.SetActive(false);
            }

            //Load SCORER VARIABLES
            /*
            if (scoredHandUIOn == true)
            {
                humanGreenPlayer.countSummaryUI.SetActive(true);
            }

            if (scoredHandUIOn == false)
            {
                humanGreenPlayer.countSummaryUI.SetActive(true);
            }


            gameScorer.fifteensParent = savedFifteensParent;
            gameScorer.pairsParent = savedPairsParent; 
            gameScorer.runsParent = savedRunsParent;
            gameScorer.flushesParent = savedFlushesParent;
            gameScorer.nibsParent = savedNibsParent;

            for (int i = 0; i < fifteensChildren.Count; i++)
            {
                GameObject.Instantiate(gameScorer.fifteenUIPrefab, gameScorer.fifteensParent.transform);
            }
            */
            /*

            fifteenSpritesCount.Add(gameScorer.fifteensParent.GetComponentsInChildren<Image>().sprite);

            for (int i = 0; i < fifteenSpritesCount.Count; i++)
            {
                fifteenSpritesCount[i] = fifteensSprites[i];
            }
            
            savedFifteensText = gameScorer.fifteensText.text;
            savedPairsText = gameScorer.pairsText.text;
            savedRunsText = gameScorer.runsText.text;
            savedFlushesText = gameScorer.flushesText.text;
            savedNibsText = gameScorer.nibsText.text;
            savedTotalText = gameScorer.totalText.text;
            */
            //LOAD HUMAN PLAYER GREENPLAYER STATE

            humanGreenPlayer.donePlaying = savedGameState.humanDonePlaying;
            humanGreenPlayer.activePlayer = savedGameState.humanActivePlayer;
            humanGreenPlayer.playerNumber = savedGameState.humanPlayerNumber;
            humanGreenPlayer.isDealer = savedGameState.humanIsDealer;
            humanGreenPlayer.deckOfCards = savedGameState.humanDeckOfCards;

            humanGreenPlayer.playerColor = savedGameState.humanPlayerColor;


            humanGreenPlayer.totalScore = savedGameState.humanTotalScore;
            humanGreenPlayer.holes = savedGameState.humanHoles;


            humanGreenPlayer.pegs = savedGameState.humanPegs;

            for (int i = 0; i < humanGreenPlayer.pegs.Length; i++)
            {
                humanGreenPlayer.pegs[i].GetComponent<Peg>().targetPosition = savedGameState.humanPegsTargetPosition[i];
            }

            humanGreenPlayer.moveNext = savedGameState.humanMoveNext;
            humanGreenPlayer.playerHand = savedGameState.humanPlayerHand;

            humanGreenPlayer.cardImagesUI = savedGameState.humanCardImagesUI;
            humanCardImagesUIColor = savedGameState.humanCardImagesUIColor;

            for (int i = 0; i < humanGreenPlayer.playerHand.Count; i++)
            {
                if (humanGreenPlayer.playerHand[i].played == false)
                {
                    humanGreenPlayer.cardImagesUI[i].GetComponent<Image>().sprite = savedGameState.humanPlayerHand[i].cardSprite;
                    //humanGreenPlayer.cardImagesUI[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                if (humanGreenPlayer.playerHand[i].played == true) {
                }

            }

            for (int i = 0; i < humanGreenPlayer.cardImagesUI.Count; i++)
            {
                humanGreenPlayer.cardImagesUI[i].GetComponent<Image>().sprite = savedGameState.humanCardImagesUISprite[i];
                humanGreenPlayer.cardImagesUI[i].GetComponent<Image>().color = humanCardImagesUIColor[i];
                if (savedGameState.humanCardImagesUIActive[i] == true)
                {
                    humanGreenPlayer.cardImagesUI[i].SetActive(true);
                }
                if (savedGameState.humanCardImagesUIActive[i] == false)
                {
                    humanGreenPlayer.cardImagesUI[i].SetActive(false);
                }

            }

            humanGreenPlayer.scorer = savedGameState.humanScorer;
            humanGreenPlayer.summaryObject = savedGameState.humanSummaryObject;

            //Will need to save their active state
            humanGreenPlayer.speechBubble = savedGameState.humanSpeechBubble;
            if (humanSpeechBubbleActive == true)
            {
                humanGreenPlayer.speechBubble.SetActive(true);
            }
            else
                humanGreenPlayer.speechBubble.SetActive(false);

            humanMyColor = savedGameState.humanMyColor;
            humanGreenPlayer.crib = savedGameState.humanCrib;
            humanGreenPlayer.counted = savedGameState.humanCounted;
            humanScored = savedGameState.humanScored;

            humanGreenPlayer.yourTurnUI = savedGameState.humanYourTurnUI;
            if (savedGameState.humanYourTurnUIActive == true)
            {
                humanGreenPlayer.yourTurnUI.SetActive(true);
            }
            humanGreenPlayer.yourTurnUI = savedGameState.humanYourTurnUI;
            if (savedGameState.humanYourTurnUIActive == false)
            {
                humanGreenPlayer.yourTurnUI.SetActive(false);
            }

            //Round Summary Variables
            //Totals
            humanGreenPlayer.roundPlayingOfHandScore = savedGameState.humanRoundPlayingOfHandScore;
            humanGreenPlayer.roundCountingHandScore = savedGameState.humanRoundCountingHandScore;
            humanGreenPlayer.roundCountingCribScore = savedGameState.humanRoundCountingCribScore;

            //Right Jack flipped
            humanGreenPlayer.roundCuttingRightJack = savedGameState.humanRoundCuttingRightJack;

            //Counting Hand
            humanGreenPlayer.roundHandPairs = savedGameState.humanRoundHandPairs;
            humanGreenPlayer.roundHandFifteens = savedGameState.humanroundHandFifteens;

            humanGreenPlayer.roundHand5Runs = savedGameState.humanRoundHand5Runs;
            humanGreenPlayer.roundHand4Runs = savedGameState.humanRoundHand4Runs;
            humanGreenPlayer.roundHand3Runs = savedGameState.humanRoundHand3Runs;

            humanGreenPlayer.roundHand5Flushes = savedGameState.humanRoundHand5Flushes;
            humanGreenPlayer.roundHand4Flushes = savedGameState.humanRoundHand4Flushes;

            humanGreenPlayer.roundHandRightJack = savedGameState.humanRoundHandRightJack;
            //Counting Crib
            humanGreenPlayer.roundCribPairs = savedGameState.humanRoundCribPairs;
            humanGreenPlayer.roundCribFifteens = savedGameState.humanRoundCribFifteens;

            humanGreenPlayer.roundCrib5Runs = savedGameState.humanRoundCrib5Runs;
            humanGreenPlayer.roundCrib4Runs = savedGameState.humanRoundCrib4Runs;
            humanGreenPlayer.roundCrib3Runs = savedGameState.humanRoundCrib3Runs;

            humanGreenPlayer.roundCrib5Flushes = savedGameState.humanRoundCrib5Flushes;
            humanGreenPlayer.roundCrib4Flushes = savedGameState.humanRoundCrib4Flushes;
            humanGreenPlayer.roundCribRightJack = savedGameState.humanRoundCribRightJack;

            //Game Summary Variables
            //Totals
            humanGreenPlayer.gamePlayingOfHandScore = savedGameState.humanGamePlayingOfHandScore;
            humanGreenPlayer.gameCountingHandScore = savedGameState.humanGameCountingHandScore;
            humanGreenPlayer.gameCountingCribScore = savedGameState.humanGameCountingCribScore;
            //Breakdown
            humanGreenPlayer.gameRightJackFlipped = savedGameState.humanGameRightJackFlipped;
            humanGreenPlayer.gameTotalFifteens = savedGameState.humanGameTotalFifteens;
            humanGreenPlayer.gameTotalPairs = savedGameState.humanGameTotalPairs;
            humanGreenPlayer.gameTotal5Runs = savedGameState.humanGameTotal5Runs;
            humanGreenPlayer.gameTotal4Runs = savedGameState.humanGameTotal4Runs;
            humanGreenPlayer.gameTotal3Runs = savedGameState.humanGameTotal3Runs;
            humanGreenPlayer.gameTotalFlushes = savedGameState.humanGameTotalFlushes;
            humanGreenPlayer.gameRightJack = savedGameState.humanGameRightJack;

            humanGreenPlayer.SetmyColor();
            humanGreenPlayer.resetToggles();

            //LOAD HUMAN PLAYER LIFETIME STATS

            humanGreenPlayer.lifetimeStats = savedGameState.humanLifetimeStats;

            //LOAD HUMAN PLAYER PLAYERPLAYHANDS STATE;
            humanPlayerPlayCards.togglesOn = savedGameState.humanTogglesOn;
            humanPlayerPlayCards.toggles = savedGameState.humanToggles;
            humanPlayerPlayCards.handUIObject = savedGameState.humanHandUIObject;
            humanPlayerPlayCards.playedCardObects = savedGameState.humanPlayedCardObects;
            for (int i = 0; i < humanPlayerPlayCards.playedCardObects.Count; i++)
            {
                humanPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().sprite = savedGameState.humanPlayedCardSprite[i];

                if (savedGameState.humanPlayedCardObectsSpriteRendererActive[i] == true)
                {
                    humanPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled = true;
                }
                if (savedGameState.humanPlayedCardObectsSpriteRendererActive[i] == false)
                {
                    humanPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled = false;
                }

            }
            humanPlayerPlayCards.nullSprite = savedGameState.humanNullSprite;
            humanPlayerPlayCards.numberCardsPlayed = savedGameState.humanNumberCardsPlayed;
            humanPlayerPlayCards.saidGo = savedGameState.humanSaidGo;
            humanPlayerPlayCards.lastToGo = savedGameState.humanLastToGo;
            //make sure to set active state.
            humanPlayerPlayCards.goUI = savedGameState.humanGoUI;

            if (savedGameState.humanGoUIActive == true)
            {
                humanPlayerPlayCards.goUI.SetActive(true);
            }
            if (savedGameState.humanGoUIActive == false)
            {
                humanPlayerPlayCards.goUI.SetActive(false);
            }

            humanPlayerPlayCards.goText = savedGameState.humanGoText;
            humanPlayerPlayCards.canPlayCard = savedGameState.humanCanPlayCard;
            humanPlayerPlayCards.roundManager = savedGameState.humanPlayerPlayCardsRoundManager;
            humanPlayerPlayCards.playingOfHandManager = savedGameState.humanPlayingOfHandManager;

            humanPlayerPlayCards.lastCardButton = savedGameState.humanLastCardButton;

            humanPlayerPlayCards.isMyTurn = savedGameState.humanIsMyTurn;
            humanPlayerPlayCards.lastCardPlayed = savedGameState.humanLastCardPlayed;



            //LOAD HUMAN DISCARD VARIABLES
            humanDiscard.toggles = savedGameState.humanDiscardToggles;
            humanDiscard.toDiscard = savedGameState.humanToDiscard;
            humanDiscard.hasDiscarded = savedGameState.humanHasDiscarded;
            humanDiscard.handObject = savedGameState.humanHandObject;
            humanDiscard.deckObject = savedGameState.humanDeckObject;


            humanDiscard.resumeDiscard();
            humanDiscard.turnTogglesOff();

            //LOAD UPDATE HUMAN BUTTONS
            humanButtonManager.updateButtons();

            //LOAD AI PLAYER GREENPLAYER STATE

            aIGreenPlayer.donePlaying = savedGameState.aIDonePlaying;
            aIGreenPlayer.activePlayer = savedGameState.aIActivePlayer;
            aIGreenPlayer.playerNumber = savedGameState.aIPlayerNumber;
            aIGreenPlayer.isDealer = savedGameState.aIIsDealer;
            aIGreenPlayer.deckOfCards = savedGameState.aIDeckOfCards;

            aIGreenPlayer.playerColor = savedGameState.aIPlayerColor;
            aIGreenPlayer.totalScore = savedGameState.aITotalScore;
            aIGreenPlayer.holes = savedGameState.aIHoles;



            aIGreenPlayer.pegs = savedGameState.aIPegs;

            for (int i = 0; i < aIGreenPlayer.pegs.Length; i++)
            {
                aIGreenPlayer.pegs[i].GetComponent<Peg>().targetPosition = savedGameState.aIPegsTargetPosition[i];
            }

            aIGreenPlayer.moveNext = savedGameState.aIMoveNext;
            aIGreenPlayer.playerHand = savedGameState.aIPlayerHand;

            aIGreenPlayer.cardImagesUI = savedGameState.aICardImagesUI;

            aIGreenPlayer.aIUnplayedCards = savedGameState.aIUnplayedCards;
            for (int i = 0; i < savedGameState.aIUnplayedCards.Count; i++)
            {
                if (savedGameState.aIUnplayedCardsActive[i] == true)
                {
                    aIGreenPlayer.aIUnplayedCards[i].SetActive(true);
                }
                if (savedGameState.aIUnplayedCardsActive[i] == false)
                {
                    aIGreenPlayer.aIUnplayedCards[i].SetActive(false);
                }
            }

            for (int i = 0; i < aIGreenPlayer.playerHand.Count; i++)
            {
                if (aIGreenPlayer.playerHand[i].played == false)
                {
                    aIGreenPlayer.cardImagesUI[i].GetComponent<Image>().sprite = savedGameState.aIPlayerHand[i].cardSprite;
                    aIGreenPlayer.cardImagesUI[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }

            }

            aIGreenPlayer.summaryObject = savedGameState.aISummaryObject;

            //Will need to save their active state
            aIGreenPlayer.speechBubble = savedGameState.aISpeechBubble;
            if (aISpeechBubbleActive == true)
            {
                aIGreenPlayer.speechBubble.SetActive(true);
            }
            else
                aIGreenPlayer.speechBubble.SetActive(false);

            aIMyColor = savedGameState.aIMyColor;
            aIGreenPlayer.crib = savedGameState.aICrib;
            aIGreenPlayer.counted = savedGameState.aICounted;
            aIScored = savedGameState.aIScored;
            aIGreenPlayer.scorer = savedGameState.aIScorer;

            aIGreenPlayer.yourTurnUI = savedGameState.aIYourTurnUI;
            if (savedGameState.aIYourTurnUIActive == true)
            {
                aIGreenPlayer.yourTurnUI.SetActive(true);
            }
            aIGreenPlayer.yourTurnUI = savedGameState.aIYourTurnUI;
            if (savedGameState.aIYourTurnUIActive == false)
            {
                aIGreenPlayer.yourTurnUI.SetActive(false);
            }

            //Round Summary Variables
            //Totals
            aIGreenPlayer.roundPlayingOfHandScore = savedGameState.aIRoundPlayingOfHandScore;
            aIGreenPlayer.roundCountingHandScore = savedGameState.aIRoundCountingHandScore;
            aIGreenPlayer.roundCountingCribScore = savedGameState.aIRoundCountingCribScore;

            //Right Jack flipped
            aIGreenPlayer.roundCuttingRightJack = savedGameState.aIRoundCuttingRightJack;

            //Counting Hand
            aIGreenPlayer.roundHandPairs = savedGameState.aIRoundHandPairs;
            aIGreenPlayer.roundHandFifteens = savedGameState.aIroundHandFifteens;

            aIGreenPlayer.roundHand5Runs = savedGameState.aIRoundHand5Runs;
            aIGreenPlayer.roundHand4Runs = savedGameState.aIRoundHand4Runs;
            aIGreenPlayer.roundHand3Runs = savedGameState.aIRoundHand3Runs;

            aIGreenPlayer.roundHand5Flushes = savedGameState.aIRoundHand5Flushes;
            aIGreenPlayer.roundHand4Flushes = savedGameState.aIRoundHand4Flushes;

            aIGreenPlayer.roundHandRightJack = savedGameState.aIRoundHandRightJack;
            //Counting Crib
            aIGreenPlayer.roundCribPairs = savedGameState.aIRoundCribPairs;
            aIGreenPlayer.roundCribFifteens = savedGameState.aIRoundCribFifteens;

            aIGreenPlayer.roundCrib5Runs = savedGameState.aIRoundCrib5Runs;
            aIGreenPlayer.roundCrib4Runs = savedGameState.aIRoundCrib4Runs;
            aIGreenPlayer.roundCrib3Runs = savedGameState.aIRoundCrib3Runs;

            aIGreenPlayer.roundCrib5Flushes = savedGameState.aIRoundCrib5Flushes;
            aIGreenPlayer.roundCrib4Flushes = savedGameState.aIRoundCrib4Flushes;
            aIGreenPlayer.roundCribRightJack = savedGameState.aIRoundCribRightJack;

            //Game Summary Variables
            //Totals
            aIGreenPlayer.gamePlayingOfHandScore = savedGameState.aIGamePlayingOfHandScore;
            aIGreenPlayer.gameCountingHandScore = savedGameState.aIGameCountingHandScore;
            aIGreenPlayer.gameCountingCribScore = savedGameState.aIGameCountingCribScore;
            //Breakdown
            aIGreenPlayer.gameRightJackFlipped = savedGameState.aIGameRightJackFlipped;
            aIGreenPlayer.gameTotalFifteens = savedGameState.aIGameTotalFifteens;
            aIGreenPlayer.gameTotalPairs = savedGameState.aIGameTotalPairs;
            aIGreenPlayer.gameTotal5Runs = savedGameState.aIGameTotal5Runs;
            aIGreenPlayer.gameTotal4Runs = savedGameState.aIGameTotal4Runs;
            aIGreenPlayer.gameTotal3Runs = savedGameState.aIGameTotal3Runs;
            aIGreenPlayer.gameTotalFlushes = savedGameState.aIGameTotalFlushes;
            aIGreenPlayer.gameRightJack = savedGameState.aIGameRightJack;

            //LOAD AI PLAYER LIFETIME STATS

            aIGreenPlayer.lifetimeStats = savedGameState.aILifetimeStats;

            //LOAD AI PLAYER PLAYERPLAYHANDS STATE;
            aIPlayerPlayCards.togglesOn = savedGameState.aITogglesOn;
            aIPlayerPlayCards.toggles = savedGameState.aIToggles;
            aIPlayerPlayCards.handUIObject = savedGameState.aIHandUIObject;

            aIPlayerPlayCards.playedCardObects = savedGameState.aIPlayedCardObects;
            for (int i = 0; i < aIPlayerPlayCards.playedCardObects.Count; i++)
            {
                aIPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().sprite = savedGameState.aIPlayedCardSprite[i];

                if (savedGameState.aIPlayedCardObectsSpriteRendererActive[i] == true)
                {
                    aIPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled = true;
                }
                if (savedGameState.aIPlayedCardObectsSpriteRendererActive[i] == false)
                {
                    aIPlayerPlayCards.playedCardObects[i].GetComponent<SpriteRenderer>().enabled = false;
                }

            }
            aIPlayerPlayCards.nullSprite = savedGameState.aINullSprite;
            aIPlayerPlayCards.numberCardsPlayed = savedGameState.aINumberCardsPlayed;
            aIPlayerPlayCards.aINumberCardsPlayed = savedGameState.aIAINumberCardsPlayed;
            aIPlayerPlayCards.saidGo = savedGameState.aISaidGo;
            aIPlayerPlayCards.lastToGo = savedGameState.aILastToGo;
            //make sure to set active state.
            aIPlayerPlayCards.goUI = savedGameState.aIGoUI;

            if (savedGameState.aIGoUIActive == true)
            {
                aIPlayerPlayCards.goUI.SetActive(true);
            }
            if (savedGameState.aIGoUIActive == false)
            {
                aIPlayerPlayCards.goUI.SetActive(false);
            }

            aIPlayerPlayCards.goText = savedGameState.aIGoText;
            aIPlayerPlayCards.canPlayCard = savedGameState.aICanPlayCard;
            aIPlayerPlayCards.roundManager = savedGameState.aIPlayerPlayCardsRoundManager;
            aIPlayerPlayCards.playingOfHandManager = savedGameState.aIPlayingOfHandManager;

            aIPlayerPlayCards.lastCardButton = savedGameState.aILastCardButton;

            aIPlayerPlayCards.isMyTurn = savedGameState.aIIsMyTurn;
            aIPlayerPlayCards.lastCardPlayed = savedGameState.aILastCardPlayed;

            for (int i = 0; i < aIPlayerPlayCards.toggles.Count; i++)
            {
                aIPlayerPlayCards.toggles[i].GetComponent<Image>().color = Color.white;
                //Debug.Log("Sprite color set");
            }

            //LOAD AI DISCARD VARIABLES
            aIDiscard.toggles = savedGameState.aIDiscardToggles;
            aIDiscard.toDiscard = savedGameState.aIToDiscard;
            aIDiscard.hasDiscarded = savedGameState.aIHasDiscarded;
            aIDiscard.handObject = savedGameState.aIHandObject;
            aIDiscard.deckObject = savedGameState.aIDeckObject;

            aIDiscard.resumeDiscard();

            aIGreenPlayer.SetmyColor();

            //LOAD UPDATE AI BUTTONS
            aIButtonManager.updateButtons();

            //LOAD CURRENT SCORE UP STATE
            savedScoreUI.players = savedGameState.savedCurrentScoreUIPlayers;
            savedScoreUI.playerText = savedGameState.savedCurrentScoreUIPlayerText;

            savedScoreUI.scoreUIParent = savedGameState.savedCurrentScoreUIScoreUIParent;
            savedScoreUI.scoreUIParent.SetActive(true);

            savedScoreUI.gameOn = savedGameState.savedCurrentScoreUIGameOn;

            savedScoreUI.startGame();

            //ENDS COUNTING STAGE IF BOTH PLAYERS ARE DONE PLAYING


            //ACTIVATE STAGE SPECIFIC THINGS
            if (roundManager.activeStage == RoundManager.Stage.Discard)
            {

            }

            if (roundManager.activeStage == RoundManager.Stage.Cut)
            {
                GameObject.Find("FifthCard").GetComponent<SpriteRenderer>().sprite = gameDeck.cardBackSprite;
                gameDeck.resetAnimation();
                //gameDeck.anim.SetBool("Cutting", true);
                roundManager.startCutStage();
            }

            if (roundManager.activeStage == RoundManager.Stage.PlayingOfHand)
            {
                Debug.Log("Loading Playing of Hand Stage");
                gameDeck.RevealFifthCard();
                gamePlayingOfHand.playCountUIObject.SetActive(true);
                humanPlayerPlayCards.populateTogglesList();
                gamePlayingOfHand.checkPlayRoundOverOnLoad();
                gamePlayingOfHand.checkStageOverOnLoad();

            }
            if (roundManager.activeStage == RoundManager.Stage.CountHands)
            {
                gameDeck.RevealFifthCard();
                //roundManager.updateDonePlaying();

                Debug.Log("HumanGreenPlayer.Active player is " + humanGreenPlayer.activePlayer);
                Debug.Log("Humancountd is " + humanGreenPlayer.counted);
                Debug.Log("humanScored is " + humanGreenPlayer.scored);
                    if(savedGameState.humanActivePlayer == true && savedGameState.humanCounted == true && savedGameState.humanScored == false)
                    {
                        Debug.Log("Counting human player's hand");
                        humanGreenPlayer.counted = false;
                        humanGreenPlayer.handScore = 0;
                        humanGreenPlayer.countHand();
                    }

                Debug.Log("savedgameAICounted is " + savedGameState.aICounted);
                Debug.Log("savedgameAIScored is " + savedGameState.aIScored);
                if (savedGameState.aICounted == true && savedGameState.aIScored == false)
                {
                    Debug.Log("Counting AI player's hand");
                    aIGreenPlayer.counted = false;
                    aIGreenPlayer.handScore = 0;
                    aIGreenPlayer.activePlayer = true;
                    aIGreenPlayer.playerAI.DoAI();
                    //aIGreenPlayer.countHand();
                }



                gameStageCountingHand.checkForEndOfCountingStage();

            }
            if (roundManager.activeStage == RoundManager.Stage.CountCrib)
            {
                gameDeck.RevealFifthCard();
                //roundManager.updateDonePlaying();
                gameStageCountingHand.checkForEndOfCribStage();

            }

            if (roundManager.activeStage == RoundManager.Stage.Summary)
            {
                roundSummary.updateSummaryValues();
                roundSummary.printRoundSummary();
            }

            humanGreenPlayer.setCardImageUIColors();
            Debug.Log("Difficulty is " + aIGreenPlayer.playerAI.aIDifficulty);

            gameCameraAngleManager.startGame();

        }

        else
            Debug.Log("failed to load data");
    }



}

