using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]

public class SaveLoadGameState : MonoBehaviour
{
    public GameState currentGameState;

    public GameObject resumeGameUIMenu;
    public GameObject confirmForfeitUI;

    public GameObject gameStartUI;

    private float timer = 0;


    // Start is called before the first frame update
    void Awake()
    {
        
        resumeGameUIMenu.SetActive(false);
        confirmForfeitUI.SetActive(false);
        if((PlayerPrefs.GetInt("GameInProgress") == 1))
        {
            resumeGameUIMenu.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            saveState();
            timer = 0;
        }

         
    }

    public void saveState()
    {
        currentGameState.saveGameState(currentGameState);
    }

    public void loadState()
    {
        resumeGameUIMenu.SetActive(false);
        currentGameState.loadGameState();
    }

    /*private void OnApplicationQuit()
    {
        if (PlayerPrefs.GetInt("GameInProgress") == 1 )
        {
            saveState();
        }
    }*/
    

    
#if UNITY_ANDROID

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("Paused");
        if (PlayerPrefs.GetInt("GameInProgress") == 1 && gameStartUI.activeInHierarchy == false)
        {
            saveState();
        }
    }

#endif
    

    public void forfeit()
    {
        confirmForfeitUI.SetActive(true);
    }

    public void areYouSure()
    {
        confirmForfeitUI.SetActive(true);
    }

    public void confirmForfeit()
    {
        PlayerPrefs.SetInt("GameInProgress", 0);
        GameObject.Find("GreenPlayer").GetComponent<GreenPlayer>().lifetimeStats.losses += 1;
        GameObject.Find("GreenPlayer").GetComponent<GreenPlayer>().lifetimeStats.gameInProgress = 0;
        resumeGameUIMenu.SetActive(false);
        Debug.Log(PlayerPrefs.GetInt("GameInProgress"));
    }

    public void cancelForfeit()
    {
        confirmForfeitUI.SetActive(false);
    }

}
