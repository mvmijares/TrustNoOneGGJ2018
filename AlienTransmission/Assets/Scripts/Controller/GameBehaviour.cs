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
                    GameObject playerOneObject = GameObject.FindGameObjectWithTag("PlayerOne");
                    GameObject playerTwoObject = GameObject.FindGameObjectWithTag("PlayerTwo");
                    GameObject playerThreeObject = GameObject.FindGameObjectWithTag("PlayerThree");

                   
                    playerOneCam = playerOneObject;
                    playerTwoCam = playerTwoObject;
                    playerThreeCam = playerThreeObject;    

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
                        //Debug.Log("npc ok");
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
