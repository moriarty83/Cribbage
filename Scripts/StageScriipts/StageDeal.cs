using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDeal : MonoBehaviour
{
    public bool dealStageOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doneDealing()
    {
        dealStageOver = true;
    }

    public void readyToDeal()
    {
        dealStageOver = false;
    }

}
