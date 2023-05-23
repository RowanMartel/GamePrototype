using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private GameObject currentObject; // current interactable object
    private PlantingMenu plantingMenu;
    private Animator anim;

    void Start()
    {
        anim = FindObjectOfType<Animator>();
        plantingMenu = FindObjectOfType<PlantingMenu>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Interact();
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
        if (other.gameObject.layer == 3 && currentObject != null && currentObject.TryGetComponent( out Outline outline)) {
            outline.enabled = false;
            currentObject = null;
        }
    }

    void Interact()
    {
        if (!currentObject) return;
        switch (currentObject.tag)
        {
            case "PlantingSpot":
                if (currentObject.GetComponent<PlantingSpot>().growth == Item.Growth.Ripe && currentObject.GetComponent<PlantingSpot>().hasPlant) {
                    currentObject.GetComponent<PlantingSpot>().Harvest();
                    anim.Play("Base Layer.Harvest");
                    return;
                }
                plantingMenu.RegisterPlantingSpot(currentObject.GetComponent<PlantingSpot>());
                plantingMenu.OpenMenu();
                break;
        }
    }
}