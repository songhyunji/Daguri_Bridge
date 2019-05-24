using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {


    public string pooltemName = "Ball";
    public float moveSpeed = -20f;
    public float lifeTime = 30f;
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

	// Update is called once per frame
	void Update ()
    {
        transform.Translate(transform.forward *moveSpeed * Time.deltaTime);
        if (GetTimer() > lifeTime)
        {
            SetTimer();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //gameObject.SetActive(false);
            
            //GetComponent<Rigidbody>().
            ObjectPool.Instance.PushToPool(pooltemName, gameObject);
            
        }
	}
   
}
