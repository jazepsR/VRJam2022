using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiControl : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameMenu;
    public GameObject drone;
    float currentTime = 0;
    bool runningTimer = false;
    public TMP_Text timerText;
    public TMP_Text ringText;
    public static UiControl instance;
    public bool gameRunning = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startMenu.gameObject.SetActive(true);
        drone.gameObject.SetActive(false);
        gameMenu.gameObject.SetActive(false);
    }
    public void UpdateRingCount(int collected, int total )
    {
        ringText.text = "Rings: " + collected + "/" + total;
        if(collected == total)
        {
            runningTimer = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (runningTimer)
        {
            currentTime += Time.deltaTime;
        }
        timerText.text = "Time: " + ((int)currentTime / 60).ToString("00") + ":" + (currentTime % 60).ToString("00");
       /* if (currentTime / 60 > 0)
        {
            timerText.text = "Time: " + (currentTime / 60).ToString("00") + ":" + (currentTime % 60).ToString("00")
              ///  + ":" + ((currentTime % 60) * 100).ToString("00");
        }
        else
        {
            timerText.text = "Time: " +(currentTime % 60).ToString("00")
                   + ":" + ((currentTime % 60) * 100).ToString("00");

        }*/
    }
    private void OnSelect()
    {
        if (gameRunning)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameRunning = true;
        runningTimer = true;
        startMenu.gameObject.SetActive(false);
        drone.gameObject.SetActive(true);
        gameMenu.gameObject.SetActive(true);
        FindObjectOfType<Level>().StartLevel();
        // Debug.LogError("SELECT");
    }
}
