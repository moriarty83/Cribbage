using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{

    public Vector3 startingPosition;
    public Vector3 raiseToPosition;
    public Vector3 lowerFromPosition;
    public Vector3 targetPosition;

    public GameObject startingGameObject;

    Vector3 velocity = Vector3.zero;
    float smoothTime = .1f;
    float smoothHeight = 9f;
    float smoothDistance = 0.01f;

    public bool inPosition;

    public List<GameObject> otherPegs;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject peg in GameObject.FindGameObjectsWithTag("GreenPeg"))
        {
            otherPegs.Add(peg);
        }

        foreach (GameObject peg in GameObject.FindGameObjectsWithTag("RedPeg"))
        {
            otherPegs.Add(peg);
        }

        otherPegs.Remove(this.gameObject);

        startingPosition = this.transform.position;
        targetPosition = this.transform.position;
        inPosition = true;
        lowerFromPosition = new Vector3(targetPosition.x, targetPosition.y + 7, targetPosition.z);
        raiseToPosition = new Vector3(startingPosition.x, startingPosition.y + 7, startingPosition.z);

    }

    // Update is called once per frame
    void Update()
    {


        //changes our "inPosition" each frame (maybe)?
        if (this.transform.position == this.targetPosition)
        {
            inPosition = true;
        }
        else
        {
            inPosition = false;

        }

        bool canmove = true;

        /*for (int i = 0; i < otherPegs.Count; i++)
        {
            if (otherPegs[i].GetComponent<Peg>().inPosition == false)
            {
                canmove = false;
            }
        }*/

        //Quill18 Code
        if (canmove == true)
        {
            if (Vector3.Distance(
                   new Vector3(this.transform.position.x, targetPosition.y, this.transform.position.z),
                   targetPosition) < smoothDistance)

            {
                // We've reached the target position x & z coordinate -- do we lower?

                if (this.transform.position.y > targetPosition.y)

                {
                    // We are totally out of moves (and too high up), the only thing left to do is drop down.
                    this.transform.position = Vector3.SmoothDamp(
                        this.transform.position,
                        new Vector3(targetPosition.x, targetPosition.y, targetPosition.z),
                        ref velocity,
                        smoothTime);
                }

            }
            else if (this.transform.position.y < (smoothHeight - smoothDistance))
            {

                // We want to rise up before we move sideways.
                this.transform.position = Vector3.SmoothDamp(
                    this.transform.position,
                    new Vector3(this.transform.position.x, smoothHeight, this.transform.position.z),
                    ref velocity,
                    smoothTime);
            }
            else
            {
                // Normal movement (sideways)
                this.transform.position = Vector3.SmoothDamp(
                    this.transform.position,
                    new Vector3(targetPosition.x, smoothHeight, targetPosition.z),
                    ref velocity,
                    smoothTime);
            }

            //Debug.Log(this.gameObject + " inPosition status is" + inPosition);
        }
    }

        
    
    public void updateRaiseLowerTargets()
    {
        lowerFromPosition = new Vector3(targetPosition.x, targetPosition.y + 7, targetPosition.z);
        raiseToPosition = new Vector3(startingPosition.x, startingPosition.y + 7, startingPosition.z);
    }

    IEnumerator waitASecond()
    {
        
        yield return new WaitForSeconds(1);
    }

    public void pegToStartingPosition()
    {
        Debug.Log("Peg to starting postion");
        targetPosition = startingGameObject.transform.position;
    }
}
