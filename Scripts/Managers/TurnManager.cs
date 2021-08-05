using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> players;
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

    public void startGame()
    {
       /*
        * for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<GreenPlayer>().yourTurnBorderUI.GetComponent<Image>().color = players[i].GetComponent<GreenPlayer>().myColor;
        }
        */
    }

    public void updatePlayerTurn()
    {
        Debug.Log("Updating Player Turn from Turn Manager Script");
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<GreenPlayer>().activePlayer == true)
            {
                int j = (i + 1) % players.Count;
                //Debug.Log("j = " + j);

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
}
