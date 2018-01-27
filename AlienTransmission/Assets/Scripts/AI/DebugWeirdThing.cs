using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeirdThing : MonoBehaviour {
    [SerializeField]
    public GameObject[] dummyScript;
    bool trigger1 = false;
    bool trigger2 = false;

    int timer = 0;

    private void Start()
    {
        dummyScript = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
		if (timer > 20 && !trigger1)
        {
            trigger1 = true;
            for (int i = 0; i < dummyScript.Length; i++)
            {
                NPCMind npc = dummyScript[i].GetComponent<NPCMind>();
                npc.setState(MINDSTATES.RUN);
            }
        }
        else if (timer > 200 && !trigger2)
        {
            trigger2 = true;
            Debug.Log("PAAAAAAAANIC");
            for (int i = 0; i < dummyScript.Length; i++)
            {
                NPCMind npc = dummyScript[i].GetComponent<NPCMind>();
                npc.setState(MINDSTATES.PANIC);
            }
        }
        else
        {
            timer++;
        }
	}
}
