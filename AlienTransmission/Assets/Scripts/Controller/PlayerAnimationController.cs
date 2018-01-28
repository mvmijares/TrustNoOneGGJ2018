using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    HumanPlayer player;

    public bool abducted;
    public bool isfalling;

    float speed = 0f;

    float xVal;
    float yVal;
    Animator anim;
    private void Awake() {
        player = GetComponent<HumanPlayer>();
        anim = GetComponent<Animator>();

    }
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (!abducted || !isfalling) {
            xVal = player.leftHorizontal;
            yVal = player.leftVertical;

            if (player.sprint) {
                anim.SetBool("IsRunning", true);
            } else
                anim.SetBool("IsRunning", false);

            if (yVal > 0.1f || yVal < -0.1f) {
                speed = 2f;
            } else {
                speed = 0f;
            }
           
        }else if (abducted) {
            anim.SetBool("Abducted", true);
        }else if (isfalling) {
            anim.SetBool("IsFalling", true);
        }

        anim.SetFloat("Speed", speed);
    }

}
