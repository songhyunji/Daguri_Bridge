using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_ : MonoBehaviour {
	
	public Sprite itemImg;
	public Items_ itemScript;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) // get Item
	{
		if(other.CompareTag("player"))
		{
			itemScript.GetItem(this.gameObject);
		}
	}

	private void OnMouseDown()
	{
		itemScript.GetItem(this.gameObject);
		Debug.Log("Item click");
	}
}
