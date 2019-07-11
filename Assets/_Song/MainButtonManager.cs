using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtonManager : MonoBehaviour {

	public Button gameStartBtn;
	public Button weapon1Btn;
	public Button weapon2Btn;
	public Button settingBtn;

	private bool gameStartBtnOn = false;

	public void gameStartBtnPress()
	{
		if(gameStartBtnOn)
		{
			gameStartBtnOn = false;
		}
		else
		{
			gameStartBtnOn = true;
		}
		weapon1Btn.gameObject.SetActive(gameStartBtnOn);
		weapon2Btn.gameObject.SetActive(gameStartBtnOn);
		settingBtn.GetComponent<Button>().enabled = !gameStartBtnOn;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
