using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aIDifficulty : MonoBehaviour
{
    public GreenPlayer aIGreenPlayer;
    public GameObject sliderObject;
    public Text difficultyText;
    public Slider difficultySlider;

    // Start is called before the first frame update
    void Awake()
    {
        loadDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        if(sliderObject.GetComponent<Slider>().value == 1)
        {
            difficultyText.text = "Easy";
            difficultyText.color = Color.green;
            aIGreenPlayer.aISetEasy();
        }

        if (sliderObject.GetComponent<Slider>().value == 2)
        {
            difficultyText.text = "Medium";
            difficultyText.color = new Color32(255, 125, 0, 255);
            aIGreenPlayer.aISetMedium();
        }

        if (sliderObject.GetComponent<Slider>().value == 3)
        {
            difficultyText.text = "Hard";
            difficultyText.color = Color.red;
            aIGreenPlayer.aISetHard();
        }

    }

    void loadDifficulty()
    {
        if (PlayerPrefs.GetString("aIDifficulty") == "easy")
        {
            difficultySlider.value = 1;
        }
        if (PlayerPrefs.GetString("aIDifficulty") == "medium")
        {
            difficultySlider.value = 2;
        }
        if (PlayerPrefs.GetString("aIDifficulty") == "hard")
        {
            difficultySlider.value = 3;
        }
    }
}
