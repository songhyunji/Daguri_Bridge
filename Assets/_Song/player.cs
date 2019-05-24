using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Joystick joystick;
	public float moveSpeed;
	public GameObject hpSlider;
	[Tooltip("적의 공격 피해량 (0-1)")]
	public float damage = 0.05f;

	private Vector3 moveVector;
	private Rigidbody _rigidbody;
	private Slider hpSliderScript;
	private Slider progressSliderScript;


	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		hpSliderScript = hpSlider.GetComponent<Slider>();
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

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			hpSliderScript.value -= damage;
		}
	}
}
