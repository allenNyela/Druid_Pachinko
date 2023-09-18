using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloud : MonoBehaviour
{
    [Header("Movement Fields")]
    [SerializeField, Tooltip("The transform of the leftmost point this rain cloud should move to")]private Transform leftMoveEdge;
    [SerializeField, Tooltip("The transform of the rightmost point this rain cloud should move to")]private Transform rightMoveEdge;
    private Transform currentMoveGoal;
    private bool currentGoalLeft;
    [SerializeField, Tooltip("if marked true, the cloud will start by moving to the left. if marked false, it will start by moving to the right")]private bool startMovingLeft;
    [SerializeField, Tooltip("how fast this rain cloud should move")]private float moveSpeed = 2;
    [Header("Raindrop Fields")]
    [SerializeField, Tooltip("The prefab of the raindrop to spawn")]private GameObject rainDropPrefab;
    [SerializeField, Tooltip("The delay between each raindrop being spawned")]private float rainDropSpawnDelay = 2;
    private float rainDropTimer = 0;
    [SerializeField, Tooltip("If a raindrop should start immediately at the beginning of the game. \n If marked false, will have spawn delay before spawning the first raindrop")]private bool spawnRainDropOnGameStart = false;
    [SerializeField, Tooltip("The transform the raindrop should be spawned from. Uses this object's transform by default")]private Transform rainSpawnTransform;    

    [Header("Pollution Fields")]
    [SerializeField, Tooltip("the prefab for the pollution drop")]private GameObject pollutionDropPrefab;
    [SerializeField, Tooltip("The number of raindrops that must fall at the start of the game before any polluted drops can fall. \nIf <=0, then pollution drops are able to fall from the very start of the game")]private int rainNeededBeforePollution = 5;
    private bool canPollutionSpawn = false;
    [SerializeField, Tooltip("the number of raindrops that must fall after a pollution drop falls before another is able to fall. \nIf <=0, then pollution drops always have a chance to fall once the first has fallen")]private int rainNeededBetweenPollution = 1;
    private float prevRainCount = 0;
    [SerializeField, Tooltip("the percent chance for a polluted drop to spawn at the start of the game")]private float pollutionChance = .05f;
    [SerializeField, Tooltip("the rate at which the pollution spawn chance increases per second")]private float pollutionChanceIncrease = .001f;
    [SerializeField, Tooltip("The maximum pollution spawn chance")]private float maxPollutionChance = .4f;
    

    private void Start() {
        if(!rainSpawnTransform){
            rainSpawnTransform = transform;
        }
        if(spawnRainDropOnGameStart){
            SpawnRainDrop();
        }
        else{
            rainDropTimer = rainDropSpawnDelay;
        }
        if(startMovingLeft){
            currentMoveGoal = leftMoveEdge;
            currentGoalLeft = true;
        }
        else{
            currentMoveGoal = rightMoveEdge;
            currentGoalLeft = false;
        }
    }

    public void SpawnRainDrop(){
        if(!canPollutionSpawn && prevRainCount >= rainNeededBeforePollution){
            canPollutionSpawn = true;
        }
        if(canPollutionSpawn && prevRainCount >= rainNeededBetweenPollution && Random.Range(0.0f,1.0f) <= pollutionChance){
            Instantiate(pollutionDropPrefab, rainSpawnTransform.position, Quaternion.identity);
            prevRainCount = 0;
        }
        else{
            Instantiate(rainDropPrefab, rainSpawnTransform.position, Quaternion.identity);
            prevRainCount++;
        }
        rainDropTimer = rainDropSpawnDelay;
    }

    private void FixedUpdate() {
        if(Vector3.Distance(transform.position, currentMoveGoal.position) < .01f){
            if(currentGoalLeft){
                currentMoveGoal = rightMoveEdge;
                currentGoalLeft = false;
            }
            else{
                currentMoveGoal = leftMoveEdge;
                currentGoalLeft = true;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, currentMoveGoal.position, moveSpeed * Time.fixedDeltaTime);


        rainDropTimer -= Time.fixedDeltaTime;
        if(rainDropTimer <= 0){
            SpawnRainDrop();
        }
        pollutionChance = Mathf.Min(pollutionChance+(pollutionChanceIncrease * Time.fixedDeltaTime), maxPollutionChance);

    }
}
