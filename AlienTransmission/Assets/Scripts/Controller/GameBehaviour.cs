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

    public static GameBehaviour gameManager = null;

    //player cameras for game scene
    public GameObject playerOneCam;
    public GameObject playerTwoCam;
    public GameObject playerThreeCam;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public string currentScene;

    private void Awake() {
        if (!gameManager) {
            gameManager = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
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
            case "GameScene": {
                    GameObject playerPosObjects = GameObject.FindGameObjectWithTag("PlayerPosition");
                    GameObject[] playerOneObjects = GameObject.FindGameObjectsWithTag("PlayerOne");
                    GameObject[] playerTwoObjects = GameObject.FindGameObjectsWithTag("PlayerTwo");
                    GameObject[] playerThreeObjects = GameObject.FindGameObjectsWithTag("PlayerThree");

                    foreach (GameObject obj in playerOneObjects)
                        if (obj.layer == LayerMask.NameToLayer("Camera")) {
                            playerOneCam = obj;
                            playerOneCam.SetActive(false);
                        }
                    foreach (GameObject obj in playerTwoObjects)
                        if (obj.layer == LayerMask.NameToLayer("Camera")) {
                            playerTwoCam = obj;
                            playerTwoCam.SetActive(false);
                        }
                    foreach (GameObject obj in playerThreeObjects)
                        if (obj.layer == LayerMask.NameToLayer("Camera")) {
                            playerThreeCam = obj;
                            playerThreeCam.SetActive(false);
                        }
                    if (playerPosObjects) {
                        List<Vector3> playerPositions = new List<Vector3>();
                        foreach (Transform child in playerPosObjects.transform) {
                            playerPositions.Add(child.position);
                        }
                        playerManager.CreatePlayers(playerPositions);
                        
                    }

                    GameObject[] NPCS = GameObject.FindGameObjectsWithTag("NPC");
                    foreach (GameObject NPC in NPCS)
                    {
                        NPCMind npcMind = NPC.GetComponent<NPCMind>();
                        npcMind.setState(MINDSTATES.WALK);
                    }

                    break;

                    
                }
        }
    }   

    public void SwitchToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
