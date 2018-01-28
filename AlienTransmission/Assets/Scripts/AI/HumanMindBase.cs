using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MINDSTATES {
    IDLE,
    WALK,
    RUN,
    ABDUCTED,
    PANIC,
    DEAD,
    TRANSMISSIONING //Only used for players
}

public abstract class HumanMindBase : MonoBehaviour {

    protected MINDSTATES currentState;
    protected float walkSpeed = 10f;
    protected float runSpeed = 20f;

    // Use this for initialization
    //Or is Awake better?
    protected virtual void Start () {
		currentState = MINDSTATES.IDLE;
	}

    protected virtual void onIdle()
    {
        //play idle animation
    }

    protected virtual void onWalk()
    {
        //play walk animation
    }

    protected virtual void onRun()
    {
        //play run animation
    }

    protected virtual void onAbducted()
    {
        //play abducted animation
    }

    protected virtual void onPanic()
    {
        //play panic animation
    }

    protected virtual void onDead()
    {
        //Not technically dead but when you get removed from the game
    }

    protected abstract void onEnterState(MINDSTATES state);

    protected abstract void onExitState(MINDSTATES state);

    public bool setState(MINDSTATES newState)
    {
        if (currentState != newState)
        {
            onExitState(currentState);
            currentState = newState;
            onEnterState(newState);
            return true;
        }

        return false;
    }
}
