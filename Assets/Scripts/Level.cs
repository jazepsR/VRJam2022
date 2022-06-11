using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level Data", order = 1)]
public class Level : MonoBehaviour
{
    public string levelName;
    public List<Ring> rings;
    public int activeRing = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        foreach(Ring ring in rings)
        {
            ring.level = this;
            ring.gameObject.SetActive(false);
        }
    }
    public void StartLevel()
    {
        UpdateRings();
        UiControl.instance.UpdateRingCount(activeRing, rings.Count);
    }

    public void CollectRing()
    {
        activeRing++;
        UpdateRings();
        UiControl.instance.UpdateRingCount(activeRing,rings.Count);
    }

    public void UpdateRings()
    {
        for(int i = 0; i<rings.Count;i++)
        {
            if(activeRing == i || activeRing + 1 == i)
            {
                rings[i].OnEnable();
            }
            else
            {
                rings[i].Disable();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
