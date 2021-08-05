using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerUI : MonoBehaviour
{
    private Image dealerImage;
    private Text dealerText;
    public List<GameObject> players;
    

    // Start is called before the first frame update
    void Start()
    {
        dealerImage = this.gameObject.GetComponent<Image>();
        dealerText = this.GetComponentInChildren<Text>();

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].GetComponent<GreenPlayer>().isDealer == true)
            {
                if (players[i].GetComponent<GreenPlayer>().playerColor == "Green")
                {
                    dealerImage.color = new Color32(0, 155, 0, 255);
                }

                if (players[i].GetComponent<GreenPlayer>().playerColor == "Red")
                {
                    dealerImage.color = Color.red;
                }

                if (players[i].GetComponent<GreenPlayer>().playerColor == "Blue")
                {
                    dealerImage.color = Color.blue;
                }

                if(players[i].GetComponent<GreenPlayer>().isAIPlayer == true)
                {
                    dealerText.text = "Their \n" +
                        "Crib";
                }

                if (players[i].GetComponent<GreenPlayer>().isAIPlayer == false)
                {
                    dealerText.text = "Your \n" +
                        "Crib";
                }
            }
        }
    }
}
