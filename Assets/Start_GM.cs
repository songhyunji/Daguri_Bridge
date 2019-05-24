using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_GM : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void StartGame()
    {
        //Application.LoadLevel("1_play");
        SceneManager.LoadScene("1_play");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
