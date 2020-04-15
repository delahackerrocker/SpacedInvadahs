using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    // variable to hold Mario sprite
    public Player goMario;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses SPACE bar we'll make Mario Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //print("SPACE");
            goMario.Shoot();
        }

        // If Move Mario right
        if ( ( Input.GetKeyDown(KeyCode.Keypad6) ) || (Input.GetKeyDown(KeyCode.RightArrow) ) )
        {
            // if move right key is down
            goMario.horizontalGear = HorizontalMovement.RIGHT;
        } else if ( (Input.GetKeyUp(KeyCode.Keypad6) ) || (Input.GetKeyUp(KeyCode.RightArrow) ) ) {
            // if move right key is released
            goMario.horizontalGear = HorizontalMovement.NOTHING;
        }

        // If the player presses KeyPad4 then move Mario left
        if ( (Input.GetKeyDown(KeyCode.Keypad4)) || (Input.GetKeyDown(KeyCode.LeftArrow)) )
        {
            goMario.horizontalGear = HorizontalMovement.LEFT;
        } else if ((Input.GetKeyUp(KeyCode.Keypad4)) || (Input.GetKeyUp(KeyCode.LeftArrow)) ) {
            goMario.horizontalGear = HorizontalMovement.NOTHING;
        }

        // If the player presses Keypad8 then move Mario Up
        if ((Input.GetKeyDown(KeyCode.Keypad8)) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            goMario.VerticalGear = VerticalMovement.FORWARD;
        }
        else if ((Input.GetKeyUp(KeyCode.Keypad8)) || (Input.GetKeyUp(KeyCode.UpArrow)))
        {
            goMario.VerticalGear = VerticalMovement.NOTHING;
        }

        // If the player presses Keypad2 then make Mario Crouch
        if ((Input.GetKeyDown(KeyCode.Keypad2)) || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            goMario.VerticalGear = VerticalMovement.BACKWARD;
        }
        else if ((Input.GetKeyUp(KeyCode.Keypad2)) || (Input.GetKeyUp(KeyCode.DownArrow)))
        {
            goMario.VerticalGear = VerticalMovement.NOTHING;
        }
    }
}
