using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : MonoBehaviour {

    public List<HumanPlayer> players;
    
    [SerializeField]
    List<InputDevice> inputDevices;

    List<Vector3> playerPositions;

    
    const int maxPlayers = 4;
    [SerializeField]
    int playerIndex;
    public GameObject playerPrefab;
    public GameObject ufoPrefab;

    bool maxPlayersReached = false;
    float maxStartTime; // pause time before game scene transition.
    float currTimer; // current time before game scene transition.
    public static PlayerManager playerManager = null;
    public static GameBehaviour gameManager = null;

    
    private void Awake() {
        //Create a static instance of player Manager
        if (!playerManager) {
            playerManager = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
    void Start() {
        InputManager.OnDeviceDetached += OnDeviceDetached; // Add a listener for when device is detached during gameplay

        
        players = new List<HumanPlayer>();
        playerPositions = new List<Vector3>();
        //Will assign at a later date
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));

        inputDevices = new List<InputDevice>();
        playerIndex = 0;
        currTimer = 0.0f;
        maxStartTime = 3.0f;
        if (!gameManager) {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBehaviour>();
        }
        
        //Will change this for a better setup
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.currentScene == "SetupControllers") {
            InputDevice activeInputDevice = InputManager.ActiveDevice; // active device
            if (activeInputDevice.Action1) {
                if (FindPlayerWithDevice(activeInputDevice) == null) {
                    //Test if statement. Will change once I get 4 controllers
                    if (playerIndex < maxPlayers) {
                        SetupNewPlayer(activeInputDevice);
                    }
                    if(playerIndex >= maxPlayers) {
                        maxPlayersReached = true;
                    }
                }
                Debug.Log(playerIndex);
            }

            if (maxPlayersReached) {
                currTimer += Time.deltaTime;
                if (currTimer >= maxStartTime) {
                    gameManager.SwitchToScene("GameScene");
                    maxPlayersReached = false;
                    currTimer = 0.0f;
                    playerIndex = 0;
                }
            }
        }
        
    }
    InputDevice FindPlayerWithDevice(InputDevice searchDevice) {
        int inputDeviceCount = inputDevices.Count;
        for (int i = 0; i < inputDeviceCount; i++) {
            InputDevice inputDevice = inputDevices[i];
            if (inputDevice == searchDevice) {
                return inputDevice;
            }
        }
        return null;
    }
    void SetupNewPlayer(InputDevice inputDevice) {
        if (players.Count < maxPlayers) {
            inputDevices.Add(inputDevice);
            gameManager.sceneManager.SetPlayerTextActive(playerIndex);
            playerIndex++;
        }
    }
    public void CreatePlayers(List<Vector3> playerPositions) {
        playerIndex = 0;
        foreach(InputDevice device in inputDevices) {
            CreateNewPlayer(device, playerPositions);
            playerIndex++;
        }
    }
    void CreateNewPlayer(InputDevice device, List<Vector3> playerPositions) {
        if (playerPrefab) {
            //Setting up player
            if (playerIndex < 1) {
                //Instantiate UF0
                GameObject ufoPlayer = GameObject.FindGameObjectWithTag("UFOPlayer");
                if (ufoPlayer) {
                    HumanPlayer playerComponent = ufoPlayer.GetComponent<HumanPlayer>();
                    playerComponent.inputDevice = device;
                }
            } else if(playerIndex < 4) {
                GameObject newPlayerObject = Instantiate(playerPrefab, playerPositions[playerIndex - 1], playerPrefab.transform.rotation);
                HumanPlayer playerComponent = newPlayerObject.GetComponent<HumanPlayer>();

                switch (playerIndex) {
                    case 1: {
                            gameManager.playerOneCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                            playerComponent.cam = gameManager.playerOneCam.GetComponent<Camera>();
                            gameManager.playerOneCam.SetActive(true);
                            break;
                        }
                    case 2: {
                            gameManager.playerTwoCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                            playerComponent.cam = gameManager.playerTwoCam.GetComponent<Camera>();
                            gameManager.playerTwoCam.SetActive(true);
                            break;
                        }
                    case 3: {
                            gameManager.playerThreeCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                            playerComponent.cam = gameManager.playerThreeCam.GetComponent<Camera>();
                            gameManager.playerThreeCam.SetActive(true);
                            break;
                        }
                }
                playerComponent.inputDevice = device;
            }
            //Camera Set up
        }
    }

    void OnDeviceDetached(InputDevice inputDevice) {
        var playerCount = players.Count;
        for (int i = 0; i < playerCount; i++) {
            HumanPlayer player = players[i];
            if (player.inputDevice == inputDevice) {
                //Notify the game that player has disconnected
                //Pause Game
                //Give time for the player to reconnect
                //RemovePlayer(player);
            }
        }
    }

    void RemovePlayer(HumanPlayer player) {
        player.inputDevice = null;
        //Destroy player Device

    }
}

