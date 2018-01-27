using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras {
    public class PlayerManager : MonoBehaviour {

        List<HumanPlayer> players;
        List<Transform> playerPositions;
        const int maxPlayers = 4;
        int playerIndex;
        public GameObject playerPrefab;
        public GameObject playerCameraPrefab;

        List<Rect> cameraViewports; // List of viewport rects for individual cameras
        // Use this for initialization
        void Start() {
            InputManager.OnDeviceDetached += OnDeviceDetached; // Add a listener for when device is detached during gameplay
            players = new List<HumanPlayer>();
            cameraViewports = new List<Rect>();
            //Alien
            cameraViewports.Add(new Rect(0, 0.5f, 1f, 0.5f));
            //Human Players
            cameraViewports.Add(new Rect(0, 0, 0.33f, 0.5f));
            cameraViewports.Add(new Rect(0.33f, 0, 0.34f, 0.5f));
            cameraViewports.Add(new Rect(0.67f, 0, 0.33f, 0.5f));

            playerIndex = 0;
            //Will change this for a better setup

       
        }

        // Update is called once per frame
        void Update() {
            InputDevice activeInputDevice = InputManager.ActiveDevice; // active device
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

                    //Setting up player camera
                    GameObject newPlayerCamera = Instantiate(playerCameraPrefab, new Vector3(0, 0, 0), playerCameraPrefab.transform.rotation);
                    ThirdPersonCamera cameraComponent = newPlayerCamera.GetComponent<ThirdPersonCamera>();
                    cameraComponent.SetTarget(newPlayerObject.transform);
                    cameraComponent.humanPlayer = playerComponent;
                    cameraComponent.cam.rect = cameraViewports[playerIndex];

                    playerIndex++;
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
}
