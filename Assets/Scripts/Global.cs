using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    // constants
    public static int playerHpStart = 100;
    public static int enemyPoolCount = 100;
    public static float enemyAtkTimer = 0.5f;

    // layer consts
    public LayerMask layerEnemy;
    public LayerMask layerPlayer;

    static ExamplePlayer playerController;

    [SerializeField]
    Transform playerTransform;
    public static Vector3 playerPos;

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
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                playerController.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void Start()
    {
        playerController = FindObjectOfType<ExamplePlayer>();
        MenuOpen = false;
    }

    private void Update()
    {
        playerPos = playerTransform.position;
    }
}