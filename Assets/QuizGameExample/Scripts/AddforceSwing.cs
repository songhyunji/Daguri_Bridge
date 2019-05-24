using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddforceSwing : MonoBehaviour {


    public UILabel stLabel;
    private UILabel tmp;

    public GameObject explosion;
    int Hitballcnt = 0;
    //GameObject TextBallcnt = GameObject.FindGameObjectWithTag("Label");

    
    // Use this for initialization
    void Start () {
        //tmp = TextBallcnt.guiText
        // tmp.text = " ";
        
    }

    void HitBallUI()
    {
        
        ++Hitballcnt;
        stLabel.text = Hitballcnt.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        //Vector3 direction = transform.position - collision.gameObject.transform.position;
        // direction = direction.normalized * 1000;
        //direction = Vector3.Cross(direction, Yforce);
        Vector3 Yforce = new Vector3(0f,1.0f,0f);
        Yforce = Yforce.normalized *104;
        
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Yforce); //잘안날라가서 넣어줌
        collision.gameObject.GetComponent<Rigidbody>().useGravity = true;

        HitBallUI(); //맞은볼갯수출력
        
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
