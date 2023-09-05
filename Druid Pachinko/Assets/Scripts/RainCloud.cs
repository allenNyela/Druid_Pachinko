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
        Instantiate(rainDropPrefab, rainSpawnTransform.position, Quaternion.identity);
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
        

    }
}
