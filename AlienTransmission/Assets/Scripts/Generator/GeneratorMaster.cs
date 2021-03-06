﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorMaster : MonoBehaviour {

    List<GameObject> generators;
    List<GameObject> activeGenerators;
    List<GameObject> finishedGenerators;
    public UFOMovement ufo;
    int maxGeneratorsOutAtATime = 3;

	// Use this for initialization
	void Awake() {
        generators = new List<GameObject>();
        finishedGenerators = new List<GameObject>();
        ufo = GameObject.FindGameObjectWithTag("UFOPlayer").gameObject.GetComponent<UFOMovement>();

        GameObject[] foundGens = GameObject.FindGameObjectsWithTag("Generator");
        for (int i = 0; i < foundGens.Length; i++)
        {
            GameObject generator = foundGens[i];
            generators.Add(generator);

            GeneratorSingle genSingle = generator.GetComponent<GeneratorSingle>();
            genSingle.linkToMaster(this);
            genSingle.setGenerator(false);
        }

        activateRandomGenerators();
	}

    void activateRandomGenerators()
    {
        activeGenerators = new List<GameObject>();
        while (activeGenerators.Count < maxGeneratorsOutAtATime)
        {
            int index = Mathf.FloorToInt(Random.Range(0, generators.Count));
            GameObject generator = generators[index];
            if (finishedGenerators.IndexOf(generator) == -1 && activeGenerators.IndexOf(generator) == -1)
            {
                generator.GetComponent<GeneratorSingle>().setGenerator(true);
                activeGenerators.Add(generator);
            }
        }
    }

    public void generatorTimerFinished(GeneratorSingle finishedGenerator)
    {
        //show the generator doing its EMP stuff
        StartCoroutine(waitForAnimToFinishAndKill(finishedGenerator));
        //waitForAnimToFinishAndKill(finishedGenerator);
        finishedGenerators.Add(finishedGenerator.gameObject);
        checkIfAllGeneratorsDone();
    }

    void checkIfAllGeneratorsDone()
    {
        int numberOfFinishedGeneratorsThisRound = 0;
        foreach (GameObject generatorGO in generators)
        {
            if (finishedGenerators.IndexOf(generatorGO) > -1)
            {
                numberOfFinishedGeneratorsThisRound++;
            }
        }

        if (numberOfFinishedGeneratorsThisRound == generators.Count)
        {
            Debug.Log("Everything is done we win");
            ufo.TakeDamage(1);
            //Do win stuff here
        }
        else if (numberOfFinishedGeneratorsThisRound % 3 == 0)
        {
            Debug.Log("to the next");
            //Shoot down 1 hp
            ufo.TakeDamage(1);
            activateRandomGenerators();
        }
    }

    //void waitForAnimToFinishAndKill(GeneratorSingle finishedGenerator)
    //{
    //    GameObject prop = finishedGenerator.gameObject.transform.Find("GeneratorProp").gameObject;
    //    //GameObject explosionParticle = finishedGenerator.gameObject.transform.Find("BigExplosionEffect").gameObject;
    //    //explosionParticle.SetActive(true);
    //    new WaitForSeconds(2);
    //    Destroy(prop);
    //    new WaitForSeconds(3);
    //    finishedGenerator.setGenerator(false);
    //}

    IEnumerator waitForAnimToFinishAndKill(GeneratorSingle finishedGenerator)
    {
        yield return new WaitForSeconds(3); //Supposed to be however long it takes

        finishedGenerator.setGenerator(false);
    }
}
