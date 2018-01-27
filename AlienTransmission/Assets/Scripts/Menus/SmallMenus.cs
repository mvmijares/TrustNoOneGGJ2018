using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SmallMenus : MonoBehaviour {


    int i = 0;


    public SpriteRenderer Play;
    public Sprite PlayHighlight;
    public Sprite PlayNotHighlight;

    public SpriteRenderer Menu;
    public Sprite MenuHighlight;
    public Sprite MenuNoHighlight;

    InputDevice Ninput;


    // Use this for initialization
    void Start () {
        i = 0;
	}
	
	// Update is called once per frame
	void Update () {

        Ninput = InputManager.ActiveDevice;



        if (i == 0)
        {
            Play.sprite = PlayHighlight;
            Menu.sprite = MenuNoHighlight;

            if (Ninput.Action1.WasReleased || Input.GetKey(KeyCode.A))
            {
                
                SceneManager.LoadScene("Menu");
            }

        }
        else if (i == 1)
        {


            Play.sprite = PlayNotHighlight;
            Menu.sprite = MenuHighlight;

            if (Ninput.Action1.WasReleased || Input.GetKey(KeyCode.A))
            {

                SceneManager.LoadScene("Menu");
            }

        }



        if (Ninput.DPadLeft.WasReleased || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            i = 1;
        }
        else if (Ninput.DPadRight.WasReleased || Input.GetKeyUp(KeyCode.RightArrow))
        {
            i = 0;
        }



    }
}
