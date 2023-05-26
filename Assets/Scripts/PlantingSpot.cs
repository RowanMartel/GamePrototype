using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlantingSpot : MonoBehaviour
{
    private Item plant;
    public bool hasPlant;
    private float growTimer;
    public Item.Growth growth;
    private Inventory inventory;
    private AudioSource audioSource;
    [SerializeField] private AudioClip s_Plant;
    [SerializeField] private AudioClip s_Pick;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                Matrix4x4.TRS(transform.TransformPoint(plant.positionOffset), Quaternion.Euler(plant.rotation.x, plant.rotation.y, plant.rotation.z), desiredScale),
                plant.plantMaterial, 0);
        }
    }

    public void PlantSomething(Item plant)
    {
        if (hasPlant || !plant.plantable) return;
        growth = Item.Growth.Sprout;
        this.plant = plant.growsInto;
        hasPlant = true;
        growTimer = 0;
        audioSource.PlayOneShot(s_Plant);
    }
    public void Harvest() {
        inventory.AddItem(plant.hashId);
        Score.modifyScore(10);
        RemovePlant();
        audioSource.PlayOneShot(s_Pick);
    }
    public void RemovePlant() {
        hasPlant = false;
        audioSource.PlayOneShot(s_Pick);
    }
}