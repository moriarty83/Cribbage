using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuButtonsParent;

    public GameObject gameMenuBlockObject;

    public GameObject confirmButton;
    public GameObject cancelButton;

    public bool menuOpen;

    public bool cancelOptionsOpen;
    // Start is called before the first frame update
    void Start()
    {
        menuButtonsParent.SetActive(false);

        cancelOptionsOpen = false;
        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void toggleMenu()
    {
        if(menuOpen == true)
        {
            menuOpen = false;
            menuButtonsParent.SetActive(false);
            gameMenuBlockObject.SetActive(false);
            return;
        }

        if (menuOpen == false)
        {
            menuOpen = true;
            gameMenuBlockObject.SetActive(true);
            menuButtonsParent.SetActive(true);
            return;
        }

    }

    public void clickEndGame()
    {
        if(cancelOptionsOpen == false)
        {
            cancelOptionsOpen = true;
            confirmButton.SetActive(true);
            cancelButton.SetActive(true);
            return;
        }
        if (cancelOptionsOpen == true)
        {
            cancelOptionsOpen = false;
            confirmButton.SetActive(false);
            cancelButton.SetActive(false);
            return;
        }
    }

    public void cancelEndGame()
    {
        
        cancelOptionsOpen = false;
        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void showRules()
    {
        Application.OpenURL("https://en.wikipedia.org/wiki/Rules_of_cribbage#The_crib");
    }
}
