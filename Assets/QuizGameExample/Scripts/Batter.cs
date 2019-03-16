using UnityEngine;
using System.Collections;

public class Batter : MonoBehaviour {
    public GameManager gm;
    Animator animator;
    public bool ikActive = false;
    public Transform rightHandObj = null;

	void Start () {
        animator = GetComponent<Animator>();
    }

    void OnUpdateEffect()
    {
        if (!gm.cam) return;
        animator.SetLookAtWeight(1.0f);
        if (gm.ball)
        {
            Vector3 targetPoint = gm.ball.transform.position - gm.cam.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint);
            animator.SetLookAtPosition(gm.ball.transform.position);
        }
        else if (gm.ball2)
        {
            Vector3 targetPoint = gm.ball2.transform.position - gm.cam.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint);
            animator.SetLookAtPosition(gm.ball2.transform.position);
        }
        else
        {
            animator.SetLookAtPosition(gm.pitcherPos);
        }
    }

    void OnAnimatorIK()
    {
        OnUpdateEffect();
        if (animator)
        {
            if (ikActive)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
                if (rightHandObj != null)
                {
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }

	void Update () {
	
	}
}
