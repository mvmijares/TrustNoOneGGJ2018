using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is the state animation controller and stuff for PLAYERS.
 * It does not move you, use Mike's code for that.
 * For playing animation triggers, do it on HumanMindBase so it can affect NPCs too
 */
public class PlayerStateController : HumanMindBase {

	// Use this for initialization
	void Start () {
        base.Start();
	}

    void onTransmission()
    {
        //Play the animation
        //The collider will do the timer stuff in GeneratorSingle.cs
    }

    protected override void onAbducted()
    {
        base.onAbducted();  //Play abduct animation
        //Lock the player movement somehow and stuff
    }

    protected override void onEnterState(MINDSTATES state)
    {
        switch (state)
        {
            case MINDSTATES.WALK:
                onWalk();
                break;
            case MINDSTATES.PANIC:
                onPanic();
                break;
            case MINDSTATES.IDLE:
                onIdle();
                break;
            case MINDSTATES.ABDUCTED:
                onAbducted();
                break;
            case MINDSTATES.TRANSMISSIONING:
                onTransmission();
                break;
            case MINDSTATES.DEAD:
                //kill thyself
                break;
        }
    }

    protected override void onExitState(MINDSTATES state)
    {
        switch (state)
        {
            case MINDSTATES.WALK:
                //stop walking
                break;
            case MINDSTATES.RUN:
                //stop running
                break;
            case MINDSTATES.PANIC:
                //stop panicking
                break;
            case MINDSTATES.IDLE:
                //stop idling
                break;
            case MINDSTATES.TRANSMISSIONING:
                //stop transmission
                break;
            case MINDSTATES.ABDUCTED:
                //I guess you'd be falling here and dying
                break;
            case MINDSTATES.DEAD:
                Debug.Log("Exit Dead"); //Dead people should be killed
                break;
        }
    }
}
