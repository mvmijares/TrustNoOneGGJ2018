using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DestroyAudio : MonoBehaviour {





    public static DestroyAudio DestroyAud;

    private void Awake()
    {

        if (!DestroyAud)
        {
            DestroyAud = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(gameObject);
        }


        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {

       
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
