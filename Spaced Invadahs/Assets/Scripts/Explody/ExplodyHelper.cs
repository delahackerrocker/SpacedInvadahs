using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodyHelper : MonoBehaviour
{
    public GameObject[] explodies;
    protected int currentlySelected = 0; 

    public void HideExplodies()
    {
        for (int index = 0; index < explodies.Length; index++)
        {
            explodies[index].SetActive(false);
        }
    }

    public void FirstExplody()
    {
        explodies[currentlySelected].SetActive(true);
    }

    public void NextExplody()
    {
        HideLastExplody();
        currentlySelected++;
        if (currentlySelected >= explodies.Length) currentlySelected = 0;
        explodies[currentlySelected].SetActive(true);
    }

    public void PreviousExplody()
    {
        HideLastExplody();
        currentlySelected--;
        if (currentlySelected <= 0) currentlySelected = explodies.Length - 1;
        explodies[currentlySelected].SetActive(true);
    }

    public void HideLastExplody()
    {
        explodies[currentlySelected].SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        HideExplodies();
        FirstExplody();
    }

    void Update()
    {
        //Debug.LogWarning("Update");
        // If the player presses Left Shift
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
            //Debug.LogWarning("Left Shift Pressed");
            if ((Input.GetKeyDown(KeyCode.Keypad6)) || (Input.GetKeyDown(KeyCode.RightArrow)))
            {
                Debug.LogWarning("RightArrow or Keypad6 Pressed");
                // Reveal Next Explody and Hide the previous one
                NextExplody();
            } else if ((Input.GetKeyDown(KeyCode.Keypad4)) || (Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                Debug.LogWarning("LeftArrow or Keypad4 Pressed");
                // Reveal Next Explody and Hide the previous one
                PreviousExplody();
            }
        //}
    }
}
