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

    HumanPlayer player;

    float xVal;
    float yVal;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<HumanPlayer>();
    }

    private void Update()
    {
        if (currentState != MINDSTATES.ABDUCTED || currentState != MINDSTATES.FALLING)
        {
            xVal = player.leftHorizontal;
            yVal = player.leftVertical;

            if (player.sprint)
            {
                setState(MINDSTATES.RUN);
            }
            else
            {
                setState(MINDSTATES.WALK);
            }

            if (yVal > 0.1f || yVal < -0.1f)
            {
                currentSpeed = 2f;
            }
            else
            {
                currentSpeed = 0f;
            }
        }

        anim.SetFloat("Speed", currentSpeed);
    }

    void onTransmission()
    {
        //Play the animation
        //The collider will do the timer stuff in GeneratorSingle.cs
        anim.SetBool("isTransmitting", true);
    }

    protected override void onAbducted()
    {
        base.onAbducted();  //Play abduct animation
        //Lock the player movement somehow and stuff
    }

    protected override void onFalling()
    {
        base.onFalling();
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
            case MINDSTATES.FALLING:
                onFalling();
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
            case MINDSTATES.FALLING:
                //something
                break;
        }
    }
}
