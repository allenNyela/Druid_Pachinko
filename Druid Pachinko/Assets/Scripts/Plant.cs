using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    enum PlantState
    {
        Seed,
        Growing,
        Ripe,
        Wilted
    }

    [SerializeField]
    private bool isThirsty;

    [SerializeField]
    private PlantState state;

    [SerializeField, Tooltip("The amount of time in seconds a plant can be thirsty before wilting.")]
    private float timeToWilt;

    [SerializeField, Tooltip("The amount of time in seconds since the plant has been watered.")]
    private float timeSinceWater;

    [SerializeField, Tooltip("Total number of times plant must be watered while growing before becoming ripe")]
    private int numGrowingStages;

    [SerializeField, Tooltip("Current growing stage. While this number is < numGrowingStages, the plant will continue growing.")]
    private int currentGrowingStage;

    [SerializeField, Tooltip("The amount of score recieved for harvesting while ripe")]
    private int score;

    [SerializeField, Tooltip("The amount of money recieved for harvesting while ripe")]
    private int money;

    void Start()
    {
        isThirsty = true;
        state = PlantState.Seed;
        timeSinceWater = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Animation States
        switch (state)
        {
            case PlantState.Seed:
                break;
            case PlantState.Growing:
                break;
            case PlantState.Ripe:
                break;
            case PlantState.Wilted:
                break;
        }

        if (state != PlantState.Wilted && state != PlantState.Seed)
            CheckTimer();
    }

    private void OnMouseDown()
    {
        Harvest();
    }

    public void WaterPlant()
    {
        // Dead plants can't drink, silly
        if (state == PlantState.Wilted)
            return;
        else
        {
            isThirsty = false;

            switch (state)
            {
                case PlantState.Seed:
                    state = PlantState.Growing;
                    currentGrowingStage = 1;

                    break;
                case PlantState.Growing:
                    if (currentGrowingStage < numGrowingStages)
                        currentGrowingStage++;
                    else
                        state = PlantState.Ripe;

                    break;
                default:
                    break;
            }

            // Reset Countdown Coroutine Timer after Drinking.
            timeSinceWater = 0;
        }
    }

    public void Harvest()
    {
        // Only add score/money if harvested plant is ripe. Otherwise, just destroy it.
        if (state == PlantState.Ripe)
        {
            GameManager.Instance.AddMoney(money);
            GameManager.Instance.IncreaseScore(score);
        }

        Destroy(gameObject);
    }

    void CheckTimer()
    {
        timeSinceWater += Time.deltaTime;

        if (timeSinceWater < timeToWilt)
            return;

        // double check that we're not still thirsty (should only get here if thirsty, just in case).
        state = PlantState.Wilted;
        isThirsty = false;
    }
}
