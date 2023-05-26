using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantTypeHandler;

public class Pickup : MonoBehaviour
{
    public Item item;
    [SerializeField]
    int amount;
    [SerializeField]
    AudioClip s_Pickup;

    Inventory inventory;

    private void Start()
    {
        if (item) Init();
    }

    public void Init()
    {
        GetComponent<MeshFilter>().mesh = item.plantMesh;
        GetComponent<MeshRenderer>().material = item.plantMaterial;
        inventory = FindObjectOfType<Inventory>();
        transform.localScale = item.scale;
        GetComponent<MeshCollider>().sharedMesh = item.plantMesh;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * (Time.deltaTime * 50));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
            inventory.AddItem(item.id, amount);
            Score.modifyScore(1);
            PlayerHandler.PlayClip(s_Pickup);
            Destroy(gameObject);
        }
    }
}