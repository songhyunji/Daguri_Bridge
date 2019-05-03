using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {

	public Items_ itemSript;

	public enum inventoryEnum
	{
		InvenA,
		InvenB,
		InvenC
	};
	public inventoryEnum inventory = inventoryEnum.InvenA;

	public Items_ itemScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ItemUse()
	{
		itemScript.UseItem(this.gameObject);
		Debug.Log("Item Use");
	}
}
