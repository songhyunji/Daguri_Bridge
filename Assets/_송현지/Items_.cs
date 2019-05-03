using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items_ : MonoBehaviour {

	public GameObject InvenA;
	public GameObject InvenB;
	public GameObject InvenC;
	public GameObject weapon;

	private Image spriteA;
	private Image spriteB;
	private Image spriteC;
	private Image weaponImg;

	private bool emptyA = true;
	private bool emptyB = true;
	private bool emptyC = true;
	

	// Use this for initialization
	void Start () {
		spriteA = InvenA.GetComponent<Image>();
		spriteB = InvenB.GetComponent<Image>();
		spriteC = InvenC.GetComponent<Image>();
		weaponImg = weapon.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetItem(GameObject item)
	{
		Sprite itemImg = item.GetComponent<Item_>().itemImg;
		if (emptyA && emptyB && emptyC)
		{
			Debug.Log("A에 stack");
			emptyA = false;
			spriteA.sprite = itemImg;
		}
		else if(!emptyA && emptyB && emptyC)
		{
			Debug.Log("B에 stack");
			emptyB = false;
			spriteB.sprite= itemImg;
		}
		else if(!emptyA && !emptyB && emptyC)
		{
			Debug.Log("C에 stack");
			emptyC = false;
			spriteC.sprite = itemImg;
		}
		else
		{
			Debug.Log("C에 stack, A delete");
			spriteA.sprite = spriteB.sprite;
			spriteB.sprite = spriteC.sprite;
			spriteC.sprite = itemImg;
		}

		Destroy(item);
	}

	public void UseItem(GameObject inventoryItem)
	{
		InventoryItem.inventoryEnum invenName = inventoryItem.GetComponent<InventoryItem>().inventory;
		if (invenName == InventoryItem.inventoryEnum.InvenA && !emptyA)
		{
			weaponImg.sprite = spriteA.sprite;
			if(emptyB) // inventory A에만 item
			{
				emptyA = true;
				spriteA.sprite = null;
			}
			else if(emptyC) // inventory A, B에만 item
			{
				emptyB = true;
				spriteA.sprite = spriteB.sprite;
				spriteB.sprite = null;
			}
			else    // inventory A, B, C에 item
			{
				emptyC = true;
				spriteB.sprite = spriteC.sprite;
				spriteC.sprite = null;
			}
		}
		else if(invenName == InventoryItem.inventoryEnum.InvenB && !emptyB)
		{
			weaponImg.sprite = spriteB.sprite;
			if (emptyC) // inventory A, B에만 item
			{
				emptyB = true;
				spriteA.sprite = spriteB.sprite;
				spriteB.sprite = null;
			}
			else    // inventory A, B, C에 item
			{
				emptyC = true;
				spriteB.sprite = spriteC.sprite;
				spriteC.sprite = null;
			}
		}
		else if(invenName == InventoryItem.inventoryEnum.InvenC && !emptyC)
		{
			weaponImg.sprite = spriteC.sprite;
			emptyC = true;
			spriteC.sprite = null;
		}
		else
		{
			Debug.Log("empty inventory");
		}
	}


}
