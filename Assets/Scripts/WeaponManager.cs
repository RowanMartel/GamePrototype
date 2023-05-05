using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons;

    void Start()
    {
        foreach (GameObject weapon in weapons)
            weapon.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Equip(Item weapon)
    {
        foreach (GameObject weapon2 in weapons)
        {
            weapon2.SetActive(false);
            if (weapon == weapon2.GetComponent<VisualWeapon>().weapon)
                weapon2.SetActive(true);
        }
    }
}