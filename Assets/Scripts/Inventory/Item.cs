using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Data", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public string id;
    public int hashId;
    public Texture2D icon;
    public string name;
    public string description;
    public Mesh plantMesh;
    public Material plantMaterial;
    public Vector3 positionOffset;
    public Vector3 rotation;
    public Vector3 scale; 
    public bool plantable = true;
    public Item growsInto;
    public bool equippable;
    public int strength;
    public int tier;
    public enum Growth
    {
        Sprout,
        Growing,
        Ripe
    }
}