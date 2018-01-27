using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeirdThing : MonoBehaviour {

    public NPCMind dummyScript;

    int timer = 0;
	
	// Update is called once per frame
	void Update () {
		if (timer > 100)
        {
            dummyScript.setState(MINDSTATES.RUN);
        }
        else
        {
            timer++;
        }
	}
}
