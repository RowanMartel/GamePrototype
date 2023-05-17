using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<GameObject> weapons;

    void Start()
    {
        weapons = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
            weapons.Add(transform.GetChild(i).gameObject);
        foreach (GameObject weapon in weapons)
            weapon.SetActive(false);
    }

    public void EquipEnemy(int tier)
    {
        weapons = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
            weapons.Add(transform.GetChild(i).gameObject);
        foreach (GameObject item in weapons)
            item.SetActive(false);

        tier /= 10;
        GameObject weapon;
        bool breakout = false;
        while (!breakout)
        {
            weapon = weapons[Random.Range(0, weapons.Count)];
            if (weapon.GetComponent<VisualWeapon>().weapon.tier <= tier)
            {
                breakout = true;
                Equip(weapon.GetComponent<VisualWeapon>().weapon);
                GetComponentInParent<Enemy>().weapon = weapon.GetComponent<VisualWeapon>();
            }
            weapon.SetActive(true);
        }
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