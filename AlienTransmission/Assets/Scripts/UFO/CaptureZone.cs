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

    HumanPlayer player;
	void Start () {
        PlayerCaptured = false;

        ufoRefrence = Ufo.GetComponent<UFOMovement>();
        player = ufoRefrence.transform.GetComponent<HumanPlayer>();
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

    private void FixedUpdate() {
        if (player.buttonA && capAttack == true && !PlayerCaptured && CapturePlayer == null) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 3.0f);
            foreach(Collider col in colliders) {
                if(col.gameObject.layer == LayerMask.NameToLayer("Player")) {
                    CapturePlayer = col.gameObject;
                    col.gameObject.GetComponent<PlayerStateController>().setState(MINDSTATES.ABDUCTED);
                    PlayerCaptured = true;
                }
            }
        }
        if (PlayerCaptured) {
            if (player.buttonA) {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 3.0f);
                foreach (Collider col in colliders) {
                    if (col.gameObject.layer == LayerMask.NameToLayer("Capture")) {
                        CapturePlayer.GetComponent<HumanPlayer>().isCaptured = true;
                        CapturePlayer.transform.position = CapturePoint.transform.position;
                        CapturePlayer = null;
                        PlayerCaptured = false;
              
                    }
                }
               
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 3.0f);
    }

   
}
