using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Item> Items = new List<Item>();
    public static Dictionary<int, Item> ItemDictionary = new Dictionary<int, Item>();
    private void Awake() {
        instance = this;
        ItemDictionary.Clear();
        for (int i = 0; i < Items.Count; i++) {
            Items[i].hashId = Animator.StringToHash(Items[i].id.Trim().ToUpper());
            if (Items[i].scale.magnitude < 0.00001f) Items[i].scale = Vector3.one;
            ItemDictionary[Items[i].hashId] = Items[i];
        }
    }

    public static bool GetItem(string str, out Item item) => GetItem(str.ToHashId(), out item);
    public static bool GetItem(int hashId, out Item item) => ItemDictionary.TryGetValue(hashId, out item);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
