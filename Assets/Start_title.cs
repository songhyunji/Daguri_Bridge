using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Start_title : MonoBehaviour {

    // Use this for initialization
    public GameObject stopsign;
    private bool isStop = false;
	void Start () {
		
	}
    void InitGame()
    {
        ReleaseGame();
        SceneManager.LoadScene("0_start");
        
    }
    void PauseGame()
    {
        if (isStop)
        {
            ReleaseGame();  
        }
        else
        {
            StopGame();
        }       
    }
    void ReleaseGame()
    {
        Time.timeScale = 1.0f;
        isStop = false;
        stopsign.SetActive(false);
    }
    void StopGame()
    {
        Time.timeScale = 0.0f;
        isStop = true;
        stopsign.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
