using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<PlantTypeHandler.Plant> Weapons;
    List<PlantTypeHandler.Plant> Seeds;

    GameController controller;
    PlantTypeHandler typeHandler;

    private void Start()
    {
        Weapons = new List<PlantTypeHandler.Plant>();
        Seeds = new List<PlantTypeHandler.Plant>();
        controller = FindObjectOfType<GameController>();
        typeHandler = FindObjectOfType<PlantTypeHandler>();
    }

    public void AddItem(PlantTypeHandler.Plant plant, bool isSeed = false)
    {
        if (!isSeed)
        {
            Seeds.Add(plant);
            controller.AddIcon(typeHandler.PlantGuids[(int)plant]);
        }
    }
}