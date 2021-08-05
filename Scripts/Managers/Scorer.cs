using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Scorer : MonoBehaviour
{
    public static Scorer scorer1;
    public int handScore;

    public int totalFifteens;

    public RoundManager roundManagerScript;

    public GameObject fifteenUIPrefab;
    public GameObject pairUIPrefab;
    public GameObject runUIPrefab;
    public GameObject flushUIPrefab;
    public GameObject nibsUIPrefab;

    public GameObject fifteensParent;
    public GameObject pairsParent; 
    public GameObject runsParent;
    public GameObject flushesParent;
    public GameObject nibsParent;

    public Text fifteensText;
    public Text pairsText;
    public Text runsText;
    public Text flushesText;
    public Text nibsText;
    public Text totalText;

    public Sprite nullSprite;

    //public List<int> runValues;

    void Awake()
    {
        scorer1 = this;
    }

    public int ScoreHand(List<Card> cardsToScore, GreenPlayer me,
        out int totalFifteens,
        out int totalPairs,
        out int total5CardRuns,
        out int total4CardRuns,
        out int total3CardRuns,
        out int total5CardFlushes,
        out int total4CardFlushes,
        out int totalRightJack)
    {
        int numberFifteens;
        int numberPairs;

        int number5CardRuns;
        int number4CardRuns;
        int number3CardRuns;

        int number5CardFlushes;
        int number4CardFlushes;

        int numberRightJack;

        Count15s(cardsToScore, out numberFifteens);
        CountPairs(cardsToScore, out numberPairs);
        CountRuns(cardsToScore, out number5CardRuns, out number4CardRuns, out number3CardRuns);
        CountFlush(cardsToScore, out number5CardFlushes, out number4CardFlushes);
        RightJack(cardsToScore, out numberRightJack);

        totalFifteens = numberFifteens;

        totalPairs = numberPairs;

        total5CardRuns = number5CardRuns;
        total4CardRuns = number4CardRuns;
        total3CardRuns = number3CardRuns;

        total5CardFlushes = number5CardFlushes;
        total4CardFlushes = number4CardFlushes;

        totalRightJack = numberRightJack;

        if (me.isAIPlayer == true)
        {
            totalText.text = "Opponent Scores " + ((numberFifteens * 2) +
                (numberPairs * 2) +
                (number5CardRuns * 5) + (number4CardRuns * 4) + (number3CardRuns * 3) +
                (number5CardFlushes * 5) + (number4CardFlushes * 4) +
                (numberRightJack)) + " Points";
        }

        if (me.isAIPlayer == false)
        {
            totalText.text = "You Score " + ((numberFifteens * 2) +
                (numberPairs * 2) +
                (number5CardRuns * 5) + (number4CardRuns * 4) + (number3CardRuns * 3) +
                (number5CardFlushes * 5) + (number4CardFlushes * 4) +
                (numberRightJack)) + " Points";
        }

        return
            (numberFifteens * 2) +
            (numberPairs * 2) +
            (number5CardRuns * 5) + (number4CardRuns * 4) + (number3CardRuns * 3) +
            (number5CardFlushes * 5) + (number4CardFlushes * 4) +
            (numberRightJack);
            //Count15s(cardsToScore, out numberFifteens) +
            //CountPairs(cardsToScore, out numberPairs) +
            //CountRuns(cardsToScore, out number5CardRuns, out number4CardRuns, out number3CardRuns) +
            //CountFlush(cardsToScore, out number5CardFlushes, out number4CardFlushes) +
            //RightJack(cardsToScore, out numberRightJack);

    }

    #region Count Fifteens
    public int Count15s(List<Card> cardsToScore, out int fifteensCount)
    {
        int number15s = 0;

        //5 Card 15
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject five15one = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                five15one.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                five15one.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                five15one.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                five15one.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                five15one.transform.GetChild(4).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
            }
        }

        //4 Card 15s

        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject four15one = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                four15one.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                four15one.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                four15one.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                four15one.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                four15one.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject four15two = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                four15two.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                four15two.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                four15two.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                four15two.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                four15two.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject four15three = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                four15three.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                four15three.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                four15three.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                four15three.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                four15three.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject four15four = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                four15four.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                four15four.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                four15four.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                four15four.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                four15four.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject four15five = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                four15five.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                four15five.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                four15five.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                four15five.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                four15five.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }



        //3 Card Fifteens
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15one = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15one.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                three15one.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                three15one.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                three15one.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15one.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15two = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15two.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                three15two.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                three15two.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                three15two.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15two.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15three = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15three.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                three15three.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                three15three.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                three15three.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15three.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15four = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15four.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                three15four.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                three15four.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                three15four.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15four.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15five = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15five.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                three15five.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                three15five.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                three15five.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15five.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15six = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15six.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                three15six.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                three15six.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                three15six.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15six.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }


        if (cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15seven = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15seven.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                three15seven.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                three15seven.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                three15seven.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15seven.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15eight = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15eight.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                three15eight.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                three15eight.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                three15eight.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15eight.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[1].cardCountValue + cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15nine = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15nine.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                three15nine.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                three15nine.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                three15nine.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15nine.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }


        if (cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject three15ten = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                three15ten.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                three15ten.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                three15ten.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                three15ten.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                three15ten.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }

        //Two card fifteens
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");
                GameObject two15one = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15one.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                two15one.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                two15one.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15one.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15one.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;

            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[2].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15two = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15two.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                two15two.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                two15two.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15two.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15two.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15three = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15three.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                two15three.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                two15three.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15three.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15three.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15four = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15four.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                two15four.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                two15four.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15four.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15four.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }

        if (cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15five = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15five.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                two15five.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                two15five.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15five.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15five.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[1].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15six = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15six.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                two15six.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                two15six.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15six.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15six.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[1].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15seven = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15seven.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                two15seven.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                two15seven.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15seven.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15seven.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }

        if (cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15eight = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15eight.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                two15eight.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                two15eight.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15eight.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15eight.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }
        if (cardsToScore[2].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15nine = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15nine.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                two15nine.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                two15nine.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15nine.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15nine.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }


        if (cardsToScore[3].cardCountValue + cardsToScore[4].cardCountValue == 15)
        {
            number15s += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                Debug.Log("Adding 15 UI");

                GameObject two15ten = Instantiate(fifteenUIPrefab, fifteensParent.transform);
                two15ten.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                two15ten.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;
                two15ten.transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
                two15ten.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                two15ten.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
            }
        }


        Debug.Log(number15s + " Fifteens counted");
        fifteensCount = number15s;
        fifteensText.text = "Fifteens:\n" + (number15s * 2) + " Points";
        return number15s * 2;

    }


    #region Count Pairs
    public int CountPairs(List<Card> cardsToScore, out int pairsCount)
    {

        int pair = 0;
        if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
        {
            if (cardsToScore[0].cardValue == cardsToScore[1].cardValue)
            {
                pair += 1;
                GameObject pairOne = Instantiate(pairUIPrefab, pairsParent.transform);
                pairOne.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                pairOne.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;

            }
        }

        if (cardsToScore[0].cardValue == cardsToScore[2].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairTwo = Instantiate(pairUIPrefab, pairsParent.transform);
                pairTwo.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                pairTwo.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;

            }
        }

        if (cardsToScore[0].cardValue == cardsToScore[3].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairThree = Instantiate(pairUIPrefab, pairsParent.transform);
                pairThree.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                pairThree.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;

            }
        }

        if (cardsToScore[0].cardValue == cardsToScore[4].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairFour = Instantiate(pairUIPrefab, pairsParent.transform);
                pairFour.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                pairFour.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;

            }
        }

        if (cardsToScore[1].cardValue == cardsToScore[2].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairFive = Instantiate(pairUIPrefab, pairsParent.transform);
                pairFive.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                pairFive.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;

            }
        }

        if (cardsToScore[1].cardValue == cardsToScore[3].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairSix = Instantiate(pairUIPrefab, pairsParent.transform);
                pairSix.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                pairSix.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;

            }
        }

        if (cardsToScore[1].cardValue == cardsToScore[4].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairSeven = Instantiate(pairUIPrefab, pairsParent.transform);
                pairSeven.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                pairSeven.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;

            }
        }

        if (cardsToScore[2].cardValue == cardsToScore[3].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairEight = Instantiate(pairUIPrefab, pairsParent.transform);
                pairEight.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                pairEight.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;

            }
        }

        if (cardsToScore[2].cardValue == cardsToScore[4].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairNine = Instantiate(pairUIPrefab, pairsParent.transform);
                pairNine.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                pairNine.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;

            }
        }

        if (cardsToScore[3].cardValue == cardsToScore[4].cardValue)
        {
            pair += 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject pairTen = Instantiate(pairUIPrefab, pairsParent.transform);
                pairTen.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                pairTen.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;

            }
        }

        Debug.Log(pair + " Pairs Counted");
        pairsCount = pair;
        pairsText.text = "Pairs:\n" + (pairsCount * 2) + " Points";
        return (pair * 2);

    }
    #endregion

    #region Count Runs
    public int CountRuns(List<Card> runsToScore, out int fiveCardRunsCounted, out int fourCardRunsCounted, out int threeCardRunsCounted)
    {
        List<int> runValues = new List<int>();
        for (int i = 0; i < runsToScore.Count; i++)
        {
            runValues.Add(runsToScore[i].cardValue);
        }
        List<Card> sortedRuns = new List<Card>();

        for (int i = 0; i < runsToScore.Count; i++)
        {
            sortedRuns.Add(runsToScore[i]);
        }

        //New function for sorted 
        void sortForRuns()
        {

            sortedRuns.Sort(SortCardsFunct);
            foreach (Card item in sortedRuns)
            {
                Debug.Log("Sorted Runs Card Value is " + item.cardValue);
            }

            runValues.Sort(SortFunct);
        }

        sortForRuns();

        for (int i = 0; i < runValues.Count; i++)
        {
            Debug.Log("runValue " + i + " is " + runValues[i]);
        }

        int number5CardRuns = 0;
        int number4CardRuns = 0;
        int number3CardRuns = 0;
        //5 Card Run
        if (runValues[0] + 1 == runValues[1] && runValues[0] + 2 == runValues[2] && runValues[0] + 3 == runValues[3] && runValues[0] + 4 == runValues[4])
        {
            number5CardRuns = 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject fiveRunOne = Instantiate(runUIPrefab, runsParent.transform);
                fiveRunOne.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                fiveRunOne.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                fiveRunOne.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                fiveRunOne.transform.GetChild(3).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                fiveRunOne.transform.GetChild(4).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;

                runsText.text = "Runs:\n" + (number5CardRuns * 5) + " Points";
            }
        }

        //4 Card Runs
        if (number5CardRuns == 0)
        {
            if (runValues[0] + 1 == runValues[1] && runValues[0] + 2 == runValues[2] && runValues[0] + 3 == runValues[3])
            {
                number4CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject fourRunOne = Instantiate(runUIPrefab, runsParent.transform);
                    fourRunOne.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    fourRunOne.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    fourRunOne.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    fourRunOne.transform.GetChild(3).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    fourRunOne.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                }
            }
            if (runValues[0] + 1 == runValues[1] && runValues[0] + 2 == runValues[2] && runValues[0] + 3 == runValues[4])
            {
                number4CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject fourRunTwo = Instantiate(runUIPrefab, runsParent.transform);
                    fourRunTwo.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    fourRunTwo.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    fourRunTwo.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    fourRunTwo.transform.GetChild(3).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    fourRunTwo.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                }
            }
            if (runValues[0] + 1 == runValues[1] && runValues[0] + 2 == runValues[3] && runValues[0] + 3 == runValues[4])
            {
                number4CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject fourRunThree = Instantiate(runUIPrefab, runsParent.transform);
                    fourRunThree.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    fourRunThree.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    fourRunThree.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    fourRunThree.transform.GetChild(3).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    fourRunThree.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                }
            }
            if (runValues[0] + 1 == runValues[2] && runValues[0] + 2 == runValues[3] && runValues[0] + 3 == runValues[4])
            {
                number4CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject fourRunFour = Instantiate(runUIPrefab, runsParent.transform);
                    fourRunFour.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    fourRunFour.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    fourRunFour.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    fourRunFour.transform.GetChild(3).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    fourRunFour.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                }
            }
            if (runValues[1] + 1 == runValues[2] && runValues[1] + 2 == runValues[3] && runValues[1] + 3 == runValues[4])
            {
                number4CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject fourRunFive = Instantiate(runUIPrefab, runsParent.transform);
                    fourRunFive.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    fourRunFive.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    fourRunFive.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    fourRunFive.transform.GetChild(3).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    fourRunFive.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;

                }
            }

            runsText.text = "Runs:\n" + (number4CardRuns * 4) + " Points";

        }

        if (number4CardRuns == 0 && number5CardRuns == 0)
        {
            //3 Card Runs
            if (runValues[0] + 1 == runValues[1] &&
                runValues[0] + 2 == runValues[2])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunOne = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunOne.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    threeRunOne.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    threeRunOne.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    threeRunOne.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunOne.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 1");
                    //have observed this being counted

                }
            }
            if (runValues[0] + 1 == runValues[1] &&
                runValues[0] + 2 == runValues[3])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunTwo = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunTwo.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    threeRunTwo.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    threeRunTwo.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    threeRunTwo.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunTwo.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 2");
                    //have observed this being counted

                }
            }
            if (runValues[0] + 1 == runValues[1] &&
                runValues[0] + 2 == runValues[4])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunThree = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunThree.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    threeRunThree.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    threeRunThree.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    threeRunThree.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunThree.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 3");
                    //have observed this being counted
                }
            }
            if (runValues[0] + 1 == runValues[2] &&
                runValues[0] + 2 == runValues[3])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunFour = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunFour.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    threeRunFour.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    threeRunFour.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    threeRunFour.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunFour.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 4");
                    //have observed this being counted
                }
            }
            if (runValues[0] + 1 == runValues[2] &&
                runValues[0] + 2 == runValues[4])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunFive = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunFive.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    threeRunFive.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    threeRunFive.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    threeRunFive.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunFive.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 5");
                }
            }
            if (runValues[0] + 1 == runValues[3] &&
                runValues[0] + 2 == runValues[4])
            //have observed this being counted

            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunSix = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunSix.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[0].cardSprite;
                    threeRunSix.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    threeRunSix.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    threeRunSix.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunSix.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 6");
                    //have observed this being counted

                }
            }



            if (runValues[1] + 1 == runValues[2] &&
                runValues[1] + 2 == runValues[3])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunSeven = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunSeven.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    threeRunSeven.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    threeRunSeven.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    threeRunSeven.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunSeven.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 7");
                    //have observed this being counted
                }
            }
            if (runValues[1] + 1 == runValues[2] &&
                runValues[1] + 2 == runValues[4])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunEight = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunEight.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    threeRunEight.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    threeRunEight.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    threeRunEight.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunEight.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 8");
                }
            }
            if (runValues[1] + 1 == runValues[3] &&
                runValues[1] + 2 == runValues[4])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunNine = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunNine.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[1].cardSprite;
                    threeRunNine.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    threeRunNine.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    threeRunNine.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunNine.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 9");
                    //have observed being counted
                }
            }


            if (runValues[2] + 1 == runValues[3] &&
                runValues[2] + 2 == runValues[4])
            {
                number3CardRuns += 1;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject threeRunTen = Instantiate(runUIPrefab, runsParent.transform);
                    threeRunTen.transform.GetChild(0).GetComponent<Image>().sprite = sortedRuns[2].cardSprite;
                    threeRunTen.transform.GetChild(1).GetComponent<Image>().sprite = sortedRuns[3].cardSprite;
                    threeRunTen.transform.GetChild(2).GetComponent<Image>().sprite = sortedRuns[4].cardSprite;
                    threeRunTen.transform.GetChild(3).GetComponent<Image>().sprite = nullSprite;
                    threeRunTen.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;
                    //Debug.Log("Three Card Run 10");
                    //have observed being counted
                }
            }

            runsText.text = "Runs:\n" + (number3CardRuns * 3) + " Points";

        }

        Debug.Log(number5CardRuns + " Five Card runs counted. " + number4CardRuns + " Four Card runs counted. " + number3CardRuns + " Three Card runs counted.");
        fiveCardRunsCounted = number5CardRuns;
        fourCardRunsCounted = number4CardRuns;
        threeCardRunsCounted = number3CardRuns;
        return ((number5CardRuns * 5) + (number4CardRuns * 4) + (number3CardRuns * 3));

    }


    #endregion

    public int CountFlush(List<Card> cardsToScore, out int fiveCardFlushCounted, out int fourCardFlushCounted)
    {
        List<Card> handCards = new List<Card>();
        List<Card> fifthCard = new List<Card>();
        bool fourCardFlush = new bool();
        bool fiveCardFlush = new bool();

        for (int j = 0; j < cardsToScore.Count; j++)
        {
            if (cardsToScore[0].cardSuit == cardsToScore[1].cardSuit &&
               cardsToScore[0].cardSuit == cardsToScore[2].cardSuit &&
               cardsToScore[0].cardSuit == cardsToScore[3].cardSuit)
            {
                fourCardFlush = true;

            }
            if (cardsToScore[0].cardSuit == cardsToScore[4].cardSuit && fourCardFlush == true)
            {
                fiveCardFlush = true;
            }
        }

        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands &&
            fourCardFlush == true &&
            fiveCardFlush == true)
        {
            fiveCardFlushCounted = 1;
            fourCardFlushCounted = 0;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject fiveFlushOne = Instantiate(flushUIPrefab, flushesParent.transform);
                fiveFlushOne.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                fiveFlushOne.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                fiveFlushOne.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                fiveFlushOne.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                fiveFlushOne.transform.GetChild(4).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;

                flushesText.text = "Flush:\n" + "5 Points";
            }
            return 5;
        }
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountHands &&
            fourCardFlush == true)
        {
            fiveCardFlushCounted = 0;
            fourCardFlushCounted = 1;
            if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
            {
                GameObject fourFlushOne = Instantiate(flushUIPrefab, flushesParent.transform);
                fourFlushOne.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
                fourFlushOne.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
                fourFlushOne.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
                fourFlushOne.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
                fourFlushOne.transform.GetChild(4).GetComponent<Image>().sprite = nullSprite;

                flushesText.text = "Flush:\n" + "4 Points";
            }
            return 4;
        }
        if (GameObject.Find("RoundManager").GetComponent<RoundManager>().activeStage == RoundManager.Stage.CountCrib &&
            fourCardFlush == true &&
            fiveCardFlush == true)
        {
            fiveCardFlushCounted = 1;
            fourCardFlushCounted = 0;

            GameObject fiveFlushOne = Instantiate(flushUIPrefab, flushesParent.transform);
            fiveFlushOne.transform.GetChild(0).GetComponent<Image>().sprite = cardsToScore[0].cardSprite;
            fiveFlushOne.transform.GetChild(1).GetComponent<Image>().sprite = cardsToScore[1].cardSprite;
            fiveFlushOne.transform.GetChild(2).GetComponent<Image>().sprite = cardsToScore[2].cardSprite;
            fiveFlushOne.transform.GetChild(3).GetComponent<Image>().sprite = cardsToScore[3].cardSprite;
            fiveFlushOne.transform.GetChild(4).GetComponent<Image>().sprite = cardsToScore[4].cardSprite;

            flushesText.text = "Flush:\n" + "5 Points";
            return 5;
        }
        else
            flushesText.text = "Flush:\n" + "0 Points";
        fiveCardFlushCounted = 0;
        fourCardFlushCounted = 0;
        return 0;
    }

    public int RightJack(List<Card> cardsToScore, out int jack)
    {
        List<Card> handCards = new List<Card>();
        List<Card> fifthCard = new List<Card>();
        bool rightJack = new bool();
        for (int i = 0; i < cardsToScore.Count; i++)
        {
            if (cardsToScore[i].cardOwnedBy != 9)
            {
                handCards.Add(cardsToScore[i]);
            }

            if (cardsToScore[i].cardOwnedBy == 9)
            {
                fifthCard.Add(cardsToScore[i]);
            }
        }

        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards[i].cardValue == 11 && handCards[i].cardSuit == fifthCard[0].cardSuit)
            {
                rightJack = true;
                if (roundManagerScript.activeStage == RoundManager.Stage.CountHands || roundManagerScript.activeStage == RoundManager.Stage.CountCrib)
                {
                    GameObject nibsOne = Instantiate(nibsUIPrefab, nibsParent.transform);
                    nibsOne.transform.GetChild(0).GetComponent<Image>().sprite = handCards[i].cardSprite;

                    nibsText.text = "Nibs:\n" + "1 Point";
                }
            }

        }

        if (rightJack == true)
        {
            jack = 1;
            return 1;
        }
        else
            nibsText.text = "Nibs:\n" + "0 Points";
        jack = 0;
        return 0;
    }




    #endregion

    #region clear Count Summary
    public void clearCountSummary()
    {
        Debug.Log("Clearing Count UI Summary");
        int fifteensChildCount = fifteensParent.transform.childCount - 1;
        int pairsChildCount = pairsParent.transform.childCount - 1;
        int runsChildCount = runsParent.transform.childCount - 1;
        int flushChildCout = fifteensParent.transform.childCount - 1;
        int nibsChildCount = nibsParent.transform.childCount - 1;

        foreach (Transform child in fifteensParent.transform)
        {
            Debug.Log("Destroying fifteen children");

            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in pairsParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in runsParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in flushesParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in nibsParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    #endregion

    public void updateHandScore(int playerHandScore)
    {
        playerHandScore = handScore;
    }


    //Sorting for runs
    private int SortCardsFunct(Card a, Card b)
    {

        if (a.cardValue < b.cardValue)
        {
            return -1;
        }

        else if (a.cardValue > b.cardValue)
        {
            return 1;
        }

        return 0;
    }


    private int SortFunct(int a, int b)
    {
        if (a < b)
        {
            return -1;
        }

        else if (a > b)
        {
            return 1;
        }

        return 0;
    }



    //AI DISCARD SELECTOR
    //Create Subsets
    List<Card> handSubset1;


    public int scoreFourCards(List<Card> cardsToScore)
    {
        int numberFifteens;
        int numberPairs;

        int number4CardRuns;
        int number3CardRuns;

        int number4CardFlushes;

        CountFourCard15s(cardsToScore, out numberFifteens);
        CountFourCardPairs(cardsToScore, out numberPairs);
        CountFourCardRuns(cardsToScore, out number4CardRuns, out number3CardRuns);
        CountFourCardFlush(cardsToScore, out number4CardFlushes);

        return
            (numberFifteens * 2) + (numberPairs * 2) +
            (number4CardRuns * 4) + (number3CardRuns * 3) +
            (number4CardFlushes * 4);

    }

    #region Count Fifteens
    public int CountFourCard15s(List<Card> cardsToScore, out int fifteensCount)
    {
        int number15s = 0;

        //4 Card 15s

        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
        }

        //3 Card Fifteens
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue == 15)
        {
            number15s += 1;
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
        }

        if (cardsToScore[0].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
        }




        if (cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
        }


        //Two card fifteens
        if (cardsToScore[0].cardCountValue + cardsToScore[1].cardCountValue == 15)
        {
            number15s += 1;
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[2].cardCountValue == 15)
        {
            number15s += 1;
        }
        if (cardsToScore[0].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
        }




        if (cardsToScore[1].cardCountValue + cardsToScore[2].cardCountValue == 15)
        {
            number15s += 1;
        }
        if (cardsToScore[1].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
        }




        if (cardsToScore[2].cardCountValue + cardsToScore[3].cardCountValue == 15)
        {
            number15s += 1;
        }

        fifteensCount = number15s;
        return number15s * 2;

    }


    #region Count Pairs
    public int CountFourCardPairs(List<Card> cardsToScore, out int pairsCount)
    {

        int pair = 0;
        if (cardsToScore[0].cardValue == cardsToScore[1].cardValue)
        {
            pair += 1;
        }

        if (cardsToScore[0].cardValue == cardsToScore[2].cardValue)
        {
            pair += 1;
        }

        if (cardsToScore[0].cardValue == cardsToScore[3].cardValue)
        {
            pair += 1;
        }

        if (cardsToScore[1].cardValue == cardsToScore[2].cardValue)
        {
            pair += 1;
        }

        if (cardsToScore[1].cardValue == cardsToScore[3].cardValue)
        {
            pair += 1;
        }

        if (cardsToScore[2].cardValue == cardsToScore[3].cardValue)
        {
            pair += 1;
        }

        pairsCount = pair;
        return (pair * 2);

    }
    #endregion

    #region Count Runs
    public int CountFourCardRuns(List<Card> runsToScore, out int fourCardRunsCounted, out int threeCardRunsCounted)
    {
        List<int> runValues = new List<int>();
        for (int i = 0; i < runsToScore.Count; i++)
        {
            runValues.Add(runsToScore[i].cardValue);
        }


        //New function for sorted 
        void sortForRuns()
        {
            runValues.Sort(SortFunct);
        }

        sortForRuns();


        int number4CardRuns = 0;
        int number3CardRuns = 0;

        if (runValues[0] + 1 == runValues[1] && runValues[0] + 2 == runValues[2] && runValues[0] + 3 == runValues[3])
        {
            number4CardRuns += 1;
        }


        if (number4CardRuns == 0)
        {
            //3 Card Runs
            if (runValues[0] + 1 == runValues[1] &&
                runValues[0] + 2 == runValues[2])
            {
                number3CardRuns += 1;
                //have observed this being counted

            }
            if (runValues[0] + 1 == runValues[1] &&
                runValues[0] + 2 == runValues[3])
            {
                number3CardRuns += 1;
                //have observed this being counted

            }

            if (runValues[0] + 1 == runValues[2] &&
                runValues[0] + 2 == runValues[3])
            {
                number3CardRuns += 1;
                //have observed this being counted
            }


            if (runValues[1] + 1 == runValues[2] &&
                runValues[1] + 2 == runValues[3])
            {
                number3CardRuns += 1;
                //have observed this being counted
            }
        }

        Debug.Log(number4CardRuns + " Four Card runs counted. " + number3CardRuns + " Three Card runs counted.");
        fourCardRunsCounted = number4CardRuns;
        threeCardRunsCounted = number3CardRuns;
        return ((number4CardRuns * 4) + (number3CardRuns * 3));

    }


    #endregion

    public int CountFourCardFlush(List<Card> cardsToScore, out int fourCardFlushCounted)
    {
        List<Card> handCards = new List<Card>();
        List<Card> fifthCard = new List<Card>();
        bool fourCardFlush = new bool();

        for (int j = 0; j < handCards.Count; j++)
        {
            if (handCards[0].cardSuit == handCards[1].cardSuit &&
               handCards[0].cardSuit == handCards[2].cardSuit &&
               handCards[0].cardSuit == handCards[3].cardSuit)
            {
                fourCardFlush = true;
            }

        }

        if (fourCardFlush == true)
        {
            fourCardFlushCounted = 1;
            return 4;
        }
        else
            fourCardFlushCounted = 0;
        return 0;
    }

    public int fiveCardPotentialScoreWithFlush(List<Card> cardsToScore)
    {

        int numberFifteens;
        int numberPairs;

        int number5CardRuns;
        int number4CardRuns;
        int number3CardRuns;

        int flushScore;

        int numberRightJack;

        Count15s(cardsToScore, out numberFifteens);
        CountPairs(cardsToScore, out numberPairs);
        CountRuns(cardsToScore, out number5CardRuns, out number4CardRuns, out number3CardRuns);
        flushScore = CountPotentialFlush(cardsToScore);
        potentialRightJack(cardsToScore, out numberRightJack);

        return ((numberFifteens * 2) + (numberPairs * 2) +
            (number5CardRuns * 1) + (number4CardRuns * 4) + (number3CardRuns * 3) +
            (flushScore) + (numberRightJack * 1));
    }

    public int potentialRightJack(List<Card> cards, out int numberRightJack)
    {

        bool rightJack = new bool();
        for (int i = 0; i < 3; i++)
        {
            if (cards[i].cardValue == 11 && cards[i].cardSuit == cards[4].cardSuit)
            {
                rightJack = true;
            }

            else
                rightJack = false;
        }

        if (rightJack == true)
        {
            numberRightJack = 1;
            return 1;
        }
        else
            numberRightJack = 0;
        return 0;

    }

    public int CountPotentialFlush(List<Card> cardsToScore)
    {
        List<Card> handCards = new List<Card>();
        List<Card> fifthCard = new List<Card>();
        bool fourCardFlush = new bool();
        bool fiveCardFlush = new bool();

        for (int j = 0; j < cardsToScore.Count; j++)
        {
            if (cardsToScore[0].cardSuit == cardsToScore[1].cardSuit &&
               cardsToScore[0].cardSuit == cardsToScore[2].cardSuit &&
               cardsToScore[0].cardSuit == cardsToScore[3].cardSuit)
            {
                fourCardFlush = true;
            }
            if (cardsToScore[0].cardSuit == cardsToScore[4].cardSuit && fourCardFlush == true)
            {
                fiveCardFlush = true;
            }
        }
        if (fiveCardFlush == true)
        {
            return 5;
        }
        if (fourCardFlush == true)
        {
            return 5;
        }
        else return 0;
    }

    public int fiveCardPotentialScoreWithoutFlush(List<Card> cardsToScore)
    {
        Debug.Log("Runing potential no flush");
        int numberFifteens;
        int numberPairs;

        int number5CardRuns;
        int number4CardRuns;
        int number3CardRuns;

        int flushScore;

        int numberRightJack;

        Count15s(cardsToScore, out numberFifteens);
        CountPairs(cardsToScore, out numberPairs);
        CountRuns(cardsToScore, out number5CardRuns, out number4CardRuns, out number3CardRuns);

        return ((numberFifteens * 2) + (numberPairs * 2) +
            (number5CardRuns * 1) + (number4CardRuns * 4) + (number3CardRuns * 3));
    }
}
#endregion