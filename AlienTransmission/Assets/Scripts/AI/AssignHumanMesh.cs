using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignHumanMesh : MonoBehaviour {

    private void Awake()
    {
        Transform tempMesh = transform.Find("MeshPlaceholder");
        Destroy(tempMesh.gameObject);
        GameObject mesh = getRandomMesh();
        GetComponent<HumanMindBase>().giveAnimator(mesh.GetComponent<Animator>());
        this.enabled = false;
    }

    GameObject getRandomMesh()
    {
        int rng = Random.Range(0, 3) + 1;
        string meshString = "Character" + rng;

        GameObject randomMesh = Instantiate((GameObject)Resources.Load(meshString), transform);
        randomMesh.isStatic = true;
        return randomMesh;
    }
}
