using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour {

	private Transform _transform;

	public float moveSpeed;
	public GameObject player;

	private Transform playerTransform;

	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform>();
		playerTransform = player.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	private void Move()
	{
		transform.LookAt(playerTransform);
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

	private void OnMouseDown()
	{
		Destroy(this.gameObject);
		Debug.Log("click");
	}
}
