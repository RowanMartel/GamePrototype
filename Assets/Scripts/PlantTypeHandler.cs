using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTypeHandler : MonoBehaviour
{
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
}