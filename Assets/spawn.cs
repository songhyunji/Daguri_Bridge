using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {


    public string Ballname = "Ball";
    public GameObject Ball;
    public float speed = 1f;
    public float throwPower = 3f;
    public float _elapsedTime = 30f;


    // Use this for initialization
    void Start () {
		
	}

    float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }
    void SetTimer()
    {
        _elapsedTime = 0f;
    }

    void Shoot()
    {
        GameObject Ball = ObjectPool.Instance.PopFromPool(Ballname);
       
        Ball.transform.position = transform.position;  


        Ball.SetActive(true);
    }

    // Update is called once per frame
    void Update () {

        if (GetTimer() > 1.0f)
        {

            Shoot();
        }

	}
}
