using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public float runSpeed;

    GameObject player;
    UnityEngine.AI.NavMeshAgent nav;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = runSpeed;
        //nav.enabled = false;
       
	}
	
	// Update is called once per frame
	void Update () {
        //nav.enabled = true;
       // Vector3 a = player.transform.position;
        //nav.SetDestination(a);
        nav.SetDestination(player.transform.position);
        Debug.Log("dq");
	}
}
