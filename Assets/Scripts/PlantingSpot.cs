using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingSpot : MonoBehaviour
{
    private Item plant;
    private MeshFilter plantMesh;
    public bool hasPlant;
    private float growTimer;
    public Item.Growth growth;
    private Inventory inventory;

    void Start()
    {
        hasPlant = false;
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (hasPlant && growth != Item.Growth.Ripe)
        {
            growTimer += Time.deltaTime;
            if (growTimer > 5) {
                growth++;
                growTimer = 0;
            }
        }

        if (hasPlant && plant.plantMesh != null)
        {
            Vector3 desiredScale = plant.scale;
            switch (growth)
            {
                case Item.Growth.Sprout:
                    desiredScale *= 0.2f;
                    break;
                case Item.Growth.Growing:
                    desiredScale *= 0.5f;
                    break;
            }
            Graphics.DrawMesh(plant.plantMesh,
                Matrix4x4.TRS(transform.TransformPoint(plant.positionOffset), transform.rotation,desiredScale),
                plant.plantMaterial, 0);
        }
    }

    public void PlantSomething(Item plant)
    {
        if (hasPlant || !plant.plantable) return;
        growth = Item.Growth.Sprout;
        this.plant = plant;
        hasPlant = true;
        growTimer = 0;
    }
    public void Harvest() {
        inventory.AddItem(plant.hashId);
        RemovePlant();
    }
    public void RemovePlant() {
        hasPlant = false;
    }
}