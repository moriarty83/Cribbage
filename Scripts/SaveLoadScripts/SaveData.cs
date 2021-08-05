using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]

public class SaveData
{
    public int gamesPlayed;
    public int wins;
    public int losses;

    public int biggestHand;
    public int biggestCrib;
    public int biggestPlaying;

    public int lifetimeHand;
    public int lifetimeCrib;
    public int lifetimePlaying;

    public int lifetimePoints;

    public int gameInProgress;

    public void updateWinLoss(GameObject player)
        {
        GreenPlayer playerScript = player.GetComponent<GreenPlayer>();
        if(playerScript.isAIPlayer == true)
        {
            return;
        }
        if (playerScript.isAIPlayer == false)
        {
            Debug.Log("adding win/loss");
            if(playerScript.totalScore > 120)
            {
                wins += gameInProgress;
                gameInProgress = 0;
                playerScript.lifetimeStats.saveData(playerScript.lifetimeStats, playerScript);
                return;
            }
            if (playerScript.totalScore <= 120)
            {
                losses += gameInProgress;
                gameInProgress = 0;
                playerScript.lifetimeStats.saveData(playerScript.lifetimeStats, playerScript);
                return;
            }
        }
    }

    public void saveData(SaveData data, GreenPlayer player)
    {
        if(player.isAIPlayer == true)
        {
            return;
        }
        string json = JsonUtility.ToJson(data);
#if UNITY_EDITOR_OSX
        File.WriteAllText(Application.dataPath + "/lifetimeStats.txt", json);
#endif
#if UNITY_STANDALONE_OSX
        File.WriteAllText(Application.dataPath + "/lifetimeStats.txt", json);
#endif
#if UNITY_STANDALONE_WIN
        File.WriteAllText(Application.dataPath + "/lifetimeStats.txt", json);
#endif
#if UNITY_IOS
        File.WriteAllText(Application.persistentDataPath + "/lifetimeStats.txt", json);
#endif
#if UNITY_ANDROID
        File.WriteAllText(Application.persistentDataPath + "/lifetimeStats.txt", json);
#endif
        Debug.Log("Saved");
    }

    public void loadData(SaveData data, GreenPlayer player)
    {
        if(player.isAIPlayer == true)
        {
            return;
        }
        Debug.Log("trying to load data");
#if UNITY_EDITOR_OSX
        if (File.Exists(Application.dataPath + "/lifetimeStats.txt"))
#endif
#if UNITY_STANDALONE_OSX
            if (File.Exists(Application.dataPath + "/lifetimeStats.txt"))
#endif
#if UNITY_STANDALONE_WIN
                if (File.Exists(Application.dataPath + "/lifetimeStats.txt"))
#endif
#if UNITY_IOS
                if (File.Exists(Application.persistentDataPath + "/lifetimeStats.txt"))
#endif
#if UNITY_ANDROID
                if (File.Exists(Application.persistentDataPath + "/lifetimeStats.txt"))
#endif
            {
                Debug.Log("Data Loading");

#if UNITY_STANDALONE_OSX
                string saveString = File.ReadAllText(Application.dataPath + "/lifetimeStats.txt");
#endif
#if UNITY_STANDALONE_WIN
                string saveString = File.ReadAllText(Application.dataPath + "/lifetimeStats.txt");
#endif
#if UNITY_IOS
                string saveString = File.ReadAllText(Application.persistentDataPath + "/lifetimeStats.txt");
#endif
#if UNITY_ANDROID
                string saveString = File.ReadAllText(Application.persistentDataPath + "/lifetimeStats.txt");
#endif
                SaveData savedData = JsonUtility.FromJson<SaveData>(saveString);

            data.wins = savedData.wins;
            data.losses = savedData.losses;
            data.biggestHand = savedData.biggestHand;
            data.biggestCrib = savedData.biggestCrib;
            data.biggestPlaying = savedData.biggestPlaying;

            data.lifetimeHand = savedData.lifetimeHand;
            data.lifetimeCrib = savedData.lifetimeCrib;
            data.lifetimePlaying = savedData.lifetimePlaying;

            data.lifetimePoints = savedData.lifetimePoints;

            data.gameInProgress = savedData.gameInProgress;

        }
        else
            Debug.Log("failed to load data");
    }

        


    public void updateLifetimeStats(GreenPlayer player, RoundManager roundManager, int pointsScored)
    {
        Debug.Log(player.playerColor + roundManager.activeStage + pointsScored);
            
            if(player.isAIPlayer == true)
        {
            return;
        }
        if (player.isAIPlayer == false)
        {
            lifetimePoints += pointsScored;

            if (roundManager.activeStage == RoundManager.Stage.Discard || roundManager.activeStage == RoundManager.Stage.PlayingOfHand)
            {
                lifetimePlaying += pointsScored;
                updateBiggestPlaying(player.roundPlayingOfHandScore);
            }
            if (roundManager.activeStage == RoundManager.Stage.CountHands)
            {
                lifetimeHand += pointsScored;
                updateBiggestHand(pointsScored);
            }
            if (roundManager.activeStage == RoundManager.Stage.CountCrib)
            {
                lifetimeCrib += pointsScored;
                updateBiggestCrib(pointsScored);
            }
        }
        }

        public void updateBiggestHand(int handValue)
        {
            if (handValue > biggestHand)
            {
                biggestHand = handValue;
            }
            else
                return;
        }

        public void updateBiggestCrib(int cribScore)
        {
            if (cribScore > biggestCrib)
            {
                biggestCrib = cribScore;
            }
        }

        public void updateBiggestPlaying(int playScore)
        {
            Debug.Log("Play Score: " + playScore);
            Debug.Log("Previous Biggest Play Score: " + biggestPlaying);
            if (playScore > biggestPlaying)
            {
                biggestPlaying = playScore;
            }
        }

        public void updateLifetimeHand(int gameHand)
        {
            lifetimeHand += gameHand;
        }

        public void updateLifetimeCrib(int gameCrib)
        {
            lifetimeCrib += gameCrib;
        }

        public void updateLifetimePlaying(int gamePlaying)
        {
            lifetimePlaying += gamePlaying;
        }

        public void updateLifetimePoints(int pointsScored)
        {
            lifetimePoints += pointsScored;
        }

    public void resetStats(SaveData data, GreenPlayer player)
    {
        gamesPlayed = 0; 
        wins = 0;
        losses = 0;


        biggestHand = 0;
        biggestCrib = 0;
        biggestPlaying = 0;

        lifetimeHand = 0;
        lifetimeCrib = 0;
        lifetimePlaying = 0;

        lifetimePoints = 0;


}
    
}


