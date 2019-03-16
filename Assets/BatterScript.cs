using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterScript : MonoBehaviour {

    // Use this for initialization

    private Animator anim;
    private Transform tr;
    private bool isBatting = false;
    public float speed = 10f;
    private float rotLeftRight;
    private float rotUpDown;
    public float mouseSensitivity = 2f;
    private float verticalRotation;


 



    AnimatorStateInfo anistate;
	void Start () {

        //tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
	}

    void updateAnistate()
    {
        anistate = anim.GetCurrentAnimatorStateInfo(0);
        if (anistate.IsName("Bat") && anistate.normalizedTime >= 1f)
        {
            isBatting = false;
        }
        //Debug.Log(isBatting);

        anim.SetBool("isBatting", isBatting);
    }



    // Update is called once per frame
    void Update () {


        if (Input.GetKey(KeyCode.Space))
        {
            isBatting = true;
        }
        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(Vector3.right * speed *Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(-Vector3.right * speed * Time.deltaTime);
        //}
        //rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        //transform.Rotate(0f, rotLeftRight, 0f);


        ////verticalRotation = Input.GetAxis("Mouse Y") * mouseSensitivity;
        ////transform.Rotate(0f, 0f, verticalRotation);
        //rotUpDown = Input.GetAxis("Mouse Y") * mouseSensitivity;
        //transform.Rotate(0f, rotUpDown, 0f);


        //transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Vector3 mPosition = Input.mousePosition; // 마우스 좌표저장
        //Vector3 oPosition = transform.position; // 게임오브젝트 좌표저장

        ////카메라가 앞면에서 뒤로 보고 있기 때문에 마우스 Poisition의 z 축 정보 
        //// 게임 오브젝트와 카메라와의 z축 차이를 입력 

        //mPosition.y = oPosition.y - Camera.main.transform.position.y;

        //Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        //float dz = target.z - oPosition.z;
        //float dx = target.x - oPosition.x;

        //float rotateDegree = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f,0f,rotateDegree);




        updateAnistate();


    }
}
