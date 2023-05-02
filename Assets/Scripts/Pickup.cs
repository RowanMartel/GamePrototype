using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantTypeHandler;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    Item item;
    [SerializeField]
    int amount;

    Inventory inventory;

    void Start()
    {
        GetComponent<MeshFilter>().mesh = item.plantMesh;
        GetComponent<MeshRenderer>().material = item.plantMaterial;
        inventory = FindObjectOfType<Inventory>();
        transform.localScale = Vector3.one * 10;
        GetComponent<MeshCollider>().sharedMesh = item.plantMesh;
    }

    void Update()
    {
        /*Graphics.DrawMesh(item.plantMesh,
                Matrix4x4.TRS(transform.TransformPoint(item.positionOffset), transform.rotation, Vector3.one * 10),
                item.plantMaterial, 0);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
            inventory.AddItem(item.id, amount);
        }
    }
}