using UnityEngine;
using System.Collections;

public class Pitcher : MonoBehaviour {
    public GameManager gm;
    public GameObject ball;
    public Camera cam;
    Animator animator;

    Vector3 direction;
	void Start () {
        animator = GetComponent<Animator>();
    }

    void OnUpdateEffect()
    {
        //if (!gm.cam) return;
        //animator.SetLookAtWeight(1.0f);
        //if (ball)
        //{
        //    Vector3 targetPoint = ball.transform.position - cam.transform.position;
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint);

        //    direction = targetPoint;
        //    direction = direction.normalized;

        //    GetComponent<Rigidbody>().velocity = direction * 5;

           

        //    animator.SetLookAtPosition(ball.transform.position);
        //}
        //else if (gm.ball2)
        //{
        //    Vector3 targetPoint = gm.ball2.transform.position - gm.cam.transform.position;
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint);
        //    animator.SetLookAtPosition(gm.ball2.transform.position);
        //}
        //else
        //{
        //    animator.SetLookAtPosition(gm.batterPos);
        //}
    }

    void OnPitch()
    {
       // gm.SendMessage("OnPitch", SendMessageOptions.DontRequireReceiver);
    }

    void OnAnimatorIK()
    {
        //OnUpdateEffect();
    }
	
	void Update () {
	
	}
}
