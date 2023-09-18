using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHolder : MonoBehaviour
{
    [SerializeField]
    private List<Plant> plantsHeld;

    public void WaterPlants()
    {
        foreach (Plant p in plantsHeld)
            p.WaterPlant();
    }
}
