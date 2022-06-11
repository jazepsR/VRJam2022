using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [HideInInspector] public Level level;
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
            level.CollectRing();
            Disable();

        }

       
    }
    public void OnEnable()
    {
        gameObject.SetActive(true);

    }

    public void Disable()
    {
        gameObject.SetActive(false);
      //  GetComponent<Animator>().SetTrigger("collect");
       // GetComponent<BoxCollider>().enabled = false;
    }
    /*public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "drone")
        {
            Destroy(gameObject);
        }
    }*/
}
