using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMind : HumanMindBase {

    bool isMoving = false;
    Vector3 gotoLocation;
    float maxTravelDistance = 10;

    protected override void Start()
    {
        base.Start();
        gotoLocation = gameObject.transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 currentPosition = gameObject.transform.position;
            Ray ray = new Ray(currentPosition, gameObject.transform.forward);
            RaycastHit hit;
            bool rayCastResult = Physics.Raycast(ray, out hit, 10);
            if (rayCastResult)
            {
                //Pick a random direction and turn and ditch
                float rng = Random.Range(0, 10);
                if (rng < 5)
                {
                    //turn left
                    gotoLocation = currentPosition + new Vector3(0, 0, -Random.Range(0, 180)) * maxTravelDistance;
                    Debug.Log(gotoLocation);
                }
                else
                {
                    //turn right
                    gotoLocation = currentPosition + new Vector3(0, 0, Random.Range(0, 180)) * maxTravelDistance;
                    Debug.Log(gotoLocation);
                }
            }
            else
            {
                if (Vector3.Distance(currentPosition, gotoLocation) < 0.01) //If we're really close
                {
                    //Find a new location to go to
                    gotoLocation = currentPosition + gameObject.transform.forward * maxTravelDistance;
                }
                else
                {
                    Quaternion lookRotation = Quaternion.LookRotation(currentPosition, gotoLocation);
                    gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
                    gameObject.transform.Translate(gameObject.transform.forward * moveSpeed);
                }
            }
        }
    }

    protected override void onWalk()
    {
        base.onWalk();
        isMoving = true;
    }

    protected override void onRun()
    {
        base.onRun();
        isMoving = true;
    }

    protected override void onPanic()
    {
        base.onPanic();
        isMoving = true;
    }

    protected override void onEnterState(MINDSTATES state)
    {
        switch (state)
        {
            case MINDSTATES.WALK:
                onWalk();
                break;
            case MINDSTATES.RUN:
                onRun();
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
                isMoving = false;
                break;
            case MINDSTATES.RUN:
                //stop running
                isMoving = false;
                break;
            case MINDSTATES.PANIC:
                //stop panicking
                isMoving = false;
                break;
            case MINDSTATES.IDLE:
                //stop idling
                break;
            case MINDSTATES.ABDUCTED:
                //I guess you'd be falling here.
                break;
            case MINDSTATES.DEAD:
                Debug.Log("Exit Dead"); //Dead people should be killed
                break;
        }
    }
}
