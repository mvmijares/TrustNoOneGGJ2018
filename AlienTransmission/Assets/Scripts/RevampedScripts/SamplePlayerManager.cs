using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SamplePlayerManager : MonoBehaviour {
    public Player player; // Player
    List<Player> players; //input devices that are currently registered

    List<InputDevice> devices;
    // Use this for initialization
    void Start () {
        players = new List<Player>();

        players.Add(player);
        devices = new List<InputDevice>();
        InitializeKeyboardControls();
    }
	void InitializeKeyboardControls() { // initializes controls to keyboard
        Key[] keyboardInput = new Key[] { Key.A, Key.D, Key.W, Key.S, Key.E, Key.Q, Key.UpArrow, Key.DownArrow, Key.LeftArrow, Key.RightArrow };

        KeyboardControllerSet keyboardSet = new KeyboardControllerSet();
        keyboardSet.Left.AddDefaultBinding(keyboardInput[0]);
        keyboardSet.Right.AddDefaultBinding(keyboardInput[1]);
        keyboardSet.Forward.AddDefaultBinding(keyboardInput[2]);
        keyboardSet.Backward.AddDefaultBinding(keyboardInput[3]);

        keyboardSet.Sprint.AddDefaultBinding(keyboardInput[4]);
        keyboardSet.Action1.AddDefaultBinding(keyboardInput[5]);

        keyboardSet.Up.AddDefaultBinding(keyboardInput[6]);
        keyboardSet.Down.AddDefaultBinding(keyboardInput[7]);
        keyboardSet.LookLeft.AddDefaultBinding(keyboardInput[8]);
        keyboardSet.LookRight.AddDefaultBinding(keyboardInput[9]);

        
        player.usingKeyboard = true;
        player.keyboardDevice = keyboardSet;

    }
    // Update is called once per frame
    void Update () {
        InputDevice activeInputDevice = InputManager.ActiveDevice; // active input device being pressed
        if (activeInputDevice.AnyButtonIsPressed) {
            if (FindInputDevice(activeInputDevice) == null) { // checks if the input device is already on the list
                devices.Add(activeInputDevice);
                CreateNewController(activeInputDevice);
            }
        }
    }
    /// <summary>
    /// This function checks all the current registered devices. If it doesnt find one, it returns an empty variable
    /// </summary>
    /// <param name="device">
    /// Deivce that is being checked
    /// </param>
    /// <returns></returns>
    InputDevice FindInputDevice(InputDevice device) {
        foreach (InputDevice d in devices) {
            if (d == device) {
                return d;
            }
        }
        return null;
    }

    void CreateNewController(InputDevice device) {
        KeyboardControllerSet customSet = new KeyboardControllerSet(); // uses same player actions as keyboard
        customSet.Device = device;

        customSet.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        customSet.Right.AddDefaultBinding(InputControlType.LeftStickRight);

        customSet.Forward.AddDefaultBinding(InputControlType.LeftStickUp);
        customSet.Backward.AddDefaultBinding(InputControlType.LeftStickDown);
        customSet.Sprint.AddDefaultBinding(InputControlType.RightTrigger);
        customSet.Action1.AddDefaultBinding(InputControlType.Action1);

        customSet.Up.AddDefaultBinding(InputControlType.RightStickUp);
        customSet.Down.AddDefaultBinding(InputControlType.RightStickDown);
        customSet.LookLeft.AddDefaultBinding(InputControlType.RightStickLeft);
        customSet.LookRight.AddDefaultBinding(InputControlType.RightStickRight);

        player.keyboardDevice = customSet;
    }
}
