using UnityEngine;
using System.Collections;

public class Enable : MonoBehaviour {

    public GameObject objectToDisable;
    public static bool disabled = true;

    public void changeDisabled() { disabled = false; }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (disabled)
            objectToDisable.SetActive(false);
        else
            objectToDisable.SetActive(true);
	
	}
}
