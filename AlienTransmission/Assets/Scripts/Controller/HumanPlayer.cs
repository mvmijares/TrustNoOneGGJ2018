using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class HumanPlayer : MonoBehaviour {


    public InputDevice inputDevice { get; set; } //individual device set up in the player manager;

    float leftHorizontal; // left stick, horizontal axis
    float leftVertical; // left stick, vertical axis

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (inputDevice != null) {
            leftHorizontal = inputDevice.LeftStickX.Value;
            leftVertical = inputDevice.LeftStickY.Value;
        }

    }
}
