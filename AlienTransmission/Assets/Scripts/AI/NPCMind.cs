using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMind : HumanMindBase {

    bool isMoving = false;
    Wander2 wander;
    SteeringBasics steering;
    Flee flee;

    public GameObject fleeFrom;

    protected override void Start()
    {
        base.Start();
        wander = GetComponent<Wander2>();
        steering = GetComponent<SteeringBasics>();
        flee = GetComponent<Flee>();
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 accel = Vector3.zero;
            switch (currentState)
            {
                case MINDSTATES.RUN:
                    accel = wander.getSteering();
                    break;
                case MINDSTATES.PANIC:
                    if (fleeFrom != null && fleeFrom.activeInHierarchy) { 
                        accel = flee.getSteering(fleeFrom.transform.position);
                    }
                    break;

            }

            steering.steer(accel);
            steering.lookWhereYoureGoing();
        }

        //stayStanding();
    }

    protected void stayStanding()
    {
        //Collisions end up tipping it over
        Vector3 stayDown = transform.position;
        stayDown.y = 0;
        transform.position = stayDown;

        Vector3 stayUpright = transform.rotation.eulerAngles;
        stayUpright.x = 0;
        stayUpright.z = 0;
        transform.rotation = Quaternion.Euler(stayUpright);
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




    //Old scatter algorithm
    /*
    Vector3 gotoLocation;
    public float maxTravelDistance = 10;
    public float jitter = 40;

    private void Start()
    {
        gotoLocation = gameObject.transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = gameObject.transform.position;
        if (Vector3.Distance(gotoLocation, currentPosition) < 0.05)
        {
            seekNewNode();
        }
        transform.LookAt(gotoLocation);
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }

    //Using collision instead
    private void OnCollisionEnter(Collision collision)
    {
        gotoLocation = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
    }

    void seekNewNode()
    {
        Vector2 randomCircle = Random.insideUnitCircle;
        float wanderJitter = jitter * Time.deltaTime;

        gotoLocation = new Vector3(randomCircle.x, 0, randomCircle.y) * maxTravelDistance;

        Debug.Log("new Node " + gotoLocation);
    }
    */
}
