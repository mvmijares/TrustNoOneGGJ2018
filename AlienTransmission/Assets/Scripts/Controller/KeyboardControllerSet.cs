using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class KeyboardControllerSet : PlayerActionSet {

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

    public KeyboardControllerSet()
    {
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
    }
}
