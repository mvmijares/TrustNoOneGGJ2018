using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerManager : MonoBehaviour {

    List<HumanPlayer> players;
    List<Transform> playerPositions;
    const int maxPlayers = 4;
    int numPlayers;
    public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
        InputManager.OnDeviceDetached += OnDeviceDetached; // Add a listener for when device is detached during gameplay
        numPlayers = 0;
        players = new List<HumanPlayer>();

        //Will change this for a better setup
    }
	
	// Update is called once per frame
	void Update () {
        InputDevice activeInputDevice = InputManager.ActiveDevice; // active desi
        if (activeInputDevice.Action1) {
            if (!FindPlayerWithDevice(activeInputDevice)) {
                CreateNewPlayer(activeInputDevice);
            }
        }

	}
    HumanPlayer FindPlayerWithDevice(InputDevice inputDevice) {
        int playerCount = players.Count;
        for (int i = 0; i < playerCount; i++) {
            HumanPlayer player = players[i];
            if(player.inputDevice == inputDevice) {
                return player;
            }
        }
        return null;
    }
    void CreateNewPlayer(InputDevice inputDevice) {
        if (players.Count < maxPlayers) {
            if (playerPrefab) {
                GameObject newPlayerObject = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), playerPrefab.transform.rotation);
                HumanPlayer playerComponent = newPlayerObject.GetComponent<HumanPlayer>();
                playerComponent.inputDevice = inputDevice;


                players.Add(playerComponent);
                Debug.Log("New Player has joined");
                //Instantiate a new player object
            }
        }
    }
    void OnDeviceDetached(InputDevice inputDevice) {
        var playerCount = players.Count;
        for (int i = 0; i < playerCount; i++) {
            HumanPlayer player = players[i];
            if(player.inputDevice == inputDevice) {
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
