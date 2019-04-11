using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar_ : MonoBehaviour {

	[Tooltip("총 적의 수")]
	public float enemiesAmount;
	[Tooltip("현재 적의 수")]
	public float currentAmount;

	private Slider sliderScript;

	[SerializeField]
	private float enemyValue;

	void Start () {

		sliderScript = GetComponent<Slider>();
		enemiesAmount = GameObject.FindGameObjectsWithTag("Enemy").Length;
		currentAmount = enemiesAmount;
	}
	
	void Update () {

		enemyValue = currentAmount / enemiesAmount;
		sliderScript.value = enemyValue;
	}
}
