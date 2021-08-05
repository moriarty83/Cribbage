using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public GameObject[] players = new GameObject[2];
    public GameObject[] playerText = new GameObject[2];

    public GameObject scoreUIParent;

    public bool gameOn;


    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        scoreUIParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOn == true)
        {
            for (int i = 0; i < players.Length; i++)
            {
                playerText[i].GetComponent<Text>().text = players[i].GetComponent<GreenPlayer>().totalScore.ToString();
            }
        }
    }

    public void startGame()
    {
        gameOn = true;
        scoreUIParent.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {
            playerText[i].GetComponent<Text>().color = players[i].GetComponent<GreenPlayer>().myColor;
        }
    }

    public void endGame()
    {
        gameOn = false;
        scoreUIParent.SetActive(false);
    }
}
