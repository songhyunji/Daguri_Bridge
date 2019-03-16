using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterMoveMent : MonoBehaviour {

    // Use this for initialization

    Rigidbody rigidBody;
    public float speed = 4;

    Vector3 lookPos;

	void Start () {

        rigidBody = GetComponent<Rigidbody>();
	}

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }
        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir, Vector3.up);
        
    }
    // Update is called once per frame
    void FixedUpdate () {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0 , vertical);

        rigidBody.AddForce(movement * speed / Time.deltaTime);
	}
}
