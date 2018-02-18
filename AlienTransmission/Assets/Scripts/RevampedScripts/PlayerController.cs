using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
/// <summary>
/// This is a test script for the Player Scene
/// We will be using Player Manager to assign the controls to each player
/// </summary>
public class PlayerController : MonoBehaviour {

    public float x;
    public float y;
    public float z;

    public float rightAnalogHorizontal;
    public float rightAnalogVertical;

    public bool actionKey;

    public bool keyboard;
    public bool lShift;
        
    PlayerMovement playerMovement;
                               
    Player player;

    void Start() {

        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        if(player.keyboardDevice != null)
            KeyInputs();
    }
    void KeyInputs() {
        x = player.keyboardDevice.Movement.Value.x;
        z = player.keyboardDevice.Movement.Value.y;
        actionKey = player.keyboardDevice.Action1.IsPressed;

        if (playerMovement)
            playerMovement.SetDirection(new Vector3(player.keyboardDevice.Movement.Value.x, 0, player.keyboardDevice.Movement.Value.y));
   
        CameraControls();
    }
    void CameraControls() {
        if (player.keyboardDevice.LookLeft) {
            rightAnalogHorizontal = -1;
        } else if(player.keyboardDevice.LookRight){
            rightAnalogHorizontal = 1;
        } else {
            rightAnalogHorizontal = 0;
        }

        if (player.keyboardDevice.Down) {
            rightAnalogVertical = -1;
        } else if (player.keyboardDevice.Up) {
            rightAnalogVertical = 1;
        } else {
            rightAnalogVertical = 0;
        }
    }
}

