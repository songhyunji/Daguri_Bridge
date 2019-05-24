using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	
	public float moveSpeed;
	public GameObject player;
	[Tooltip("진행 상황 바 스크립트")]
	public ProgressBar_ progressBarScript;

	private Transform _transform;
	private Transform playerTransform;

	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform>();
		playerTransform = player.GetComponent<Transform>();
		progressBarScript = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<ProgressBar_>();
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
		progressBarScript.currentAmount--;
		Destroy(this.gameObject);
		Debug.Log("click");
	}
}
