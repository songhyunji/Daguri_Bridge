using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsControl : MonoBehaviour {

	public GameObject Items;
	private bool isPress = false;

	public void InventoryBtnPress()
	{
		if(isPress)
		{
			Items.SetActive(true);
			isPress = false;
		}
		else
		{
			Items.SetActive(false);
			isPress = true;
		}
	}
}
