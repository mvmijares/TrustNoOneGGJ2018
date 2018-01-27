using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour {
    static PlayerManager playerManager = null;
    [SerializeField]
    public SceneBehaviour sceneManager;
    [SerializeField]
    GameObject sceneObject;
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public string currentScene;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        currentScene = scene.name;
        if (sceneManager) {
            sceneManager = null;  
        }
        sceneObject = GameObject.FindGameObjectWithTag("SceneManager");
        if(sceneObject)
            sceneManager = sceneObject.GetComponent<SceneBehaviour>();
       
        switch (scene.name) {
            case "SetupControllers": {
                    if (!playerManager) {
                        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
                    }
                    break;
                }
            case "Game": {

                    break;
                }
        }
    }

    public void SwitchToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    void CameraAssignment() {

    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
