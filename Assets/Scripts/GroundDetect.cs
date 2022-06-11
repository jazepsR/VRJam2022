using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    public bool flying;
    // Start is called before the first frame update
    public void OnTriggerEnter()
    {
        flying = false;
    }

    public void OnTriggerExit()
    {
        flying = true;
    }
}
