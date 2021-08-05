using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public enum CameraAngle { birdsEye, firstPerson}
    
    public RoundManager gameRoundManager;
    public Camera gameCamera;
    public CameraAngle angle;
    public Slider cameraAngleSlider;
    public GameObject uIButtons;
    public GameObject uIYourTurn;
    public GameObject uIOpponentsTurn;
    public Image opponentUIBubble;
    public Sprite birdsEyeSpeechBubble;
    public Sprite firstPersonSpeechBubble;
    public GameObject opponentUIBubbleText;

    //public Text cameraSliderText;

    public Text gameMenuCameraAngleText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setBirdsEyeView()
    {

        angle = CameraAngle.birdsEye;
        PlayerPrefs.SetString("CameraAngle", "BirdsEye");
        gameCamera.transform.position = new Vector3(0, 97, 24);
        gameCamera.transform.rotation = Quaternion.Euler(80, 0, 0);


        uIButtons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 50);
        uIYourTurn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200);
        uIOpponentsTurn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200);
        opponentUIBubble.sprite = birdsEyeSpeechBubble;
        opponentUIBubbleText.transform.localPosition = new Vector3(5.5f, -40f, 0f);


        if (cameraAngleSlider.value != 1)
        {
            cameraAngleSlider.value = 1;
        }
        //gameMenuCameraAngleText.text = "Camera: Overhead";

        //Postition 97,24,90
        //Rotation 80, 0, 0
    }

    public void setFirstPersonView()
    {
        angle = CameraAngle.firstPerson;
        PlayerPrefs.SetString("CameraAngle", "FirstPerson");
        gameCamera.transform.position = new Vector3(0f, 78.1f, -60.5f);
        gameCamera.transform.rotation = Quaternion.Euler(37f, 0f, 0f);

        uIButtons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 50);
        uIYourTurn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 140);
        uIOpponentsTurn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 140);
        opponentUIBubble.sprite = firstPersonSpeechBubble;
        opponentUIBubbleText.transform.localPosition = new Vector3(5.5f, -30f, 0f);


        if (cameraAngleSlider.value != 0)
        {
            cameraAngleSlider.value = 0;
        }
        //gameMenuCameraAngleText.text = "Camera: First Person";
    }

    public void toggleAngleInGame()
    {
        if(angle == CameraAngle.birdsEye)
        {
            angle = CameraAngle.firstPerson;
            setFirstPersonView();
            
            return;
        }

        if(angle == CameraAngle.firstPerson)
        {
            angle = CameraAngle.birdsEye;
            setBirdsEyeView();
            return;
        }
    }

    public void startGame()
    {

        if (PlayerPrefs.GetString("CameraAngle") == "BirdsEye")
        {
            setBirdsEyeView();
            //gameMenuCameraAngleText.text = "Camera: Overhead";
        }
        else

            setFirstPersonView();

    }

    public void setCameraSlider()
    {
        Debug.Log("slider activated");
        if(cameraAngleSlider.value == 0)
        {
            setFirstPersonView();
        }

        if (cameraAngleSlider.value == 1)
        {
            setBirdsEyeView();
        }
    }


}
