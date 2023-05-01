using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTypeHandler : MonoBehaviour
{
    GameController gameController;

    public enum Plant
    {
        CarrotSpear,
        GrassBlade
    }
    public string[] PlantGuids;
    public enum Growth
    {
        Sprout,
        Growing,
        Ripe
    }

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        PlantGuids = new string[4];
        PlantGuids[0] = "8B0EF21A-F2D9-4E6F-8B79-031CA9E202BA";
        PlantGuids[1] = "992D3386-B743-4CD3-9BB7-0234A057C265";
        PlantGuids[2] = "1B9C6CAA-754E-412D-91BF-37F22C9A0E7B";
        PlantGuids[3] = "0916405d-8dd2-443d-8a2d-2baa83b91738";
    }
}