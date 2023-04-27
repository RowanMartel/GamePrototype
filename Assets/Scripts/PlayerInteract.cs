using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private GameObject currentObject; // current interactable object

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Interact();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (currentObject) currentObject.GetComponent<Outline>().enabled = false;
            currentObject = other.gameObject;
            currentObject.GetComponent<Outline>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            currentObject.GetComponent<Outline>().enabled = false;
            currentObject = null;
        }
    }

    void Interact()
    {
        if (!currentObject) return;
        switch (currentObject.tag)
        {
            case "PlantingSpot":
                currentObject.GetComponent<PlantingSpot>().Harvest();
                currentObject.GetComponent<PlantingSpot>().PlantSomething(PlantTypeHandler.Plant.CarrotSpear);
                break;
        }
    }
}