using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringUI : MonoBehaviour
{

    public Sprite[] scoreSprite = new Sprite[30];

    public GameObject scoreUIObject;
    public GameObject pointsScoredUIObject;
    public GameObject scoreNumberUI;

    private Animator anim;

    private Image scoreImage;
    private Image pointsScoredImage;

    public int waitTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = scoreUIObject.GetComponent<Animator>();
        scoreImage = scoreNumberUI.GetComponent<Image>();
        pointsScoredImage = pointsScoredUIObject.GetComponent<Image>();
        scoreUIObject.SetActive(false);
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayScore(Color playerColor, int score)
    {


        scoreUIObject.SetActive(true);
        scoreImage.sprite = scoreSprite[score];
        scoreImage.color = playerColor;

        pointsScoredImage.color = playerColor;
        
        anim.Play("ScoreMe", 0);
        StartCoroutine(waitForTime());
    }

    IEnumerator waitForTime()
    {
        yield return new WaitForSeconds(waitTime);
        scoreUIObject.SetActive(false);

    }


}
