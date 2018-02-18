using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;



//ALIEN


public class UFOMovement : MonoBehaviour {

    public int health = 3;
    public float playerSpeed;
    public float turnSpeed;
    public GameObject Beam;
    public float cooldownTime;
    public float timeStamp;

    public GameObject CaptureZoneRef;

    CaptureZone cz;

    public float speed;

    bool playerCap;
    public bool cappingPlayer;

    PlayerController player;

    float xVal;
    float yVal;

    GameBehaviour gameBehaviour;
    // Use this for initialization
    void Start () {

        player = GetComponent<PlayerController>();
        cz = CaptureZoneRef.GetComponent<CaptureZone>();

        Beam.SetActive(false);
        timeStamp = 0;

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager)
            gameBehaviour = gameManager.GetComponent<GameBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= 0) {
            gameBehaviour.SwitchToScene("UFOLoseScene");
        }
        xVal = player.x;
        yVal = player.z;

        transform.Translate(0, yVal * playerSpeed * Time.deltaTime, 0);
        transform.Rotate(-xVal * Vector3.forward * turnSpeed * Time.deltaTime);

        playerCap = cz.PlayerCaptured;

        if (timeStamp <= Time.time)
        {
            //add input for controller
            if (player.actionKey && playerCap==false)
            {
                Debug.Log("Pressed");
                cappingPlayer = true;
                CastBeam();
                timeStamp = Time.time + cooldownTime;
            }
        }

        if (Time.time >= timeStamp - 2 && playerCap == false)
        {
            Beam.SetActive(false);
            cappingPlayer = false;
        }

        if (playerCap == true)
        {
            CastBeam();
        }

	}

    public void TakeDamage(int damage) {
        health -= damage;
        if(health < 0) {
            gameBehaviour.SwitchToScene("UFOLoseScene");
        }
    }
    void CastBeam()
    {
        Beam.SetActive(true);
    }
}
