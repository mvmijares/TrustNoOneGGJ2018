using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneBehaviour : MonoBehaviour {

    public GameObject AlienPlayerText;
    public GameObject PlayerOneText;
    public GameObject PlayerTwoText;
    public GameObject PlayerThreeText;

    [Tooltip("Alien Camera")]
    public GameObject alienCamera;
    [Tooltip("Player One Camera")]
    public GameObject playerOneCamera;
    [Tooltip("Player Two Camera")]
    public GameObject playerTwoCamera;
    [Tooltip("Player Three Camera")]
    public GameObject playerThreeCamera;

    private void Awake() {
        if(AlienPlayerText)
        AlienPlayerText.SetActive(false);
        if(PlayerOneText)
            PlayerOneText.SetActive(false);
        if(PlayerTwoText)
            PlayerTwoText.SetActive(false);
        if(PlayerThreeText)
            PlayerThreeText.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Assigns players to cameras
    public void CameraAssignment(HumanPlayer player) {
        UnityStandardAssets.Cameras.ThirdPersonCamera cameraComponent;
        switch (player.playerIndex) {
            case 1: {
                    cameraComponent = alienCamera.GetComponent<UnityStandardAssets.Cameras.ThirdPersonCamera>();
                    SetTargetPlayer(cameraComponent, player.transform);
                    break;
                }
            case 2: {
                    cameraComponent = playerOneCamera.GetComponent<UnityStandardAssets.Cameras.ThirdPersonCamera>();
                    SetTargetPlayer(cameraComponent, player.transform);
                    break;
                }
            case 3: {
                    cameraComponent = playerTwoCamera.GetComponent<UnityStandardAssets.Cameras.ThirdPersonCamera>();
                    SetTargetPlayer(cameraComponent, player.transform);
                    break;
                }
            case 4: {
                    cameraComponent = playerThreeCamera.GetComponent<UnityStandardAssets.Cameras.ThirdPersonCamera>();
                    SetTargetPlayer(cameraComponent, player.transform);
                    break;
                }
        }
     
    }

    void SetTargetPlayer(UnityStandardAssets.Cameras.ThirdPersonCamera cameraComponent, Transform playerObject) {
        cameraComponent.SetTarget(playerObject);
    }

    /// <summary>
    /// Sets the text active to when the player joins
    /// </summary>
    /// <param name="playerIndex">which player just connected</param>
    public void SetPlayerTextActive(int playerIndex) {
  
        switch (playerIndex + 1) {
            case 1: {
                    AlienPlayerText.SetActive(true);
                    break;
                }
            case 2: {
                    PlayerOneText.SetActive(true);
                    break;
                }
            case 3: {
                    PlayerTwoText.SetActive(true);
                    break;
                }
            case 4: {
                    PlayerThreeText.SetActive(true);
                    break;
                }
        }
    }


}
