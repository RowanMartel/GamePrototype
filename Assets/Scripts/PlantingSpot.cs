using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingSpot : MonoBehaviour
{
    private PlantTypeHandler.Plant plant;
    private MeshFilter plantMesh;
    public bool hasPlant;
    private float growTimer;
    public PlantTypeHandler.Growth growth;
    private Transform plantTransform;
    private Vector3 plantScale;
    private Inventory inventory;

    void Start()
    {
        hasPlant = false;
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (hasPlant && growth != PlantTypeHandler.Growth.Ripe)
        {
            growTimer += Time.deltaTime;
            if (growTimer > 5)
            {
                switch (growth)
                {
                    case PlantTypeHandler.Growth.Sprout:
                        growth = PlantTypeHandler.Growth.Growing;
                        plantTransform.localScale = new Vector3(plantScale.x / 2, plantScale.y /2, plantScale.z / 2);
                        break;
                    case PlantTypeHandler.Growth.Growing:
                        growth = PlantTypeHandler.Growth.Ripe;
                        plantTransform.localScale = plantScale;
                        break;
                }
                growTimer = 0;
            }
        }
    }

    public void PlantSomething(PlantTypeHandler.Plant plant)
    {
        if (hasPlant) return;
        growth = PlantTypeHandler.Growth.Sprout;
        this.plant = plant;
        hasPlant = true;
        growTimer = 0;
        plantTransform = transform.parent.GetChild(1).GetChild((int)plant);
        plantTransform.GetComponent<MeshRenderer>().enabled = true;
        plantScale = new Vector3(plantTransform.localScale.x, plantTransform.localScale.y, plantTransform.localScale.z);
        plantTransform.localScale = new Vector3(plantScale.x / 4, plantScale.y / 4, plantScale.z / 4);
    }
    public void Harvest()
    {
        inventory.AddItem(plant);
        RemovePlant();
    }
    public void RemovePlant()
    {
        plantTransform.GetComponent<MeshRenderer>().enabled = false;
        hasPlant = false;
    }
}