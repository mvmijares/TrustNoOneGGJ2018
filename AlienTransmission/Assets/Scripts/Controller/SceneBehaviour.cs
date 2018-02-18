using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehaviour : MonoBehaviour {

    public GameObject AlienPlayerText;
    public GameObject PlayerOneText;
    public GameObject PlayerTwoText;
    public GameObject PlayerThreeText;

    //Default Player Text
   

    //When Players have joined
    public GameObject AlienPlayerJoined;
    public GameObject PlayerOneJoined;
    public GameObject PlayerTwoJoined;
    public GameObject PlayerThreeJoined;

    
    [Tooltip("Alien Camera")]
    public GameObject alienCamera;
    [Tooltip("Player One Camera")]
    public GameObject playerOneCamera;
    [Tooltip("Player Two Camera")]
    public GameObject playerTwoCamera;
    [Tooltip("Player Three Camera")]
    public GameObject playerThreeCamera;


    GameBehaviour gameManager;
    private void Awake() {
       
    }

    private void Start() {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerObject)
            gameManager = gameManagerObject.GetComponent<GameBehaviour>();

        if (gameManager.currentScene == "SetupControllers") {
            AlienPlayerJoined.SetActive(false);
            PlayerOneJoined.SetActive(false);
            PlayerTwoJoined.SetActive(false);
            PlayerThreeJoined.SetActive(false);
        }
    }

    /// <summary>
    /// Sets the text active to when the player joins
    /// </summary>
    /// <param name="playerIndex">which player just connected</param>
    public void SetPlayerTextActive(int playerIndex) {
        switch (playerIndex + 1) {
            case 1: {
                    Debug.Log("Player Index is " + (playerIndex + 1));
                    AlienPlayerText.SetActive(false);
                    AlienPlayerJoined.SetActive(true);
                    break;
                }
            case 2: {
                    PlayerOneText.SetActive(false);
                    PlayerOneJoined.SetActive(true);
                    break;
                }
            case 3: {
                    PlayerTwoText.SetActive(false);
                    PlayerTwoJoined.SetActive(true);
                    break;
                }
            case 4: {
                    PlayerThreeText.SetActive(false);
                    PlayerThreeJoined.SetActive(true);
                    break;
                }
        }
    }
}
