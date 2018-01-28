using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;



//ALIEN


public class UFOMovement : MonoBehaviour {


    public float playerSpeed;
    public float turnSpeed;
    public GameObject Beam;
    public float cooldownTime;
    private float timeStamp;

    public GameObject CaptureZoneRef;

    CaptureZone cz;

    public float speed;

    bool playerCap;

    

    // Use this for initialization
    void Start () {


        cz = CaptureZoneRef.GetComponent<CaptureZone>();

        Beam.SetActive(false);
        timeStamp = 0;
	}
	
	// Update is called once per frame
	void Update () {

        
        MoveForward();
        Turn();
        MoveBack();

        playerCap = cz.PlayerCaptured;

        print(playerCap);

        if (timeStamp <= Time.time)
        {
            //add input for controller
            if (Input.GetKey(KeyCode.A) && playerCap==false)
            {
                CastBeam();
                timeStamp = Time.time + cooldownTime;
            }
        }

        if (Time.time >= timeStamp-2 && playerCap == false)
        {
            Beam.SetActive(false);
        }

	}

    
    void CastBeam()
    {
        Beam.SetActive(true);
    }


    void MoveForward()
    {      //add input for controller
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
        }
    }

    void MoveBack()
    {       //add input for controller
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
        }
    }

    void Turn()
    {      //add input for controller
        if (Input.GetKey("right")) //Right arrow key to turn right
        {
            transform.Rotate(-Vector3.forward * turnSpeed * Time.deltaTime);
        }
        //add input for controller
        if (Input.GetKey("left"))//Left arrow key to turn left
        {
            transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
        }
    }

    




}
