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
    TRANSMISSIONING, //Only used for players,
    FALLING
}

public abstract class HumanMindBase : MonoBehaviour {

    public MINDSTATES currentState { get; protected set; }
    protected float walkSpeed = 2.5f;
    protected float runSpeed = 5f;
    protected float currentSpeed = 0f;

    protected Animator anim;

    // Use this for initialization
    //Or is Awake better?
    protected virtual void Start () {
		currentState = MINDSTATES.IDLE;
	}

    protected virtual void Awake ()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void onIdle()
    {
        //play idle animation   
        anim.SetFloat("speed", 0);
    }

    protected virtual void onWalk()
    {
        //play walk animation
        anim.SetBool("IsRunning", false);
    }

    protected virtual void onRun()
    {
        //play run animation
        anim.SetBool("IsRunning", true);
    }

    protected virtual void onAbducted()
    {
        //play abducted animation
        anim.SetBool("Abducted", true);
    }

    protected virtual void onPanic()
    {
        //play panic animation
        anim.SetBool("IsRunning", true);
    }

    protected virtual void onDead()
    {
        //Not technically dead but when you get removed from the game
    }

    protected virtual void onFalling()
    {
        //???
        anim.SetBool("isFalling", true);
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
