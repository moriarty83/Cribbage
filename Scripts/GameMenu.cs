using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject humanPlayer;
    public GameObject aIPlayer;

    public GameObject humanColorText;
    public GameObject aIColorText;

    public GameObject errorText;
    public GameObject startGameButton;

    public Vector3 menuCameraPosition = new Vector3(350, 79.1f, 475.8f);
    public Quaternion menuCameraRotation = Quaternion.Euler(15.257f, 0f, 0f);

    public Vector3 gamePlayCameraPosition = new Vector3(0f, 78.1f, -60.5f);
    public Quaternion gamePlayCameraRotation = Quaternion.Euler(32f, 0f, 0f);

    public GameObject mainCamera;

    public CameraManager gameCameraManager;

    public GameObject mainMenuUI;

    public GameObject gameSummaryObjectUI; 

    public GameObject dealerObjectUI;

    public GameObject firstLoadUI;

    public Slider difficultySlider;

    public Tutorial gameTutorial;



    // Start is called before the first frame update
    void Start()
    {
        mainMenu();
        gameTutorial.tutorialOn = true;
        if(PlayerPrefs.GetInt("FirstLoad") == 0)
        {
            displayFirstLoadWelcome();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void mainMenu()
    {
        Debug.Log("Running Main Menu");

        humanColorText.GetComponent<Text>().color = Color.white;
        aIColorText.GetComponent<Text>().color = Color.white;

        mainMenuUI.SetActive(true);

        gameSummaryObjectUI.SetActive(false);
        dealerObjectUI.SetActive(false);

        disableHumanPlayerChildren();
        disableAIPlayerChildren();
        mainCamera.transform.position = new Vector3(350, 79.1f, 475.8f); ;
        mainCamera.transform.rotation = Quaternion.Euler(30f, 0f, 0f); ;

        GameObject.Find("RoundManager").GetComponent<ScoreUI>().endGame();
        startGameButton.SetActive(false);
        errorText.SetActive(false);
        
    }

    public void startGame()
    {
        
        enableHumanPlayerChildren();
        gameTutorial.startGame();
        enableAIPlayerChildren();

        humanPlayer.GetComponent<GreenPlayer>().startGame();
        aIPlayer.GetComponent<GreenPlayer>().startGame();

        humanPlayer.GetComponent<Discard>().startGame();
        aIPlayer.GetComponent<Discard>().startGame();

        GameObject.Find("RoundManager").GetComponent<RoundManager>().startGame();
        GameObject.Find("RoundManager").GetComponent<ScoreUI>().startGame();
        GameObject.Find("RoundManager").GetComponent<TurnManager>().startGame();


        dealerObjectUI.SetActive(true);


        gameCameraManager.startGame();
        //mainCamera.transform.position = new Vector3(0f, 78.1f, -60.5f);
        //mainCamera.transform.rotation = Quaternion.Euler(37f, 0f, 0f);

        mainMenuUI.SetActive(false);


    }



    public void disableHumanPlayerChildren()
    {
        foreach (Transform child in humanPlayer.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(false);
        }
    }

    public void enableHumanPlayerChildren()
    {
        foreach (Transform child in humanPlayer.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }
    }

    public void disableAIPlayerChildren()
    {
        foreach (Transform child in aIPlayer.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(false);
        }
    }

    public void enableAIPlayerChildren()
    {
        foreach (Transform child in aIPlayer.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }
    }

    private void cameraPlayLocation()
    {
        mainCamera.transform.position = gamePlayCameraPosition;
        mainCamera.transform.rotation = gamePlayCameraRotation;
    }

    private void cameraMenuLocation()
    {
        mainCamera.transform.position = menuCameraPosition;
        mainCamera.transform.rotation = menuCameraRotation;
    }

    public void displayFirstLoadWelcome()
    {
        firstLoadUI.SetActive(true);
        PlayerPrefs.SetInt("FirstLoad", 1);
    }

    public void closeFirstLoadWelcome()
    {
        firstLoadUI.SetActive(false);
    }

    public void humanPlayerSelectRed()
    {
        if (aIPlayer.GetComponent<GreenPlayer>().playerColor != "Red")
        {
            humanPlayer.GetComponent<GreenPlayer>().playerColor = "Red";
            humanColorText.GetComponent<Text>().color = Color.red;
            readyToStart();
            return;
        }
        else
            errorText.GetComponent<Text>().text = "Color Taken By \n" +
                "Other Player"; ;
            errorText.GetComponent<Text>().color = Color.red;
        StartCoroutine(flashColorTaken());
    }

    public void humanPlayerSelectGreen()
    {
        if (aIPlayer.GetComponent<GreenPlayer>().playerColor != "Green")
        {
            humanPlayer.GetComponent<GreenPlayer>().playerColor = "Green";
            humanColorText.GetComponent<Text>().color = new Color32(0, 155, 0, 255);
            readyToStart();
            return;


        }
        else
            errorText.GetComponent<Text>().text = "Color Taken By \n" +
                "Other Player"; ;
        errorText.GetComponent<Text>().color = new Color32(0, 155, 0, 255);
        StartCoroutine(flashColorTaken());
    }

    public void humanPlayerSelectBlue()
    {
        if (aIPlayer.GetComponent<GreenPlayer>().playerColor != "Blue")
        {
            humanPlayer.GetComponent<GreenPlayer>().playerColor = "Blue";
            humanColorText.GetComponent<Text>().color = Color.blue;
            readyToStart();
            return;

        }
        else
            errorText.GetComponent<Text>().text = "Color Taken By \n" +
                "Other Player"; ;
        errorText.GetComponent<Text>().color = Color.blue;
        StartCoroutine(flashColorTaken());
    }

    public void aIPlayerSelectRed()
    {
        if (humanPlayer.GetComponent<GreenPlayer>().playerColor != "Red")
        {
            aIPlayer.GetComponent<GreenPlayer>().playerColor = "Red";
            aIColorText.GetComponent<Text>().color = Color.red;
            readyToStart();
            return;

        }
        else
            errorText.GetComponent<Text>().text = "Color Taken By \n" +
                "Other Player"; ;
        errorText.GetComponent<Text>().color = Color.red;
        StartCoroutine(flashColorTaken());
    }

    public void aIPlayerSelectGreen()
    {
        if (humanPlayer.GetComponent<GreenPlayer>().playerColor != "Green")
        {
            aIPlayer.GetComponent<GreenPlayer>().playerColor = "Green";
            aIColorText.GetComponent<Text>().color = new Color32(0, 155, 0, 255);
            readyToStart();
            return;

        }
        else
            errorText.GetComponent<Text>().text = "Color Taken By \n" +
                "Other Player"; ;
        errorText.GetComponent<Text>().color = new Color32(0, 155, 0, 255);
        StartCoroutine(flashColorTaken());
    }

    public void aIPlayerSelectBlue()
    {
        if (humanPlayer.GetComponent<GreenPlayer>().playerColor != "Blue")
        {
            aIPlayer.GetComponent<GreenPlayer>().playerColor = "Blue";
            aIColorText.GetComponent<Text>().color = Color.blue;
            readyToStart();
            return;

        }
        else
            errorText.GetComponent<Text>().text = "Color Taken By \n" +
                "Other Player";
        errorText.GetComponent<Text>().color = Color.blue;
        StartCoroutine(flashColorTaken());
    }

    private void readyToStart()
    {
        if (humanPlayer.GetComponent<GreenPlayer>().playerColor != "" &&
           aIPlayer.GetComponent<GreenPlayer>().playerColor != "" &&
           humanPlayer.GetComponent<GreenPlayer>().playerColor !=
           aIPlayer.GetComponent<GreenPlayer>().playerColor)
        {
            startGameButton.SetActive(true);
        }
        else
            return;
    }

    IEnumerator flashColorTaken()
    {
        errorText.SetActive(true);
        yield return new WaitForSeconds(1f);
        errorText.SetActive(false);
        yield return new WaitForSeconds(.5f);
        errorText.SetActive(true);
        yield return new WaitForSeconds(1f);
        errorText.SetActive(false);

    }

    public void gameOver()
    {
        //GameObject.Find("Deck").GetComponent<Deck>().ResetRound();
        GameObject.Find("RoundManager").GetComponent<RoundManager>().gameOver();
        mainMenu();
    }


}
