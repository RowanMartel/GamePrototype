using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    static ExamplePlayer playerController;

    static bool menuOpen;
    public static bool MenuOpen
    {
        get { return menuOpen; }
        set
        {
            menuOpen = value;
            if (value)
            {
                playerController.enabled = false;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                playerController.enabled = true;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void Start()
    {
        playerController = FindObjectOfType<ExamplePlayer>();
        MenuOpen = false;
    }
}