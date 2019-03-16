using UnityEngine;
using System.Collections;

public class DelayDestroy : MonoBehaviour {
	public float delayTime = 2f;
	void Start () {
		Destroy(gameObject, delayTime);
	}
}
