using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : MonoBehaviour {

    List<HumanPlayer> players;
    List<Transform> playerPositions;
    const int maxPlayers = 4;
    int playerIndex;
    public GameObject playerPrefab;
    public GameObject playerCameraPrefab;

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
                if (!FindPlayerWithDevice(activeInputDevice)) {
                    //Test if statement. Will change once I get 4 controllers
                    if (playerIndex <= 2) {
                        CreateNewPlayer(activeInputDevice);
                    }
                    if (playerIndex >= 2) {
                        maxPlayersReached = true;
                    }
                }
            }

            if (maxPlayersReached) {
                currTimer += Time.deltaTime;
                if (currTimer >= maxStartTime) {
                    gameManager.SwitchToScene("Game");
                    maxPlayersReached = false;
                    currTimer = 0.0f;
                }
            }
        }
    }
    HumanPlayer FindPlayerWithDevice(InputDevice inputDevice) {
        int playerCount = players.Count;
        for (int i = 0; i < playerCount; i++) {
            HumanPlayer player = players[i];
            if (player.inputDevice == inputDevice) {
                return player;
            }
        }
        return null;
    }
    void CreateNewPlayer(InputDevice inputDevice) {
        if (players.Count < maxPlayers) {
            if (playerPrefab) {
                //Setting up player
                GameObject newPlayerObject = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), playerPrefab.transform.rotation);
                HumanPlayer playerComponent = newPlayerObject.GetComponent<HumanPlayer>();
                playerComponent.inputDevice = inputDevice;

                PlayerAssignment(playerComponent);
                gameManager.sceneManager.SetPlayerTextActive(playerIndex);
                playerIndex++;
                players.Add(playerComponent);
                //Instantiate a new player object
            }
        }
    }

    void PlayerAssignment(HumanPlayer player) {
        player.playerIndex = playerIndex;
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

