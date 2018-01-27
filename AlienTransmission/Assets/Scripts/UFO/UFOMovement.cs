using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour {


    public float playerSpeed;
    public float turnSpeed;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



        MoveForward();
        Turn();


	}



    void MoveForward()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
        }
    }

    void Turn()
    {
        if (Input.GetKey("right")) //Right arrow key to turn right
        {
            transform.Rotate(-Vector3.forward * turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey("left"))//Left arrow key to turn left
        {
            transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
        }
    }



  
}
