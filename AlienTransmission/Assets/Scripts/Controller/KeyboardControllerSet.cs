using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class KeyboardControllerSet : PlayerActionSet {

    public bool isBinded;
    public bool isKeyboard;

    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Forward;
    public PlayerAction Backward;
    public PlayerAction Sprint;

    public PlayerAction Up;
    public PlayerAction Down;
    public PlayerAction LookLeft;
    public PlayerAction LookRight;

    public PlayerAction Action1;

    public PlayerTwoAxisAction Movement;
    public PlayerTwoAxisAction Camera;

    public PlayerAction Action2;

    public KeyboardControllerSet()
    {   
        isBinded = false;
        isKeyboard = true;
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Forward = CreatePlayerAction("Move Forward");
        Backward = CreatePlayerAction("Move Backwards");

        Up = CreatePlayerAction("Camera Up");
        Down = CreatePlayerAction("Camera Down");
        LookLeft = CreatePlayerAction("Camera Left");
        LookRight = CreatePlayerAction("Camera Right");

        Movement = CreateTwoAxisPlayerAction(Left, Right, Backward, Forward);
        Sprint = CreatePlayerAction("Sprint");
        Camera = CreateTwoAxisPlayerAction(LookLeft, LookRight, Down, Up);
        Action1 = CreatePlayerAction("AButton");

        Action2 = CreatePlayerAction("SwitchButton");
    }
}
