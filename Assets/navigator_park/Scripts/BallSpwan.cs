using UnityEngine;
using System.Collections;

public class BallSpwan : MonoBehaviour {
    public GameObject ball;

    public float MIN_TIME = 1;
    public float MAX_TIME = 5;
	// Use this for initialization
	void Start () {
        StartCoroutine("CreateBall");
	}

    IEnumerator CreateBall()
    {
        while(Application.isPlaying)
        {
            float createTime = Random.Range(MIN_TIME, MAX_TIME);
            yield return new WaitForSeconds(createTime);

            Instantiate(ball, transform.position, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
