using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BasicAI
{
    
    GameObject me;

    RoundManager roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();

    public enum difficulty {easy, medium, hard };

    public difficulty aIDifficulty;


    public void findme()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.GetComponent<GreenPlayer>().isAIPlayer == true)
            {
                me = player;
            }
        }
    }

    public BasicAI()
    {
    }

    virtual public void DoAI()
    {
        Debug.Log("Doing AI");
        if(roundManager.activeStage == RoundManager.Stage.Deal &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            GameObject.Find("Deck").GetComponent<Deck>().shuffleAndDeal();
        }
        if (roundManager.activeStage == RoundManager.Stage.Discard &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            
            AIDiscard();
        }

        if (roundManager.activeStage == RoundManager.Stage.PlayingOfHand &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            AIPlayCard();
        }

        if (roundManager.activeStage == RoundManager.Stage.CountHands &&
            me.GetComponent<GreenPlayer>().activePlayer == true &&
            me.GetComponent<GreenPlayer>().counted == false)
        {
            Debug.Log("AI Counting Hand");
            AICountHand();
            Debug.Log("AI Done Counting Hand");
        }

        if (roundManager.activeStage == RoundManager.Stage.CountHands &&
        me.GetComponent<GreenPlayer>().activePlayer == true &&
        me.GetComponent<GreenPlayer>().counted == true)
        {
            Debug.Log("AI Score Hand Points");
            AIScorepoints();
            Debug.Log("AI Done Scoreing Hand Points");
        }

        if (roundManager.activeStage == RoundManager.Stage.CountCrib &&
            me.GetComponent<GreenPlayer>().activePlayer == true &&
            me.GetComponent<GreenPlayer>().counted == false) 
        {
            AICountCrib();
        }

        if(roundManager.activeStage == RoundManager.Stage.Summary &&
            me.GetComponent<GreenPlayer>().donePlaying == false)
        {
            AIRoundSummary();
        }
        me.GetComponent<GreenPlayer>().aIRunning = false;
        me.GetComponent<GreenPlayer>().turnOffSpeechBubble();
    }

    IEnumerator delayedDoAI()
    {
        yield return new WaitForSeconds(3);
        if (roundManager.activeStage == RoundManager.Stage.Deal &&
        me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            GameObject.Find("Deck").GetComponent<Deck>().shuffleAndDeal();
        }
        if (roundManager.activeStage == RoundManager.Stage.Discard &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            AIDiscard();
        }

        if (roundManager.activeStage == RoundManager.Stage.PlayingOfHand &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            AIPlayCard();
        }

        if (roundManager.activeStage == RoundManager.Stage.CountHands &&
            me.GetComponent<GreenPlayer>().activePlayer == true &&
            me.GetComponent<GreenPlayer>().counted == false && me.GetComponent<GreenPlayer>().aIProceed == false)
        {
            Debug.Log("AI Counting Hand");
            AICountHand();
            Debug.Log("AI Done Counting Hand");
        }

        if (roundManager.activeStage == RoundManager.Stage.CountHands &&
        me.GetComponent<GreenPlayer>().activePlayer == true &&
        me.GetComponent<GreenPlayer>().counted == true && me.GetComponent<GreenPlayer>().aIProceed == true)
        {
            Debug.Log("AI Counting Hand");
            AIScorepoints();
            Debug.Log("AI Done Counting Hand");
        }

        if (roundManager.activeStage == RoundManager.Stage.CountCrib &&
            me.GetComponent<GreenPlayer>().donePlaying == false && me.GetComponent<GreenPlayer>().aIProceed == false)
        {
            AICountCrib();
        }

        if (roundManager.activeStage == RoundManager.Stage.CountCrib &&
        me.GetComponent<GreenPlayer>().donePlaying == true && me.GetComponent<GreenPlayer>().aIProceed == true)
        {
            AIScorepoints();
        }

        if (roundManager.activeStage == RoundManager.Stage.Summary &&
            me.GetComponent<GreenPlayer>().donePlaying == false)
        {
            AIRoundSummary();
        }
    }

    virtual protected void AIDiscard()
    {
        Debug.Log("Runing AIDiscard");
        if (aIDifficulty == difficulty.easy)
        {
            me.GetComponent<Discard>().AISelectToDiscard();
        }
        if (aIDifficulty == difficulty.medium)
        {
            Debug.Log("Medium discard performed");
            me.GetComponent<Discard>().AIMediumSelectToDiscard();
        }
        if (aIDifficulty == difficulty.hard)
        {
            Debug.Log("Hard discard performed");
            me.GetComponent<Discard>().AIHardSelectToDiscard();
        }
        me.GetComponent<Discard>().DiscardToCrib();
        me.GetComponent<GreenPlayer>().UpdateHand();
    }

    virtual protected void AIPlayCard()
    {
        Debug.Log("Running AIPlaycard");
        if (aIDifficulty == difficulty.easy)
        {
            me.GetComponent<PlayerPlayCards>().AIPlayCard();
        }
        if (aIDifficulty == difficulty.medium)
        {
            me.GetComponent<PlayerPlayCards>().aIMediumPlayCard();
        }
        if (aIDifficulty == difficulty.hard)
        {
            me.GetComponent<PlayerPlayCards>().aIHardPlayCard();
        }
    }

    virtual protected void AICountHand()
    {
        Debug.Log("Running AICountHand");
        
        me.GetComponent<GreenPlayer>().countHand();
        me.GetComponent<GreenPlayer>().activePlayer = false;
    }

    virtual protected void AICountCrib()
    {
        Debug.Log("Running AICountCrib");
        me.GetComponent<GreenPlayer>().countCrib();
    }

    virtual protected void AIScorepoints()
    {
        Debug.Log("Running AICountHand");
        me.GetComponent<GreenPlayer>().scorePoints();
    }

    virtual protected void AIRoundSummary()
    {
        Debug.Log("Running AISummary");
        me.GetComponent<GreenPlayer>().isDonePlaying();
        me.GetComponent<GreenPlayer>().roundSummaryScript.SummaryStageEndCheck();
    }

    virtual public void showAISpeechBubble()
    {
        Debug.Log("Showing Speech Bubble");
        if (roundManager.activeStage == RoundManager.Stage.Deal &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            me.GetComponent<GreenPlayer>().speechBubble.SetActive(true);
            me.GetComponent<GreenPlayer>().speechBubbleText.GetComponent<Text>().text = "Dealing";
        }
        if (roundManager.activeStage == RoundManager.Stage.Discard &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            me.GetComponent<GreenPlayer>().speechBubble.SetActive(true);
            me.GetComponent<GreenPlayer>().speechBubbleText.GetComponent<Text>().text = "Discarding";
        }

        if (roundManager.activeStage == RoundManager.Stage.PlayingOfHand &&
            me.GetComponent<GreenPlayer>().activePlayer == true)
        {
            me.GetComponent<GreenPlayer>().speechBubble.SetActive(true);
            me.GetComponent<GreenPlayer>().speechBubbleText.GetComponent<Text>().text = "My Turn";
        }

        if (roundManager.activeStage == RoundManager.Stage.CountHands &&
            me.GetComponent<GreenPlayer>().activePlayer == true &&
            me.GetComponent<GreenPlayer>().counted == false)
        {
            me.GetComponent<GreenPlayer>().speechBubble.SetActive(true);
            me.GetComponent<GreenPlayer>().speechBubbleText.GetComponent<Text>().text = "Counting My Hand";
        }

        if (roundManager.activeStage == RoundManager.Stage.CountCrib &&
            me.GetComponent<GreenPlayer>().activePlayer == true &&
            me.GetComponent<GreenPlayer>().counted == false)
        {
            me.GetComponent<GreenPlayer>().speechBubble.SetActive(true);
            me.GetComponent<GreenPlayer>().speechBubbleText.GetComponent<Text>().text = "Counting My Crib";
        }

        if (roundManager.activeStage == RoundManager.Stage.Summary &&
            me.GetComponent<GreenPlayer>().donePlaying == false)
        {
            return;
        }
        //me.GetComponent<GreenPlayer>().turnOffSpeechBubble();
    }
}

