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

	
	void Start () {
        PlayerCaptured = false;

        ufoRefrence = Ufo.GetComponent<UFOMovement>();
        timeStamp = 0;
    }
	
	
	void Update () {



        if (timeStamp <= Time.time)
        {
            //add input for controller<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<---------------------------------------------
            if (Input.GetKey(KeyCode.A))
            {
                
                timeStamp = Time.time + cooldownTime;
            }
        }

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
        if (other.gameObject.tag == "Player" && Input.GetKeyUp(KeyCode.A) && timeStamp <=Time.time)
        {

            CapturePlayer = other.gameObject;

            PlayerCaptured = true;
            timeStamp = Time.time + cooldownTime;

            print("working");
            
        }

        if (other.gameObject.tag == "DropOff" && PlayerCaptured)
        {
            
            CapturePlayer.transform.position = CapturePoint.transform.position;
            PlayerCaptured = false;
            

        }

    }
}
