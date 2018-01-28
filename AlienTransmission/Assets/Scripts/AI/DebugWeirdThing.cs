using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeirdThing : MonoBehaviour {
    public GameObject[] dummyScript;
    bool trigger1 = false;
    bool trigger2 = false;
    bool trigger3 = false;
    bool trigger4 = false;
    public GeneratorMaster genMaster;

    public int timer = 0;

    private void Start()
    {
        dummyScript = GameObject.FindGameObjectsWithTag("NPC");
    }

    // Update is called once per frame
    void Update () {
		if (timer > 100 && !trigger1)
        {
            trigger1 = true;
            for (int i = 0; i < dummyScript.Length; i++)
            {
                NPCMind npc = dummyScript[i].GetComponent<NPCMind>();
                npc.setState(MINDSTATES.WALK);
            }
            genMaster.derpderpderp();
        }
        else
        {
            timer++;
        }
	}
}
