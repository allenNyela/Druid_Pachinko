using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantHolder : MonoBehaviour
{
    [SerializeField]
    private List<Plant> plantsHeld;

    [SerializeField]
    private GameObject spawnPoint;

    public void WaterPlants(int points, bool waterIsHealthy)
    {
        foreach (Plant p in plantsHeld)
            p.WaterPlant(points, waterIsHealthy);
    }

    public void Harvest()
    {
        plantsHeld.Clear();
    }

    private void OnMouseDown()
    {
        if (plantsHeld.Count > 0)
            return;
        
        Plant plant2Plant = ShopManager.Instance.plantPlant();
        AudioManager.Instance.FXPlant();
        Plant plant2actuallyPlant = Instantiate(plant2Plant, spawnPoint.transform.position, spawnPoint.transform.rotation);
        plant2actuallyPlant.plantHolder = this;
        plantsHeld.Add(plant2actuallyPlant);
    }

    public bool HasPlant()
    {
        return plantsHeld.Count > 0;
    }
}
