using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;



//ALIEN


public class CaptureZone : MonoBehaviour {

    
    public Transform holdingPoint;
    public Transform CapturePoint;
    GameObject CapturePlayer;
    public bool PlayerCaptured;

    public float cooldownTime;
    private float timeStamp;

    public float speed;

    public GameObject Ufo;
    UFOMovement ufoRefrence;

    public bool capAttack;

	
	void Start () {
        PlayerCaptured = false;

        ufoRefrence = Ufo.GetComponent<UFOMovement>();
        capAttack = ufoRefrence.cappingPlayer;
        //timeStamp = ufoRefrence.timeStamp;
    }
	
	
	void Update () {

        timeStamp = ufoRefrence.timeStamp;
        capAttack = ufoRefrence.cappingPlayer;


        if (PlayerCaptured)
        {
            float step = speed * Time.deltaTime;
            CapturePlayer.transform.position = Vector3.MoveTowards(CapturePlayer.transform.position, holdingPoint.position, step);

            CapturePlayer.transform.position = holdingPoint.transform.position;
                                   
        }
              

    }

    void OnTriggerStay(Collider other)
    {

         //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>add input for controller
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.A) && capAttack ==true)
        {
            CapturePlayer = other.gameObject;
            PlayerCaptured = true;
             
            
        }

        if (other.gameObject.tag == "DropOff" && PlayerCaptured)
        {
            
            CapturePlayer.transform.position = CapturePoint.transform.position;
            PlayerCaptured = false;
            

        }

    }
}
