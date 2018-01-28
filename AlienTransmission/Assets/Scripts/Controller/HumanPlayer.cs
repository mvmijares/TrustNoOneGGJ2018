using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public enum Identity {
    Alien, Player, NPC
}
public class HumanPlayer : HumanMindBase {

    Rigidbody rb;
    public InputDevice inputDevice { get; set; } //individual device set up in the player manager;
    public Identity playerIdentity { get; set; }

    public int playerIndex; //player number

    public float leftHorizontal;// left stick, horizontal axis
    public float leftVertical;// left stick, vertical axis

    public float rightHorizontal;// left stick, horizontal axis
    public float rightVertical;// left stick, vertical axis

    public bool sprint;
    public bool debugMode;
    public Camera cam;

    public float speed = 5f;
    
    protected override void Start() {
        base.Start();

        rb = GetComponent<Rigidbody>();
    }


    protected override void onEnterState(MINDSTATES state) {

    }

    protected override void onExitState(MINDSTATES state) {

    }

    // Update is called once per frame
    void Update () {
        if (!debugMode) {
            if (inputDevice != null) {
                leftHorizontal = inputDevice.LeftStickX.Value;
                leftVertical = inputDevice.LeftStickY.Value;

                rightHorizontal = inputDevice.RightStickX.Value;
                rightVertical = inputDevice.RightStickY.Value;

                sprint = inputDevice.RightTrigger.IsPressed;
            }
        } else {
            leftHorizontal = InputManager.ActiveDevice.LeftStickX.Value;
            leftVertical = InputManager.ActiveDevice.LeftStickY.Value;

            rightHorizontal = InputManager.ActiveDevice.RightStickX.Value;
            rightVertical = InputManager.ActiveDevice.RightStickY.Value;

            sprint = InputManager.ActiveDevice.RightTrigger.IsPressed;
        }
    }
}
