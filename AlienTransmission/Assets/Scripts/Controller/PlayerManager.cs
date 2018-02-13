using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityStandardAssets.CrossPlatformInput;

/*
 *  Single Player Controls are:
 *      First Keyboard Controls : 
 *      Key.A, Key.D, Key.W, Key.S, Key.LeftShift, Key.Q, Key.U, Key.J, Key.H, Key.K, Key.Space };
        
        Second Keyboard Controls : 
        Key.LeftArrow, Key.RightArrow, Key.UpArrow, Key.DownArrow, Key.LeftControl, 
        Key.Pad7, Key.Pad5, Key.Pad2, Key.Pad1, Key.Pad3, Key.Pad9 

        Action 2 is used to switch between players
 * 
 */
public class PlayerManager : MonoBehaviour {

    List<Player> players;

    [SerializeField]
    List<KeyboardControllerSet> inputDevices;
    List<KeyboardControllerSet> keyboardDevices;
    Key[] keyboardInput; 
    Key[] keyboardInput2;

    KeyboardControllerSet singlePlayerController; // Single-player controls
    GameObject activePlayer; // Reference to active player for single-player 

    List<Vector3> playerPositions;
    List<Player> singlePlayers;
    int singlePlayerIndex; // reference to index of active player in the list

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

    public bool isSinglePlayer = false;

    bool singlePlayerSetup = false;
    float singlePlayerTimer = 1.0f;
    float currentPlayerTimer;
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

        if (!gameManager) {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBehaviour>();
        }

        keyboardDevices = new List<KeyboardControllerSet>();
        singlePlayers = new List<Player>();
        inputDevices = new List<KeyboardControllerSet>();
        players = new List<Player>();
        playerPositions = new List<Vector3>();

        //Will assign at a later date
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));
        playerPositions.Add(new Vector3(0, 0, 0));

        playerIndex = 0;
        //Timer for switching scenes
        currTimer = 0.0f;
        maxStartTime = 3.0f;


        KeyboardSetup(); // Initialize controls for 1 player.
        ControllerSetup();
        //Will change this for a better setup
    }
    void KeyboardSetup() {
        keyboardInput = new Key[] { Key.A, Key.D, Key.W, Key.S, Key.LeftShift, Key.Q, Key.U, Key.J, Key.H, Key.K, Key.Space };
        keyboardInput2 = new Key[] { Key.LeftArrow, Key.RightArrow, Key.UpArrow, Key.DownArrow, Key.LeftControl, Key.Pad7, Key.Pad5, Key.Pad2, Key.Pad1, Key.Pad3, Key.Pad9 };

        keyboardDevices.Add(BindKeyboardControls(keyboardInput));
        keyboardDevices.Add(BindKeyboardControls(keyboardInput2));
    }
    void ControllerSetup() {

    }
    // Old Setup input
    //private void setupInputs()
    //{
    //    Key[] playerInput1 = new Key[] { Key.A, Key.D, Key.W, Key.S, Key.LeftShift, Key.Q };
    //    Key[] playerInput2 = new Key[] { Key.H, Key.L, Key.U, Key.J, Key.I, Key.Y };
    //    Key[] playerInput3 = new Key[] { Key.LeftArrow, Key.RightArrow, Key.UpArrow, Key.DownArrow, Key.End, Key.Delete };
    //    Key[] playerInput4 = new Key[] { Key.Pad4, Key.Pad6, Key.Pad8, Key.Pad2, Key.Pad9, Key.Pad7 };
    //    List<Key[]> playerInputs = new List<Key[]>();
    //    playerInputs.Add(playerInput1);
    //    playerInputs.Add(playerInput2);
    //    playerInputs.Add(playerInput3);
    //    playerInputs.Add(playerInput4);

    //    foreach (Key[] playerin in playerInputs)
    //    {
    //        bindKeys(playerin);
    //    }
    //}
    // Old Keybindings
    //void bindKeys(Key[] playerInput)
    //{
    //    KeyboardControllerSet customSet = new KeyboardControllerSet();

    //    customSet.Left.AddDefaultBinding(playerInput[0]);
    //    customSet.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
    //    customSet.Left.AddDefaultBinding(InputControlType.DPadLeft);

    //    customSet.Right.AddDefaultBinding(playerInput[1]);
    //    customSet.Right.AddDefaultBinding(InputControlType.LeftStickRight);
    //    customSet.Right.AddDefaultBinding(InputControlType.DPadRight);

    //    customSet.Forward.AddDefaultBinding(playerInput[2]);
    //    customSet.Forward.AddDefaultBinding(InputControlType.LeftStickUp);
    //    customSet.Forward.AddDefaultBinding(InputControlType.DPadUp);

    //    customSet.Backward.AddDefaultBinding(playerInput[3]);
    //    customSet.Backward.AddDefaultBinding(InputControlType.LeftStickDown);
    //    customSet.Backward.AddDefaultBinding(InputControlType.DPadDown);

    //    customSet.Sprint.AddDefaultBinding(playerInput[4]);
    //    customSet.Sprint.AddDefaultBinding(InputControlType.RightTrigger);

    //    customSet.Action1.AddDefaultBinding(playerInput[5]);
    //    customSet.Action1.AddDefaultBinding(InputControlType.Action1);

    //    customSet.Up.AddDefaultBinding(InputControlType.RightStickUp);
    //    customSet.Down.AddDefaultBinding(InputControlType.RightStickDown);
    //    customSet.LookLeft.AddDefaultBinding(InputControlType.RightStickLeft);
    //    customSet.LookRight.AddDefaultBinding(InputControlType.RightStickRight);

    //    InputManager.AttachPlayerActionSet(customSet);

    //    inputDevices.Add(customSet);
    //}

    // Update is called once per frame
    void Update() {
        if (gameManager.currentScene == "SetupControllers") {
            if(playerIndex > 3) {
                maxPlayersReached = true;
            }
            CheckGameplayMode();
            CheckForInputs();
            if (maxPlayersReached) {
                currTimer += Time.deltaTime;
                if (currTimer >= maxStartTime) {
                    ResetSetupVariables();
                    gameManager.SwitchToScene("GameScene");
                }
            }
        } else if(gameManager.currentScene == "GameScene") {
            if (isSinglePlayer) {
                if (inputDevices[0].Action2.WasReleased) {
                    singlePlayerIndex++;
                    if (singlePlayerIndex > 3) {
                        singlePlayerIndex = 0;
                    }

                    Debug.Log("Single Player Index" + singlePlayerIndex);
                    singlePlayers[singlePlayerIndex].keyboardDevice = singlePlayerController;
                    activePlayer.GetComponent<Player>().keyboardDevice = null;
                    activePlayer = singlePlayers[singlePlayerIndex].gameObject;
                }
              
            }
        }
    }
    /// <summary>
    /// Reset setup variables
    /// </summary>
    void ResetSetupVariables() {
        maxPlayersReached = false;
        currTimer = 0.0f;
        playerIndex = 0;
    }

    void CheckGameplayMode() {
        //Debug.Log(inputDevices.Count);
        if (inputDevices.Count == 1) {
            InputDevice activeControllerDevice = InputManager.ActiveDevice;
            if (inputDevices[0].Sprint.IsPressed) {
                isSinglePlayer = true;
            }
            if (isSinglePlayer) {
                gameManager.SwitchToScene("GameScene");
            }
        }else if(inputDevices.Count > 1) {
            isSinglePlayer = false;
        }
    }
    void CheckForInputs() {
        InputDevice activeControllerDevice = InputManager.ActiveDevice;
        foreach (KeyboardControllerSet keyboard in keyboardDevices) {
            if (keyboard.Action1.IsPressed) {
                if (FindPlayerWithKeyboard(keyboard) == null) {
                    if (players.Count < maxPlayers) {
                        inputDevices.Add(keyboard);
                        gameManager.sceneManager.SetPlayerTextActive(playerIndex);
                        playerIndex++;
                    }
                }
            }
        }
        if (activeControllerDevice.Action1.IsPressed) {
            if (FindPlayerWithDevice(activeControllerDevice) == null) {
                if (players.Count < maxPlayers) {
                    inputDevices.Add(BindNewController(activeControllerDevice));
                    gameManager.sceneManager.SetPlayerTextActive(playerIndex);
                    playerIndex++;
                }
            }
        }
    }
    KeyboardControllerSet FindPlayerWithKeyboard(KeyboardControllerSet keyboard) {
        KeyboardControllerSet searchSet = null;
        if (inputDevices.Count > 0) {
            foreach (KeyboardControllerSet input in inputDevices) {
                if (input == keyboard) {
                    searchSet = input;
                }
            }
        }
        return searchSet;
    }
    KeyboardControllerSet FindPlayerWithDevice(InputDevice searchDevice) {
        int inputDeviceCount = inputDevices.Count;
        if (inputDeviceCount > 0) {
            foreach(KeyboardControllerSet device in inputDevices) {
                if (device.Device == searchDevice) {
                    return device;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// Removes the device at index. 
    /// For our purposes we just want to switch the keyboard to a controller for the first player.
    /// </summary>
    /// <param name="device"></param>
    void RemoveFromInputList(KeyboardControllerSet device) {
        if (inputDevices.Count > 1) {
            foreach (KeyboardControllerSet d in inputDevices) {
                if (d == device) {
                    inputDevices.Remove(d);
                    playerIndex--;
                }
            }
        }
    }
    /// <summary>
    /// Keyboard bindings for a new ControllerSet device
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    KeyboardControllerSet BindKeyboardControls(Key[] input) {
        KeyboardControllerSet newKeyboardInput = new KeyboardControllerSet();

        newKeyboardInput.Left.AddDefaultBinding(input[0]);
        newKeyboardInput.Right.AddDefaultBinding(input[1]);
        newKeyboardInput.Forward.AddDefaultBinding(input[2]);
        newKeyboardInput.Backward.AddDefaultBinding(input[3]);

        newKeyboardInput.Sprint.AddDefaultBinding(input[4]);
        newKeyboardInput.Action1.AddDefaultBinding(input[5]);

        newKeyboardInput.Up.AddDefaultBinding(input[6]);
        newKeyboardInput.Down.AddDefaultBinding(input[7]);
        newKeyboardInput.LookLeft.AddDefaultBinding(input[8]);
        newKeyboardInput.LookRight.AddDefaultBinding(input[9]);

        newKeyboardInput.Action2.AddDefaultBinding(input[10]);

        newKeyboardInput.isKeyboard = true;
        newKeyboardInput.isBinded = true;

        // Key.A, Key.D, Key.W, Key.S, Key.LeftShift, Key.Q, Key.U, Key.J, Key.H, Key.K, Key.Space
        InputManager.AttachPlayerActionSet(newKeyboardInput);

        return newKeyboardInput;
    }

    /// <summary>
    /// Bind Controller setup for new ControllerSet device
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    KeyboardControllerSet BindNewController(InputDevice device) {
        KeyboardControllerSet newControllerDevice = new KeyboardControllerSet(); // uses same player actions as keyboard

        newControllerDevice.Device = device;
        newControllerDevice.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        newControllerDevice.Right.AddDefaultBinding(InputControlType.LeftStickRight);

        newControllerDevice.Forward.AddDefaultBinding(InputControlType.LeftStickUp);
        newControllerDevice.Backward.AddDefaultBinding(InputControlType.LeftStickDown);
        newControllerDevice.Sprint.AddDefaultBinding(InputControlType.RightTrigger);
        newControllerDevice.Action1.AddDefaultBinding(InputControlType.Action1);

        newControllerDevice.Up.AddDefaultBinding(InputControlType.RightStickUp);
        newControllerDevice.Down.AddDefaultBinding(InputControlType.RightStickDown);
        newControllerDevice.LookLeft.AddDefaultBinding(InputControlType.RightStickLeft);
        newControllerDevice.LookRight.AddDefaultBinding(InputControlType.RightStickRight);

        newControllerDevice.Action2.AddDefaultBinding(InputControlType.Action2);

        newControllerDevice.isKeyboard = false;
        newControllerDevice.isBinded = true;

        return newControllerDevice;
    }
    /// <summary>
    /// Attaches device to the list.
    /// </summary>
    /// <param name="device"></param>
    void AttachInputDevice(KeyboardControllerSet device) {
        if (inputDevices.Count < maxPlayers) {
            inputDevices.Add(device);
        }
    }

    /// <summary>
    /// Player setup 
    /// </summary>
    /// <param name="inputDevice"></param>
    void SetupNewPlayer(KeyboardControllerSet inputDevice) {
        if (players.Count < maxPlayers) {
            inputDevice.isBinded = true;
            gameManager.sceneManager.SetPlayerTextActive(playerIndex);
            playerIndex++;
        }
    }

    /// <summary>
    /// Creates each player in the game scene.
    /// </summary>
    /// <param name="playerPositions"></param>
    public void CreatePlayers(List<Vector3> playerPositions) {
        playerIndex = 0;
        foreach (KeyboardControllerSet device in inputDevices) {
            BindControlsToPlayer(device, playerPositions);
            playerIndex++;
        }
    }
    /// <summary>
    /// Binds controls to devices
    /// </summary>
    /// <param name="device"></param>
    /// <param name="playerPositions"></param>
    void BindControlsToPlayer(KeyboardControllerSet device, List<Vector3> playerPositions) {
        if (playerPrefab) {
            //Setting up player
            if (isSinglePlayer) {
                SinglePlayerMode(device, playerPositions);
            } else {
                MultiplayerMode(device, playerPositions);
            }
        }
    }
    /// <summary>
    /// Multiplayer mode setup
    /// </summary>
    /// <param name="device"></param>
    /// <param name="playerPositions"></param>
    void MultiplayerMode(KeyboardControllerSet device, List<Vector3> playerPositions) {
        if (playerIndex < 1) {
            //Instantiate UF0
            GameObject ufoPlayer = GameObject.FindGameObjectWithTag("UFOPlayer");
            if (ufoPlayer) {
                Player playerComponent = ufoPlayer.GetComponent<Player>();
                playerComponent.keyboardDevice = device;
            }
        } else if (playerIndex < 4) {
            GameObject newPlayerObject = Instantiate(playerPrefab, playerPositions[playerIndex - 1], playerPrefab.transform.rotation);
            Player playerComponent = newPlayerObject.GetComponent<Player>();

            switch (playerIndex) {
                case 1: {
                        gameManager.playerOneCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                        playerComponent.playerCamera = gameManager.playerOneCam.GetComponent<Camera>();
                        break;
                    }
                case 2: {
                        gameManager.playerTwoCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                        playerComponent.playerCamera = gameManager.playerTwoCam.GetComponent<Camera>();
                        break;
                    }
                case 3: {
                        gameManager.playerThreeCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                        playerComponent.playerCamera = gameManager.playerThreeCam.GetComponent<Camera>();
                        break;
                    }
            }
            playerComponent.keyboardDevice = device;
        }
    }
    /// <summary>
    /// Singleplayer mode setup
    /// </summary>
    /// <param name="device"></param>
    /// <param name="playerPositions"></param>
    void SinglePlayerMode(KeyboardControllerSet device, List<Vector3> playerPositions) {
        GameObject ufoPlayer = GameObject.FindGameObjectWithTag("UFOPlayer");
        if (ufoPlayer) {
            Player ufoPlayerComponent = ufoPlayer.GetComponent<Player>();
            ufoPlayerComponent.keyboardDevice = device;
            singlePlayerController = ufoPlayerComponent.keyboardDevice;
            activePlayer = ufoPlayer;
            singlePlayerIndex = 0;
            singlePlayers.Add(ufoPlayerComponent);
        }
        for (int i = 1; i < maxPlayers; i++) {
            GameObject newPlayerObject = Instantiate(playerPrefab, playerPositions[i - 1], playerPrefab.transform.rotation);
            Player playerComponent = newPlayerObject.GetComponent<Player>();

            switch (i) {
                case 1: {
                        gameManager.playerOneCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                        playerComponent.playerCamera = gameManager.playerOneCam.GetComponent<Camera>();
                        break;
                    }
                case 2: {
                        gameManager.playerTwoCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                        playerComponent.playerCamera = gameManager.playerTwoCam.GetComponent<Camera>();
                        break;
                    }
                case 3: {
                        gameManager.playerThreeCam.GetComponent<ThirdPersonOrbit>().player = newPlayerObject.transform;
                        playerComponent.playerCamera = gameManager.playerThreeCam.GetComponent<Camera>();
                        break;
                    }
            }
            singlePlayers.Add(playerComponent);
        }
    }

    /// <summary>
    /// This functions switches between players for single-player
    /// </summary>
    /// <param name="player"></param>
    public void SwitchControls(GameObject player) {
        activePlayer = null;
        activePlayer = player;
        player.GetComponent<Player>().keyboardDevice = singlePlayerController;
    }
    void RemovePlayer(HumanPlayer player) {
        player.inputDevice = null;
        //Destroy player Device
    }
}

