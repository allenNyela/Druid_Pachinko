using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum PlantState
    {
        Seed,
        Growing,
        Ripe,
        Harvested,
        Wilted
    }

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
    private int moneyFromHarvest;

    [SerializeField, Tooltip("The money cost to purchase this plant from the shop.")]
    public int marketPrice;

    [SerializeField, Tooltip("The HP of a plant (number of times it can be polluted before wilting)")]
    private int health;

    public PlantHolder plantHolder;

    private int pointsLostPerPollution;
    private int moneyLostPerPollution;

    private Animator myAnimator;

    void Start()
    {
        state = PlantState.Seed;
        timeSinceWater = 0;

        myAnimator = GetComponent<Animator>();

        if (health > 0)
        {
            pointsLostPerPollution = score / health;
            moneyLostPerPollution = moneyFromHarvest / health;
        } else
        {
            pointsLostPerPollution = score;
            moneyLostPerPollution = moneyFromHarvest;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Animation States
        switch (state)
        {
            case PlantState.Seed:
                myAnimator.SetInteger("GrowState", 0);
                break;
            case PlantState.Growing:
                myAnimator.SetInteger("GrowState", currentGrowingStage);
                break;
            case PlantState.Ripe:
                myAnimator.SetInteger("GrowState", currentGrowingStage);
                break;
            case PlantState.Wilted:
                myAnimator.SetTrigger("Wilted");
                break;
        }

        if (state != PlantState.Wilted && state != PlantState.Seed)
            CheckTimer();
    }

    private void OnMouseDown()
    {
        Harvest();
    }

    public void WaterPlant(int points, bool waterIsHealthy)
    {
        if (!waterIsHealthy) 
        {
            PolluteThyself();
            return;
        }

        // Dead plants can't drink, silly
        if (state == PlantState.Wilted)
            return;
        else
        {
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
                    {
                        state = PlantState.Ripe;
                        currentGrowingStage++;
                    }

                    break;
                default:
                    break;
            }

            // Reset Countdown Coroutine Timer after Drinking.
            timeSinceWater = 0;
        }
    }

    void PolluteThyself() 
    {
        health--;
        score -= pointsLostPerPollution;
        moneyFromHarvest -= moneyLostPerPollution;

        if (health < 0)
            state = PlantState.Wilted;
    }

    public void Harvest()
    {
        // Only add score/money if harvested plant is ripe. Otherwise, just destroy it.
        if (state == PlantState.Ripe)
        {
            GameManager.Instance.AddMoney(moneyFromHarvest);
            GameManager.Instance.IncreaseScore(score);
            AudioManager.Instance.FXPlant();
            Destroy(gameObject);
            plantHolder.Harvest();
        }
        else if (state == PlantState.Wilted)
        {
            Destroy(gameObject);
            AudioManager.Instance.FXPlant();
            plantHolder.Harvest();
        }
    }

    public PlantState GetState()
    {
        return state;
    }

    void CheckTimer()
    {
        timeSinceWater += Time.deltaTime;

        if (timeSinceWater < timeToWilt)
            return;

        // double check that we're not still thirsty (should only get here if thirsty, just in case).
        state = PlantState.Wilted;
    }
}
