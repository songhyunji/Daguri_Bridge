using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherAUTO : MonoBehaviour {

    // Use this for initialization

    
    private bool isPitching =true;
    private bool isThrow = false;
    private bool isAimming = false;
    private bool isPicking = false;
    private bool isShoot = false;
    private int Shootballcnt = 0;
    private int allmountball = 0;
   

    private Animator anim;
    private GameObject RightHand;

    public string Ballname = "Ball";
    public GameObject Ball;

    public UILabel UIballcnt;
    public Camera cam;
    public float speed = 1f;
    public float throwPower = 3f;
    


    AnimatorStateInfo anistate;
    // GameObject ball;
    //Rigidbody rigidbody;

    void Start () {

        anim = GetComponent<Animator>();
        
        //rigidbody = GetComponent<Rigidbody>();
       // ball = transform.Find("Ball").gameObject;
        //ball.SetActive(false)
	}

    void Aim()
    {
        //if (!isAiming)
        //    return;
        Debug.DrawRay(transform.position, -Vector3.forward * 1000f, Color.red);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        float rayLength = 500f;
        int floorMask = LayerMask.GetMask("BatBoundary");

        if (Physics.Raycast(ray, out rayHit, rayLength, floorMask))
        {
            Debug.DrawRay(cam.transform.position, rayHit.point, Color.red);

            Vector3 playerToMouse = rayHit.point - cam.transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //rigidbody.MoveRotation(newRotation);
        }
    }

    void Drop()
    {
        GameObject item = Ball.GetComponentInChildren<Rigidbody>().gameObject;
        Rigidbody rigidbody = item.GetComponent<Rigidbody>(); //아이템 호출

        //Detach Item
        //SetEquip(item, false);    //작성자가 만든 함수 같음. 
        Ball.transform.DetachChildren(); //종속 관계 해제

        Debug.Log("a");


        //RaycastHit For Throw Angle 
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        float rayLength = 500f;
        int floorMask = LayerMask.GetMask("Floor");
        Vector3 throwAngle;

        //ThrowAngle : RaycastHit
        if (Physics.Raycast(ray, out rayHit, rayLength, floorMask))
        {
            throwAngle = rayHit.point - Ball.transform.position;
        }
        else
        {
            throwAngle = transform.forward * 50f;
        }

        //Throw Item
        throwAngle.y = 25f;
        rigidbody.AddForce(throwAngle * throwPower, ForceMode.Impulse);

        // anim.SetTrigger("doThrow");
        isAimming = false;
        isPicking = false;
    }

    void AllBallUI()
    {
        UIballcnt.text = " /" + allmountball.ToString();
    }

    void Shoot()
    {
        GameObject Ball = ObjectPool.Instance.PopFromPool(Ballname);
        GameObject RightHand = GameObject.FindWithTag("RightHand");
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Ball.transform.position = RightHand.transform.position;  //여기서 손목을 부를까 ?


        Ball.SetActive(true);
    }
    void Countball()
    {
        ++Shootballcnt;
        
    }

    // Update is called once per frame
    void Update () {

        //isPitching = true;    //한번만하게 주석처리

        anistate = anim.GetCurrentAnimatorStateInfo(0);

        if (anistate.IsName("Pitch") && anistate.normalizedTime >=0.45f ) //피칭 시작할때 공을 만들어줘보자
        {
            isShoot = true;
            Countball();
        }
        if (anistate.IsName("Pitch") && anistate.normalizedTime >= 1f) //피칭이 끝날때.
        {
            isPitching = false;
            //isThrow = true;
            //Ball.SetActive(false);
            Shootballcnt = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isPitching = true;
            //Shoot();
        }

        if (isThrow)
        {
            Drop();
        }
        if(isShoot && Shootballcnt == 1)
        {
            Shoot();
            ++allmountball;
            isShoot = false;
        }

        AllBallUI();
        Aim();


        //if(isThrow)
        //Ball.transform.Translate(cam.transform.forward * speed * Time.deltaTime);


        anim.SetBool("isPitch", isPitching);

        
    }
   
}
