using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public enum Identity {
    Alien, Player, NPC
}
public class HumanPlayer : MonoBehaviour {

    Rigidbody rb;
    public KeyboardControllerSet inputDevice { get; set; } //individual device set up in the player manager;
    public Identity playerIdentity { get; set; }

    public int playerIndex; //player number

    public float leftHorizontal;// left stick, horizontal axis
    public float leftVertical;// left stick, vertical axis

    public float rightHorizontal;// left stick, horizontal axis
    public float rightVertical;// left stick, vertical axis

    public bool buttonA;
    public bool sprint;
    public bool debugMode;
    public Camera cam;

    public float speed = 5f;

    public bool isCaptured;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        isCaptured = false;
    }

    // Update is called once per frame
    void Update () {
        if (!debugMode) {
            if (inputDevice != null) {
                //if (!isCaptured) { 
                //    leftHorizontal = inputDevice.LeftStickX.Value;
                //    leftVertical = inputDevice.LeftStickY.Value;
                //}
                //rightHorizontal = inputDevice.RightStickX.Value;
                //rightVertical = inputDevice.RightStickY.Value;

                //sprint = inputDevice.RightTrigger.IsPressed;

                //buttonA = inputDevice.Action1.IsPressed;


                leftHorizontal = inputDevice.Movement.Value.x;
                leftVertical = inputDevice.Movement.Value.y;

                rightHorizontal = inputDevice.Camera.Value.x;
                rightVertical = inputDevice.Camera.Value.y;

                sprint = inputDevice.Sprint.IsPressed;

                buttonA = inputDevice.Action1.IsPressed;

                Debug.Log("EVERYTHING " + inputDevice.Device + " " + leftHorizontal + " " + leftVertical + " " + rightHorizontal + " " + rightVertical + " " + sprint + " " + buttonA);
            }
        } else {
            leftHorizontal = InputManager.ActiveDevice.LeftStickX.Value;
            leftVertical = InputManager.ActiveDevice.LeftStickY.Value;

            rightHorizontal = InputManager.ActiveDevice.RightStickX.Value;
            rightVertical = InputManager.ActiveDevice.RightStickY.Value;

            sprint = InputManager.ActiveDevice.RightTrigger.IsPressed;

            //leftHorizontal = inputDevice.Movement.Value.x;
            //leftVertical = inputDevice.Movement.Value.y;

            //rightHorizontal = inputDevice.Camera.Value.x;
            //rightVertical = inputDevice.Camera.Value.y;

            //sprint = inputDevice.Sprint.IsPressed;

            //buttonA = inputDevice.Action1.IsPressed;
        }
    }
}
