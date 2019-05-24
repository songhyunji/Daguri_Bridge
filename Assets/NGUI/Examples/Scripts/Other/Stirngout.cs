using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stirngout : MonoBehaviour {

    // Use this for initialization

    public UILabel stLabel;
	void Start () {

        string stText = stLabel.text;
        Debug.Log(stText.Substring(0, 2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
