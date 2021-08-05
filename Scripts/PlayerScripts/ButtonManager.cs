using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    //public int numberOfPlayers;

    public List<GameObject> players;

    public RoundManager currentStage;

    public List<GameObject> UIButtons;

    public List<bool> donePlaying;

    public bool lastCardPlayed;

    RoundManager round;

    private void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player);

        }
    }

    private void Update()
    {
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage != RoundManager.Stage.PlayingOfHand && GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage != RoundManager.Stage.CountHands)
        {
            return;
        }
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.PlayingOfHand)
        {
            if (this.GetComponent<GreenPlayer>().activePlayer == true)
            {
                if (this.GetComponent<PlayerPlayCards>().lastCardPlayed == true && this.GetComponent<GreenPlayer>().donePlaying == false)
                {
                    for (int i = 0; i < UIButtons.Count; i++)
                    {
                        UIButtons[i].SetActive(false);
                    }
                    //UIButtons[0].SetActive(true);
                    UIButtons[10].SetActive(true);
                }

                else
                if (this.GetComponent<PlayerPlayCards>().lastCardPlayed == true && this.GetComponent<GreenPlayer>().donePlaying == true || GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().playCount == 31)
                {
                    for (int i = 0; i < UIButtons.Count; i++)
                    {
                        UIButtons[i].SetActive(false);
                    }

                }
                else
                if (this.GetComponent<PlayerPlayCards>().lastCardPlayed == false)
                {
                    if (this.GetComponent<PlayerPlayCards>().canPlayACard() == true)
                    {
                        for (int i = 0; i < UIButtons.Count; i++)
                        {
                            UIButtons[i].SetActive(false);
                        }
                        //UIButtons[0].SetActive(true);
                        UIButtons[4].SetActive(true);
                    }
                    if (this.GetComponent<PlayerPlayCards>().canPlayACard() == false && this.GetComponent<PlayerPlayCards>().saidGo == false)
                    {
                        for (int i = 0; i < UIButtons.Count; i++)
                        {
                            UIButtons[i].SetActive(false);
                        }
                        //UIButtons[0].SetActive(true);
                        UIButtons[5].SetActive(true);
                    }
                    if(this.GetComponent<PlayerPlayCards>().saidGo == true)
                    {
                        for (int i = 0; i < UIButtons.Count; i++)
                        {
                            UIButtons[i].SetActive(false);
                        }
                    }
                }
            }

            else
                for (int i = 0; i < UIButtons.Count; i++)
                {
                    UIButtons[i].SetActive(false);
                }
        }
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands)
        {
            if(this.GetComponent<GreenPlayer>().activePlayer == true && this.GetComponent<GreenPlayer>().donePlaying == false)
            {
                //Debug.Log("Count hands stage and I'm the active player, showing buttons");


                if (this.GetComponent<GreenPlayer>().counted == false)
                {
                    for (int i = 0; i < UIButtons.Count; i++)
                    {
                        UIButtons[i].SetActive(false);
                    }
                    //UIButtons[0].SetActive(true);
                    UIButtons[6].SetActive(true);
                    //UIButtons[7].SetActive(true);
                }

                if (this.GetComponent<GreenPlayer>().counted == true && this.GetComponent<GreenPlayer>().scored == false)
                {
                    for (int i = 0; i < UIButtons.Count; i++)
                    {
                        UIButtons[i].SetActive(false);
                    }
                    //UIButtons[0].SetActive(true);
                    //UIButtons[6].SetActive(true);
                    UIButtons[7].SetActive(true);
                }

                if (this.GetComponent<GreenPlayer>().counted == true && this.GetComponent<GreenPlayer>().scored == true)
                {
                    for (int i = 0; i < UIButtons.Count; i++)
                    {
                        UIButtons[i].SetActive(false);
                    }
                    //UIButtons[0].SetActive(true);
                    //UIButtons[6].SetActive(true);
                    //UIButtons[7].SetActive(true);
                }
            }

            else
                for (int i = 0; i < UIButtons.Count; i++)
                {
                    UIButtons[i].SetActive(false);
                }
        }

        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.GameOver)
        {
            for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
            }
        }
    }

    #region SetStage

    public void updateButtons()
    {
        //Debug.Log("Buttons Updating");
        if(this.GetComponent<GreenPlayer>().isAIPlayer == true)
        {
            for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
            }
            if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Deal)
            {

                this.GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);
            }
            if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Discard)
            {

                this.GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);
            }
            return;
        }
        //GetActiveStage();
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Deal)
        {

            for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
            }

            this.GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);
            //UIButtons[0].SetActive(true);
            if (this.GetComponent<GreenPlayer>().isDealer == true)
            {
                UIButtons[1].SetActive(true);
            }
        }

        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Discard)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<Discard>().enterDiscartStage();
                //Debug.Log("Discard Entered");
            }

            this.GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);

            for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
            }
            //UIButtons[0].SetActive(true);
            UIButtons[2].SetActive(true);
            //Debug.Log("Stage 2 Active");
        }
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Cut)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<Discard>().removeListeners();
            }

            for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
            }
            //UIButtons[0].SetActive(true);
            //UIButtons[3].SetActive(true);
        }

        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.PlayingOfHand)
        {

            //Section moved to PlayerPlayCards Script
            //GameObject.Find("RoundManager").GetComponent<PlayingOfHand>().populateTogglesList();
            for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
            }
            //UIButtons[0].SetActive(true);
            //UIButtons[4].SetActive(true);
            //UIButtons[5].SetActive(true);
        }

        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands)
        {
            /*for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
            }
            //UIButtons[0].SetActive(true);
            UIButtons[6].SetActive(true);
            //UIButtons[7].SetActive(true);*/
        }

        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountCrib)
        {
            if (this.GetComponent<GreenPlayer>().isDealer == false)
            {
                for (int i = 0; i < UIButtons.Count; i++)
                {
                    UIButtons[i].SetActive(false);
                    //Debug.Log("Buttons deactivated");
                }
                return;
            }
            if (this.GetComponent<GreenPlayer>().isDealer == true && this.GetComponent<GreenPlayer>().counted == false)
            {

                for (int i = 0; i < UIButtons.Count; i++)
                {
                    UIButtons[i].SetActive(false);
                }
                //UIButtons[0].SetActive(true);
                if (this.GetComponent<GreenPlayer>().isDealer == true)
                {
                    //UIButtons[7].SetActive(true);
                    UIButtons[8].SetActive(true);
                    //UIButtons[9].SetActive(true);
                }

            }

            if (this.GetComponent<GreenPlayer>().isDealer == true && this.GetComponent<GreenPlayer>().counted == true)
            {

                for (int i = 0; i < UIButtons.Count; i++)
                {
                    UIButtons[i].SetActive(false);
                }
                //UIButtons[0].SetActive(true);
                if (this.GetComponent<GreenPlayer>().isDealer == true)
                {
                    UIButtons[7].SetActive(true);
                    //UIButtons[8].SetActive(true);
                    //UIButtons[9].SetActive(true);
                }

            }

        }

        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.Summary)
        {
            for (int i = 0; i < UIButtons.Count; i++)
            {
                UIButtons[i].SetActive(false);
                UIButtons[9].SetActive(true);
            }
        }

    }


    public void DeactivateButtons()
    {
        for (int i = 0; i < UIButtons.Count; i++)
        {
            UIButtons[i].SetActive(false);
        }
    }

}
    
#endregion