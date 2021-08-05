using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public enum Section
    {
        section01, section02, section03, section04, section05
    };

    public Section activeSection;

    public RoundManager gameRoundManager;
    public GreenPlayer humanGreenPlayer;
    public GreenPlayer aIGreenPlayer;
    public InGameMenu inGameMenu;

    public Slider difficultySlider;

    public showLifetimeStats lifetimeStatsScript;

    public GameObject tutorialUI;
    public Text tutorialText;
    public GameObject gotItButton;
    public GameObject nextButton;
    public GameObject backButton;
    public GameObject closeTutorialButton;

    public GameObject tutorialPrompt;

    public GameObject uIBlock;
    public Image stageManagerIcon;
    public Text stageManagerText;
    public Image tutorialStageIcon;
    public Text tutorialStageText;

    public GameObject tutorialMenuButton;
    public Text tutorialMenuButtonText;

    //public GameObject useTutorial;

    //public GameObject tellMeMoreObject;
    //public Text tellMeMoreText;

    public bool tutorialOn;

    public bool tutorialGotIt;

    // Start is called before the first frame update
    void Start()
    {
        humanGreenPlayer.lifetimeStats.loadData(humanGreenPlayer.lifetimeStats, humanGreenPlayer);
        tutorialOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorialOn == false)
        {
            return;
        }

        if(tutorialOn == true && tutorialGotIt == false)
        {
            tutorialStageIcon.sprite = stageManagerIcon.sprite;
            tutorialStageText.text = stageManagerText.text;
            //Debug.Log("tutorial is on and GotIt is false");
            //tutorialUI.SetActive(true);
            if(gameRoundManager.activeStage == RoundManager.Stage.Deal)
            {
                if(activeSection == Section.section01)
                {
                    shuffleAndDealTutorialSection01();
                }

                if (activeSection == Section.section02)
                {
                    shuffleAndDealTutorialSection02();
                }
            }

            if (gameRoundManager.activeStage == RoundManager.Stage.Discard)
            {
                discardTutorial();
            }

            if (gameRoundManager.activeStage == RoundManager.Stage.Cut)
            {
                cutTutorial();
            }

            if (gameRoundManager.activeStage == RoundManager.Stage.PlayingOfHand)
            {
                if (activeSection == Section.section01)
                {
                    playingOfHandTutorial01();
                }
                if (activeSection == Section.section02)
                {
                    playingOfHandTutorial02();
                }
                if (activeSection == Section.section03)
                {
                    playingOfHandTutorial03();
                }
                if (activeSection == Section.section04)
                {
                    playingOfHandTutorial04();
                }
            }

            if (gameRoundManager.activeStage == RoundManager.Stage.CountHands)
            {
                if (activeSection == Section.section01)
                {
                    countingOfHandTutorial01();
                }
                if (activeSection == Section.section02)
                {
                    countingOfHandTutorial02();
                }
                if (activeSection == Section.section03)
                {
                    countingOfHandTutorial03();
                }

            }

            if (gameRoundManager.activeStage == RoundManager.Stage.CountCrib)
            {
                countingOfCribTutorial();
            }

            if (gameRoundManager.activeStage == RoundManager.Stage.Summary)
            {
                roundSummaryTutorial();
            }
        }

        if(tutorialOn == true && tutorialGotIt == true && gameRoundManager.activeStage == RoundManager.Stage.Cut)
        {
            gameRoundManager.startCutStage();
        }
            
    }



    public void startGame()
    {
        Debug.Log("Tutorial Game Start Running");
        Debug.Log("Games played is " + humanGreenPlayer.lifetimeStats.gamesPlayed);
        tutorialOn = true;

        if (difficultySlider.value == 1 && (humanGreenPlayer.lifetimeStats.wins + humanGreenPlayer.lifetimeStats.losses) < 4)
        {
            Debug.Log("Difficulty is " + aIGreenPlayer.playerAI.aIDifficulty + "and Games Played is " + (humanGreenPlayer.lifetimeStats.wins + humanGreenPlayer.lifetimeStats.losses));
            tutorialPrompt.SetActive(true);
            tutorialOn = true;
            
            //StartCoroutine(tutorialPromptFrameEnd());
        }
        else
        { 
            tutorialPrompt.SetActive(false);
            useTutorialNo();
            tutorialOn = false;
        }
    }

    IEnumerator tutorialPromptFrameEnd()
    {
        yield return new WaitForEndOfFrame();
        if (difficultySlider.value == 0 && (humanGreenPlayer.lifetimeStats.wins + humanGreenPlayer.lifetimeStats.losses) < 3)
        {

            Debug.Log("Difficulty is " + aIGreenPlayer.playerAI.aIDifficulty + "and Games Played is " + (humanGreenPlayer.lifetimeStats.wins + humanGreenPlayer.lifetimeStats.losses));
            tutorialPrompt.SetActive(true);
            tutorialOn = true;
        }

    }

    public void tutorialToggle()
    {
        if(tutorialOn == true)
        {
            useTutorialNo();
            return;
        }
        if (tutorialOn == false)
        {
            useTutorialYes();
            inGameMenu.toggleMenu();
            return;
        }
    }

    public void useTutorialPrompt()
    {
        tutorialPrompt.SetActive(true);
    }

    public void useTutorialYes()
    {
        tutorialPrompt.SetActive(false);
        tutorialOn = true;
        tutorialMenuButtonText.text = "Tutorial: On";
        tutorialGotIt = false;
        tutorialPrompt.SetActive(false);
    }

    public void useTutorialNo()
    {
        tutorialPrompt.SetActive(false);
        tutorialOn = false;
        tutorialMenuButtonText.text = "Tutorial: Off";
        tutorialGotIt = false;
        tutorialPrompt.SetActive(false);
        tutorialUI.SetActive(false);
    }

    public void tutorialNextStage()
    {
        tutorialGotIt = false;
        activeSection = Section.section01;
    }

    public void iGotIt()
    {
        tutorialGotIt = true;
        //uIBlock.SetActive(false);
        tutorialUI.SetActive(false);
    }

    public void shuffleAndDealTutorialSection01()
    {
        activeSection = Section.section01;

        uIBlock.SetActive(true);
        nextButton.SetActive(true);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(false);
        backButton.SetActive(false);
        closeTutorialButton.SetActive(false);


            tutorialText.text = "Welcome to Cribbage! \n\n" +
                "Cribbage was invented in 17th Century England by Sir John Suckling\n\n" +

                "Played with a standard deck of 52 cards and the peg board used to keep score, " +
                "the objective is to get 121 points before your opponent\n\n" +

                "The front most peg marks a player's score and when points are scored the rear peg is moved ahead " +
                "to mark the new score.\n\n" +

                "Hit 'Next' to Continue.\n\n" +
                "The 'Tell Me More' button will always " +
                "bring you to the appropriate section of " +
                "WikiPedia's Cribbage Rules";
    }

    public void shuffleAndDealTutorialSection02()
    {
        activeSection = Section.section02;

        uIBlock.SetActive(true);
        nextButton.SetActive(false);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(true);
        backButton.SetActive(true);
        closeTutorialButton.SetActive(false);

        if (humanGreenPlayer.isDealer == true)
        {
            tutorialText.text =
                "To begin, 6 cards are delt to each player.\n\n" +
                "You are the dealer this round and your opponent will deal the next round.\n\n" +
                "You will then take turns dealing " +
                "for each round until somebody wins.\n\n" +
                "Hit 'Got it' to close this window and then the 'Shuffle & Deal' button.\n\n";
        }

        if (humanGreenPlayer.isDealer == false)
        {
            tutorialText.text =
                "To begin, 6 cards are delt to each player.\n\n" +
                "Your opponent is the dealer this round and you will deal the next round.\n\n" +
                "You will then take turns dealing " +
                "for each round until somebody wins.\n\n" +
                "Hit 'Got it' to close this window and your opponent will deal.";
        }
    }

        public void discardTutorial()
    {
        activeSection = Section.section01;

        uIBlock.SetActive(true);
        nextButton.SetActive(false);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(true);
        backButton.SetActive(false);
        closeTutorialButton.SetActive(false);


        if (humanGreenPlayer.isDealer == true)
        {
            tutorialText.text = "Both players pick two cards to discard.\n\n" +
                "As the dealer, all of these cards will become part of your Crib\n\n" +
                "Hit 'Got it' to close this window, then select 2 cards to discard and hit the 'Discard' button.";
        }

        if (humanGreenPlayer.isDealer == false)
        {
            tutorialText.text = "Both players pick two cards to discard.\n\n" +
                "Your opponent is the dealer so all of these cards will become part of their Crib\n\n" +
                "Hit 'Got it' to close this window, then select 2 cards to discard and hit the 'Discard' button.";
        }
    }

    public void cutTutorial()
    {
        activeSection = Section.section01;

        uIBlock.SetActive(true);

        nextButton.SetActive(false);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(true);
        backButton.SetActive(false);
        closeTutorialButton.SetActive(false);

        if (humanGreenPlayer.isDealer == true)
        {
            tutorialText.text = "Now, your opponent will cut the deck and you will turn up the underlying card.\n\n" +
                "This card is called the 'cut' and will become the 5th card in both players' hands and in your Crib.\n\n" +
                "If the 'cut' is a Jack, the dealer gets 2 points.\n\n" +
                "Hit 'Got it' to continue.";
        }

        if (humanGreenPlayer.isDealer == false)
        {
            tutorialText.text = "Now, you will cut the deck and your opponent will turn up the underlying card.\n\n" +
                "This card is called the 'cut' and will become the 5th card in both players' hands and in the dealer's Crib\n\n" +
                "If the 'cut' is a Jack, the dealer gets 2 points.\n\n" +
                "Hit 'Got it' to continue.";
        }
    }

    public void playingOfHandTutorial01()
    {
        activeSection = Section.section01;

        uIBlock.SetActive(true);

        tutorialUI.SetActive(true);
        nextButton.SetActive(true);
        gotItButton.SetActive(false);
        backButton.SetActive(false);
        closeTutorialButton.SetActive(false);

        if (humanGreenPlayer.isDealer == true)
        {
            tutorialText.text = 
                "During the Play, players take turns playing cards and " +
                "adding their values together up to 31. \n\n" +

                "All face cards add 10 to the Count and Aces add 1.\n\n" +

                "Since you are the dealer, your opponent will play first.\n\n" +
                
                "Hit 'Next' to Continue.";
        }
        
        if (humanGreenPlayer.isDealer == false)
        {
            tutorialText.text = 
                "During the Play, players take turns playing cards and " +
                "adding their values together up to 31. \n\n" +

                "All face cards add 10 to the Count and Aces add 1.\n\n" +

                "Since your opponent is the dealer, you will play first.\n\n" +

                "Hit 'Next' to Continue.";
        }
    }

    public void playingOfHandTutorial02()
    {
        activeSection = Section.section02;

        Debug.Log(activeSection);
        uIBlock.SetActive(true);

        tutorialUI.SetActive(true);
        nextButton.SetActive(true);
        gotItButton.SetActive(false);
        backButton.SetActive(true);
        closeTutorialButton.SetActive(false);


        tutorialText.text = "During the Play, it is possible to score points\n\n" +

                "Play a card to land the Play Count on 15 for 2 Points.\n\n" +

                "Play the same card as the one before it, i.e. a Pair, and get 2 Points. 3 of a Kind " +
                "gets 6 Points, 4 of a Kind gets 12 Points.\n\n" +

                "A run of 3 or more cards is worth 1 Point per card. " +
                "The cards in a Run do not have to be in order to score points.\n\n" +

                "Hit 'Next' to Continue.";

    }

    public void playingOfHandTutorial03()
    {
        activeSection = Section.section03;

        uIBlock.SetActive(true);

        tutorialUI.SetActive(true);
        nextButton.SetActive(true);
        //gotItButton.SetActive(false);
        backButton.SetActive(true);
        closeTutorialButton.SetActive(false);

        tutorialText.text = "If a player cannot play a card without the count going over 31 "+
                "they must say 'Go'.\n\n" +
                "The other player continues playing until they also must say 'Go'.\n\n" +

                "The last player to say 'Go' gets 1 Point, or 2 Points if the Count is 31.\n\n" +
                "The Count then resets with the player who first said 'Go' plays. ";
    }

    public void playingOfHandTutorial04()
    {
        activeSection = Section.section04;

        uIBlock.SetActive(true);

        tutorialUI.SetActive(true);
        nextButton.SetActive(false);
        gotItButton.SetActive(true);
        backButton.SetActive(true);
        closeTutorialButton.SetActive(false);

        tutorialText.text = "When both players run out of cards, the player who played the last card " +
            "gets 1 Point, or 2 Points if the Count is 31.\n\n" +
            
            "Hit 'Got it' to continue and on your turn, select a card to play and hit the 'Play Card' button.";
    }

    public void countingOfHandTutorial01()
    {
        activeSection = Section.section01;

        uIBlock.SetActive(true);

        nextButton.SetActive(true);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(false);
        backButton.SetActive(false);
        closeTutorialButton.SetActive(false);

        if (humanGreenPlayer.isDealer == true)
        {
            tutorialText.text = "The Play is over. It is time for The Count.\n\n" +

                "Your opponent will count the points in their hand, as the dealer, you count second.\n\n" +

                "Pairs count for 2 points and so do any combinations of cards that add to 15.\n\n" +

                "Rememeber at 3 of a Kind is actually 3 pairs and 4 of a Kind is 6 Pairs.";

        }

        if (humanGreenPlayer.isDealer == false)
        {
            tutorialText.text =

                "The Play is over. It is time for The Count.\n\n" +

                "You will count the points in your hand first, then the dealer.\n\n" +

                "Pairs count for 2 points and so do any combinations of cards that add to 15.\n\n" +

                "Rememeber at 3 of a Kind is actually 3 pairs and 4 of a Kind is 6 Pairs.";

            //"Hit 'Got it' to continue and 'Count Hand' when it's your turn to count your hand and score points.";
        }
    }

    public void countingOfHandTutorial02()
    {
        activeSection = Section.section02;

        uIBlock.SetActive(true);

        nextButton.SetActive(true);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(false);
        backButton.SetActive(true);
        closeTutorialButton.SetActive(false);

            tutorialText.text =
                "Runs of 3 or more cards count for 1 point per card.\n\n" +

                "It's possible to use some of the same cards to make multiple runs. " +
                "Look out for hands with Pairs as well as Runs; you might have a Double, Triple or even Quandruple Run.\n\n" +

                "Remember, Aces always count as 1 when making a Run or a Fifteen.";
    }


    public void countingOfHandTutorial03()
    {
        activeSection = Section.section03;

        uIBlock.SetActive(true);

        nextButton.SetActive(false);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(true);
        backButton.SetActive(true);
        closeTutorialButton.SetActive(false);

        tutorialText.text =
                "A flush of the 4 cards in your hand is 4 points.\n\n" +

                "A flush that includes the 'Cut' card is 5 points\n\n" +

                "A Jack in your hand that matches the suit of the Cut is worth 1 point.\n\n" +

                "Hit 'Got it' to continue and 'Count Hand' to count and score the points in your hand";
    }

    public void countingOfCribTutorial()
    {
        activeSection = Section.section01;

        uIBlock.SetActive(true);

        nextButton.SetActive(false);
        tutorialUI.SetActive(true);
        gotItButton.SetActive(true);
        backButton.SetActive(true);
        closeTutorialButton.SetActive(false);

        if (humanGreenPlayer.isDealer == true)
        {
            tutorialText.text = "As the dealer, you get the Crib. The Crib is a bonus hand made up of" +
                "the cards that were discarded earlier in the round, and the cut/fifth card.\n\n" +
                "Crib points are counted the same way they are for the hand. After the Crib is counted" +
                "the round is over\n\n" +
                "Hit 'Got it' to continue and 'Count Crib' to count your Crib and score points.";
        }
        if (humanGreenPlayer.isDealer == false)
        {
            tutorialText.text = "Your opponent is the dealer and will get the Crib. The Crib is bonus hand made up of" +
                "the cards that were discarded earlier in the round, and the Cut/fifth card.\n\n" +
                "Crib points are counted the same way they are for the hand. After the Crib is counted" +
                "the round is over\n\n" +
                "Hit 'Got it' to continue and your Opponent will count their Crib.";
        }
    }

    public void roundSummaryTutorial()
    {
        activeSection = Section.section01;

        tutorialUI.SetActive(true);
        uIBlock.SetActive(true);
        nextButton.SetActive(false);
        gotItButton.SetActive(true);
        backButton.SetActive(false);
        closeTutorialButton.SetActive(true);

        if (humanGreenPlayer.isDealer == true)
        {
            tutorialText.text = "You've completed the round.\n\n" +
                "The Round Summary will give you an overview of how you and your opponent did.\n\n" +
                "A new round begins with your opponent as the dealer so they will get the points in the Crib instead of you. You'll keep" +
                "playing this way, taking turns as dealer, until somebody has achieved 121 Points or more points\n\n" +
                "Hit 'Got it' to keep the tutorial on for the next round or 'Close Tutorial' to turn it off.";
        }

        if (humanGreenPlayer.isDealer == false)
        {
            tutorialText.text = "You've completed the round.\n\n" +
                "The Round Summary will give you an overview of how you and your opponent did.\n\n" +
                "A new round begins with you as the dealer so you'll will get the points in the Crib instead of your opponent. You'll keep" +
                "playing this way, taking turns as dealer, until somebody has achieved 121 Points or more points\n\n" +
                "Hit 'Got it' to keep the tutorial on for the next round or 'Close Tutorial' to turn it off.";
        }
    }

    public void tutorialNextSection()
    {
        if (gameRoundManager.activeStage == RoundManager.Stage.Deal)
        {
            if (activeSection == Section.section01)
            {
                activeSection = Section.section02;
                return;
            }
        }
        if (gameRoundManager.activeStage == RoundManager.Stage.PlayingOfHand)
        {
            if (activeSection == Section.section01)
            {
                activeSection = Section.section02;
                return;
            }
            if (activeSection == Section.section02)
            {
                activeSection = Section.section03;
                return;
            }
            if (activeSection == Section.section03)
            {
                activeSection = Section.section04;
                return;
            }
        }

        if (gameRoundManager.activeStage == RoundManager.Stage.CountHands)
        {
            if (activeSection == Section.section01)
            {
                activeSection = Section.section02;
                return;
            }
            if (activeSection == Section.section02)
            {
                activeSection = Section.section03;
                return;
            }
        }
    }

    public void tutorialPreviousSection()
    {
        if (gameRoundManager.activeStage == RoundManager.Stage.Deal)
        {
            if (activeSection == Section.section02)
            {
                activeSection = Section.section01;
                return;
            }
        }
        if (gameRoundManager.activeStage == RoundManager.Stage.PlayingOfHand)
        {
            if (activeSection == Section.section04)
            {
                activeSection = Section.section03;
                return;
            }
            if (activeSection == Section.section03)
            {
                activeSection = Section.section02;
                return;
            }
            if (activeSection == Section.section02)
            {
                activeSection = Section.section01;
                return;
            }
        }

        if (gameRoundManager.activeStage == RoundManager.Stage.CountHands)
        {
            if (activeSection == Section.section03)
            {
                activeSection = Section.section02;
                return;
            }
            if (activeSection == Section.section02)
            {
                activeSection = Section.section01;
                return;
            }
        }
    }

    public void tellMeMore()
    {
        if (gameRoundManager.activeStage == RoundManager.Stage.Deal)
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Rules_of_cribbage#The_deal");
        }

        if (gameRoundManager.activeStage == RoundManager.Stage.Discard)
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Rules_of_cribbage#The_crib");
        }

        if (gameRoundManager.activeStage == RoundManager.Stage.Cut)
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Rules_of_cribbage#The_starter");
        }

        if (gameRoundManager.activeStage == RoundManager.Stage.PlayingOfHand)
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Rules_of_cribbage#The_play");
        }

        if (gameRoundManager.activeStage == RoundManager.Stage.CountHands)
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Rules_of_cribbage#The_show");
        }

        if (gameRoundManager.activeStage == RoundManager.Stage.CountCrib)
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Rules_of_cribbage#The_show");
        }

    }

}
