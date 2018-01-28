using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour {

    float timer = 0.0f;
    float maxTimer = 3.0f;

    GameBehaviour gameManager;
    private void Awake() {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerObject)
            gameManager = gameManagerObject.GetComponent<GameBehaviour>();
            
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > maxTimer)
            gameManager.SwitchToScene("Credits");
	}
}
