using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSingle : MonoBehaviour {

    GeneratorMaster genMaster;
    public float generatorTimer;
    List<GameObject> touchingPlayers;
    public float growth = 0.5f;
    public float maxTouchTime = 3f;
    public bool isActive { get; private set; }

    private void Start()
    {
        isActive = true;
        generatorTimer = 0;
        touchingPlayers = new List<GameObject>();
    }

    private void Update()
    {
        if (isActive)
        {
            if (generatorTimer < maxTouchTime)
            {
                if (touchingPlayers.Count > 0)
                {
                    generatorTimer += growth * touchingPlayers.Count;
                }
            }
            else
            {
                //Generator sets off and something happens
                genMaster.generatorTimerFinished(this);
                isActive = false;
            }
        }
    }

    public void linkToMaster(GeneratorMaster gm)
    {
        genMaster = gm;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            touchingPlayers.Add(collision.transform.gameObject);
            HumanMindBase playerState = collision.transform.gameObject.GetComponent<HumanMindBase>();
            if (playerState != null)
            {
                playerState.setState(MINDSTATES.TRANSMISSIONING);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (touchingPlayers.IndexOf(collision.transform.gameObject) > -1)
        {
            touchingPlayers.Remove(collision.transform.gameObject);
            HumanMindBase playerState = collision.transform.gameObject.GetComponent<HumanMindBase>();
            if (playerState != null)
            {
                playerState.setState(MINDSTATES.WALK);
            }
        }
    }

    public void setGenerator(bool isOn)
    {
        gameObject.SetActive(isOn);
        gameObject.GetComponentInChildren<Renderer>().enabled = isOn;
    }
}
