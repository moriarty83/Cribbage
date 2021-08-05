using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showLifetimeStats : MonoBehaviour
{
    public GreenPlayer playerGreenPlayer;
    public SaveData lifetimeStats;

    public GameObject lifetimeStatsEraseConfirmObjectInGame;
    public GameObject lifetimeStatsEraseConfirmObjectInMenu;

    public GameObject lifetimeStatsUIInGame;
    public Text lifetimeWinLossTextInGame;
    public Text lifetimeStatsTextInGame;

    public GameObject lifetimeStatsUIInMenu;
    public Text lifetimeWinLossTextInMenu;
    public Text lifetimeStatsTextInMenu; 

    public float winPercentage;

    private void Awake()
    {
        lifetimeStats = playerGreenPlayer.lifetimeStats;
    }

    public void printLifeTimeStatsInGame()
    {
        if(playerGreenPlayer.lifetimeStats.wins + playerGreenPlayer.lifetimeStats.losses > 0)
        {
            Debug.Log("More than 0 games played");
            Debug.Log("Wins = " + playerGreenPlayer.lifetimeStats.wins);
            Debug.Log("Losses = " + playerGreenPlayer.lifetimeStats.losses);

            winPercentage = (100 * ((float)playerGreenPlayer.lifetimeStats.wins / ((float)playerGreenPlayer.lifetimeStats.wins + (float)playerGreenPlayer.lifetimeStats.losses)));
            //100 * (playerGreenPlayer.lifetimeStats.wins / (playerGreenPlayer.lifetimeStats.wins + playerGreenPlayer.lifetimeStats.losses));

            Debug.Log("Win Percentage is " + winPercentage);
        }

        if (playerGreenPlayer.lifetimeStats.wins + playerGreenPlayer.lifetimeStats.losses == 0)
            if (playerGreenPlayer.lifetimeStats.gamesPlayed > 0)
            {
                Debug.Log("Games plaed is zero");

                winPercentage = 0;
            }

        lifetimeStatsUIInGame.SetActive(true);
        lifetimeWinLossTextInGame.text = "\n" + 
            "Games Played: " + (playerGreenPlayer.lifetimeStats.wins + playerGreenPlayer.lifetimeStats.losses) +
            "\n\nLifetime Wins: " + playerGreenPlayer.lifetimeStats.wins +
            "\nLifetime Losses: " + playerGreenPlayer.lifetimeStats.losses +
            "\nWin Percentage: " + winPercentage.ToString("F0") + "%";

        lifetimeStatsTextInGame.text = "\n" +
            "Lifetime Points: " + playerGreenPlayer.lifetimeStats.lifetimePoints +
            "\n\nLifetime Hand Points: " + playerGreenPlayer.lifetimeStats.lifetimeHand +
            "\nLifetime Crib Points: " + playerGreenPlayer.lifetimeStats.lifetimeCrib +
            "\nLifetime Playing Points: " + playerGreenPlayer.lifetimeStats.lifetimePlaying +

            "\n\nBiggest Hand: " + playerGreenPlayer.lifetimeStats.biggestHand +
            "\nBiggest Crib: " + playerGreenPlayer.lifetimeStats.biggestCrib +
            "\nBiggest Playing of Hand: " + playerGreenPlayer.lifetimeStats.biggestPlaying;

    }

    public void hideLifetimeStatsInGame()
    {
        lifetimeStatsUIInGame.SetActive(false);
    }

    public void resetLifetimeStatsInGame()
    {
        winPercentage = 0;
        lifetimeStats.resetStats(lifetimeStats, playerGreenPlayer);
        lifetimeStats.saveData(lifetimeStats, playerGreenPlayer);
        StartCoroutine(reprintLifetimeStatsInGame());
        StartCoroutine(reprintLifetimeStatsInMenu());

    }

    public void confirmResetLifetimeStatsUIInGame()
    {
        lifetimeStatsEraseConfirmObjectInGame.SetActive(true);
    }

    public void cancelEraseLifetimeStatsInGame()
    {
        lifetimeStatsEraseConfirmObjectInGame.SetActive(false);
    }

    IEnumerator reprintLifetimeStatsInGame()
    {
        yield return new WaitForEndOfFrame();
        lifetimeStats.loadData(lifetimeStats, playerGreenPlayer);
        printLifeTimeStatsInGame();
        lifetimeStatsEraseConfirmObjectInGame.SetActive(false);

    }



    public void printLifeTimeStatsInMenu()
    {
        if (playerGreenPlayer.lifetimeStats.wins + playerGreenPlayer.lifetimeStats.losses > 0)
        {
            Debug.Log("More than 0 games played");
            Debug.Log("Wins = " + playerGreenPlayer.lifetimeStats.wins);
            Debug.Log("Losses = " + playerGreenPlayer.lifetimeStats.losses);

            winPercentage = (100 * ((float)playerGreenPlayer.lifetimeStats.wins / ((float)playerGreenPlayer.lifetimeStats.wins + (float)playerGreenPlayer.lifetimeStats.losses)));

            Debug.Log("Win Percentage is " + winPercentage);
        }

        if (playerGreenPlayer.lifetimeStats.wins + playerGreenPlayer.lifetimeStats.losses == 0)
            if (playerGreenPlayer.lifetimeStats.gamesPlayed > 0)
            {
                Debug.Log("Games plaed is zero");
                winPercentage = 0;
            }


        lifetimeStatsUIInMenu.SetActive(true);
        lifetimeWinLossTextInMenu.text = "\n" +
            "Games Played: " + (playerGreenPlayer.lifetimeStats.wins + playerGreenPlayer.lifetimeStats.losses) +
            "\n\nLifetime Wins: " + playerGreenPlayer.lifetimeStats.wins +
            "\nLifetime Losses: " + playerGreenPlayer.lifetimeStats.losses +
            "\nWin Percentage: " + winPercentage.ToString("F0") + "%";

        lifetimeStatsTextInMenu.text = "\n" +
            "Lifetime Points: " + playerGreenPlayer.lifetimeStats.lifetimePoints +
            "\n\nLifetime Hand Points: " + playerGreenPlayer.lifetimeStats.lifetimeHand +
            "\nLifetime Crib Points: " + playerGreenPlayer.lifetimeStats.lifetimeCrib +
            "\nLifetime Playing Points: " + playerGreenPlayer.lifetimeStats.lifetimePlaying +

            "\n\nBiggest Hand: " + playerGreenPlayer.lifetimeStats.biggestHand +
            "\nBiggest Crib: " + playerGreenPlayer.lifetimeStats.biggestCrib +
            "\nBiggest Playing of Hand: " + playerGreenPlayer.lifetimeStats.biggestPlaying;

    }

    public void hideLifetimeStatsInMenu()
    {
        lifetimeStatsUIInMenu.SetActive(false);
    }

    public void resetLifetimeStatsInMenu()
    {
        Debug.Log("Reset Lifetime Stats Confirmed");
        lifetimeStats.resetStats(lifetimeStats, playerGreenPlayer);
        lifetimeStats.saveData(lifetimeStats, playerGreenPlayer);
        winPercentage = 0;
        StartCoroutine(reprintLifetimeStatsInMenu());

    }

    public void confirmResetLifetimeStatsUIInMenu()
    {
        lifetimeStatsEraseConfirmObjectInMenu.SetActive(true);
    }

    public void cancelEraseLifetimeStatsInMenu()
    {
        lifetimeStatsEraseConfirmObjectInMenu.SetActive(false);
    }

    IEnumerator reprintLifetimeStatsInMenu()
    {
        yield return new WaitForEndOfFrame();
        lifetimeStats.loadData(lifetimeStats, playerGreenPlayer);
        printLifeTimeStatsInMenu();
        lifetimeStatsEraseConfirmObjectInMenu.SetActive(false);

    }
}
