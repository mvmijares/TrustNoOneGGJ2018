using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public enum Identity {
    Alien, Player, NPC
}
public class HumanPlayer : MonoBehaviour {


    public InputDevice inputDevice { get; set; } //individual device set up in the player manager;
    public Identity playerIdentity { get; set; }

    public int playerIndex; //player number
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
