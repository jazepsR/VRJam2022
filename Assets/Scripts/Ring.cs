using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "drone")
        {
            Destroy(gameObject);
        }

        if (finish)
        {
            // TODO: finish the game
        }
    }

    /*public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "drone")
        {
            Destroy(gameObject);
        }
    }*/
}
