using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NearSensor : MonoBehaviour {

	public HashSet<Rigidbody> targets = new HashSet<Rigidbody>();

	void OnTriggerEnter(Collider other) {
        Rigidbody otherRigid = other.GetComponent<Rigidbody>();
        if (otherRigid != null)
        {
            targets.Add(other.GetComponent<Rigidbody>());
        }
	}
	
	void OnTriggerExit(Collider other)
    {
        Rigidbody otherRigid = other.GetComponent<Rigidbody>();
        if (otherRigid != null)
        {
            targets.Remove (other.GetComponent<Rigidbody>());
        }
    }
}
