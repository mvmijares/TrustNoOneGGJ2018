using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : MonoBehaviour {

    public List<HumanPlayer> players;
    
    [SerializeField]
    List<KeyboardControllerSet> inputDevices;

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
        //Don't think we need it now
        //InputManager.OnDeviceDetached += OnDeviceDetached; // Add a listener for when device is detached during gameplay

        
        players = new List<HumanPlayer>();
        playerPositions = new List<Vector3>();
        //Will assign at a later date
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));

        inputDevices = new List<KeyboardControllerSet>();
        setupInputs();
        playerIndex = 0;
        currTimer = 0.0f;
        maxStartTime = 3.0f;
        if (!gameManager) {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBehaviour>();
        }
        
        //Will change this for a better setup
    }

    private void setupInputs()
    {
        Key[] playerInput1 = new Key[] { Key.A, Key.D, Key.W, Key.S, Key.E, Key.Q };
        Key[] playerInput2 = new Key[] { Key.H, Key.K, Key.U, Key.J, Key.I, Key.Y };
        Key[] playerInput3 = new Key[] { Key.LeftArrow, Key.RightArrow, Key.UpArrow, Key.DownArrow, Key.End, Key.Delete };
        Key[] playerInput4 = new Key[] { Key.Pad4, Key.Pad6, Key.Pad8, Key.Pad2, Key.Pad9, Key.Pad7 };
        List<Key[]> playerInputs = new List<Key[]>();
        playerInputs.Add(playerInput1);
        playerInputs.Add(playerInput2);
        playerInputs.Add(playerInput3);
        playerInputs.Add(playerInput4);

        foreach (Key[] playerin in playerInputs)
        {
            bindKeys(playerin);
        }
    }

    void bindKeys(Key[] playerInput)
    {
        KeyboardControllerSet customSet = new KeyboardControllerSet();

        customSet.Left.AddDefaultBinding(playerInput[0]);
        customSet.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        customSet.Left.AddDefaultBinding(InputControlType.DPadLeft);

        customSet.Right.AddDefaultBinding(playerInput[1]);
        customSet.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        customSet.Right.AddDefaultBinding(InputControlType.DPadRight);

        customSet.Forward.AddDefaultBinding(playerInput[2]);
        customSet.Forward.AddDefaultBinding(InputControlType.LeftStickUp);
        customSet.Forward.AddDefaultBinding(InputControlType.DPadUp);

        customSet.Backward.AddDefaultBinding(playerInput[3]);
        customSet.Backward.AddDefaultBinding(InputControlType.LeftStickDown);
        customSet.Backward.AddDefaultBinding(InputControlType.DPadDown);

        customSet.Sprint.AddDefaultBinding(playerInput[4]);
        customSet.Sprint.AddDefaultBinding(InputControlType.RightTrigger);

        customSet.Action1.AddDefaultBinding(playerInput[5]);
        customSet.Action1.AddDefaultBinding(InputControlType.Action1);

        customSet.Up.AddDefaultBinding(InputControlType.RightStickUp);
        customSet.Down.AddDefaultBinding(InputControlType.RightStickDown);
        customSet.LookLeft.AddDefaultBinding(InputControlType.RightStickLeft);
        customSet.LookRight.AddDefaultBinding(InputControlType.RightStickRight);
        InputManager.AttachPlayerActionSet(customSet);

        inputDevices.Add(customSet);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentScene == "SetupControllers") {
            //InputDevice activeInputDevice = InputManager.ActiveDevice; // active device
            //if (activeInputDevice.AnyButtonIsPressed) {
            //    KeyboardControllerSet set = FindPlayerWithDevice(activeInputDevice);
            //    if (set == null) {
            //        //Test if statement. Will change once I get 4 controllers
            //        if (playerIndex < maxPlayers) {
            //            SetupNewPlayer(set);
            //        }
            //        if(playerIndex >= maxPlayers) {
            //            maxPlayersReached = true;
            //        }
            //    }
            //    Debug.Log(playerIndex);
            //}

            foreach (KeyboardControllerSet input in inputDevices)
            {
            //InputDevice activeInputDevice = InputManager.ActiveDevice;
            //if (activeInputDevice.AnyButtonIsPressed)
            //{
                //KeyboardControllerSet input = FindPlayerWithDevice(activeInputDevice);
                if (input.Action1.WasPressed)
                {
                    if (playerIndex < maxPlayers)
                    {
                        SetupNewPlayer(input);
                    }
                    if (playerIndex >= maxPlayers)
                    {
                        maxPlayersReached = true;
                    }
                    //Debug.Log(playerIndex);
                }
            //}
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
    KeyboardControllerSet FindPlayerWithDevice(InputDevice searchDevice) {
        int inputDeviceCount = inputDevices.Count;
        for (int i = 0; i < inputDeviceCount; i++) {
            KeyboardControllerSet inputDevice = inputDevices[i];
            if (inputDevice.ActiveDevice == searchDevice && !inputDevice.isBinded) {
                return inputDevice;
            }
        }
        return null;
    }
    void SetupNewPlayer(KeyboardControllerSet inputDevice) {
        if (players.Count < maxPlayers) {
            //inputDevices.Add(inputDevice);
            inputDevice.isBinded = true;
            gameManager.sceneManager.SetPlayerTextActive(playerIndex);
            playerIndex++;
        }
    }
    public void CreatePlayers(List<Vector3> playerPositions) {
        playerIndex = 0;
        foreach (KeyboardControllerSet device in inputDevices) {
            CreateNewPlayer(device, playerPositions);
            playerIndex++;
        }
    }
    void CreateNewPlayer(KeyboardControllerSet device, List<Vector3> playerPositions) {
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

    void OnDeviceDetached(KeyboardControllerSet inputDevice) {
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

