using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{

    public List<int> playersNumber;
    //public List<Camera> playerCameras;
    public List<GameObject> Players;

    public int activePlayer;
    public List<bool> donePlaying;

    

    // Start is called before the first frame update
    void Awake()
    {


        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Players.Add(player);
        }

        for (int i = 0; i < Players.Count; i++)
        {
            //playerCameras.Add(Players[i].GetComponentInChildren<Camera>());
            Players[i].GetComponent<GreenPlayer>().playerNumber = i + 1;
        }        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextPlayer()
    {

        for (int i = 0; i < Players.Count; i++)
        {
            if(Players[i].GetComponent<GreenPlayer>().activePlayer == true)
            {
                if (i + 1 < Players.Count)
                {
                    Players[i + 1].GetComponent<GreenPlayer>().activePlayer = true;
                    Players[i].GetComponent<GreenPlayer>().activePlayer = false;
                    //playerCameras[i+1].SetActive(true);
                    return;
                }

            if (i + 1 >= Players.Count)
                {
                    Players[0].GetComponent<GreenPlayer>().activePlayer = true;
                    Players[i].GetComponent<GreenPlayer>().activePlayer = false;
                    //playerCameras[0].SetActive(true);
                    return;
                }
            }
        }
    }

    public void newRound()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<GreenPlayer>().donePlaying = false;
            Players[i].GetComponent<GreenPlayer>().activePlayer = false;
        }

        Players[0].GetComponent<GreenPlayer>().activePlayer = true;
    }
}
