using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    Enable en = new Enable();
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();

             Destroy(other.gameObject);

            en.changeDisabled();
        }
    }
}
