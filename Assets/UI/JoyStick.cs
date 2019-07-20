using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    public Transform Stick;

    GameObject player;

    private Vector3 StickFirstPos; //조이스틱 처음 위치
    private Vector3 JoyVec; //조이스틱의 벡터
    private float Radius;   //반지름
    static bool MoveFlag;  // 플레이어 움직임 스위치
    public float speed = 4f;

    // Use this for initialization
    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        MoveFlag = false;
    }

    void Update()
    {
        if (MoveFlag)
        {
            //player.transform.Translate(Vector3.forward * Time.deltaTime * 1000f);
            player.transform.Translate(0, 0, speed * Time.deltaTime);
            //player.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
    }

    public void Drag(BaseEventData _Data)
    {
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        player = GameObject.FindGameObjectWithTag("Player");

        JoyVec = (Pos - StickFirstPos).normalized;
        float Dis = Vector3.Distance(Pos, StickFirstPos);

        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        else
            Stick.position = StickFirstPos + JoyVec * Radius;

        player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
        MoveFlag = true;
    }

    public void DragEnd()
    {
        Stick.position = StickFirstPos;
        JoyVec = Vector3.zero;
        MoveFlag = false;
    }

} 
