using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public Joystick joystick;
	public float moveSpeed;

	private Vector3 moveVector;
	private Rigidbody _rigidbody;
	

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		moveVector = InputVector();

		_rigidbody.AddForce(moveVector * moveSpeed);
	}

	private Vector3 InputVector()
	{
		Vector3 dir = Vector3.zero;

		dir.x = joystick.Horizontal();
		dir.z = joystick.Vertical();


		return dir;
	}
}
