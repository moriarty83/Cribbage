using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCountingHand : MonoBehaviour
{
    public List<GameObject> players;

    public Deck gameDeck;
    public bool countingStageOver;
    public bool cribStageOver;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void beginStage()
    {
       

        for (int i = 0; i < players.Count; i++)
        {
            //Updates PlayerPlayCards Script
            players[i].GetComponent<PlayerPlayCards>().saidGo = false;
            players[i].GetComponent<PlayerPlayCards>().isMyTurn = false;
            players[i].GetComponent<PlayerPlayCards>().canPlayCard = false;
            players[i].GetComponent<PlayerPlayCards>().lastToGo = false;

            //Updates Greenplayer Script
            players[i].GetComponent<GreenPlayer>().activePlayer = false;
            players[i].GetComponent<GreenPlayer>().donePlaying = false;
            players[i].GetComponent<GreenPlayer>().counted = false;
            players[i].GetComponent<GreenPlayer>().scored = false;

            //Sets Active Player to First Non-Dealer Player
            if(players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                int j = (i + 1) % players.Count;

                players[i].GetComponent<GreenPlayer>().activePlayer = true;

                players[j].GetComponent<GreenPlayer>().activePlayer = true;
                players[j].GetComponent<GreenPlayer>().yourTurnUI.SetActive(true);
                players[i].GetComponent<GreenPlayer>().activePlayer = false;
                players[i].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);

                //Debug.Log("Turn Updated to Player " + j);
                return;
            }
        }
    }

    public void enterCountingStage()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<GreenPlayer>().activePlayer = false;
        }

            for (int i = 0; i < players.Count; i++)
        {

            players[i].GetComponent<PlayerPlayCards>().saidGo = false;
            if (players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                int j = (i + 1) % players.Count;
                //Debug.Log("j = " + j);

                players[j].GetComponent<GreenPlayer>().activePlayer = true;
                players[j].GetComponent<GreenPlayer>().yourTurnUI.SetActive(true);
                players[i].GetComponent<GreenPlayer>().activePlayer = false;
                players[i].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);

                Debug.Log("Turn Updated to Player " + j);
                return;
            }
        }
    }

    public void updatePlayerTurn()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<GreenPlayer>().activePlayer == true)
            {
                int j = (i + 1) % players.Count;
                //Debug.Log("j = " + j);

                if(players[j].GetComponent<GreenPlayer>().donePlaying == true &&
                   GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands)
                {
                    return;
                }

                players[j].GetComponent<GreenPlayer>().activePlayer = true;
                players[j].GetComponent<GreenPlayer>().yourTurnUI.SetActive(true);
                players[i].GetComponent<GreenPlayer>().activePlayer = false;
                players[i].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);

                if (players[j].GetComponent<GreenPlayer>().activePlayer == true && players[j].GetComponent<GreenPlayer>().donePlaying == true)
                {
                    return;
                }

                Debug.Log("Turn Updated to Player " + j);
                return;
            }

        }
    }

    public void enterCountCribStage()
    {

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerPlayCards>().clearPlayedCards();
            if (players[i].GetComponent<GreenPlayer>().isDealer == false)
            {
                players[i].GetComponent<GreenPlayer>().yourTurnUI.SetActive(false);
                players[i].GetComponent<GreenPlayer>().donePlaying = true;
                players[i].GetComponent<GreenPlayer>().counted = true;
                players[i].GetComponent<GreenPlayer>().scored = true;
            }

            if (players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                players[i].GetComponent<GreenPlayer>().activePlayer = true;
                players[i].GetComponent<GreenPlayer>().yourTurnUI.SetActive(true);
                players[i].GetComponent<GreenPlayer>().donePlaying = false;
                players[i].GetComponent<GreenPlayer>().counted = false;
                players[i].GetComponent<GreenPlayer>().scored = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            gameDeck.cribCards[i].SetActive(false);
        }
        GameObject.Find("Deck").GetComponent<Deck>().ActivateCrib();
        GameObject.Find("Deck").GetComponent<Deck>().RevealCrib();
    }

    public void checkForEndOfCountingStage()
    {
        Debug.Log("Checking end of counting stage");
        List<GameObject> donePlayers = new List<GameObject>();
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].GetComponent<GreenPlayer>().donePlaying == true)
            {
                donePlayers.Add(players[i]);
            }

            Debug.Log("donePlayers list is " + donePlayers.Count + " elements long");
            if(donePlayers.Count == players.Count)
            {
                Debug.Log("all players are done playing (Counting of stage)");
                StartCoroutine(WaitToSwithCountingDoneBool());
            }
        } 
    }

    public void checkForEndOfCribStage()
    {
        List<GameObject> donePlayers = new List<GameObject>();
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<GreenPlayer>().donePlaying == false)
            {
                return;
            }

                StartCoroutine(WaitToSwithCribDoneBool());
        }
    }

    IEnumerator WaitToSwithCountingDoneBool()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<GreenPlayer>().activePlayer = false;
        }
        yield return new WaitForSeconds(2);
        countingStageOver = true;   
    }

    public IEnumerator WaitToSwithCribDoneBool()
    {
        Debug.Log("About to end crib");
        yield return new WaitForSeconds(2);
        cribStageOver = true;
    }
}
