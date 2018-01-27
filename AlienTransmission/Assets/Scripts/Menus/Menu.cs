using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class Menu : MonoBehaviour {


    int i = 0;


    public SpriteRenderer Play;
    public Sprite PlayHighlight;
    public Sprite PlayNotHighlight;

    public SpriteRenderer Controls;
    public Sprite ControlsHighlight;
    public Sprite ControlsNoHighlight;

    public SpriteRenderer Exit;
    public Sprite ExitHighlight;
    public Sprite ExitNoHighlight;

    public SpriteRenderer Credits;
    public Sprite CreditsHighlight;
    public Sprite CreditsNotHighlight;


    InputDevice Ninput;

    // Use this for initialization
    void Start () {

        //Play.sprite = PlayHighlight;
        i = 0;

	}

    // Update is called once per frame
    void Update() {

        Ninput = InputManager.ActiveDevice;

        

        print(i);

        if (i == 1)
        {
            Play.sprite = PlayHighlight;
            Controls.sprite = ControlsNoHighlight;
            Exit.sprite = ExitNoHighlight;
            Credits.sprite = CreditsNotHighlight;
        }
        else if (i == 0)
        {
            Controls.sprite = ControlsHighlight;
            Play.sprite = PlayNotHighlight;
            Exit.sprite = ExitNoHighlight;
            Credits.sprite = CreditsNotHighlight;
        }
        else if (i == -2)
        {
            Exit.sprite = ExitHighlight;
            Controls.sprite = ControlsNoHighlight;
            Play.sprite = PlayNotHighlight;
            Credits.sprite = CreditsNotHighlight;
        }
        else if (i == -1)
        {
            Credits.sprite = CreditsHighlight;
            Exit.sprite = ExitNoHighlight;
            Controls.sprite = ControlsNoHighlight;
            Play.sprite = PlayNotHighlight;
        }


        if (Ninput.DPadUp)
        {
            print("working");

            if (i == 0)
            {
                i = 1;
            }
            else if (i == -2)
            {
                i = -1;
            }
            else if (i == -1)
            {
                i = 0;
            }
            else
            {
                i = 1;
            }

        }


        if (Ninput.DPadDown)
        {
            if (i == 0)
            {
                i = -1;
            }
            else if (i == 1)
            {
                i = 0;
            }
            else if (i == 0)
            {
                i = -1;
            }
            else
            {
                i = -2;
            }
        }

        
      

	}
}
