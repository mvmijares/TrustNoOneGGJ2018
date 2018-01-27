using System.Collections;
using System.Collections.Generic;
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




    // Use this for initialization
    void Start () {

        //Play.sprite = PlayHighlight;
        i = 0;

	}

    // Update is called once per frame
    void Update() {

        print(i);

        if (i == 0)
        {
            Play.sprite = PlayHighlight;
            Controls.sprite = ControlsNoHighlight;
            Exit.sprite = ExitNoHighlight;
            Credits.sprite = CreditsNotHighlight;
        }
        else if (i == 1)
        {
            Controls.sprite = ControlsHighlight;
            Play.sprite = PlayNotHighlight;
            Exit.sprite = ExitNoHighlight;
            Credits.sprite = CreditsNotHighlight;
        }
        else if (i == -1)
        {
            Exit.sprite = ExitHighlight;
            Controls.sprite = ControlsNoHighlight;
            Play.sprite = PlayNotHighlight;
            Credits.sprite = CreditsNotHighlight;
        }
        else if (i == -2)
        {
            Credits.sprite = CreditsHighlight;
            Exit.sprite = ExitNoHighlight;
            Controls.sprite = ControlsNoHighlight;
            Play.sprite = PlayNotHighlight;
        }


        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
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


        if (Input.GetKeyUp(KeyCode.DownArrow))
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
