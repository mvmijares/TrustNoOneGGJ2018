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
            RaycastHit hit;
            Ray ray = new Ray(currentPosition, transform.forward);
            bool rayResults = Physics.Raycast(ray, out hit, moveSpeed * Time.deltaTime);
            while (rayResults || Vector3.Distance(gotoLocation, currentPosition) < 0.05)
            {
                seekNewNode();
            }

            //transform.position = Vector3.Slerp(currentPosition, gotoLocation, Time.deltaTime);
            transform.LookAt(gotoLocation);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }
    }

    void seekNewNode()
    {
        Vector2 randomCircle = Random.insideUnitCircle;

        gotoLocation = new Vector3(randomCircle.x, 0, randomCircle.y) * maxTravelDistance;
        Debug.Log("new Node " + gotoLocation);
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
